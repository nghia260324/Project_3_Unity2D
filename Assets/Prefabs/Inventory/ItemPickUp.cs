using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    void PickUp()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    private void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 1f)
        {
            PickUp();
        }
    }
}
