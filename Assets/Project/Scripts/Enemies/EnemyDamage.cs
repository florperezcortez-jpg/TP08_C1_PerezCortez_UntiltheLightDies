using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private Animator animator;
    private PlayerHealth currentPlayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
    if (player != null )
        {
            if(currentPlayer != player)
            {
                if (currentPlayer != null)
                    currentPlayer.OnDamaged -= PlayAttack;
                currentPlayer = player;
                currentPlayer.OnDamaged += PlayAttack;
            }
            player.TakeDamage(damage);
        }
    }
    private void PlayAttack()
    {
        animator.SetTrigger("IsAttacking");
    }
    private void OnDestroy()
    {
        if (currentPlayer != null)
            currentPlayer.OnDamaged -= PlayAttack;
    }
}

