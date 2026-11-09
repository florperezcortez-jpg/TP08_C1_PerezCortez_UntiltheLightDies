using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private string gameSceneName = "Gameplay";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void OnEnable()
    {
        retryButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(Retry);
        menuButton.onClick.AddListener(GoToMenu);
    }

    private void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}