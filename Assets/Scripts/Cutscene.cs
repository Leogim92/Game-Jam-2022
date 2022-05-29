using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class Cutscene : MonoBehaviour
{
    [SerializeField] string sceneToLoad = null;

    PlayableDirector cutscene;

    private void Awake()
    {
        cutscene = GetComponent<PlayableDirector>();
    }

    IEnumerator Start()
    {
        yield return LoadingManager.FadeIn();
        cutscene.Play();
    }
}
