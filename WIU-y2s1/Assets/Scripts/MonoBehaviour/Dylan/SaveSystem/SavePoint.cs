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
    private void Awake()
    {
    }
    private void Start()
    {
        FindFirstObjectByType<_PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            var player = FindAnyObjectByType<_PlayerController>();
            if (Vector3.Distance(player.transform.position, transform.position) <= _distanceToSave)
            {
                _currSavePoint = true;
                DataPersistenceManager.Instance.SaveGame();
            }
        }
    }
    public void SaveData(GameData data)
    {
        //if (data.savePoints.ContainsKey(id))
        //{ data.savePoints.Remove(id); }
        //data.savePoints.Add(id, _currSavePoint);
    }
    public void LoadData(GameData data)
    {
        
        //if (_currSavePoint)
        //{
        //    Instantiate(_playerPrefab);
        //    _currSavePoint = false;
        //}
    }
}
