using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    static LoadingManager loadingManager;

    [SerializeField] Animator fadeLoader = null;
    static Animator FadeLoader => loadingManager.fadeLoader;


    private void Awake()
    {
        if (loadingManager == null)
        {
            loadingManager = this;
            DontDestroyOnLoad(gameObject);
        }
        if (loadingManager != this)
        {
            Destroy(gameObject);
        }
    }
    public static IEnumerator FadeIn()
    {
        FadeLoader.SetBool("Fade", false);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
    }
    public static IEnumerator FadeOut()
    {
        FadeLoader.SetBool("Fade", true);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
    }

    public static void LoadLastScene()
    {
        loadingManager.StartCoroutine(LoadSceneAsync("Epilogue"));
    }

    static IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        yield return FadeOut();
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncScene.allowSceneActivation = false;
        while (!asyncScene.isDone)
        {
            if (asyncScene.progress >= 0.9f)
            {
                asyncScene.allowSceneActivation = true;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return FadeIn();
    }
}
