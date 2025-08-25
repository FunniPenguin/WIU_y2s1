using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private Slider slider;
    private Transform parentTransform;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private bool offsetPosition = true;
    void Awake()
    {
        slider = GetComponent<Slider>();
        parentTransform = GetComponentInParent<Transform>();
    }
    private void Update()
    {
        if (offsetPosition) { 
            transform.position = parentTransform.position + offset;
        }
    }

    public void UpdateStatusBar(float CurrentValue, float MaxValue)
    {
        slider.value = CurrentValue / MaxValue;
    }
}