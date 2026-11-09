using Unity.VisualScripting;
using UnityEngine;

public enum PickableType
{
   Heal,
   Damage,
   Invulnerability,
   Coin,
   Key
}
public class Pickable : MonoBehaviour
{
    [SerializeField] private PickableType type;
    [SerializeField] private int amount = 1;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float lifetime = 0f;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private string message;

    private void Start()
    {
        if(lifetime > 0f)
        Destroy(gameObject, lifetime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player == null)
            return;

        switch (type)
        {
            case PickableType.Heal:
                player.Heal(amount);
                break;

            case PickableType.Damage:
                PlayerCombat combat = player.GetComponent<PlayerCombat>();
                if (combat != null)
                    combat.AddDamageBonus(amount);
                break;

            case PickableType.Invulnerability:
                player.ActivateInvulnerability(duration);
                if (HUDMessage.Instance != null)
                    HUDMessage.Instance.ShowCountDown("¡Invulnerable!", duration);
                break;

            case PickableType.Coin:
                if (GameStats.Instance != null)
                    GameStats.Instance.AddCoins(amount);
                break;

            case PickableType.Key:
                if (GameStats.Instance != null)
                {
                    GameStats.Instance.AddKey();
                    if (HUDMessage.Instance != null)
                        HUDMessage.Instance.Show("¡Llave" + GameStats.Instance.Keys + "/" + GameStats.Instance.KeysRequired + "!");
                }
                break;
        }

        if (!string.IsNullOrEmpty(message) && HUDMessage.Instance != null)
            HUDMessage.Instance.Show(message);
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        Destroy(gameObject);
    }
}
