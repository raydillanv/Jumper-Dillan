using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 8f;
    public float jump = 12f;
    
    public AltitudeHandler altitudeHandler;

    public GameObject deathScreen;

    public Animator animController; 
    
    private Rigidbody2D rb;
    private bool canJump = false;
    private bool isDead = false;
    
    public AudioSource audioSource;
    public AudioClip jumpClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (deathScreen) deathScreen.SetActive(false);
    }

    void Update()
    {
        if (isDead)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        
        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed) moveInput = -1f;
        
        if (Keyboard.current.dKey.isPressed) moveInput = 1f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            animController.SetTrigger("run");
        }
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        
        if (Keyboard.current.wKey.wasPressedThisFrame && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            audioSource.PlayOneShot(jumpClip);
            canJump = false;
        }
    }
    
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false; 
        if (deathScreen) deathScreen.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    canJump = true;
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canJump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathBar")) Die();
    }
}