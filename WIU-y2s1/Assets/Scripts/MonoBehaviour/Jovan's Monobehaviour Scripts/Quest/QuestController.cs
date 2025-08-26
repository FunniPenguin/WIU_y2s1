using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class QuestController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform questUI; // The UI panel
    [SerializeField] private KeyCode toggleKey = KeyCode.J;

    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private Vector2 hiddenOffset = new Vector2(-300f, 0f); // Slide left when hidden

    private bool isVisible = true;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Coroutine currentRoutine;

    private void Awake()
    {
        canvasGroup = questUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = questUI.gameObject.AddComponent<CanvasGroup>();

        originalPosition = questUI.anchoredPosition;
    }

    private void Start()
    {
        ShowQuestUI(isVisible, instant: true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleQuestUI();
        }
    }

    public void ToggleQuestUI()
    {
        isVisible = !isVisible;
        ShowQuestUI(isVisible);
    }

    public void ShowQuestUI(bool show, bool instant = false)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(AnimateUI(show, instant));
    }

    private IEnumerator AnimateUI(bool show, bool instant)
    {
        float time = 0f;
        float startAlpha = canvasGroup.alpha;
        float endAlpha = show ? 1f : 0f;

        Vector2 startPos = questUI.anchoredPosition;
        Vector2 endPos = show ? originalPosition : originalPosition + hiddenOffset;

        if (instant)
        {
            canvasGroup.alpha = endAlpha;
            questUI.anchoredPosition = endPos;
            canvasGroup.blocksRaycasts = show;
            yield break;
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            questUI.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        questUI.anchoredPosition = endPos;
        canvasGroup.blocksRaycasts = show;
    }
}
