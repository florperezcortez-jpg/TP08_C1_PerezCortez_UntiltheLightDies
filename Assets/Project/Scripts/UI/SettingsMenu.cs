using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider uiSlider;

    private void Start()
    {
        Setup(masterSlider, "MasterVolume");
        Setup(musicSlider, "MusicVolume");
        Setup(fxSlider, "FXVolume");
        Setup(uiSlider, "UIVolume");
    }

    private void Setup(Slider slider, string parameter)
    {
        if (slider == null)
            return;

        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
        slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(parameter, 0.8f));

        slider.onValueChanged.AddListener(value =>
        {
            PlayerPrefs.SetFloat(parameter, value);
            PlayerPrefs.Save();
            if (AudioManager.Instance != null)
                AudioManager.Instance.SetVolume(parameter, value);
        });
    }
}