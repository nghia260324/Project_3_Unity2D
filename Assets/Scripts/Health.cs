using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    private float fillSpeed = 1f;

    public void UpdateHealth(int currentHealth,int maxHealth)
    {
        float targetFillAmount = (float)currentHealth / (float)maxHealth;
        StartCoroutine(ChangeFillAmountOverTime(targetFillAmount));
    }

    IEnumerator ChangeFillAmountOverTime(float targetFillAmount)
    {
        float currentFillAmount = healthBar.fillAmount;

        while (currentFillAmount != targetFillAmount)
        {
            currentFillAmount = Mathf.MoveTowards(currentFillAmount, targetFillAmount, fillSpeed * Time.deltaTime);
            healthBar.fillAmount = currentFillAmount;

            yield return null;
        }
    }
}
