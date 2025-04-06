using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 5f;
    public int damage = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage the player if hit
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        // Destroy the laser on any collision
        Destroy(gameObject);
    }
}
