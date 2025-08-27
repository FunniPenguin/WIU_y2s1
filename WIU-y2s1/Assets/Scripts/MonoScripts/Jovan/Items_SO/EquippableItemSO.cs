using System.Collections.Generic;
using UnityEngine;

namespace _Inventory.Model
{
    [CreateAssetMenu(fileName = "EquippableItemSO", menuName = "Inventory/EquippableItemSO")]
    public class EquippableItemSO : ItemSO
    {
        private void Awake()
        {
            ActionName = "Equip";
        }

        public override bool PerformAction(GameObject character)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();

            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, DefaultParameterList);
                return true;
            }
            return false;
        }
    }
}
