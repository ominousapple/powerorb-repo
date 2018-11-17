using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster : Character, IControllable, IInteractive, IMortal
{
    #region Animator
    public Animator animator;
    #endregion

    #region Other Methods

    private float horizontalInput = 0;
    private Rigidbody2D rb;
    [SerializeField]
    private float movementSpeed;


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
    new void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        Flip();
    }

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
    #endregion

    #region IControllable Methods
    public void DropOrb_Down() {

    }
    public void DropOrb_Up() {

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

    override public void CollidedWithOrb(Collider2D collision)
    {
        Debug.Log("Enemy is touching an orb");
    }

    override public void CollidedWithPlayer(Collider2D collision)
    {
        TalkDialogue("Monster","I will kill you!",5);
        Debug.Log("Collided with player");
    }


    #endregion

    #region IMortal Methods 
    override public void Died()
    {
        //base.Died();
        Debug.Log("Enemy died");

        //Change this:
        gameObject.SetActive(false);
    }


    #endregion
}
