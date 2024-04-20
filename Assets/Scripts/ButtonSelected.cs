using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    public ResourceManager m_ResourceManager;
    public RectTransform selectedImage;
    private Button[] buttons;

    public Transform ItemContent;
    public GameObject ResourcePackItem;

    public List<ItemResourcePack> ItemGems = new List<ItemResourcePack>();
    public List<ItemResourcePack> ItemCoins = new List<ItemResourcePack>();
    public List<ItemResourcePack> ItemFoods = new List<ItemResourcePack>();
    public List<ItemResourcePack> ItemWoods = new List<ItemResourcePack>();
    public List<ItemResourcePack> ItemStones = new List<ItemResourcePack>();

    public Sprite iconGem;
    public Sprite iconCoin;
    public Sprite iconFood;
    public Sprite iconWood;
    public Sprite iconStone;

    private int type;

    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    public void OnButtonClick(int buttonIndex)
    {
        type = buttonIndex;
        Vector2 targetPosition = new Vector2(selectedImage.position.x, buttons[buttonIndex].transform.position.y);
        StartCoroutine(MoveImageSmoothly(selectedImage, targetPosition));

        switch(buttonIndex)
        {
            case 0:
                ListItems(ItemGems, iconGem);
                break;
            case 1:
                ListItems(ItemCoins, iconCoin);
                break;
            case 2:
                ListItems(ItemFoods, iconFood);
                break;
            case 3:
                ListItems(ItemWoods, iconWood);
                break;
            case 4:
                ListItems(ItemStones, iconStone);
                break;
            default:break;
        }
    }

    IEnumerator MoveImageSmoothly(RectTransform image, Vector3 target)
    {
        float duration = 0.2f;
        float timer = 0f;

        Vector3 startPosition = image.position;

        while (timer < duration)
        {
            image.position = Vector3.Lerp(startPosition, target, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        image.position = target;
    }

    public void ListItems(List<ItemResourcePack> Items,Sprite icon)
    {
        if (Items == null) return;
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (ItemResourcePack item in Items)
        {
            GameObject obj = Instantiate(ResourcePackItem, ItemContent);
            var itemName = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("QuantityFrame").Find("IconPack").GetComponent<Image>();
            var itemQuantity = obj.transform.Find("QuantityFrame").Find("Image").Find("Quantity").GetComponent<TextMeshProUGUI>();
            var itemPrice = obj.transform.Find("ButtonBuy").Find("Price").GetComponent<TextMeshProUGUI>();
            var itemIconBuy = obj.transform.Find("ButtonBuy").Find("Image").GetComponent<Image>();
            var itemI = obj.transform.Find("QuantityFrame").Find("Image").Find("Icon").GetComponent<Image>();

            ItemPackage ip = obj.GetComponent<ItemPackage>();
            ip.item = item;
            ip.m_ResourceManager = m_ResourceManager;

            itemI.sprite = icon;

            itemName.text = item.nameItem;
            itemIcon.sprite = item.sprite;
            itemQuantity.text = FormatNumber(item.quantity);
            
            if (item.price == 0)
            {
                itemPrice.text = "Free";
                itemIconBuy.enabled = false;
            }
            else
            {
                switch (type)
                {
                    case 0:
                        itemPrice.text = "$" + item.price;
                        itemIconBuy.enabled = false;
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        itemPrice.text = item.price.ToString();
                        itemIconBuy.enabled = true;
                        itemIconBuy.sprite = iconGem;
                        break;
                }
            }
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
}
