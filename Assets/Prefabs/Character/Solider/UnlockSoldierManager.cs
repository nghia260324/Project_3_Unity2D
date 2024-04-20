using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSoliderManager : MonoBehaviour
{
    public static UnlockSoliderManager Instance;
    public List<Soldier> Soldiers = new List<Soldier>();

    public Transform ItemContent;
    public GameObject SoldierItem;

    public Sprite button;

    private int currentLevel;
    private void Awake()
    {
        Instance = this;
    }

    public void ListSolider()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Soldier item in Soldiers)
        {
            GameObject obj = Instantiate(SoldierItem, ItemContent);
            var itemName = obj.transform.Find("Name").Find("Soldier").GetComponent<TextMeshProUGUI>();
            var itemRawImage = obj.transform.Find("SoldierImage").GetComponent<RawImage>();

            var information = obj.transform.Find("SoldierImage").Find("Information").GetComponent<Transform>();

            var damage = information.Find("Damage").Find("D").GetComponent<TextMeshProUGUI>();
            var health = information.Find("Health").Find("H").GetComponent<TextMeshProUGUI>();
            var speed = information.Find("Speed").Find("S").GetComponent<TextMeshProUGUI>();
            var armor = information.Find("Armor").Find("A").GetComponent<TextMeshProUGUI>();

            var level = obj.transform.Find("Level");
            var levelText = level.transform.Find("L").GetComponent<TextMeshProUGUI>();

            obj.GetComponent<ItemSoliderManager>().SetInfSolider(item);

            itemName.text = item.nameSolider;
            itemRawImage.texture = item.textureSolider;
            damage.text = "Damage:\n" + item.damageSolider;
            health.text = "Health:\n" + item.healthSolider;
            speed.text = "Speed:\n" + item.speedSolider;
            armor.text = "Armor:\n" + item.armorSolider;


            currentLevel = LevelManager.Instance.GetCurrentLevel();

            if (item.status)
            {
                levelText.text = "Training";
                level.GetComponent<Image>().sprite = button;
            } else
            {
                if (currentLevel >= item.level)
                {
                    levelText.text = "Unlock";
                }
                else
                {
                    levelText.text = "Level " + item.level;
                }
            }
        }
    }
}
