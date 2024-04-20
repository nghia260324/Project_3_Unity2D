using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseDataAccess : MonoBehaviour
{
    public static FirebaseDataAccess Instance;
    DatabaseReference mReference;

    public string userKey;
    public string characterKey;

    public User user = null;
    public Character character = null;

    private void Awake()
    {
        Instance = this;
    }
    async void Start()
    {
        mReference = FirebaseDatabase.DefaultInstance.RootReference;
        userKey = PlayerPrefs.GetString("USERKEY");

        user = await GetUser(userKey);

        characterKey = user._idCharacter;

        character = await GetCharacter(characterKey);

        //Debug.Log(user._id.ToString() + " - " + user._email.ToString() + " - " + user._password.ToString() + " - " + user._status + " - " + user._idCharacter.ToString());

        //Debug.Log(character.id + " - " + character.name + " - " + character.damage.ToString() + " - " + character.speed.ToString());

        //Debug.Log(Guid.NewGuid().ToString());

        //WriteNewUser(Guid.NewGuid().ToString(), "nghia260324@gmail.com", "123456789", true, Guid.NewGuid().ToString());
        SpawnPlayer.Instance.SpawmPlayerFromPrefabs();
    }

    public string GetUserKey()
    {
        return this.userKey;
    }
    public string GetCharacterKey()
    {
        return this.user._idCharacter;
    }

    public async Task<User> GetUser(string key)
    {
        User user = null;
        try
        {
            DataSnapshot snapshot = await mReference.Child("USER").Child(key).GetValueAsync();

            if (snapshot != null && snapshot.Exists)
            {
                user = new User
                {
                    _id = snapshot.Child("_id").Value.ToString(),
                    _email = snapshot.Child("_email").Value.ToString(),
                    _password = snapshot.Child("_password").Value.ToString(),
                    _status = snapshot.Child("_status").Value != null ? (bool)snapshot.Child("_status").Value : false,
                    _idCharacter = snapshot.Child("_idCharacter").Value.ToString(),
                };
            }
            else
            {
                Debug.Log("User not found.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error retrieving data: " + e.Message);
        }
        return user;
    }

    public async Task<Character> GetCharacter(string key)
    {
        try
        {
            DataSnapshot snapshot = await mReference.Child("CHARACTER").Child(key).GetValueAsync();

            if (snapshot != null && snapshot.Exists)
            {
                character = new Character
                {
                    id = snapshot.Child("id").Value.ToString(),
                    name = snapshot.Child("name").Value.ToString(),
                    damage = int.Parse(snapshot.Child("damage").Value.ToString()),
                    speed = int.Parse(snapshot.Child("speed").Value.ToString()),
                    health = int.Parse(snapshot.Child("health").Value.ToString()),
                    level = int.Parse(snapshot.Child("health").Value.ToString()),
                    currentExp = int.Parse(snapshot.Child("currentExp").Value.ToString()),
                    idInventory = snapshot.Child("idInventory").Value.ToString(),
                };
            }
            else
            {
                Debug.Log("Character not found.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error retrieving data: " + e.Message);
        }
        return character;
    }

    private void WriteNewUser(string id, string email, string password, bool status, string _idCharacter)
    {
        User user = new User(id, email, password, status, _idCharacter);
        string json = JsonUtility.ToJson(user);

        mReference.Child("USER").Child(id).SetRawJsonValueAsync(json);
    }
    private void WriteNewCharacter(string id,string name,int damage,int speed,int health,int level,int currentExp,string idInv)
    {

        Character character = new Character(id,name,damage,speed,health,level,currentExp,idInv);
        string json = JsonUtility.ToJson(user);

        mReference.Child("CHARACTER").Child(id).SetRawJsonValueAsync(json);
    }
}
