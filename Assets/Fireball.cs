using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifetime = 8f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after 3 seconds
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {

    //     EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
    //     if (enemy != null)
    //     {
    //         enemy.TakeDamage(damage);
    //     }

    //     Destroy(gameObject);
    // }
}