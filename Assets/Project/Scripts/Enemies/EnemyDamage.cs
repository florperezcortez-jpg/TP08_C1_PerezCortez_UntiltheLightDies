using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float verticalRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.2f;

    private Transform target;
    private Animator animator;
    private PlayerHealth playerHealth;
    private float nextAttackTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
            return;

        target = player.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        if(playerHealth == null)
            playerHealth = player.GetComponentInParent<PlayerHealth>();
    }
    private void Update()
    {
        if (target == null || playerHealth == null) return;
        if (Time.time < nextAttackTime) return;

        float dx = Mathf.Abs(target.position.x - transform.position.x);
        float dy = Mathf.Abs(target.position.y - transform.position.y);

        if (dx <= attackRange && dy <= verticalRange)
        {
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger("IsAttacking");
            playerHealth.TakeDamage(damage);
        }
    }

}

