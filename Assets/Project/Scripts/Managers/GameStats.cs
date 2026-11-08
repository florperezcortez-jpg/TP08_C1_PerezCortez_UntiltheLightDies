using UnityEngine;

public class GameStats : MonoBehaviour
{
  public static GameStats Instance {  get; private set; }
    [SerializeField] private int keysRequired = 3;

    public int Coins {  get; private set; }
    public int Keys {  get; private set; }
    public int KeysRequired => keysRequired;

    private void Awake()
    {
        Instance = this;
        Coins = PlayerPrefs.GetInt("Coins", 0);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
    }
    public void AddKey()
    {
        Keys++;
    }
}
