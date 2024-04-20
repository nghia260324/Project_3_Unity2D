using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public TextMeshProUGUI m_Gems;
    public TextMeshProUGUI m_Coins;
    public TextMeshProUGUI m_Foods;
    public TextMeshProUGUI m_Woods;
    public TextMeshProUGUI m_Stones;

    private Dictionary<string, int> resources = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        resources.Add("stone", 0);
        resources.Add("wood", 0);
        resources.Add("food", 0);
        resources.Add("coin", 0);
        resources.Add("gem", 0);
        UpdateResource();
    }

    public void UpdateResource()
    {
        m_Gems.text = FormatNumber(GetResourceCount("gem"));
        m_Coins.text = FormatNumber(GetResourceCount("coin"));
        m_Foods.text = FormatNumber(GetResourceCount("food"));
        m_Woods.text = FormatNumber(GetResourceCount("wood"));
        m_Stones.text = FormatNumber(GetResourceCount("stone"));
    }

    private void ChangeResourceAmount(string resourceName, int amount)
    {
        if (resources.ContainsKey(resourceName))
        {
            resources[resourceName] += amount;
        }
    }

    public void AddResource(string resourceName, int amount)
    {
        ChangeResourceAmount(resourceName, amount);
    }

    public void SubtractResource(string resourceName, int amount)
    {
        ChangeResourceAmount(resourceName, -amount);
    }

    public int GetResourceCount(string resourceName)
    {
        if (resources.ContainsKey(resourceName))
        {
            return resources[resourceName];
        }
        else
        {
            return 0;
        }
    }
    public static string FormatNumber(float number)
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
    public bool CanSubtractResource(string resourceName, int amount)
    {
        if (resources.ContainsKey(resourceName))
        {
            int currentAmount = resources[resourceName];
            if (currentAmount >= amount)
            {
                SubtractResource(resourceName, amount);
                return true;
            }
        }
        return false;
    }
}
