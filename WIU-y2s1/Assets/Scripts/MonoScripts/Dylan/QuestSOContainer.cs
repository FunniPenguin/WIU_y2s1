using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSOContainer", menuName = "Containers/QuestSOContainer")]
public class QuestSOContainer : ScriptableObject
{
    [SerializeField] private QuestData[] _questList;

    public QuestData FindQuest(string ID)
    {
        foreach (QuestData data in _questList)
        {
            if (data.GetGUID() == ID)
            {
                return data;
            }
        }
        return null;
    }
    public QuestData[] GetQuestList()
    {
        return _questList;
    }
}
