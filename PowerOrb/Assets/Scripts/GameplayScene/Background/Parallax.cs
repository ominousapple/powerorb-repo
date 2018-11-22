using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Parallax : MonoBehaviour {
    #region Parallax Attributes:
    private GameObject[] BackgroundGameObjects;
    private Transform[] backgrounds; //Array of all the back- and foregrounds to be parallaxed
    private float[] parallaxScales; //The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;    // How smooth the parallax is going to be. Must be above 0

    private Transform cam;
    private Vector3 previousCamPos;
    #endregion

    #region GenerateBackgrounds Attrivutes:

    private string GeneratedBGTag = "SetBG";
    private bool isStarted = false;
    [SerializeField]
    private int numberBGLeft = 0;
    [SerializeField]
    private int numberBGRight = 0;

    private int allGeneratedBGs = 0;

    [SerializeField]
    private float distanceBetweenBGSetsX = 0f;
    [SerializeField]
    private float distanceBetweenBGSetsY = 0f;

    [SerializeField]
    private GameObject BackgroundPrefab = null;

    [SerializeField]
    private GameObject DownwardsBackgroundPrefab = null;
    

    [SerializeField]
    private GameObject ParentGameObjectOfBackgrounds = null;

    //private GameObject[] AllGeneratedBGs;
    private GameObject[] AllGeneratedBGsDELETE;

    #endregion

    void Awake()
    {
        //allGeneratedBGs = numberBGLeft + numberBGRight;
        //AllGeneratedBGs = new GameObject[numberBGLeft + numberBGRight];
        cam = Camera.main.transform;

    }
    // Use this for initialization
    void Start () {

        //GeneratingBackgrounds:
        AllGeneratedBGsDELETE = GameObject.FindGameObjectsWithTag(GeneratedBGTag);
        if (!isStarted)
        {
            DestroySetBGs();
            GenerateBackgrounds();

            isStarted = true;
        }


            //Parallax
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

        if (!isStarted) {
            DestroySetBGsUpdate();
            GenerateBackgrounds();
            isStarted = true;
        }


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



    void OnValidate()
    {
        isStarted = false;
     
    }

    private void GenerateBackgrounds() {
        if (ParentGameObjectOfBackgrounds != null && BackgroundPrefab != null && DownwardsBackgroundPrefab != null)
        {

            
            


            for (int i = 0; i < numberBGRight; i++) { 
            
            GameObject childBackgroundSet = Instantiate(BackgroundPrefab, ParentGameObjectOfBackgrounds.transform.position, Quaternion.identity);
            childBackgroundSet.transform.parent = ParentGameObjectOfBackgrounds.transform;
            childBackgroundSet.transform.position = new Vector2(distanceBetweenBGSetsX*(i+1), ParentGameObjectOfBackgrounds.transform.position.y);
                childBackgroundSet.tag = GeneratedBGTag;

                GameObject childBackgroundSet2 = Instantiate(DownwardsBackgroundPrefab, ParentGameObjectOfBackgrounds.transform.position, Quaternion.identity);
                childBackgroundSet2.transform.parent = ParentGameObjectOfBackgrounds.transform;
                childBackgroundSet2.transform.position = new Vector2(distanceBetweenBGSetsX * (i + 1), ParentGameObjectOfBackgrounds.transform.position.y + distanceBetweenBGSetsY);
                childBackgroundSet2.tag = GeneratedBGTag;



            }

            for (int i = 0; i < numberBGLeft; i++)
            {

                GameObject childBackgroundSet = Instantiate(BackgroundPrefab, ParentGameObjectOfBackgrounds.transform.position, Quaternion.identity);
                childBackgroundSet.transform.parent = ParentGameObjectOfBackgrounds.transform;
                childBackgroundSet.transform.position = new Vector2(-distanceBetweenBGSetsX * (i + 1), ParentGameObjectOfBackgrounds.transform.position.y);
                childBackgroundSet.tag = GeneratedBGTag;

                    GameObject childBackgroundSet2 = Instantiate(DownwardsBackgroundPrefab, ParentGameObjectOfBackgrounds.transform.position, Quaternion.identity);
                    childBackgroundSet2.transform.parent = ParentGameObjectOfBackgrounds.transform;
                    childBackgroundSet2.transform.position = new Vector2(-distanceBetweenBGSetsX * (i + 1), ParentGameObjectOfBackgrounds.transform.position.y + distanceBetweenBGSetsY);
                    childBackgroundSet2.tag = GeneratedBGTag;

            }
        }
    }


    private void DestroySetBGs()
    {
        if (AllGeneratedBGsDELETE != null)
        {
            for (int i = 0; i < AllGeneratedBGsDELETE.Length; i++)
            {
                if (AllGeneratedBGsDELETE[i] != null)
                    DestroyImmediate(AllGeneratedBGsDELETE[i]);//DestroyImmediate
            }
        }

    }

    private void DestroySetBGsUpdate()
    {
        AllGeneratedBGsDELETE = null;
        AllGeneratedBGsDELETE = GameObject.FindGameObjectsWithTag(GeneratedBGTag);
        if (AllGeneratedBGsDELETE != null)
        {
            for (int i = 0; i < AllGeneratedBGsDELETE.Length; i++)
            {
                if (AllGeneratedBGsDELETE[i] != null)
                    DestroyImmediate(AllGeneratedBGsDELETE[i]);//DestroyImmediate
            }
        }


        //Restart Parallax algorithm attributes:
        BackgroundGameObjects = null;
        BackgroundGameObjects = GameObject.FindGameObjectsWithTag("Parallax");

        backgrounds = new Transform[BackgroundGameObjects.Length];

        for (int i = 0; i < BackgroundGameObjects.Length; i++)
        {
            backgrounds[i] = BackgroundGameObjects[i].GetComponent<Transform>();
        }

    }

}
