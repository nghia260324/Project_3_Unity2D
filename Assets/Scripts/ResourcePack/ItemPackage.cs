using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPackage : MonoBehaviour
{
    public ResourceManager m_ResourceManager;
    public ItemResourcePack item;

    public void BuyResources()
    {
        if (item.type == 0)
        {
            m_ResourceManager.AddResource("gem", item.quantity);
        } else
        {
            if (m_ResourceManager.CanSubtractResource(("gem"), (int)item.price))
            {
                switch (item.type)
                {
                    case 1:
                        m_ResourceManager.AddResource("coin", item.quantity);
                        break;
                    case 2:
                        m_ResourceManager.AddResource("food", item.quantity);
                        break;
                    case 3:
                        m_ResourceManager.AddResource("wood", item.quantity);
                        break;
                    case 4:
                        m_ResourceManager.AddResource("stone", item.quantity);
                        break;
                }
            }
        }
        m_ResourceManager.UpdateResource();
    }
}
