using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldierManager : MonoBehaviour
{
    public static SoldierManager Instance;
    public List<GameObject> soldiers = new List<GameObject>();
    public List<int> numberOfSoldiers = new List<int>();
    public List<int> currentOfSoliders = new List<int>();

    private int currentNumberOfSoldier;
    public GameObject soldierPrefab;
    public Transform spawnPoint;

    public Transform ItemContent;
    public GameObject SoldierItem;

    void Awake()
    {
        Instance = this;
    }
    public void AddEnemy(GameObject solider)
    {
        if (spawnPoint == null) return;
        GameObject newSolider = Instantiate(solider, spawnPoint.position, Quaternion.identity);
        newSolider.name = "Solider";
    }
    private void Start()
    {
/*        if (spawnPoint == null) return;
        for (int i = 0; i < soldiers.Count; i++)
        {
            for (int j = 0; j < numberOfSoldiers[i]; j++)
            {
                AddEnemy(soldiers[i]);
                currentNumberOfSoldier++;
            }
        }*/
    }

    public void UpdateSoldierQuantity(Soldier s, int quantity)
    {
        SoldierController soldierController = null;
        int index = soldiers.FindIndex(soldierObj =>
        {
            soldierController = soldierObj.GetComponent<SoldierController>();
            return soldierController != null && soldierController.soldier.id == s.id;
        });

        if (index != -1)
        {
            numberOfSoldiers[index] += quantity;
        }
    }

    public int CheckSoldierQuantity(Soldier soldier)
    {
        SoldierController soldierController = null;
        int index = soldiers.FindIndex(soldierObj =>
        {
            soldierController = soldierObj.GetComponent<SoldierController>();
            return soldierController != null && soldierController.soldier.id == soldier.id;
        });

        return numberOfSoldiers[index];
    }

    public int CheckSoldierIndex(Soldier soldier)
    {
        SoldierController soldierController = null;
        return soldiers.FindIndex(soldierObj =>
        {
            soldierController = soldierObj.GetComponent<SoldierController>();
            return soldierController != null && soldierController.soldier.id == soldier.id;
        });
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (GameObject item in soldiers)
        {
            SoldierController s = item.GetComponent<SoldierController>();
            if (s.soldier.status)
            {
                GameObject obj = Instantiate(SoldierItem, ItemContent);
                var itemName = obj.transform.Find("Name").Find("Soldier").GetComponent<TextMeshProUGUI>();
                var itemTexture = obj.transform.Find("SoldierImage").GetComponent<RawImage>();

                ItemSpawnSoliderManager i = obj.GetComponent<ItemSpawnSoliderManager>();
                i.soldier = s.soldier;
                i.soldierPrefabs = item;

                itemName.text = s.soldier.nameSolider;
                itemTexture.texture = s.soldier.textureSolider;
            }
        }
    }
}
