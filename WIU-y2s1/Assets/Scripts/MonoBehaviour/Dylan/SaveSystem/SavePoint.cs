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
    GameObject _player;
    [SerializeField] float _distanceToSave = 1.0f;
    [SerializeField] int _currentLevel;
    [SerializeField] string id = "";
    private void Awake()
    {
    }
    private void Start()
    {
        _player = FindFirstObjectByType<_PlayerController>().gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            var player = FindAnyObjectByType<_PlayerController>();
            if (Vector3.Distance(player.transform.position, transform.position) <= _distanceToSave)
            {
                //Mark this location as the current save point and save the game
                _currSavePoint = true;
                GameSceneManager.Instance.LoadMenu(
                    GameSceneManager.Instance.GetSaveIndex());
                //DataPersistenceManager.Instance.SaveGame();
            }
        }
    }
    public void SaveData(GameData data)
    {
        if (data.savePoints.ContainsKey(id))
        { data.savePoints.Remove(id); }
        data.savePoints.Add(id, _currSavePoint);
        data._currentLevel = _currentLevel;
    }
    public void LoadData(GameData data)
    {
        if (data.savePoints.TryGetValue(id, out bool isSavePoint))
            _currSavePoint = isSavePoint;
        if (_currSavePoint)
        {
            _player.transform.position = transform.position;
            //So that when saving the game only will have one save point marked as true
            _currSavePoint = false;
        }
    }
}
