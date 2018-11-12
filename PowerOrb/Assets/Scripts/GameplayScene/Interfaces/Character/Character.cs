using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskNames {
    public const string Fire = "Water";
    public const string Slime = "Slime";
    public const string Dirt = "Dirt";
    public const string Ice = "Ice";
    public const string Stone = "Stone";

}


public class collidableObjects{
    public const string Enemy = "Enemy";
    public const string Enemy_attack = "EnemyAttack";
    public const string Orb = "Orb";
    public const string Player = "Player";
}
public class Character : MonoBehaviour, IInteractive {

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

    #endregion


    public void Awake()
    {
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



    private void OnTriggerEnter2D(Collider2D collision)
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
            default:
                break;
        }
    

      



    }

    private void OnTriggerExit2D(Collider2D collision)
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





}
