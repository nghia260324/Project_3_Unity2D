using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventUnlockManager : MonoBehaviour
{
    public Image parentImage;

    public void SoliderClick()
    {
        if (parentImage != null && parentImage.gameObject.activeSelf)
        {
            parentImage.gameObject.SetActive(false);
        }
    }
}
