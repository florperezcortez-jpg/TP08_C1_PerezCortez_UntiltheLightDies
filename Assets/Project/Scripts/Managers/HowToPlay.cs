using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private TextMeshProUGUI languageButtonText;
    [SerializeField] private Button languageButton;

    [TextArea(5, 12)]
    [SerializeField]
    private string spanish =
        "CÓMO JUGAR\n\n" +
        "A / D o flechas: moverte\n" +
        "Espacio: saltar (doble salto en el aire)\n" +
        "Click izquierdo: lanzar una orbe de luz\n\n" +
        "Juntá las llaves, derrotá esqueletos y escapá por la reja.\n" +
        "Cuidado: tenés solo 3 corazones.";

    [TextArea(5, 12)]
    [SerializeField]
    private string english =
        "HOW TO PLAY\n\n" +
        "A / D or arrows: move\n" +
        "Space: jump (double jump in the air)\n" +
        "Left CLick: throw a light orb\n\n" +
        "Collect the keys, defeat the skeletons and escape through the gate.\n" +
        "Careful: you only have 3 hearts.";

    private bool isSpanish = true;
    private bool wired;

    private void Awake()
    {
        if (languageButton != null && !wired)
        {
            languageButton.onClick.AddListener(ToggleLanguage);
            wired = true;
        }
    }

    private void OnEnable()
    {
        isSpanish = true;
        Refresh();
    }

    public void ToggleLanguage()
    {
        isSpanish = !isSpanish;
        Refresh();
    }

    private void Refresh()
    {
        contentText.text = isSpanish ? spanish : english;
        languageButtonText.text = isSpanish ? "English" : "Español";
    }
}