using UnityEngine;

public class DragonScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float idleBounceSpeed = 0.4f;
    public float idleBounceHeight = 12.8f; //Current 12.8 (this legitametly affects nothing somehow)
    public float squashAmount = 0.05f;
    public float squishAmount = 2f;
    public int health = 10; //Dragon Health
    public GameObject Fireball;
    public float fireballSpeed = 10f;

    public GameObject Dragon_Up;
    public GameObject Dragon_Down;
    public GameObject Dragon_Left;
    public GameObject Dragon_Right;

    private Vector2 movement;
    //UpDaTeD
    private Vector3 upOriginalPos;
    private Vector3 downOriginalPos;
    private Vector3 leftOriginalPos;
    private Vector3 rightOriginalPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowOnly(Dragon_Up); //Default Sprite


        upOriginalPos = Dragon_Up.transform.localPosition;
        downOriginalPos = Dragon_Down.transform.localPosition;
        leftOriginalPos = Dragon_Left.transform.localPosition;
        rightOriginalPos = Dragon_Right.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the user input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Choose the sprite based on direction
        if (movement.x > 0) {
            ShowOnly(Dragon_Right);
        } else if (movement.x < 0) {
            ShowOnly(Dragon_Left);
        } else if (movement.y > 0) {
            ShowOnly(Dragon_Up);
        } else if (movement.y < 0) {
            ShowOnly(Dragon_Down);
        }

        //Check if idle (no input)
        if (movement == Vector2.zero) {
            //Apply bounce
            var (activeSprite, originalPos) = GetActiveSpriteWithOriginalPos();
            if (activeSprite != null) {
                float bounce = Mathf.Sin(Time.time * idleBounceSpeed) * idleBounceHeight;
                activeSprite.transform.localPosition = originalPos + new Vector3(0, bounce, 0);

                //Squash and stretch based on the bounce value
                float squash = Mathf.Sin((Time.time + 0.5f) * squashAmount);
                float scaleY = 1 + squash * squashAmount;
                float scaleX = 1 - squash * squashAmount;
                activeSprite.transform.localScale = new Vector3(scaleX, scaleY, 1);
            }
        } else {
            //Reset position on character move
            var (activeSprite, originalPos) = GetActiveSpriteWithOriginalPos();
            if (activeSprite != null) {
                activeSprite.transform.localPosition = originalPos;
                activeSprite.transform.localScale = Vector3.one;
            }
        }
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            ShootFireball();
        }
    }


    void FixedUpdate()
    {
        //move the player
        transform.Translate(movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void ShowOnly(GameObject activeSprite) 
    {
        Dragon_Up.SetActive(false);
        Dragon_Down.SetActive(false);
        Dragon_Left.SetActive(false);
        Dragon_Right.SetActive(false);

        if (activeSprite != null) {
            activeSprite.SetActive(true);
        }
    }
    
    (GameObject, Vector3) GetActiveSpriteWithOriginalPos() {
        if (Dragon_Up.activeSelf) return (Dragon_Up, upOriginalPos);
        if (Dragon_Down.activeSelf) return (Dragon_Down, downOriginalPos);
        if (Dragon_Left.activeSelf) return (Dragon_Left, leftOriginalPos);
        if (Dragon_Right.activeSelf) return (Dragon_Right, rightOriginalPos);
        return (null, Vector3.zero);
    }

    //HEALTH STUFF:
      public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // You can play death animation or particles here
        Destroy(gameObject);
    } 
    void ShootFireball()
    {
        // Get mouse position in world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // Ensure z is 0 in 2D

        // Calculate direction from dragon to mouse
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Instantiate fireball at dragon's position
        GameObject fireball = Instantiate(Fireball, transform.position, Quaternion.identity);
        fireball.transform.right = direction;

        // Apply velocity or movement
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * fireballSpeed;
        }
    }
}
