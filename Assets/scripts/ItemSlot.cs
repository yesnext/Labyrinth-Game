using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSlot : MonoBehaviour
{
    private GameObject item; // Item assigned to this slot

    public void SetItem(GameObject newItem)
    {
        item = newItem;
    }

    public GameObject GetItem()
    {
        return item;
    }
}
