using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private float gameOverDelay = 1.5f;

    private void OnEnable()
    {
        retryButton.onClick.AddListener(Retry);
        menuButton.onClick.AddListener(GoToMainMenu);
    }
    private void OnDisable()
    {
        retryButton.onClick.RemoveListener(Retry);
        menuButton.onClick.RemoveListener(GoToMainMenu);
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {

        Invoke(nameof(ShowGameOverPanel), gameOverDelay);

    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
