using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    private GameObject item;

    public void SetItem(GameObject newItem)
    {
        item = newItem;
    }

    public GameObject GetItem()
    {
        return item;
    }

    public void ClearSlot()
    {
        item = null;
    }
}
