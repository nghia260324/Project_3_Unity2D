using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    public Canvas canvas;
    public bool type;
    private bool check;

    private void Update()
    {
        if (check && type && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            SceneManager.LoadScene("InsideTown");
        } else if (check && !type && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            SceneManager.LoadScene("OutsideTown");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            check = true;
            canvas.transform.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            check = false;
            canvas.transform.gameObject.SetActive(false);
        }
    }
}
