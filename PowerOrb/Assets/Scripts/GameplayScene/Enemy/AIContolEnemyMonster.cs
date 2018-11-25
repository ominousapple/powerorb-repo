using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIContolEnemyMonster : InputIControllable
{
    int horInp = -1;
    bool chasingPlayer = false;
    Transform LastPlayerTransform;
    
    public float moveSpeed;

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "MenuTurnAroundBox")
        {
            horInp = -horInp;
            ChangedHorizontal(0);
            ChangedHorizontal(horInp);
            //StartCoroutine(TalkEverySevenSeconds());

        }

        if(collision.tag == "Player")
        {
            chasingPlayer = true;
            LastPlayerTransform = collision.transform;
            //Chase(collision.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            chasingPlayer = false;
            ChangedHorizontal(0);
            GetComponent<EnemyMonster>().TalkDialogue("Stop!", 3);
        }
    }

    void Chase(Transform ChasePlayerTransform)
    {
        if(transform.position.x > ChasePlayerTransform.position.x)
        {
            ChangedHorizontal(-1);
        }

        if (transform.position.x < ChasePlayerTransform.position.x)
        {
            ChangedHorizontal(1);
        }

        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(ChasePlayerTransform.position.x, transform.position.y), moveSpeed * Time.deltaTime);
    }



    //IEnumerator TalkEverySevenSeconds()
    //{

    //    GetComponent<Player>().TalkDialogue();
    //    yield return new WaitForSeconds(7);
    //    ChangedHorizontal(horInp);

    //}

    void FixedUpdate()
    {
        if (chasingPlayer)
        {
            Chase(LastPlayerTransform);
            GetComponent<EnemyMonster>().ShouldAttack();
        }
    }

    // Use this for initialization
    void Start()
    {
        ChangedHorizontal(horInp);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
