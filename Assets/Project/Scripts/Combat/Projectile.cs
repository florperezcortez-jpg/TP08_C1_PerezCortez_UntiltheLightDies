
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EnemyHealth enemy;

    private int damage;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);


        }

    }

    public void Launch(float direction, WeaponData data)
    {
        damage = data.damage;
        speed = data.projectileSpeed;
        Destroy(gameObject, data.projectileLifetime);

        if (rb == null)
            return;

        rb.linearVelocity = new Vector2(direction * speed, 0f);
        if (sr != null)
        {
            sr.flipX = direction < 0f;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, direction < 0f ? data.projectileTilt : -data.projectileTilt);
    }
}