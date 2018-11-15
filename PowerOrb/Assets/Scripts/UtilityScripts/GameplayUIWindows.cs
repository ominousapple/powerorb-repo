using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIWindows : MonoBehaviour
{
    public static GameplayUIWindows instance = null;

    [SerializeField]
    private Image PauseUI;
    [SerializeField]
    private Image CompleteLevelUI;
    [SerializeField]
    private Button PauseButton;

    void Awake()
    {

        //Check if instance already exists
        if (instance == null)
            instance = this;


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    #region Help Methods for Enabling/Disabling UI Windows:

    public void EnablePauseUI() {
        PauseButton.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(true);
    }
    public void DisablePauseUI()
    {
        PauseButton.gameObject.SetActive(true);
        PauseUI.gameObject.SetActive(false);
    }

    public void EnableCompleteLevelUI()
    {
        PauseButton.gameObject.SetActive(false);
        CompleteLevelUI.gameObject.SetActive(true);
    }
    public void DisableCompleteLevelUI()
    {
        PauseButton.gameObject.SetActive(true);
        CompleteLevelUI.gameObject.SetActive(false);
    }




    #endregion
}
