﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMonster : Character, IControllable, IInteractive, IMortal
{
    //[SerializeField]
    //public Transform Player;
    //public float PlayerDistanceNeeded = 3f;
    //public float playerDistance;
    //public float rotationSpeed;
    //public float moveSpeed;

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
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y);

        isMovingHorizontal = rb.velocity.x != 0;
        Unstuck();
        Flip();

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
        
        TalkDialogue("Monster", "Gosho, prost li si we, ne vijdash li, che sum losh, mama ti deba..", 3);


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
