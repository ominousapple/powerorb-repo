using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowControlsUI : MonoBehaviour {
    public static ShowControlsUI instance = null;

    [SerializeField]
    private Image KeyboardControlsUI;
    // Use this for initialization
    void Awake () {
        instance = this;
    }
    public void FlashControls()
    {
        
        if (KeyboardControlsUI != null)
        {
            StartCoroutine(FlashControlsIEnumerator());
        }
    }

     IEnumerator FlashControlsIEnumerator() {
       
            
            KeyboardControlsUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        KeyboardControlsUI.gameObject.SetActive(false);
        
    }

}
