using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 8f;
    public float jump = 10f; 
    
    public AltitudeHandler altitudeHandler;

    private Rigidbody2D rb;

    private bool canJump = false;
    private bool isDead = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
            
            } 
        }



        float moveInput = 0f; 
        
        if (Keyboard.current.aKey.isPressed) moveInput = -1f;
        if (Keyboard.current.dKey.isPressed) moveInput = 1f;
        
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (Keyboard.current.wKey.wasPressedThisFrame && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            canJump = false;
        }
    }

    public void Die()
    {
        isDead = true;
        print("player died");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                canJump = true;
                break;
            }
        }
    }

    public void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathBar")) Die();
    }
}
