using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private Slider slider;
    private Transform targetTransform;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private bool offsetPosition = true;
    void Awake()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
            Debug.LogError("StatusBar on " + gameObject.name + " is missing a Slider component!");
    }
    private void Update()
    {

        if (targetTransform == null) return;

        if (offsetPosition) { 
            transform.position = targetTransform.position + offset;
        }
    }

    public void Initialize(Transform target)
    {
        targetTransform = target;
    }

    public void UpdateStatusBar(float CurrentValue, float MaxValue)
    {
        if (slider == null) return;
        if (MaxValue <= 0) return;
        slider.value = CurrentValue / MaxValue;
    }
}
// Added By Jovan Yeo Kaisheng