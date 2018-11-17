using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IControllable, IInteractive, IMortal, IElemental, ITalkable
{
   

    private float horizontalInput = 0;
    
    public Animator animator;

    #region Tiles Interactions
    private int framesPerSecond = 60;
    private int flashPerFrames = 6;
    private int flash = 0;
    private bool isFlashed = false;

    private int secondsToBurn = 3;

    private int isFire = 0;

    #endregion

    #region Attributes
    private GameObject LastCollidedOrb = null;
    private GameObject LastCollidedObject = null;
    private Rigidbody2D rb;

    [SerializeField]
    private float movementSpeed;

    bool IsDead;

    public float jumpForce;
    public float groundedSkin = 0.05f; //distance for detection for raycast
    public LayerMask mask;

    bool jumpRequest;
    bool lockJumpRequest = false;
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
    public void DropOrb_Down() {
        PlacePocketOrb(CheckPocketOrb());
    }
    public void DropOrb_Up() {

    }
    public void Attack_Down()
    {
        TalkDialogue("Mars","Damn he's huge",1);
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
        if (!lockJumpRequest) { 
        if (grounded)
            {
                //extraJumps = extraJumpsValue;
            }
        jumpRequest = true;
        lockJumpRequest = true;
    }


    }

    public void Jump_Up()
    {
        lockJumpRequest = false;
        //StopJumping();
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


    public void Interact_Down()
    {
        if (GetIsCollidingWithOrb()) {
            SetPocketOrb(LastCollidedOrb.GetComponent<Orb>().GetOrb());
            Destroy(LastCollidedOrb);
        }

    }

    public void Interact_Up()
    {
      
    }
    #endregion

    new void Start() {
        base.Start();
    }

    // Use this for initialization
    new void Awake()
    {
        base.Awake();
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

    new void Update () {
        base.Update();

        animator.SetFloat("Speed",Mathf.Abs( horizontalInput));  
      
    }

    new void FixedUpdate () {
        if (flash > 0) { flash--; } else { Flash(); flash = flashPerFrames; }
        base.FixedUpdate();
        
            PlayerJumping();

        if (GetIsCollidingWithFire()) {

            if (isFire <= 0)
            {
                isFire = secondsToBurn * (framesPerSecond/flashPerFrames);
                Debug.Log("Set on fire");
                SetOnFire(true);

                //GetComponent<SpriteRenderer>().color = Color.red;
                //StartCoroutine(whitecolor());
            }
            else {
                
                isFire = secondsToBurn * (framesPerSecond / flashPerFrames);
            }
         
         


        }
        

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
        //  if (GetIsCollidingWithSlime() || GetIsCollidingWithIce())
        ///  {
        //       jumpRequest = false;
        //    }

        MaskCheck();
        if (jumpRequest)
        {     if (extraJumps > 0)
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
            //Debug.Log("grounded:"+grounded);
        


        
        if (rb.velocity.y < 0)
        {
            rb.gravityScale =  GetCurrentGravityScale() * fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.gravityScale = GetCurrentGravityScale() * lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = GetCurrentGravityScale() * 1f;
        }

   

    }

    public void MaskCheck()
    {
        if (GetIsCollidingWithSlime())
        {  
            jumpRequest = false;
           
        }
        if (GetIsCollidingWithIce())
        {
            jumpRequest = false;
        }
    }

    public void StopJumping()
    {
        animator.SetBool("isGrounded", false);
    }

    private void Flash() {
        if (isFire > 0)
        {
            TakeDamage(5);
            isFire--;
            if (isFlashed) { isFlashed = false; GetComponent<SpriteRenderer>().color = Color.white; }
            else
            {
                isFlashed = true;
                GetComponent<SpriteRenderer>().color = new Color((float)255/255, (float)79/255, 0);
            }
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.white;
            SetOnFire(false);
        }

    }

    


    #endregion

    #region IInteractive Interface


    override public void CollidedWithEnemy(Collider2D collision)
    {
        TakeDamage(10);
        Debug.Log("Player touched an enemy");
    }
    override public void CollidedWithEnemyAttack(Collider2D collision)
    {
        Debug.Log("Player got hit by an enemy");
    }

    override public void CollidedWithOrb(Collider2D collision)
    {
        Debug.Log("Player is touching orb");


        LastCollidedOrb = collision.gameObject;


    }


    #endregion


    #region IMortal

    override public void Died() {
        SetOnFire(false);
        base.Died();
        Debug.Log("Died");
        IsDead = true;
        animator.SetBool("IsDead",IsDead);
   
       // UtilityAccess.instance.SceneFaderLoadScene("GameplayScene");
        UtilityAccess.instance.OpenFailLevelWinow();

  
    }


    #endregion

    #region IElemental
    override public void SetOnFire(bool active)
    {
        base.SetOnFire(active);
    }
    override public void SetOnIce(bool active)
    {
        base.SetOnIce(active);
    }
    override public void SetOnSlime(bool active)
    {
        base.SetOnSlime(active);
    }
    override public void SetOnDirt(bool active)
    {
        base.SetOnDirt(active);
    }

    #endregion



}
