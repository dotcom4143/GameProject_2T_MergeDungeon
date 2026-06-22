using UnityEngine;
using TMPro;
using System.Collections;

public class CraftWarningManager : MonoBehaviour
{
    public static CraftWarningManager Instance { get; private set; }

    [Header("경고 문구를 출력할 TMP 텍스트")]
    public TextMeshProUGUI warningText;

    [Header("문구가 유지될 시간 (초)")]
    public float displayDuration = 2f;

    private Coroutine currentCoroutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (warningText != null) warningText.gameObject.SetActive(false);
    }

    public void ShowMaterialWarning()
    {
        ShowMessage("재료가 부족하여 제작이 불가능합니다.");
    }

    public void ShowMessage(string message)
    {
        if (warningText == null) return;

        warningText.text = message;
        warningText.gameObject.SetActive(true);

        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(HideTextAfterDelay());
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        warningText.gameObject.SetActive(false);
    }
}