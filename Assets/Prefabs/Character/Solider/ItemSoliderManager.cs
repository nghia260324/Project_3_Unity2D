using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSoliderManager : MonoBehaviour
{
    public Button unlockSolider;
    private Soldier soldier;

    public void SetInfSolider(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public void UnlockSolider()
    {
        if (LevelManager.Instance.GetCurrentLevel() < soldier.level) return;
        if (!soldier.status)
        {
            GameObject canvasObject = GameObject.Find("EventUnlock");
            var img = canvasObject.transform.Find("EUnLockSolider");
            img.transform.Find("RawImage").GetComponent<RawImage>().texture = soldier.textureSolider;

            soldier.status = true;
            unlockSolider.transform.Find("L").GetComponent<TextMeshProUGUI>().text = "Training";
            img.gameObject.SetActive(true);
        } else
        {
            GameObject canvasObject = GameObject.Find("SoldierTraining");
            canvasObject.transform.Find("Cost").gameObject.SetActive(true);

            CostManager costManager = canvasObject.transform.Find("Cost").GetComponent<CostManager>();
            costManager.soldier = soldier;
            CostManager.Instance.Start();
        }
    }
}
