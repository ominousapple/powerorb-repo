using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIContolEnemyMonster : InputIControllable
{
    int horInp = 0;

    [SerializeField]
    public Transform Player;
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
            Chase();
        }
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.position.x, transform.position.y), moveSpeed * Time.deltaTime);
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
    void Start()
    {
        ChangedHorizontal(horInp);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
