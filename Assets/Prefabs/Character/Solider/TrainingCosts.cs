using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Costs", menuName = "Item Costs/Create New Item")]
public class TrainingCosts : ScriptableObject
{
    public int id;
    public int foodCosts;
    public int woodCosts;
    public int stoneCosts;
    public int cointCosts;
    public int gemCosts;
    public int timeTraining;
}
