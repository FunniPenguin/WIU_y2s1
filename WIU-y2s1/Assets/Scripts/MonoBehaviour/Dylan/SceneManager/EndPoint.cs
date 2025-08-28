using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    public UnityEvent _EndOfLevel;
    private string _id;
    GameObject _player;
    [SerializeField] float _distanceToInteract = 1.0f;
    [SerializeField] string id = "";
    private void Awake()
    {
        _player = FindFirstObjectByType<_PlayerController>().gameObject;
    }
    private void Update()
    {
        var player = FindAnyObjectByType<_PlayerController>();
        if (Vector3.Distance(player.transform.position, transform.position) <= _distanceToInteract)
        {
            _EndOfLevel.Invoke();
        }
    }
}
