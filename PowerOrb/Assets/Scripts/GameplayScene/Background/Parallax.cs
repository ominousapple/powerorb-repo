using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    #region Parallax Attributes:
    private GameObject[] BackgroundGameObjects;
    private Transform[] backgrounds; //Array of all the back- and foregrounds to be parallaxed
    private float[] parallaxScales; //The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;    // How smooth the parallax is going to be. Must be above 0

    private Transform cam;
    private Vector3 previousCamPos;
    #endregion

    void Awake()
    {
        cam = Camera.main.transform;

    }
    // Use this for initialization
    void Start () {
        BackgroundGameObjects = GameObject.FindGameObjectsWithTag("Parallax");

        backgrounds = new Transform[BackgroundGameObjects.Length];

        for (int i = 0; i < BackgroundGameObjects.Length; i++)
        {
            backgrounds[i] = BackgroundGameObjects[i].GetComponent<Transform>();
        }



        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];
        //Assining corresponding parallaxScales
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z *-1;

        }
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < backgrounds.Length; i++) {
            //The parallax is the opposite of the camera movement, because the previous frame multiplied by the scale
            //How much is moved, multiplied by the amount of parallaxScales[i]
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //Set target x position which is the current position plus the parallax
            //Storing Position of the background + the parallaxing in backgroundTargetPosX
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //Create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //Fade between current position and the target position using the built in Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos, smoothing * Time.deltaTime);



        }

        // Set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;

    }
}
