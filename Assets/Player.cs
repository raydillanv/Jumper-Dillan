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
        
        
    }
}
