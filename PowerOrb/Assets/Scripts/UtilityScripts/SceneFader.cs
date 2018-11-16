using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance = null;

    private string load_scene;

    [SerializeField]
    private GameObject LoadingIndicator;

    [SerializeField]
    private Text LoadingText;

    [SerializeField]
    private Canvas SceneFaderCanvas;

    [SerializeField]
    private Animator SceneFaderAnimator;


    //Singleton
    void Awake()
    {

        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }





    #region FadeIn and FadeOut Load Scene Scripts Scripts

    public void Load_Scene(string scene)
    {
        LoadingIndicator.SetActive(true);
        LoadingText.gameObject.SetActive(true);
        //Will be replaced by calling SceneFader
        SceneFaderAnimator.SetTrigger(SceneFaderCS.FadeOut);
        load_scene = scene;
        
    }

    public void OnFadeComplete() {

        if (load_scene != ""){
        StartCoroutine(LoadAsyncScene(load_scene));
        }
            
    }

    public void RemoveLoadingIndicator() {
        LoadingIndicator.SetActive(false);
        LoadingText.gameObject.SetActive(false);
    }

    IEnumerator LoadAsyncScene(string level)
    {
        
        AsyncOperation s = SceneManager.LoadSceneAsync(level);
        while (!s.isDone) yield return null;
        SceneFaderCanvas.worldCamera = Camera.main.GetComponent<Camera>() as Camera;
        SceneFaderAnimator.SetTrigger(SceneFaderCS.FadeIn);
    }


    #endregion

}
