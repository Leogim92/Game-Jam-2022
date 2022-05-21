using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        FadeLoader.SetTrigger("FadeIn");
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
    }
    public static IEnumerator FadeOut()
    {
        FadeLoader.SetTrigger("FadeOut");
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
    }
}
