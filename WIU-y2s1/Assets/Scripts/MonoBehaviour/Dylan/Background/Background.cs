using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
    private Transform _camera;
    private Vector3 _cameraStartPosition;
    private float _distance;
    private GameObject[] _backgrounds;
    private Material[] _materials;
    private float[] _bgSpeeds;
    private float _furthestBack;

    [SerializeField][Range(0.01f, 0.05f)] private float _parallaxSpeed;

    private void Start()
    {
        _camera = Camera.main.transform;
        _cameraStartPosition = _camera.position;

        int bgCount = transform.childCount;
        _materials = new Material[bgCount];
        _bgSpeeds = new float[bgCount];
        _backgrounds = new GameObject[bgCount];

        for (int i = 0; i < bgCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i).gameObject;
            _materials[i] = _backgrounds[i].GetComponent<Renderer>().material;
        }
        CalculateBGSpeed(bgCount);
    }

    private void CalculateBGSpeed(int bgCount)
    {
        for (int i = 0; i < bgCount; i++)
        {
            if ((_backgrounds[i].transform.position.z - _camera.position.z) > _furthestBack)
            {
                _furthestBack = _backgrounds[i].transform .position.z + _camera.position.z;
            }
        }
        for (int i = 0; i < bgCount; i++)
        {
            _bgSpeeds[i] = 1 - (_backgrounds[i].transform.position.z - _camera.position.z) / (_furthestBack);
        }
    }
    private void LateUpdate()
    {
        _distance = _camera.position.x - _cameraStartPosition.x;
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            float speed = (_bgSpeeds[i] / 100)* _parallaxSpeed * Time.deltaTime;
            _materials[i].SetTextureOffset("_MainTex", new Vector2(_distance, speed));
        }
    }
}

//This class was done by Yap Jun Hong Dylan