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
        if (slider == null)
            Debug.LogError("StatusBar on " + gameObject.name + " is missing a Slider component!");

        parentTransform = transform.root;
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