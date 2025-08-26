using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class QuestManager : MonoBehaviour, IDataPersistence
{
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

    //End of singleton

    private List<QuestData> _questList;
    [SerializeField] private QuestSOContainer _questContainer;
    private QuestData _activeQuest = null;
    private int _currentQuestIndex = 0;
    private void Start()
    {
        _questList = new List<QuestData>();
    }
    public QuestData GetActiveQuest()
    {
        return _activeQuest;
    }
    public void UpdateActiveQuest(int progress)
    {
        _activeQuest.UpdateObjectiveCount(progress);
    }
    public void CompleteQuest()
    {
        _activeQuest.SetCompletionStatus(true);
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
    public void SaveData(GameData data)
    {
        foreach (QuestData questData in _questList)
        {
            if (data.questData.ContainsKey(questData.GetGUID()))
            {
                data.questData.Remove(questData.GetGUID());
            }
            data.questData.Add(questData.GetGUID(), questData.GetCompletionCount());
        }
        data._activeQuestGUID = _activeQuest.GetGUID();
    }
    public void LoadData(GameData data)
    {
        //load all the quest data
        foreach (KeyValuePair<string, int> entry in data.questData)
        {
            QuestData LoadedQuest = _questContainer.FindQuest(entry.Key);
            if (LoadedQuest != null)
            {
                LoadedQuest.UpdateObjectiveCount(entry.Value);
                _questList.Add(LoadedQuest);
            }
        }
        //Ensure that in a new save file all the quests are loaded
        if (_questList.Count < _questContainer.GetQuetsList().Length)
        {
            foreach (QuestData quest in _questContainer.GetQuetsList())
            {
                if (!_questList.Contains(quest))
                {
                    _questList.Add(quest);
                }
            }
        }
        QuestData LoadActiveQuest = _questContainer.FindQuest(data._activeQuestGUID);
        if (LoadActiveQuest != null) { _activeQuest = LoadActiveQuest; }
        else { _activeQuest = _questList[0]; }
    }
}
