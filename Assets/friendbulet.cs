using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10f;
    public float lifeTime = 20f;

    void Start()
    {
        Destroy(gameObject, lifeTime);  // Destroy the bullet after its lifetime expires
    }

    void Update()
    {
        // Bullet movement logic can go here if needed
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object the bullet collides with implements IDamageable
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            // Apply damage if the object implements IDamageable
            damageable.TakeDamage(damage);
            Debug.Log("Bullet hit: " + other.name);
            Debug.Log("Damage: " + damage);
        }

        Destroy(gameObject);  // Destroy the bullet after collision
    }
}
