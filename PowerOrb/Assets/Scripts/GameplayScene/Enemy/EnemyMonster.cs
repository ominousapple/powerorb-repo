using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster : Character, IControllable, IInteractive, IMortal
{
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
        Debug.Log("Monster walks left");
    }

    public void MoveLeft_Up()
    {
        
    }

    public void MoveRight_Down()
    {
        Debug.Log("Monster walks right");
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
