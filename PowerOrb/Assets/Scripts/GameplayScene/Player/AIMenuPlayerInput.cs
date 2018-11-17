using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMenuPlayerInput : InputIControllable
{
    int horInp = -1;

	// Use this for initialization
	void Start () {
        ChangedHorizontal(horInp);
        //StartCoroutine(TalkEveryFiveSeconds());
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

       if (collision.tag == "MenuTurnAroundBox") {
            
            horInp = -horInp;

            ChangedHorizontal(0);
            StartCoroutine(TalkEverySevenSeconds());

        }

        
    }

    IEnumerator TalkEverySevenSeconds() {

        GetComponent<Player>().TalkDialogue();
        yield return new WaitForSeconds(7);
        ChangedHorizontal(horInp);

    }


}
