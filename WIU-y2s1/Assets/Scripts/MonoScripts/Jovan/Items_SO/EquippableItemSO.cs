using UnityEngine;

namespace _Inventory.Model
{
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character)
        {
            throw new System.NotImplementedException();
        }
    }
}
