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
        if (SceneFader.instance != null)
            SceneFader.instance.Load_Scene(scene);

    }
    public SceneFader GetSceneFaderInstance() {
        //Might return null if it's not instanced for some reason 
        return SceneFader.instance;
    }
    #endregion

    #region GameplayUIWindows Utility

    public void OpenPauseWinow()
    {
        if (GameplayUIWindows.instance != null)
        GameplayUIWindows.instance.EnablePauseUI();
    }
    public void ClosePauseWinow()
    {
        if (GameplayUIWindows.instance != null)
            GameplayUIWindows.instance.DisablePauseUI();
    }

    public void OpenCompleteLevelWinow()
    {
        if (GameplayUIWindows.instance != null)
            GameplayUIWindows.instance.EnableCompleteLevelUI();
    }
    public void CloseCompleteLevelWinow()
    {
        if (GameplayUIWindows.instance != null)
            GameplayUIWindows.instance.DisableCompleteLevelUI();
    }

    public void OpenFailLevelWinow()
    {
        if (GameplayUIWindows.instance != null)
            GameplayUIWindows.instance.EnableDefeatUI();
    }
    public void CloseFailLevelWinow()
    {
        if (GameplayUIWindows.instance != null)
            GameplayUIWindows.instance.DisableDefeatUI();
    }

    public void StartStopMusic()
    {
            Debug.Log("Music Started/Stopped - feature not implemented yet");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }


    #endregion


}
