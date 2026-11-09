using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "2-Gameplay";

    [Header("Paneles")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Botones")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private Button[] backButtons;

    private void Start()
    {
        Time.timeScale = 1f;

        if (playButton != null)
            playButton.onClick.AddListener(Play);
        if (howToPlayButton != null)
            howToPlayButton.onClick.AddListener(ShowHowToPlay);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(ShowSettings);

        if (quitButton != null)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                quitButton.SetActive(false);
            else
                quitButton.GetComponent<Button>().onClick.AddListener(Quit);
        }

        foreach (Button back in backButtons)
        {
            if (back != null)
                back.onClick.AddListener(ShowMain);
        }

        ShowMain();
    }

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ShowMain()
    {
        mainPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}