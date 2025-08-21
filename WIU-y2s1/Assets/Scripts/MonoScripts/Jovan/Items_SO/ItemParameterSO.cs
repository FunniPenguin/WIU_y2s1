using UnityEngine;

namespace _Inventory.Model
{
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField] public string ParameterName { get; private set; }
    }
}
