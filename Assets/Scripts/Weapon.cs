using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerController playerController;
    public float overlapRadius;
    GameObject[] enemies;
    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToTarget = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToTarget < overlapRadius && enemy != null)
            {
                playerController.SetTarget(enemy.GetComponent<EnemyController>());
            }
        }

    }
}
