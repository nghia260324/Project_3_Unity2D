using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite newImage;

    private Image buttonImage;
    private Sprite originalImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        originalImage = buttonImage.sprite;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonImage != null && newImage != null)
        {
            buttonImage.sprite = newImage;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = originalImage;
        }
    }
}
