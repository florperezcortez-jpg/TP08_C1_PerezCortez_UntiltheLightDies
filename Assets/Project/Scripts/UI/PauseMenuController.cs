using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("Scene Config")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;

    private bool isPaused;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(ReturnToMainMenu);
    }
    private void Update()
    {
        if (gameOverPanel.activeSelf)
            return;
        // Al presionar Escape en la escena Gameplay
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    private void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReturnToMainMenu()
    {
        // Asegura que si el tiempo estaba pausado vuelva a la velocidad normal (1)
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);

    }
}