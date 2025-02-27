using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    private bool isGrounded = false;
    public int hitCount = 0;
    public SpriteRenderer spriteRenderer;

    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask groundLayer;
    public GameObject levelFailPanel;
    public bool canMove = true;
    public Button restartButton;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return; // Early return to block further input processing
        }

        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput < 0)
        {
            moveInput = 0;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }


    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);


    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("Trigger Entered with: " + other.gameObject.name);
    //     if (other.gameObject.CompareTag("Trap"))
    //     {

    //         Debug.Log("Player hit the trap!");


    //         hitCount++;


    //         Debug.Log("Hit count: " + hitCount);


    //         if (hitCount == 1)
    //         {

    //             spriteRenderer.color = Color.magenta;
    //         }
    //         else if (hitCount == 2)
    //         {

    //             spriteRenderer.color = new Color(0.8f, 0.0f, 0.4f);
    //         }
    //         else if (hitCount >= 3)
    //         {

    //             spriteRenderer.color = Color.red;
    //             canMove = false;
    //             levelFailPanel.SetActive(true);
    //             Debug.Log("Level fail panel shown");
    //         }
    //     }
    //     if (other.gameObject.CompareTag("Finish") && Collectible.collectiblesRemaining == 0)
    //     {
    //         canMove = false;
    //     }
    // }

    // public void RestartScene() {
    //   Debug.Log("Restart button clicked");
    //   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Entered with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Player hit the trap!");


            hitCount++;


            Debug.Log("Hit count: " + hitCount);


            if (hitCount == 1)
            {

                spriteRenderer.color = Color.magenta;
            }
            else if (hitCount == 2)
            {

                spriteRenderer.color = new Color(0.8f, 0.0f, 0.4f);
            }
            else if (hitCount >= 3)
                {
                    canMove = false;
                    levelFailPanel.SetActive(true);
                    GameAnalytics.instance.RecordDeath();  // Record death when the player dies
                }
        }
        if (other.gameObject.CompareTag("Finish") && Collectible.collectiblesRemaining == 0)
        {
            canMove = false;
            GameAnalytics.instance.RecordCompletion();  // Record completion on level finish
        }
    }

    public void RestartScene()
    {
        //GameAnalytics.instance.RecordAttempt();  // Record a new attempt when the player restarts the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}


