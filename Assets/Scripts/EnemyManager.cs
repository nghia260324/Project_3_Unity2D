using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int quantity;
    public void AddEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddEnemy();
            }
        }
    }
}
