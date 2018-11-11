using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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


    #endregion





}
