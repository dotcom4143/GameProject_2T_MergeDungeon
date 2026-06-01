using UnityEngine;
using UnityEngine.UI;

public class SettingWindowController : MonoBehaviour
{
    [Header("설정 패널들")]
    public GameObject audioPanel;
    public GameObject videoPanel;
    public GameObject controlsPanel;

    // 첫 시작 시 오디오 패널만 켜기
    void Start()
    {
        ShowAudioPanel();
    }

    // AUDIO 버튼에 연결할 함수
    public void ShowAudioPanel()
    {
        audioPanel.SetActive(true);
        videoPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    // VIDEO 버튼에 연결할 함수
    public void ShowVideoPanel()
    {
        audioPanel.SetActive(false);
        videoPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    // CONTROLS 버튼에 연결할 함수
    public void ShowControlsPanel()
    {
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
}