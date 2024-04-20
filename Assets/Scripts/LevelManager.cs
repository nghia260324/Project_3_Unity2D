using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public Health healthExp;
    public Image experienceBar;
    public TextMeshProUGUI level;
    public int currentExperience = 0;
    public int currentLevel = 1;
    private int maxLevel = 100;
    private int maxExp;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        maxExp = CalculateExperienceForNextLevel(currentLevel, 100);
        //healthExp.UpdateHealth(currentExperience, maxExp);
        level.text = currentLevel.ToString();
    }

    public void EnemyDefeated(int exp)
    {
        currentExperience += exp;
        if (!CheckForLevelUp())
        {
            healthExp.UpdateHealth(currentExperience, maxExp);
        }
    }

    private bool CheckForLevelUp()
    {
        if (currentExperience >= maxExp)
        {
            LevelUp();
            return true;
        }
        return false;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void LevelUp()
    {
        if (currentLevel == maxLevel) return;
        currentLevel += 1;
        level.text = currentLevel.ToString();
        currentExperience = 0;
        maxExp = CalculateExperienceForNextLevel(currentLevel, 100);
        experienceBar.fillAmount = 0;
    }

    public int CalculateExperienceForNextLevel(int currentLevel, int experienceForLevel1)
    {
        int expForCurrentLevel = experienceForLevel1;
        for (int i = 1; i < currentLevel; i++)
        {
            expForCurrentLevel += (int)Math.Round(expForCurrentLevel / 2.0);
        }
        return expForCurrentLevel;
    }


}
