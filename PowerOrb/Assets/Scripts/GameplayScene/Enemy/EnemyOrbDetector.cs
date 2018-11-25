using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbDetector : MonoBehaviour {

    [SerializeField]
    public GameObject Enemy;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Orb")
        {
            OrbType type = collision.GetComponent<Orb>().GetOrb();

            switch (type)
            {
                case OrbType.InstantDeathOrb:
                    Enemy.GetComponent<EnemyMonster>().TalkDialogue("I diededed.", 1);
                    Destroy(collision.gameObject);
                    Enemy.GetComponent<EnemyMonster>().Suicide();
                    break;
                case OrbType.CoinOrb:
                    Enemy.GetComponent<EnemyMonster>().TalkDialogue("I like this one.", 1);
                    break;
                default:
                    Destroy(collision.gameObject);
                    break;
            }
            //Debug.Log("TOUCHY ORBY");
            //Destroy(Enemy);
            //Debug.Log("ENEMY DIEDEDED");
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
