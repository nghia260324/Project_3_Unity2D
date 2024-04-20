using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Resource Pack", menuName = "Item Resource Pack/Create New ItemRP")]
public class ItemResourcePack : ScriptableObject
{
    public string nameItem;
    public Sprite sprite;
    public int quantity;
    public float price;
    public int type;
}
