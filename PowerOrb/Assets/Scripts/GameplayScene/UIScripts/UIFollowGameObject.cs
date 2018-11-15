using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIFollowGameObject : MonoBehaviour {

    [SerializeField]
    private GameObject Obj;

    private Camera myCamera;

    private RectTransform rt;

    private bool isScriptWorking = false;

    void Awake () {
        myCamera= Camera.main;
        if (Obj != null)
            isScriptWorking = true;

        rt = GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {

        if (isScriptWorking)
        {
            transform.position = Obj.transform.position;
            //Vector2 pos = RectTransformUtility.WorldToScreenPoint(myCamera, Obj.transform.position);
           // RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, pos,myCamera,out pos);
            //rt.position = pos;
        }
        

    }
}
