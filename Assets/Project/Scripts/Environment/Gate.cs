using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gateRenderer;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Collider2D blocker;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private AudioClip openSound;

    private bool isOpen;
    private bool finished;
    private float nextMessageTime;

    private void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
}
    private void Update()
    {
        if (isOpen || GameStats.Instance == null)
            return;

        if (GameStats.Instance.HasAllKeys)
            Open();
    }
    private void Open()
    {
        isOpen = true;

        if (gateRenderer != null)
        {
            if (openSprite != null)
                gateRenderer.sprite = openSprite;
            else
                gateRenderer.color = new Color(1f, 1f, 1f, 0.4f);
        }

        if (blocker != null)
            blocker.enabled = false;

        if (openSound != null)
            AudioSource.PlayClipAtPoint(openSound, transform.position);

        if (HUDMessage.Instance != null)
            HUDMessage.Instance.Show("¡La reja se abrió!", 3f);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (finished)
            return;

        if (other.GetComponentInParent<PlayerHealth>() == null)
            return;

        if (!isOpen)
        {
            if (Time.time >= nextMessageTime && HUDMessage.Instance != null && GameStats.Instance != null)
            {
                nextMessageTime = Time.time + 2f;
                HUDMessage.Instance.Show("Faltan llaves: " + GameStats.Instance.Keys + "/" + GameStats.Instance.KeysRequired);
            }
            return;
        }

        finished = true;
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}