using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    private string volumeParameter = "MasterVolume";

    [Header("UI Slider")]
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.minValue = 0.0001f;
        volumeSlider.maxValue = 1f;

        float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        float dBValue = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat(volumeParameter, dBValue);
        PlayerPrefs.SetFloat("SavedMasterVolume", sliderValue);
    }
}