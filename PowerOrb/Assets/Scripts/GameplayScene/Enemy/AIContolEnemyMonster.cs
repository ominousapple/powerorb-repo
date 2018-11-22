using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIContolEnemyMonster : InputIControllable
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack_Key_Down();

        }
        if (Input.GetKeyUp(KeyCode.A))
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

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
