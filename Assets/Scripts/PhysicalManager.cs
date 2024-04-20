using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalManager : MonoBehaviour
{
    public static PhysicalManager Instance;

    public Image physicalBar;

    [Header("Setting")]
    public float minValue;
    public float maxValue;
    public float fillSpeed = 0.2f;


    private void Awake()
    {
        Instance = this;
    }

    private bool isMoving = false;
    private bool isPhysical;

    void Update()
    {
        isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (GetPhysicalBarFillAmount() >= maxValue)
        {
            isPhysical = true;
        }
        if (GetPhysicalBarFillAmount() <= 0.01f)
        {
            isPhysical = false;
        }

        if (isMoving && isShiftPressed && isPhysical)
        {
            StartCoroutine(ChangeFillAmountOverTime(-0.01f));
        }
        else
        {
            StartCoroutine(ChangeFillAmountOverTime(0.01f));
        }
    }

    public float GetPhysicalBarFillAmount()
    {
        return physicalBar.fillAmount;
    }

    IEnumerator ChangeFillAmountOverTime(float changeAmount)
    {
        float targetFillAmount = Mathf.Clamp(physicalBar.fillAmount + changeAmount, 0f, 0.4f);
        float currentFillAmount = physicalBar.fillAmount;

        while (currentFillAmount != targetFillAmount)
        {
            currentFillAmount = Mathf.MoveTowards(currentFillAmount, targetFillAmount, fillSpeed * Time.deltaTime);
            physicalBar.fillAmount = currentFillAmount;

            yield return null;
        }
    }
}
