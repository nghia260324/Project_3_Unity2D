using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Canvas canvasShop;

    public static UiManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ActiveShop(bool active)
    {
        canvasShop.transform.gameObject.SetActive(active);
    }
}
