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
    public const string Invisible = "Invisible";
}
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IInteractive, IMortal, IElemental, ITalkable {

    [Header("Talkable Object")]
    #region Talk Methods/Attributes

    [Tooltip("Enter Default Dialogue:")]
    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    [Tooltip("If you attach TalkWindow, the character will be able to talk.")]
    private GameObject TalkWindow = null;

    //private GameObject dialogueCanvasGameObject = null;

    private DialogueManager dialogueManager = null;

    private bool isTalkable = false;

    public virtual void TalkDialogue() {
        if (isTalkable) {
            dialogueManager.StartDialogue(dialogue);
        }
    }
    public virtual void TalkDialogue(Dialogue SomeDialogue)
    {
        if (isTalkable)
        {
            dialogueManager.StartDialogue(SomeDialogue);
        }
    }

    public virtual void TalkDialogue(string name,string sentence,int secondsToWait)
    {
        if (isTalkable)
        {
            
            Dialogue dlog = new Dialogue(1);
            dlog.name = name;
            dlog.sentences[0] = sentence;
            dlog.SecondsVisableSentence[0] = secondsToWait;
            dialogueManager.StartDialogue(dlog);
        }
    }

    public virtual void TalkDialogue( string sentence, int secondsToWait)
    {
        if (isTalkable)
        {

            Dialogue dlog = new Dialogue(1);
            dlog.name = GetName();
            dlog.sentences[0] = sentence;
            dlog.SecondsVisableSentence[0] = secondsToWait;
            dialogueManager.StartDialogue(dlog);
        }
    }
    public virtual string GetName()
    {
        return dialogue.name;
    }



    #endregion


    [Header("Elemental Object")]
    #region Elemental Methods/Attributes

    [SerializeField]
    [Tooltip("If you attach Firebar, the character will be flamable.")]
    private GameObject FireOnPlayer = null;

    [SerializeField]
    [Tooltip("If you attach Icebar, the character will be icable.")]
    private GameObject IceOnPlayer = null;


    [SerializeField]
    [Tooltip("If you attach Slimebar, the character will be slimable.")]
    private GameObject SlimeOnPlayer = null;

    [SerializeField]
    [Tooltip("If you attach Dirtbar, the character will be dirtable.")]
    private GameObject DirtOnPlayer = null;

    private bool isElemental = false;

    public virtual void SetOnFire(bool active) {
        if (isElemental)
        {
        if (FireOnPlayer != null) { 
            FireOnPlayer.SetActive(active);
        }

        } 
    }

    public virtual void SetOnIce(bool active)
    {
            if (isElemental)
            {
            if (IceOnPlayer != null)
            {
                IceOnPlayer.SetActive(active);
            }

        }
        }

    public virtual void SetOnSlime(bool active)
    {
        if (isElemental)
        {
            if (SlimeOnPlayer != null)
            {
                SlimeOnPlayer.SetActive(active);
            }


        }

    }
    public virtual void SetOnDirt(bool active)
    {
        if (isElemental)
        {
            if (DirtOnPlayer != null)
            {
                DirtOnPlayer.SetActive(active);
            }

        }

    }
    #endregion

    [Header("Mortal Object")]
    #region Healthbar Methods/Attributes
    [SerializeField]
    [Tooltip("If you attach HealthbarUI, the character will become IMortal.")]
    private GameObject HealthbarUI = null;



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
    public bool GetIsCollidingWithSlime() { return isCollidingWithSlime; }
    public bool GetIsCollidingWithDirt() { return isCollidingWithDirt; }
    public bool GetIsCollidingWithIce() { return isCollidingWithIce; }
    public bool GetIsCollidingWithStone() { return isCollidingWithStone; }

    #endregion

    [Header("Other Attributes")]

    #region Attributes


    [SerializeField]
    private GameObject OrbPrefab;
    private OrbType OrbInPocket = OrbType.None;


    private bool isDead = false;
    public float touchedSkin = 0.05f; //distance for detection for raycast
    public LayerMask maskFire;
    public LayerMask maskSlime;
    public LayerMask maskDirt;
    public LayerMask maskIce;
    public LayerMask maskStone;
    public LayerMask maskOrb;
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
        // Firebar Setup
        if (FireOnPlayer != null || IceOnPlayer != null || SlimeOnPlayer != null || DirtOnPlayer != null) {
            isElemental = true;

        }
        // TalkWindow Setup

        if (TalkWindow != null) {
            TalkWindow.SetActive(true);
            isTalkable = true;
            //dialogueCanvasGameObject = TalkWindow.transform.GetChild(0).gameObject;
            //dialogueCanvasGameObject.SetActive(false);
            dialogueManager = TalkWindow.GetComponent<DialogueManager>();

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
                Debug.Log("AAAAAAA");
                CollidedWithOrb(collision);
                isCollidingWithOrb = true;
                break;
            case collidableObjects.Player:
                CollidedWithPlayer(collision);
                isCollidingWithPlayer = true;
                break;
            case collidableObjects.EndZone:
                healthbarClass.Kill();
                Died();
                break;
            case collidableObjects.Fire:
                isCollidingWithFire = true;
                CharacterRigidBody2LavaFall();
                break;
            case collidableObjects.Invisible:
                CollidedWithInvisible(collision);
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
                break;
            case collidableObjects.Orb:
                isCollidingWithOrb = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = true;
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
    public virtual void CollidedWithInvisible(Collider2D collision)
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
                if (!isDead) {
                    Died();
                }
                
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
            isDead = true;

        }
    }

    public virtual void Revive()
    {
        if (isMortal)
        {


        }

    }
    

    public virtual void Suicide()
    {
        if (isMortal)
        {
            healthbarClass.Kill();
            Died();
        }
    }


    #endregion

    #region Help Methods:

    public virtual void CharacterRigidBody2LavaFall() {
        if (isCollidingWithFire)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (gameObject.GetComponent<Rigidbody2D>().velocity  + 5 * new Vector2(0, 0) ) /6;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.05f;
            currentGravityScale = 0.05f;
            

        }
        else {
            
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            currentGravityScale = 1f;
            
        }

    }


    #endregion


    #region Character methods:
    #region Pocket methods:
    public void SetPocketOrb(OrbType newPocketOrb)
    {
        if (OrbInPocket != OrbType.None) {
            PlacePocketOrb(OrbInPocket);
        }
        OrbInPocket = newPocketOrb;
        UtilityAccess.instance.SetOrbUI(newPocketOrb);
    }
    public void PlacePocketOrb(OrbType orbToPlace)
    {
        if (orbToPlace != OrbType.None)
        {
            GameObject PlacedOrb = Instantiate(OrbPrefab, gameObject.transform.position, Quaternion.identity);
            PlacedOrb.GetComponent<Orb>().SetOrb(orbToPlace);
            PlacedOrb.transform.position = gameObject.transform.position;
            OrbInPocket = OrbType.None;
            UtilityAccess.instance.SetOrbUI(OrbType.None);
        }
    }
    public OrbType CheckPocketOrb()
    {
        return OrbInPocket;
    }

    public void ConsumeOrb()
    {
        OrbInPocket = OrbType.None;
        UtilityAccess.instance.SetOrbUI(OrbType.None);
    }


    #endregion



    #endregion


    #region  Enemy Damage Attack methods and attributes
    [SerializeField]
    private int Attack_Damage = 0;

    public int GetAttack_Damage() {
        return Attack_Damage;
    }
    public void SetAttack_Damage(int new_Attack_Damage)
    {
         Attack_Damage = new_Attack_Damage;
    }

    #endregion

}
