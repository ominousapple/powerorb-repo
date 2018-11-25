using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMonster : Character, IControllable, IInteractive, IMortal
{
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 2f;
    [SerializeField]
    private GameObject EnemyAttack;

    [SerializeField]
    private int damageFromFire;
    private int damageFromFireValue;

    private int framesPerSecond = 60;
    private int flashPerFrames = 6;
    private int flash = 0;
    private bool isFlashed = false;

    private int secondsToBurn = 3;

    private int isFire = 0;

    bool isActiveAttack = false;


    #region Animator
    public Animator animator;
    #endregion

    #region Other Methods

    #region Stuck Movement fix attributes/method:
    [Header("Unstuck Attributes:")]
    private bool isMovingHorizontal = false;
    Vector2 curPos, lastPos;
    [SerializeField]
    private int UnstuckJumpForce = 10;//Adjust According to RigidBodyMass
    private void Unstuck()
    {
        
            curPos = transform.position;
            if (curPos == lastPos)
            {
            if (isMovingHorizontal) {
                
                rb.AddForce(Vector2.up * UnstuckJumpForce, ForceMode2D.Impulse);
            }
                
            }
            lastPos = curPos;
        
    }
    #endregion


    private float horizontalInput = 0;
    private Rigidbody2D rb;
    [SerializeField]
    private float movementSpeed;

    IEnumerator DeathWait()
    {
        TalkDialogue("Oh, no! I'm dying!", 2);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    new void Awake()
    {
        base.Awake();
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        else
        {

            Debug.LogError("The GameObject has no RigidBody2D Component: " + this);
        }
    }

    new void Update()
    {
        base.Update();

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

    }





//bool WakeUPBIACH = false;;
new void FixedUpdate()
    {
        base.FixedUpdate();
        if (flash > 0) { flash--; } else { Flash(); flash = flashPerFrames; }

        if (GetIsCollidingWithFire())
        {

            if (isFire <= 0)
            {
                    isFire = secondsToBurn * (framesPerSecond / flashPerFrames);
                    Debug.Log("Set on fire");
                    SetOnFire(true);
                


                //GetComponent<SpriteRenderer>().color = Color.red;
                //StartCoroutine(whitecolor());
            }
            else
            {

                isFire = secondsToBurn * (framesPerSecond / flashPerFrames);
            }




        }


        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y);

        isMovingHorizontal = rb.velocity.x != 0;
        Unstuck();
        Flip();


        //Gravity hot fix
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = GetCurrentGravityScale() * fallMultiplier;
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


   

    private void Flash()
    {
        if (isFire > 0)
        {
            TakeDamage(damageFromFire);
            isFire--;
            //Debug.Log(hasFireResistance);
            if (isFlashed) { isFlashed = false; GetComponent<SpriteRenderer>().color = Color.white; }
            else
            {
                isFlashed = true;
                GetComponent<SpriteRenderer>().color = new Color((float)255 / 255, (float)79 / 255, 0);
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            SetOnFire(false);
        }

    }

    private void Flip()
    {
        //GetKey returns true while user holds down the key identified by name
        // public static bool GetKey(KeyCode key);
     
            if (rb.velocity.x > 0)  //check if player is moving right
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

        
       
            if (rb.velocity.x < 0) //check if player moving left
            {
                //Flip the sprite on the X axis
                GetComponent<SpriteRenderer>().flipX = true;
            }
        
    }
    #endregion

    #region IControllable Methods
    public void DropOrb_Down() {

    }
    public void DropOrb_Up() {

    }

    public IEnumerator EnemyAttackOneSecond()
    {
        
        isActiveAttack = true;
        EnemyAttack.SetActive(true);
        yield return new WaitForSeconds(1);
        EnemyAttack.SetActive(false);
        isActiveAttack = false;

    }

    public void ShouldAttack()
    {

        
        if (!isActiveAttack)
        {
            Debug.Log("jdksjdk");
            StartCoroutine(EnemyAttackOneSecond());
        }
    }

    public void Attack_Down()
    {
        Debug.Log("Monster attacks");
    }

    public void Attack_Up()
    {
        
    }

    public void HorizontalInput(float h_input)
    {
        horizontalInput = h_input;
    }

    public void Interact_Down()
    {
        
    }

    public void Interact_Up()
    {
        
    }

    public void Jump_Down()
    {
        
    }

    public void Jump_Up()
    {
       
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

    public void UseOrb_Down()
    {

    }

    public void UseOrb_Up()
    {

    }

    public void VerticalInput(float v_input)
    {

    }

    #endregion

    #region IInteractive Methods
    override public void CollidedWithEnemy(Collider2D collision)
    {
        Debug.Log("Enemy touched an enemy");
    }

    override public void CollidedWithEnemyAttack(Collider2D collision)
    {
        Debug.Log("Enemy collided with an enemy attack");
    }


    override public void CollidedWithPlayer(Collider2D collision)
    {
        
        TalkDialogue("Monster", "Gosho, prost li si we, ne vijdash li, che sum losh, mama ti deba..", 3);


        Debug.Log("Collided with player");

    }


    #endregion

    

    #region IMortal Methods 
    override public void Died()
    {
        base.Died();
        SetOnFire(false);
        animator.SetTrigger("Death");
        HorizontalInput(0);
        StartCoroutine(DeathWait());
    }


    #endregion
}
