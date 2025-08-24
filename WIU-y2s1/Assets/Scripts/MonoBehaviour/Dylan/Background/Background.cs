using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
    private float _currentPosition, _length;
    [SerializeField] private GameObject _camera;
    [SerializeField][Range(0.0f, 1.0f)] private float _parallaxEffect = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentPosition = transform.position.x;
        _length = transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float displacement = _camera.transform.position.x * _parallaxEffect;
        float backgroundMovement = _camera.transform.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_currentPosition + displacement, 0, 0);
        if (backgroundMovement > _currentPosition + _length)
        {
            _currentPosition += _length;
        }
        else if (backgroundMovement < _currentPosition - _length)
        {
            _currentPosition -= _length;
        }
        {
            
        }
    }
}

//This class was done by Yap Jun Hong Dylan