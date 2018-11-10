using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IInteractive {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        switch (tag)
        {
            case "Enemy":
                CollidedWithEnemy();
                break;
            case "Beet Armyworm":
                break;
            case "Indian Meal Moth":
                break;
            case "Ash Pug":
                break;
            case "Latticed Heath":
                break;
            case "Ribald Wave":
                break;
            case "The Streak":
                break;

            default:
                break;
        }
    

      



    }



    void IInteractive.CollidedWithEnemy()
    {
        throw new System.NotImplementedException();
    }
}
