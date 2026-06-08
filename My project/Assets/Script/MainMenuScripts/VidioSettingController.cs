using UnityEngine;
using UnityEngine.UI;

public class VidioSettingsController : MonoBehaviour
{
    [SerializeField] private Toggle fullscreenToggle;

    private void Start()
    {
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}