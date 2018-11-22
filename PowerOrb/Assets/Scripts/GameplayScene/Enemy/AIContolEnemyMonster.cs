using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIContolEnemyMonster : InputIControllable
{
    int horInp = -1;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "MenuTurnAroundBox")
        {

            horInp = -horInp;

            ChangedHorizontal(0);
            ChangedHorizontal(horInp);
            //StartCoroutine(TalkEverySevenSeconds());

        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Kappa");
    }

    //IEnumerator TalkEverySevenSeconds()
    //{

    //    GetComponent<Player>().TalkDialogue();
    //    yield return new WaitForSeconds(7);
    //    ChangedHorizontal(horInp);

    //}

    void FixedUpdate() 
    {

    }

	// Use this for initialization
	void Start () {
       ChangedHorizontal(horInp);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
