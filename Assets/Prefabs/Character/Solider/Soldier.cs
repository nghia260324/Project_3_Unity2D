using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Soldier", menuName = "Item Soldier/Create New Item")]
public class Soldier : ScriptableObject
{
    public int id;
    public string nameSolider;
    public RenderTexture textureSolider;
    public int damageSolider;
    public int healthSolider;
    public int speedSolider;
    public int armorSolider;
    public int level;
    public bool status;
    public string descibe;
    public TrainingCosts costs;
}
