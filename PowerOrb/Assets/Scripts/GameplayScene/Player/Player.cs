using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IControllable, IInteractive, IMortal, IElemental, ITalkable
{

    #region Stuck Movement fix attributes/method:
    [Header("Unstuck Attributes:")]
    Vector2 curPos, lastPos;
    private bool isMovingHorizontal = false;
    [SerializeField]
    private int UnstuckJumpForce = 1;//Adjust According to RigidBodyMass
    private void Unstuck()
    {

        curPos = transform.position;
        if (curPos == lastPos)
        {
            if (isMovingHorizontal)
            {
                rb.AddForce(Vector2.up * UnstuckJumpForce, ForceMode2D.Impulse);
            }
        }
        lastPos = curPos;

    }
    #endregion

    #region Input Variables
    private bool MovingLeft = false;
    private bool MovingRight = false;
    private float horizontalInput = 0;
    #endregion


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
    private GameObject playerHalo = null;

    [SerializeField]
    private GameObject orbPrefab = null;

    [SerializeField]
    private float movementSpeedValue;
    
    private float movementSpeed;

    private int damageFromFire;
    [SerializeField]
    private int damageFromFireValue;
    private int fireResistanceTime = 10;
    private bool hasFireResistance ;

    [Header("Jump Attributes")]
    [SerializeField]
   private int extraJumpTime = 10;

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
        OrbType type = CheckPocketOrb();
        int colorIndex = (int)type;

        switch (type)
        {   
            case OrbType.JumpOrb :
                playerHalo.GetComponent<SpriteRenderer>().color = orbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];
                StartCoroutine(giveExtraJump());
                ConsumeOrb();
                break;
            case OrbType.InstantDeathOrb :
                playerHalo.GetComponent<SpriteRenderer>().color = orbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];
                Suicide();
                StartCoroutine(TurnHaloOnAndOff());
                ConsumeOrb();
                break;
            case OrbType.HealthOrb:
                playerHalo.GetComponent<SpriteRenderer>().color = orbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];
                HealSelf(100);
                StartCoroutine(TurnHaloOnAndOff());
                ConsumeOrb();
                break;
            case OrbType.FireResistanceOrb :
                playerHalo.GetComponent<SpriteRenderer>().color = orbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];
                StartCoroutine(FireResist());
                ConsumeOrb();
                break;
            case OrbType.InvisibleOrb:
                playerHalo.GetComponent<SpriteRenderer>().color = orbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];
                StartCoroutine(turnInvisible());
                ConsumeOrb();
                break;
            case OrbType.SpeedOrb:
                playerHalo.GetComponent<SpriteRenderer>().color = orbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];
                StartCoroutine(getSuperSpeed());
                ConsumeOrb();
                break;
            default:
                break;
        }
        

    }

    public void UseOrb_Up()
    {
       
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
        MovingLeft = true;
    }

    public void MoveLeft_Up()
    {
        MovingLeft = false;
    }

    public void MoveRight_Down()
    {
        MovingRight = true;
    }

    public void MoveRight_Up()
    {
        MovingRight = false;
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


            OrbType type = CheckPocketOrb();

            switch (type)
            {
                case OrbType.JumpOrb:
                    TalkDialogue("Mighty jump awake",1);
                    break;
                case OrbType.InstantDeathOrb:
                    TalkDialogue("I have a bad feeling about this one", 1);
                    break;
                case OrbType.HealthOrb:
                    TalkDialogue("I feel amazing! Must be something in the air!", 1);
                    break;
                case OrbType.SpeedOrb:
                    TalkDialogue("Usain Bolt come at me bro!", 1);
                    break;
                case OrbType.InvisibleOrb:
                    TalkDialogue("Time to sneak around!", 1);
                    break;
                case OrbType.FireResistanceOrb:
                    TalkDialogue("Whoa it's getting hot in here!", 1);
                    break;
                default:
                    break;
            }

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
        damageFromFire = damageFromFireValue;
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
                if (!hasFireResistance)
                {
                    isFire = secondsToBurn * (framesPerSecond / flashPerFrames);
                    Debug.Log("Set on fire");
                    SetOnFire(true);
                }
          

                //GetComponent<SpriteRenderer>().color = Color.red;
                //StartCoroutine(whitecolor());
            }
            else {
                
                isFire = secondsToBurn * (framesPerSecond / flashPerFrames);
            }
         
         


        }
        

        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);


        isMovingHorizontal = rb.velocity.x != 0;
        Unstuck();
        Flip();



        
    }


    #region Help Methods
    private void Flip()
    {
        if (rb.velocity.x > 0)  //check if player is moving right
        {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (rb.velocity.x < 0) {

            GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private void StopMoving() {
        rb.velocity = new Vector2(0 * movementSpeed, rb.velocity.y);

    }


    private void PlayerJumping()
    {
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
            movementSpeed = 1;

        }else { movementSpeed = movementSpeedValue; }

        if (GetIsCollidingWithIce())
        {
            jumpRequest = false;
        }
    }

    private void Flash() {
        if (isFire > 0 && !hasFireResistance)
        {
            TakeDamage(damageFromFire);
            isFire--;
            //Debug.Log(hasFireResistance);
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


    IEnumerator giveExtraJump()
    {
       
        extraJumpsValue = 5;
        playerHalo.SetActive(true);
       
    
        yield return new WaitForSeconds(extraJumpTime);
        playerHalo.SetActive(false);
        extraJumpsValue = 1;
    }

    IEnumerator WaitOneSecondAndOpenFailWindow()
    {
        yield return new WaitForSeconds(1);
        UtilityAccess.instance.OpenFailLevelWinow();
        
    }

    IEnumerator TurnHaloOnAndOff()
    {
        playerHalo.SetActive(true);
        yield return new WaitForSeconds(1);
        playerHalo.SetActive(false);

    }

    IEnumerator FireResist()
    {
        damageFromFire = 0;
        hasFireResistance = true;
        SetOnFire(false);
        playerHalo.SetActive(true);
        yield return new WaitForSeconds(fireResistanceTime);
        playerHalo.SetActive(false);
        hasFireResistance = false;
        SetOnFire(true);
        damageFromFire = damageFromFireValue;
    }

    IEnumerator turnInvisible()
    {
        rb.tag = "Invisible";
        rb.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.2f);
        playerHalo.SetActive(true);
        yield return new WaitForSeconds(10);
        playerHalo.SetActive(false);
        rb.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        rb.tag = "Player";

    }

    IEnumerator getSuperSpeed()
    {
        movementSpeedValue = 15;
        playerHalo.SetActive(true);
        yield return new WaitForSeconds(10);
        playerHalo.SetActive(false);
        movementSpeedValue = 5;
    }




    #endregion

    #region IInteractive Interface


    override public void CollidedWithEnemy(Collider2D collision)
    {
        TalkDialogue("He can't see me ", 2);
        Debug.Log("Player touched an enemy");
    }
    override public void CollidedWithEnemyAttack(Collider2D collision)
    {
        TakeDamage(collision.transform.parent.gameObject.GetComponent<Character>().GetAttack_Damage());
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
        StartCoroutine(WaitOneSecondAndOpenFailWindow());
       

  
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
