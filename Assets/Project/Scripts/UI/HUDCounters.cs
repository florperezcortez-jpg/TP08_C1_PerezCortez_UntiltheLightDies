using TMPro;
using UnityEngine;

public class HUDCounters : MonoBehaviour
{
    [SerializeField] private TextMeshPro coinsText;
    [SerializeField] private TextMeshPro keyText;

    private void Update()
    {
        if (GameStats.Instance == null)
            return;

        coinsText.text = "Monedas: " + GameStats.Instance.Coins;
        keyText.text = "Llaves: " + GameStats.Instance.Keys + "/" + GameStats.Instance.KeysRequired;
    }
}
