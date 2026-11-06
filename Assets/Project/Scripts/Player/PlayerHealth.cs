
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invulnerabilityTime = 1.5f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HeartDisplay hearts;




    private int currentHealth;
    private float nextDamageTime;
    private Animator animator;
    private Rigidbody2D rb;
    private bool chakraInvulnerable;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public System.Action OnDamaged;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        hearts.SetHealth(currentHealth, maxHealth);
    }
    public void TakeDamage(int amount)
    {

        Debug.Log("TakeDamage llamado. Daño: " + amount);
        if (chakraInvulnerable)
            return;

        if (Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + invulnerabilityTime;
        currentHealth -= amount;
        OnDamaged?.Invoke();

        hearts.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            
        }
    }

    public void SetChakraInvulnerable(bool value)
    {
        chakraInvulnerable = value;
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        hearts.SetHealth(currentHealth, maxHealth);
    }
    private void Die()
    {
       
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        GetComponent<PlayerController>().enabled = false;
        gameManager.GameOver();
    }
}
