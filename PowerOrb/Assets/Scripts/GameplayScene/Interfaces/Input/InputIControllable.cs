﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIControllable : MonoBehaviour
{

    [SerializeField]
    private GameObject ControllableGameObject;

    private IControllable controllable = null;

    void Awake()
    {
        MonoBehaviour[] list = ControllableGameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is IControllable)
            {
                controllable = mb as IControllable;
            }
        }

        if (controllable == null)
        {
            Debug.LogError("The attatched GameObject does not implement IControllable. \n Check Object with name: " + this + " Inspector: Controllable Game Object");
        }

    }

    public void Attack_Key_Down()
    {
        controllable.Attack_Down();
    }
    public void Attack_Key_Up()
    {
        controllable.Attack_Up();
    }



    public void Jump_Key_Down()
    {
        controllable.Jump_Down();
    }
    public void Jump_Key_Up()
    {
        controllable.Jump_Up();
    }



    public void Left_Key_Down()
    {
        controllable.MoveLeft_Down();
    }
    public void Left_Key_Up()
    {
        controllable.MoveLeft_Up();
    }


    public void Right_Key_Down()
    {
        controllable.MoveRight_Down();
    }
    public void Right_Key_Up()
    {
        controllable.MoveRight_Up();
    }


    public void UseOrb_Key_Down()
    {
        controllable.UseOrb_Down();
    }
    public void UseOrb_Key_Up()
    {
        controllable.UseOrb_Up();
    }

}