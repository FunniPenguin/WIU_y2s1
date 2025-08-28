using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        // Add a button to the panel
        public void AddButton(string name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        // Toggle the panel visibility
        internal void Toggle(bool val)
        {
            if (val == true)
            {
                RemoveOldButtons();
            }
            gameObject.SetActive(val);
        }

        // Remove all old buttons
        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}

// Made by Jovan Yeo Kaisheng