using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    public static CostManager Instance;
    public Soldier soldier;

    public RawImage rawImage;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI health;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI food;
    public TextMeshProUGUI wood;
    public TextMeshProUGUI stone;
    public TextMeshProUGUI coint;
    public TextMeshProUGUI gem;
    public TextMeshProUGUI time;
    public TextMeshProUGUI describe;
    public TextMeshProUGUI quantity;

    [Header("Setting")]
    public Slider slider;
    public int minValueSlider;
    public int maxValueSlider;


    private int getFood;
    private int getWood;
    private int getStone;
    private int getCoint;
    private int getGem;
    private int getTime;

    void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        getFood = soldier.costs.foodCosts;
        getWood = soldier.costs.woodCosts;
        getStone = soldier.costs.stoneCosts;
        getCoint = soldier.costs.cointCosts;
        getGem = soldier.costs.gemCosts;
        getTime = (int)soldier.costs.timeTraining;

        slider.minValue = minValueSlider;
        slider.maxValue = maxValueSlider;
        slider.value = maxValueSlider;
        slider.onValueChanged.AddListener(delegate { UpdateTextValue(); });
        FillInf();
        UpdateTextValue();
    }

    public void UpdateTextValue()
    {
        quantity.text = slider.value.ToString();
        getFood = soldier.costs.foodCosts * (int)slider.value;
        getWood = soldier.costs.woodCosts * (int)slider.value;
        getStone = soldier.costs.stoneCosts * (int)slider.value;
        getCoint = soldier.costs.cointCosts * (int)slider.value;
        getGem = soldier.costs.gemCosts * (int)slider.value;
        getTime = soldier.costs.timeTraining * (int)slider.value;
        FillCost();
    }

    public void FillInf()
    {
        rawImage.texture = soldier.textureSolider;
        damage.text = "Damage: " + soldier.damageSolider;
        health.text = "Health: " + soldier.healthSolider;
        speed.text = "Speed: " + soldier.speedSolider;
        armor.text = "Armor: " + soldier.armorSolider;
        describe.text = "   - " + soldier.descibe;
        FillCost();
    }

    public void TrainImmediately()
    {
        if (ResourceManager.Instance.CanSubtractResource("gem", getGem))
        {
            SoldierManager.Instance.UpdateSoldierQuantity(soldier, (int)slider.value);
            ResourceManager.Instance.UpdateResource();
        }
    }

    public void TrainSlowly()
    {

    }

    public void FillCost()
    {
        food.text = "Food: " + FormatNumber((float)getFood);
        wood.text = "Wood: " + FormatNumber((float)getWood);
        stone.text = "Stone: " + FormatNumber((float)getStone);
        coint.text = "Coint: " + FormatNumber((float)getCoint);
        gem.text = FormatNumber((float)getGem);
        time.text = FormatSecondsToTime(getTime);
    }

    public string FormatSecondsToTime(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        return timeString;
    }
    private static string FormatNumber(float number)
    {
        string suffix = "";
        if (number >= 1000)
        {
            suffix = "k";
            number /= 1000f;
        }
        if (number >= 1000)
        {
            suffix = "M";
            number /= 1000f;
        }
        if (number >= 1000)
        {
            suffix = "B";
            number /= 1000f;
        }
        return number.ToString("F1") + suffix;
    }
}
