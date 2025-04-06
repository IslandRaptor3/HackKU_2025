using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable  // Implements the IDamageable interface
{
    public int maxHealth = 3;
    private int currentHealth;
    public float moveSpeed = 3f;  // Adjust the speed of the enemy's movement
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component

    public float wanderRadius = 5f;  // How far the enemy can wander
    public float wanderSpeed = 2f;   // The speed while wandering
    public float chaseRange = 5f;    // The range at which the enemy detects the player
    public float chaseSpeed = 5f;    // The speed when chasing the player
    public Transform player;         // Reference to the player's Transform

    private Vector2 wanderTarget;    // The current target the enemy is wandering towards
    private bool isChasing = false;  // Whether the enemy is chasing the player

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component

        // Start wandering by setting an initial target
        SetWanderTarget();
    }

    void Update()
    {
        if (currentHealth > 0)
        {
            if (isChasing)
            {
                ChasePlayer();
            }
            else
            {
                Wander();
            }
        }
    }

    void Wander()
    {
        // Move towards the wander target
        Vector2 direction = (wanderTarget - (Vector2)transform.position).normalized;

        // Move the enemy towards the target using Rigidbody2D
        rb.MovePosition((Vector2)transform.position + direction * wanderSpeed * Time.deltaTime);

        // If the enemy is close to the wander target, set a new target
        if (Vector2.Distance(transform.position, wanderTarget) < 0.1f)
        {
            SetWanderTarget();
        }

        // Check if the player is within the chase range
        if (Vector2.Distance(transform.position, player.position) <= chaseRange)
        {
            isChasing = true;
        }
    }

    void ChasePlayer()
    {
        // Move the enemy towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        rb.MovePosition((Vector2)transform.position + direction * chaseSpeed * Time.deltaTime);

        // If the player moves out of range, stop chasing and resume wandering
        if (Vector2.Distance(transform.position, player.position) > chaseRange)
        {
            isChasing = false;
        }
    }

    void SetWanderTarget()
    {
        // Set a new wander target randomly within the wander radius
        wanderTarget = new Vector2(Random.Range(-wanderRadius, wanderRadius), Random.Range(-wanderRadius, wanderRadius)) + (Vector2)transform.position;
    }

    // This is the implementation of the TakeDamage method from IDamageable
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took " + amount + " damage! Health: " + currentHealth);  // Debugging log

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);  // Destroy the enemy when health reaches zero
    }
}
