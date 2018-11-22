using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Character, IControllable, IInteractive, IMortal, IElemental, ITalkable
{

    private bool givingQuest = false;
    private bool givenQuest = false;
    private bool lookingRight = true;
    [SerializeField]
    private LayerMask groundedMask;
    private float groundedSkin = 0.05f; //distance for detection for raycast
    Vector2 ghostSize;
    Vector2 boxSize;
    private bool grounded;

    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField]
    private int orbsNeeded = 3;
    [SerializeField]
    private float jumpForce = 3f;
    private float fallMultiplier = 2.5f;
    // Use this for initialization
    new void Awake () {
        base.Awake();
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = fallMultiplier;
        }
        if (GetComponent<Animator>() != null) {

            animator = GetComponent<Animator>();
        }

        ghostSize = GetComponent<BoxCollider2D>().bounds.size;
        boxSize = new Vector2(ghostSize.x, groundedSkin);

    }
    new void Start()
    {
        base.Start();


    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (ghostSize.y + boxSize.y) * 0.5f;
        grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundedMask) != null);
        animator.SetBool("isGrounded", grounded);
    }
    // Update is called once per frame
    new void Update () {
        base.Update();
	}


    #region IControllable Methods:
    public void Attack_Down()
    {
        throw new System.NotImplementedException();
    }

    public void Attack_Up()
    {
        throw new System.NotImplementedException();
    }

    public void DropOrb_Down()
    {
        throw new System.NotImplementedException();
    }

    public void DropOrb_Up()
    {
        throw new System.NotImplementedException();
    }

    public void HorizontalInput(float h_input)
    {
        throw new System.NotImplementedException();
    }

    public void Interact_Down()
    {
        throw new System.NotImplementedException();
    }

    public void Interact_Up()
    {
        throw new System.NotImplementedException();
    }

    public void Jump_Down()
    {
        throw new System.NotImplementedException();
    }

    public void Jump_Up()
    {
        throw new System.NotImplementedException();
    }

    public void MoveLeft_Down()
    {
        throw new System.NotImplementedException();
    }

    public void MoveLeft_Up()
    {
        throw new System.NotImplementedException();
    }

    public void MoveRight_Down()
    {
        throw new System.NotImplementedException();
    }

    public void MoveRight_Up()
    {
        throw new System.NotImplementedException();
    }

    public void UseOrb_Down()
    {
        throw new System.NotImplementedException();
    }

    public void UseOrb_Up()
    {
        throw new System.NotImplementedException();
    }

    public void VerticalInput(float v_input)
    {
        throw new System.NotImplementedException();
    }

    #endregion
    #region IInteractive Methods:

    override public void CollidedWithPlayer(Collider2D collision)
    {
        if (!givingQuest) {
            
        if (!givenQuest)
        {
            FlipAccordingToCollidedObject(collision);
            TalkDialogueQuest();
                givingQuest = true;
                StartCoroutine(WaitToGiveQuest());

        }
        else
        {
            if (orbsNeeded > 0)
            {


                if (collision.gameObject.GetComponent<Player>().CheckPocketOrb() == OrbType.None)
                {
                    TalkDialogue("I will need " + orbsNeeded + " more orbs. NOW MOVE!", 3);
                }
                else
                {
                    if (collision.gameObject.GetComponent<Player>().CheckPocketOrb() == OrbType.CoinOrb)
                    {
                        collision.gameObject.GetComponent<Player>().ConsumeOrb();
                        GivenCoinOrbToGhostTalk();

                    }
                    else
                    {
                        if (collision.gameObject.GetComponent<Player>().CheckPocketOrb() == OrbType.InstantDeathOrb)
                        {
                            collision.gameObject.GetComponent<Player>().ConsumeOrb();
                            GhostDie();
                        }
                        else
                        {


                            //All other orbs
                            GhostTrashTalk();

                        }
                    }
                }
            }



        }
    }
    }

    override public void CollidedWithOrb(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Orb>().GetOrb() == OrbType.CoinOrb)
        {
            Destroy(collision.gameObject);
            GivenCoinOrbToGhostTalk();
        }
        else {
            if (collision.gameObject.GetComponent<Orb>().GetOrb() == OrbType.InstantDeathOrb)
            {
                GhostDie();

            }
            else
            {
                GhostTrashTalk();
            }
            
        }

       


    }

    public override void CollidedWithInvisible(Collider2D collision)
    {
        TalkDialogue("Hmmm...must be the wind ", 2);
        FlipAccordingToCollidedObject(collision);

    }


    #endregion
    #region IMortal Methods:
    #endregion
    #region IElemental Methods:
    #endregion
    #region ITalkable Methods:
    #endregion

    #region Help Methods:
    IEnumerator EndLevelAfterFewSeconds() {

        yield return new WaitForSeconds(8);
        UtilityAccess.instance.OpenCompleteLevelWinow();
    }

    IEnumerator EndLevelAfterFewSecondsFail()
    {

        yield return new WaitForSeconds(2);
        UtilityAccess.instance.OpenFailLevelWinow();
    }

    IEnumerator WaitToGiveQuest()
    {

        yield return new WaitForSeconds(5);
        givenQuest = true;
        givingQuest = false;
    }

    private void GivenCoinOrbToGhostTalk() {

        orbsNeeded--;
        if (orbsNeeded > 0)
        {
            Dialogue orbTakenDialogue = new Dialogue(1);
            orbTakenDialogue.name = GetName();
            orbTakenDialogue.sentences[0] = "I will need " + orbsNeeded + " more orbs. NOW MOVE!";

            orbTakenDialogue.SecondsVisableSentence[0] = 5;

            TalkDialogue(orbTakenDialogue);
        }
        else
        {
            //rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Dialogue orbTakenDialogue = new Dialogue(4);
            orbTakenDialogue.name = GetName();
            orbTakenDialogue.sentences[0] = "I can finally go home!!!@!#!@!";
            orbTakenDialogue.sentences[1] = "*Takes the orbs out of his pocket*";
            orbTakenDialogue.sentences[2] = "*Consumes the orbs, one by one*";
            orbTakenDialogue.sentences[3] = "*Makes a portal*";
            orbTakenDialogue.SecondsVisableSentence[0] = 2;
            orbTakenDialogue.SecondsVisableSentence[1] = 2;
            orbTakenDialogue.SecondsVisableSentence[2] = 2;
            orbTakenDialogue.SecondsVisableSentence[3] = 2;
            TalkDialogue(orbTakenDialogue);
            StartCoroutine(EndLevelAfterFewSeconds());

        }

    }

    private void GhostDie() {
        TalkDialogue("",0);
        animator.SetTrigger("Died");
        StartCoroutine(EndLevelAfterFewSecondsFail());
    }

    private void GhostTrashTalk() {

        string TrashTalk = "What is this trash? ";
        int randomInt = Random.Range(0, 3);
        switch (randomInt)
        {
            case 0:
                TrashTalk += "You're sort of like an inverse Einstein.";
                break;
            case 1:
                TrashTalk += "Do you use this head to keep the rain out of your neck?";
                break;
            case 2:
                TrashTalk += "You're bright as Alaska in December.";
                break;
            case 3:
                TrashTalk += "Did you forget to pay your brain bill?";
                break;

        }
        TalkDialogue(TrashTalk, 3);

    }

    private void TalkDialogueQuest()
    {

        Dialogue talkDialogueQuest = new Dialogue(2);
        talkDialogueQuest.name = GetName();
        talkDialogueQuest.sentences[0] = "Oh! What a pleasant surprise! \n You must be the new drudge!";
        talkDialogueQuest.sentences[1] = "You must bring me "+ orbsNeeded +" more yellow shiny orbs!";

        talkDialogueQuest.SecondsVisableSentence[0] = 3;
        talkDialogueQuest.SecondsVisableSentence[1] = 3;
        TalkDialogue(talkDialogueQuest);
    }


    private void FlipAccordingToCollidedObject(Collider2D collision)
    {
        if (collision.transform.position.x < transform.position.x)
        {
            if (lookingRight)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                lookingRight = false;
            }
        }
        else
        {
            if (!lookingRight)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                lookingRight = true;
            }

        }
    }
    #endregion

}
