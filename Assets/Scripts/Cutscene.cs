using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class Cutscene : MonoBehaviour
{
    PlayableDirector cutscene;

    private void Awake()
    {
        cutscene = GetComponent<PlayableDirector>();
        cutscene.stopped += EndCutscene;
    }

    IEnumerator Start()
    {
        yield return LoadingManager.FadeIn();
        cutscene.Play();
    }
    private void EndCutscene(PlayableDirector obj)
    {
        StartCoroutine(CutsceneEnd());
    }
    IEnumerator CutsceneEnd()
    {
        yield return LoadingManager.FadeOut();
    }
}
