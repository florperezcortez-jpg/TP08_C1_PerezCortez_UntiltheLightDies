using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private float despawnDistance = 25f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private Transform target;
    private bool hasDetected;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
        {
        GameObject player = GameObject.FindWithTag("Player");

       if (player != null)
        {
            target = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;
        float distance = Mathf.Abs(target.position.x - transform.position.x);

        if(distance <= detectionRange)
        {
            hasDetected = true;
        }

        if (hasDetected && distance > stopDistance)
        {
            float direction = Mathf.Sign(target.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            sr.flipX = direction > 0f;
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        if (distance > despawnDistance || transform.position.y < -20f)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void Alert()
    {
        hasDetected = true;
    }
}
