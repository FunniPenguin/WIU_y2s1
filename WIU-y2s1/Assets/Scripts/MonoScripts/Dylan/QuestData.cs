using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "QuestData", menuName = "QuestData/QuestData")]
public class QuestData : ScriptableObject
{
    private bool _completionStatus = false;
    [SerializeField] private string _name = "";
    [SerializeField] private string _description = "";
    private int _objectiveProgress = 0;
    [SerializeField] private int _completionCount = 1;
    [SerializeField] private string GUID = "";

    private void Awake()
    {
        _objectiveProgress = 0;
        _completionStatus = false;
    }
    public bool GetCompletionStatus()
    {
        return _completionStatus;
    }
    public string GetName()
    {
        return _name;
    }
    public string GetDescription()
    {
        return _description;
    }
    public int GetObjectiveProgress()
    {
        return _objectiveProgress;
    }
    public int GetCompletionCount() { 
        return _completionCount;
    }
    public void SetCompletionStatus(bool Status) { 
        _completionStatus = Status;
    }
    public void UpdateObjectiveCount(int Count)
    {
        _objectiveProgress += Count;
        if (_objectiveProgress > _completionCount)
        {
            _objectiveProgress = _completionCount;
        }
        if (_objectiveProgress == _completionCount)
        {
            QuestManager.Instance.CompleteQuest();
        }
    }
    public string GetGUID()
    {
        return GUID;
    }
}
