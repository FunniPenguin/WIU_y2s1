using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestLogUI : MonoBehaviour
{
    [Header("Quest Data Sources")]
    [SerializeField] private List<QuestData> activeQuests = new List<QuestData>();

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
            Destroy(row);
        spawnedQuestRows.Clear();

        // Spawn fresh rows
        foreach (var quest in activeQuests)
        {
            GameObject row = Instantiate(questRowPrefab, questListParent);
            var texts = row.GetComponentsInChildren<TextMeshProUGUI>();

            if (texts.Length >= 3)
            {
                texts[0].text = quest.GetName();
                texts[1].text = quest.GetDescription();
                texts[2].text = $"{quest.GetObjectiveProgress()}/{quest.GetCompletionCount()}";
                texts[3].text = "Quest";
            }

            spawnedQuestRows.Add(row);
        }
    }

    // Call this after updating a quest's progress
    public void UpdateQuestUI()
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            var row = spawnedQuestRows[i];
            var texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            texts[2].text = $"["+ $"{activeQuests[i].GetObjectiveProgress()}/{activeQuests[i].GetCompletionCount()}" + $"]";
        }
    }
}
