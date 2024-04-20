using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager instance;

    public static CoroutineManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CoroutineManager").AddComponent<CoroutineManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public void StartCustomCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
