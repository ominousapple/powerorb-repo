using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : InputIControllable
{
    // Update is called once per frame
    void Update()
    {
        
        // Example of how to set keyboard input for your Class that implements IControllable
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack_Key_Down();

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Attack_Key_Up();

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump_Key_Down();

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Jump_Key_Up();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left_Key_Down();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Left_Key_Up();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right_Key_Down();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Right_Key_Up();
        }
        ChangedHorizontal(Input.GetAxis("Horizontal"));
        ChangedVertical(Input.GetAxis("Vertical"));



        // to do : add user orb 

    }


}
