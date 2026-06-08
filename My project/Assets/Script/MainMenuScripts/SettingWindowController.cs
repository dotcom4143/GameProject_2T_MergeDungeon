using UnityEngine;
using UnityEngine.UI;

public class SettingWindowController : MonoBehaviour
{
    [Header("설정 패널들")]
    public GameObject audioPanel;
    public GameObject videoPanel;
    public GameObject controlsPanel;

    [Header("우측 상단 X(닫기) 버튼")]
    [SerializeField] private Button closeButton;

    private void Start()
    {
        ShowAudioPanel();

        if (closeButton != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(CloseWindow);
        }
    }

    public void ShowAudioPanel()
    {
        audioPanel.SetActive(true);
        videoPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ShowVideoPanel()
    {
        audioPanel.SetActive(false);
        videoPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControlsPanel()
    {
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
}