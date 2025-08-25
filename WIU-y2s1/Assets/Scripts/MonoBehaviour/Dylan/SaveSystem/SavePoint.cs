using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using UnityEditor.Overlays;
using Unity.VisualScripting;

public class SavePoint : MonoBehaviour, IDataPersistence
{
    private string _id;
    private bool _currSavePoint;
    [SerializeField] GameObject _playerPrefab;
    private GameObject _player;
    private void Awake()
    {
        
    }
    private void Start()
    {
        FindFirstObjectByType<_PlayerController>();
    }
    private void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < 2.0f)
                DataPersistenceManager.Instance.SaveGame();
        }
    }
    public void SaveData(ref GameData data)
    {

    }
    public void LoadData(GameData data)
    {
        
        if (_currSavePoint)
        {
            Instantiate(_player);
            _currSavePoint = false;
        }
    }
}
