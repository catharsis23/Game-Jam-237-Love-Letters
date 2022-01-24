using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private Rigidbody2D rb;
    public float speed;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping = false;

    public Animator inventoryAnimator;

    private Animator anim;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (!gameManager.isInDialogue)
        {

            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    void Update()
    {

        

        if (gameManager.isInInventory)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Closing Inventory");
                inventoryAnimator.SetBool("isOpen", false);
                gameManager.isInInventory = false;
            }
        } else if (!gameManager.isInDialogue)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Opening Inventory");
                inventoryAnimator.SetBool("isOpen", true);
                gameManager.isInInventory = true;
            }
        }

        //no player movement in dialogue or inventoryi
        if (!gameManager.isInDialogue && !gameManager.isInInventory)
        {

            

            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

            if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);

            }

            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("takeOff");
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetKey(KeyCode.Space) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }


            //handle idle and run animations
            if (moveInput == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (isGrounded == false)
            {
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);

            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;

            }
        }
    }
}
