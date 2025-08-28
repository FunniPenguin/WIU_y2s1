using _Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemContainerSO", menuName = "Containers/ItemContainerSO")]
public class ItemContainerSO : ScriptableObject
{
    public ItemSO[] _itemList;

    public ItemSO FindItem(string name)
    {
        foreach (ItemSO data in _itemList)
        {

            if (data.ItemKey == name)
            {
                return data;
            }
        }
        return null;
    }
    public ItemSO[] GetQuetsList()
    {
        return _itemList;
    }
}
// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.