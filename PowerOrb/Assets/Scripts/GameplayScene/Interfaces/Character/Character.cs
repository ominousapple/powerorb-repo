﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class collidableObjects{
    public const string Enemy = "Enemy";
    public const string Enemy_attack = "EnemyAttack";
    public const string Orb = "Orb";
    public const string Player = "Player";
    public const string EndZone = "EndZone";
    public const string Fire = "Fire";
}
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IInteractive, IMortal {


    #region Firebar Methods/Attributes

    [SerializeField]
    [Tooltip("If you attach Firebar, the character will become flamable when colliding with fire.")]
    private GameObject FireOnPlayer = null;
    private bool isFlamable = false;

    public void SetOnFire(bool active) {
        FireOnPlayer.SetActive(active);
    }
    #endregion
    #region Healthbar Methods/Attributes
    [SerializeField]
    [Tooltip("If you attach HealthbarUI, the character will become IMortal.")]
    private GameObject HealthbarUI = null;

    //haha

    private GameObject VisableHealth;
    private bool isMortal = false;

    private Healthbar healthbarClass;


    #endregion


    #region isCollidingWith bools:

    private bool isCollidingWithOrb = false;
    private bool isCollidingWithEnemy = false;
    private bool isCollidingWithPlayer = false;

    private bool isCollidingWithFire = false;
    private bool isCollidingWithSlime = false;
    private bool isCollidingWithDirt = false;
    private bool isCollidingWithIce = false;
    private bool isCollidingWithStone = false;

    public bool GetIsCollidingWithOrb() { return isCollidingWithOrb; }
    public bool GetIsCollidingWithEnemy() { return isCollidingWithEnemy; }
    public bool GetIsCollidingWithPlayer() { return isCollidingWithPlayer; }

    public bool GetIsCollidingWithFire() { return isCollidingWithFire; }
    public bool GetIsCollidingWithSlime () { return isCollidingWithSlime; }
    public bool GetIsCollidingWithDirt() { return isCollidingWithDirt; }
    public bool GetIsCollidingWithIce() { return isCollidingWithIce; }
    public bool GetIsCollidingWithStone() { return isCollidingWithStone; }

    #endregion



    #region Attributes


    public float touchedSkin = 0.05f; //distance for detection for raycast
    public LayerMask maskFire;
    public LayerMask maskSlime;
    public LayerMask maskDirt;
    public LayerMask maskIce;
    public LayerMask maskStone;
    Vector2 characterSize;
    Vector2 boxCharSize;

    private float currentGravityScale = 1f;

    #endregion

    #region Get/Set Methods for Character Attributes:

    public float GetCurrentGravityScale() {
        return currentGravityScale;
    }

    #endregion

    public void Awake()
    {
        //Healthbar Setup
        if (HealthbarUI != null) {
            isMortal = true;
            healthbarClass = new Healthbar();
            VisableHealth = HealthbarUI.transform.GetChild(0).gameObject;
            VisableHealth.transform.localScale = new Vector3 (healthbarClass.GetCurrentHealthPercent(),1f,1f);

        }

        if (FireOnPlayer != null) {
            isFlamable = true;

        }


        characterSize = GetComponent<BoxCollider2D>().bounds.size;
        boxCharSize = new Vector2(characterSize.x, touchedSkin);
    }
    // Use this for initialization
    public void Start () {

    }

    // Update is called once per frame
    public void Update () {

        
    }

    public void FixedUpdate() {

        //Calculate boxCenter
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (characterSize.y + boxCharSize.y) * 0.5f;


        //Check Overlapping with masks:
        isCollidingWithFire = (Physics2D.OverlapBox(boxCenter, boxCharSize, 0f, maskFire) != null);
        isCollidingWithSlime = (Physics2D.OverlapBox(boxCenter, boxCharSize, 0f, maskSlime) != null);
        isCollidingWithDirt = (Physics2D.OverlapBox(boxCenter, boxCharSize, 0f, maskDirt) != null);
        isCollidingWithIce = (Physics2D.OverlapBox(boxCenter, boxCharSize, 0f, maskIce) != null);
        isCollidingWithStone = (Physics2D.OverlapBox(boxCenter, boxCharSize, 0f, maskStone) != null);

       

        //grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        
        string tag = collision.tag;
        switch (tag)
        {
            case collidableObjects.Enemy:
                CollidedWithEnemy(collision);
                isCollidingWithEnemy = true;
                break;
            case collidableObjects.Enemy_attack:
                CollidedWithEnemyAttack(collision);
                break;
            case collidableObjects.Orb:
                isCollidingWithOrb = true;
                break;
            case collidableObjects.Player:
                CollidedWithPlayer(collision);
                isCollidingWithPlayer = true;
                break;
            case collidableObjects.EndZone:
                healthbarClass.Kill();
                break;
            case collidableObjects.Fire:
                isCollidingWithFire = true;
                CharacterRigidBody2LavaFall();
                break;
            default:
                break;
        }
    

      



    }

    void OnTriggerExit2D(Collider2D collision)
    {

        string tag = collision.tag;
        switch (tag)
        {
            case collidableObjects.Enemy:
                isCollidingWithEnemy = false;
                break;
            case collidableObjects.Enemy_attack:
                CollidedWithEnemyAttack(collision);
                break;
            case collidableObjects.Orb:
                isCollidingWithOrb = false;
                break;
            case collidableObjects.Player:
                isCollidingWithPlayer = false;
                break;
            case collidableObjects.Fire:
                isCollidingWithFire = false;
                CharacterRigidBody2LavaFall();
                break;
           

            default:
                break;
        }






    }


    #region IInteractive methods

    public virtual void CollidedWithEnemy(Collider2D collision)
    {
    }
    public virtual void CollidedWithEnemyAttack(Collider2D collision)
    {

    }
    public virtual void CollidedWithOrb(Collider2D collision)
    {

    }
    public virtual void CollidedWithPlayer(Collider2D collision)
    {

    }




    #endregion



    #region IMortal Methods


    public virtual void TakeDamage(int damageValue)
    {
        if (isMortal) {

            healthbarClass.TakeDamage(damageValue);
            VisableHealth.transform.localScale = new Vector3(healthbarClass.GetCurrentHealthPercent(),1,1);
            if (healthbarClass.GetIsDead()) {
                Died(); 
                }

        }  
    }

    public virtual void HealSelf(int damageValue)
    {
        if (isMortal)
        {
            healthbarClass.HealSelf(damageValue);
            VisableHealth.transform.localScale = new Vector3(healthbarClass.GetCurrentHealthPercent(), 1, 1);

        }
    }

    public virtual void Died()
    {
        if (isMortal)
        {


        }
    }

    public virtual void Revive()
    {
        if (isMortal)
        {


        }
    }



    #endregion

    #region Help Methods:

    public virtual void CharacterRigidBody2LavaFall() {
        if (isCollidingWithFire)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (gameObject.GetComponent<Rigidbody2D>().velocity  + 5 * new Vector2(0, 0) ) /6;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
            currentGravityScale = 0.01f;
            

        }
        else {
            
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            currentGravityScale = 1f;
            
        }

    }


    #endregion




}
