using UnityEngine;

namespace _Inventory.Model
{
    [CreateAssetMenu(fileName = "ItemParameterSO", menuName = "Inventory/ItemParameterSO", order = 0)]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField] 
        public string ParameterName { get; private set; }
    }
}
