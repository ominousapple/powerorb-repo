using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IControllable, IInteractive
{


    private float horizontalInput = 0;

    public Animator animator;


    #region Attributes

    private Rigidbody2D rb;

    [SerializeField]
    private float movementSpeed;



    public float jumpForce;
    public float groundedSkin = 0.05f; //distance for detection for raycast
    public LayerMask mask;

    bool jumpRequest;
    bool grounded;

    private int extraJumps;
    public int extraJumpsValue;

    Vector2 playerSize;
    Vector2 boxSize;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    #endregion

    //Interface Methods:

    #region Extra abilities
    public void Attack_Down()
    {
        Debug.Log("player shoots");
    }

    public void Attack_Up()
    {
        
    }
    public void UseOrb_Down()
    {
        throw new System.NotImplementedException();
    }

    public void UseOrb_Up()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Movement Abilities
    public void Jump_Down()
    {
        if (grounded)
        {
            extraJumps = extraJumpsValue;
        }
        jumpRequest = true;
        

    }

    public void Jump_Up()
    {
        StopJumping();
    }


    public void MoveLeft_Down()
    {

    }

    public void MoveLeft_Up()
    {
       
    }

    public void MoveRight_Down()
    {

    }

    public void MoveRight_Up()
    {
    }

    public void HorizontalInput(float h_input)
    {
        horizontalInput = h_input;
    }

    public void VerticalInput(float v_input)
    {
        
    }
    #endregion



    // Use this for initialization
    void Awake()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        else {

            Debug.LogError("The GameObject has no RigidBody2D Component: " + this);
        }
        extraJumps = extraJumpsValue;
        playerSize = GetComponent<BoxCollider2D>().bounds.size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }

   

    void Update () {


        animator.SetFloat("Speed",Mathf.Abs( horizontalInput));  
      
    }

	void FixedUpdate () {

        
            PlayerJumping();
        

        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        Flip();

    }


    #region Help Methods
    private void Flip()
    {
        //GetKey returns true while user holds down the key identified by name
        // public static bool GetKey(KeyCode key);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rb.velocity.x > 0)  //check if player is moving right
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (rb.velocity.x < 0) //check if player moving left
            {
                //Flip the sprite on the X axis
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void StopMoving() {
        rb.velocity = new Vector2(0 * movementSpeed, rb.velocity.y);

    }


    private void PlayerJumping()
    {
        if (jumpRequest)
        {
            if (extraJumps > 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumps--;
                grounded = false;
                animator.SetBool("isGrounded", grounded);

            }

            jumpRequest = false;
        }
            
        
        //Check players interaction with masks
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
        if (grounded)
            extraJumps = extraJumpsValue;

            animator.SetBool("isGrounded", grounded);
            Debug.Log("grounded:"+grounded);
        


        
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }

   

    }

    public void StopJumping()
    {
        animator.SetBool("isGrounded", false);
    }



    #endregion


    void IInteractive.CollidedWithEnemy()
    {
        throw new System.NotImplementedException();
    }




}
