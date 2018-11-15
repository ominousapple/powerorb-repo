using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAccess : MonoBehaviour {
    public static UtilityAccess instance = null;
    //

    void Awake() {
        instance = this;

    }

    #region SceneFader Utility
    //Used by Buttons (Mostly)
    public void SceneFaderLoadScene(string scene) {
        SceneFader.instance.Load_Scene(scene);

    }
    public SceneFader GetSceneFaderInstance() {
        return SceneFader.instance;
    }
    #endregion

    #region Popup Menu Utility




    #endregion


}
