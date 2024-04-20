using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static SpawnPlayer Instance;
    public GameObject player;
    public GameObject spawnPoint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //SpawmPlayerFromPrefabs();
    }

    public void SpawmPlayerFromPrefabs()
    {
        GameObject newPlayer = Instantiate(player, spawnPoint.transform.position,Quaternion.identity);
        newPlayer.name = "Player";
    }
}
