using UnityEngine;
using TMPro;
using System.Collections;

public class HUDMessage : MonoBehaviour
{
    public static HUDMessage Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI messageText;
    private Coroutine current;

    private void Awake()
    {
        Instance = this;
        messageText.gameObject.SetActive(false);
    }

    public void Show(string message, float seconds = 2f)
    {
        if (current != null)
            StopCoroutine(current);
        current = StartCoroutine(ShowRoutine(message, seconds));
    }
    private IEnumerator ShowRoutine(string message, float seconds)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        messageText.gameObject.SetActive(false);
    }
    public void ShowCountDown(string label, float seconds)
    {
        if (current != null)
            StopCoroutine(current);
        current = StartCoroutine(CountdownRoutine(label, seconds));
    }
    private IEnumerator CountdownRoutine(string label, float seconds)
    {
        messageText.gameObject.SetActive(true);
        float remaining = seconds;
        while (remaining > 0)
        {
            messageText.text = label + Mathf.CeilToInt(remaining) + "s";
            remaining -= Time.deltaTime;
            yield return null;
        }
        messageText.gameObject.SetActive(false);
    } 
    }
