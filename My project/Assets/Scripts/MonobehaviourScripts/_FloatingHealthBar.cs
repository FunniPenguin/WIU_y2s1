using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthbar;

    public void UpdateHealthBar(float currentvalue, float maxvalue)
    {
        healthbar.value = currentvalue / maxvalue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
