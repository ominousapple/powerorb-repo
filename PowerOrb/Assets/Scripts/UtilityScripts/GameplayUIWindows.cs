using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIWindows : MonoBehaviour
{
    public static GameplayUIWindows instance = null;


    [SerializeField]
    private GameObject OrbPrefab;

    [SerializeField]
    private Image FailUI;
    [SerializeField]
    private Image PauseUI;
    [SerializeField]
    private Image CompleteLevelUI;

    [SerializeField]
    private Button PauseButton;
    [SerializeField]
    private Image OrbUI;

    void Awake()
    {
        OrbUI.gameObject.SetActive(false);
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

    public void EnableDefeatUI()

    {
        PauseButton.gameObject.SetActive(false);
        FailUI.gameObject.SetActive(true);


    }
    public void DisableDefeatUI()
    {
        PauseButton.gameObject.SetActive(true);
        FailUI.gameObject.SetActive(false);

    }

    public void SetOrbUI(OrbType TypeOfUIOrb) {
        if (TypeOfUIOrb != OrbType.None)
        {
            OrbUI.gameObject.SetActive(true);
            int colorIndex = (int)TypeOfUIOrb;
            OrbUI.GetComponent<Image>().color = OrbPrefab.GetComponent<Orb>().colorsOfOrbTypes[colorIndex];

        }
        else {
            OrbUI.gameObject.SetActive(false);

        }

    }



    #endregion
}
