using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class QuestManager : MonoBehaviour, IDataPersistence
{
    public UnityEvent _questCompleted;
    public UnityEvent _allGameQuestsCompleted;
    //Start of singleton
    private static QuestManager _instance;
    public static QuestManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy() { if (this == _instance) { _instance = null; } }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    //End of singleton

    [SerializeField] private List<QuestData> _questList;
    private QuestData _activeQuest = null;
    private int _currentQuestIndex = 0;

    public QuestData GetActiveQuest()
    {
        return _activeQuest;
    }
    public void CompleteQuest()
    {
        _activeQuest.SetCompletionStatus(true);
        _questCompleted.Invoke();
        if (_currentQuestIndex < _questList.Count)
        {
            _currentQuestIndex++;
            _activeQuest = _questList[_currentQuestIndex];
        }
        else
        {
            _allGameQuestsCompleted.Invoke();
        }
    }
    public void SaveData(ref GameData data)
    {
        
    }
    public void LoadData(GameData data)
    {

    }
}
