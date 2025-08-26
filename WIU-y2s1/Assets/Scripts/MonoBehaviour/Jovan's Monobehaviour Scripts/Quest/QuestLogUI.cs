using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestLogUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform questListParent; // The vertical panel on the left
    [SerializeField] private GameObject questRowPrefab; // One row with TMP Texts

    private List<GameObject> spawnedQuestRows = new();

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // Clear old rows
        foreach (var row in spawnedQuestRows)
        { Destroy(row); }
        spawnedQuestRows.Clear();

        // Spawn fresh rows
       
            GameObject UIRow = Instantiate(questRowPrefab, questListParent);
            var texts = UIRow.GetComponentsInChildren<TextMeshProUGUI>();

            if (texts.Length >= 3)
            {
                texts[0].text = QuestManager.Instance.GetActiveQuest().GetName();
                texts[1].text = QuestManager.Instance.GetActiveQuest().GetDescription();
                texts[2].text = $"{QuestManager.Instance.GetActiveQuest().GetObjectiveProgress()}/{QuestManager.Instance.GetActiveQuest().GetCompletionCount()}";
                texts[3].text = "Quest";
            }

            spawnedQuestRows.Add(UIRow);
        
    }

    // Call this after updating a quest's progress
    public void UpdateQuestUI()
    {
        for (int i = 0; i < spawnedQuestRows.Count; i++)
        {
            var row = spawnedQuestRows[i];
            var texts = row.GetComponentsInChildren<TextMeshProUGUI>();

            texts[2].text = $"[" + $"{QuestManager.Instance.GetActiveQuest().GetObjectiveProgress()}/{QuestManager.Instance.GetActiveQuest().GetCompletionCount()}" + $"]";
        }
    }
}

//Class is made by Jovan
//Edited by Dylan: Linked the quest UI to the quest manager