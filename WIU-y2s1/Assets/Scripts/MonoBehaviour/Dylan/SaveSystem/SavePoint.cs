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
    [SerializeField] float _distanceToSave = 1.0f;
    [SerializeField] int _currentLevel;
    [SerializeField] string id = "";
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
            if (Vector3.Distance(_player.transform.position, transform.position) <= _distanceToSave)
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
            Instantiate(_playerPrefab);
            _currSavePoint = false;
        }
    }
}
