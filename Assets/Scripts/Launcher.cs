using Firebase.Database;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    public TMP_InputField user;
    public TMP_InputField password;
    public Slider slider;


    [Header("Setting")]
    public float timeLoad;
    public float minValue;
    public float maxValue;

    [Header("Menu")]
    public GameObject loading;
    public GameObject login;
    public GameObject lockAccount;

    DatabaseReference reference;

    private GameObject currentMenu;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child("USER");
    }

    public void Login()
    {
        string email = user.text;
        string pass = password.text;
        FindUserKeyByEmailAndPassword(email, pass);
        //CheckUserCredentials(email, pass);
    }

/*    async void CheckUserCredentials(string email, string password)
    {
        DataSnapshot snapshot = await reference.GetValueAsync();

        if (snapshot.Exists)
        {
            foreach (DataSnapshot userSnapshot in snapshot.Children)
            {
                string userEmail = userSnapshot.Child("_email").Value.ToString();
                string userPassword = userSnapshot.Child("_password").Value.ToString();

                if (userEmail == email && userPassword == password)
                {
                    LoginSuccess();
                    FindUserKeyByEmailAndPassword(email, password);
                    return;
                }
            }
            Debug.Log("Tai khoan hoac mat khau chua chinh xac");
        }
    }*/
    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(timeLoad);
        SceneManager.LoadScene("InsideTown");
    }


    private void OpenLoading()
    {
        loading.SetActive(true);
        ChangeSliderValue();
        currentMenu = loading;
    }

    private void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    private void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        currentMenu = menu;
    }

    public void BackMenu(GameObject to)
    {
        to.SetActive(true);
        currentMenu.SetActive(false);
    }

    public void ChangeSliderValue()
    {
        StartCoroutine(ChangeSliderValueOverTime(timeLoad));
    }

    IEnumerator ChangeSliderValueOverTime(float timeLoad)
    {
        slider.value = minValue;
        float timer = 0f;
        float startValue = slider.value;
        float targetValue = maxValue;

        while (timer < timeLoad)
        {
            timer += Time.deltaTime;
            slider.value = Mathf.Lerp(startValue, targetValue, timer / timeLoad);
            yield return null;
        }

        slider.value = targetValue;
    }

    void FindUserKeyByEmailAndPassword(string email, string password)
    {
        reference.OrderByChild("_email").EqualTo(email).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error retrieving data: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    if (childSnapshot.Child("_password").Value.ToString() == password)
                    {
                        string userKey = childSnapshot.Key;

                        MainThreadDispatcher.Enqueue(() =>
                        {
                            if (!(bool)childSnapshot.Child("_status").Value)
                            {
                                CloseMenu(login);
                                OpenMenu(lockAccount);
                            } else
                            {
                                LoginSuccess(userKey);
                            }
                            
                        });
                    }
                }
                //Debug.Log("User not found or password is incorrect.");
            }
        });
    }

    private void LoginSuccess(string userKey)
    {
        PlayerPrefs.SetString("USERKEY", userKey);
        CloseMenu(login);
        OpenLoading();
        StartCoroutine(UnloadScene());
    }
}
