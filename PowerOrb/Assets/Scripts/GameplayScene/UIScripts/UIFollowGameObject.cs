using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIFollowGameObject : MonoBehaviour {

    [SerializeField]
    private GameObject Obj;

    private Camera myCamera;


    private bool isScriptWorking = false;

    void Awake () {
        myCamera= Camera.main;
        if (Obj != null)
            isScriptWorking = true;

        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {

        if (isScriptWorking)
        {
            transform.position = Obj.transform.position;
        }
        

    }
}
