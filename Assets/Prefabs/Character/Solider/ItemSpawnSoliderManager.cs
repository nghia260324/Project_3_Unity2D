using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnSoliderManager : MonoBehaviour
{
    public static ItemSpawnSoliderManager Instance;
    public GameObject soldierPrefabs;
    public Soldier soldier;

    public int currentQuantity;
    private int index;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        index = SoldierManager.Instance.CheckSoldierIndex(soldier);
        currentQuantity = SoldierManager.Instance.currentOfSoliders[index];
    }

    public void SpawnSoldier()
    {
        SpawnS();
    }
    public void SpawnAllSoldier()
    {
        //StartCoroutine(SpawnAllSolider());
        CoroutineManager.Instance.StartCustomCoroutine(SpawnAllSolider());
    }

    IEnumerator SpawnAllSolider()
    {
        int count = SoldierManager.Instance.CheckSoldierQuantity(soldier) - currentQuantity;
        for (int i = 0; i < count; i++)
        {
            SpawnS();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnS()
    {
        if (currentQuantity == SoldierManager.Instance.CheckSoldierQuantity(soldier)) return;
        currentQuantity++;
        SoldierManager.Instance.currentOfSoliders[index]++;
        GameObject newSoldier = GameObject.Instantiate(soldierPrefabs,
            SoldierManager.Instance.spawnPoint.position, Quaternion.identity);
        newSoldier.name = soldier.name;
    }
}
