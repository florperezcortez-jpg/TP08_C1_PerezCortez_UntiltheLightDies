using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private float verticalTolerance = 2.5f;
    [SerializeField] private float despawnDistance = 25f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float edgeOffsetX = 0.6f;
    [SerializeField] private float edgeOriginY = 0.5f;
    [SerializeField] private float edgeRayLength = 1.2f;
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

        float dx = target.position.x - transform.position.x;
        float distance = Mathf.Abs(dx);
        float dy = Mathf.Abs(target.position.y - transform.position.y);

        if (distance <= detectionRange)
            hasDetected = true;

        bool sameLevel = dy <= verticalTolerance;
        bool shouldChase = hasDetected && sameLevel && distance > stopDistance;
        float direction = Mathf.Sign(dx);

        if (shouldChase && HasGroundAhead(direction))
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            sr.flipX = direction > 0f;
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        if (distance > despawnDistance || transform.position.y < -20f)
            Destroy(gameObject);
    
    }

    private bool HasGroundAhead(float direction)
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(direction * edgeOffsetX, edgeOriginY);
        return Physics2D.Raycast(origin, Vector2.down, edgeRayLength, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for(int d = -1; d <= 1; d += 2)
        {
            Vector2 origin = (Vector2)transform.position + new Vector2(d * edgeOffsetX, edgeOriginY);
        Gizmos.DrawLine(origin, origin + Vector2.down * edgeRayLength);
        }
    }
    public void Alert()
    {
        hasDetected = true;
    }
}
