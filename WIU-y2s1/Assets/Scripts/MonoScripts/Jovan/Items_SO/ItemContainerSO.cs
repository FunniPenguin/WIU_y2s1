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

            if (data.Name == name)
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
