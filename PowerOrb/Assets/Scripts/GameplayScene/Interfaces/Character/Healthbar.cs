using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar{

    #region Attributes

    private int currentHealth = 100;
    private int maxHealth = 100;
    private int minHealth = 0;
    private bool isDead = false;
    private int reviveHealth = 100;

    #endregion



    #region Get/Set Methods

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetCurrentHealthPercent()
    {
        return ((float)currentHealth)/maxHealth;
    }

    private void SetCurrentHealth(int value)
    {
        currentHealth = value;
    }

    public bool GetIsDead() {
        return isDead;
    }




    #endregion

    #region Healthbar Methods

    public void TakeDamage(int value) {
        if (value > 0) {

        currentHealth = currentHealth - value;
            if (currentHealth < minHealth) {
                currentHealth = minHealth;
                isDead = true;
            } 

        }

    }


    public void HealSelf(int value)
    {
        if (value > 0)
        {

            currentHealth = currentHealth + value;
            if (currentHealth > maxHealth) currentHealth = maxHealth;

        }

    }

    public void Revive() {
        currentHealth = reviveHealth;
        isDead = false;
    }

    public void Kill()
    {
        currentHealth = 0;
        isDead = true;
    }


    #endregion


}
