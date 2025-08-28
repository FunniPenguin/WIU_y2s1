using _Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField] private EquippableItemSO weapon;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemParameter> itemCurrentState;

    // set weapon, if there is an existing weapon, add it back to inventory
    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }

        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.gameObject.GetComponent<EntityStatistics>().ResetDamage();
        this.gameObject.GetComponent<EntityStatistics>().AddDamage(weapon.DefaultParameterList[0].value);
    }
}
// Made by Jovan Yeo Kaisheng