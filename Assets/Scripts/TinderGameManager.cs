using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderGameManager : MonoBehaviour
{

    [SerializeField] Player player;

    [Space, Header("Bio Selecting"), Space]
    [SerializeField] Transform bioChoosingMenu = null;
    [SerializeField] Transform biosContainer = null;
    [SerializeField] UIBio uiBioPrefab = null;
    bool bioSelected = false;


    [Space, Header("Swipe Game"), Space]
    [SerializeField] Transform swipeGameMenu = null;
    [SerializeField] Transform characterPanel = null;
    [SerializeField] CharacterBioUI characterBioPrefab = null;
    [SerializeField] List<AIBio> possibleCandidates = null;

    List<GameObject> candidates = new List<GameObject>();
    private void Start()
    {
        UIBio.OnBioSelected += SetSelectedBio;
        StartCoroutine(TinderGame());
    }
    IEnumerator TinderGame()
    {
        yield return SelectBio();

        yield return SwipeGame();
    }

    #region BioChoosing
    IEnumerator SelectBio()
    {
        PopulateBios();
        yield return LoadingManager.FadeIn();
        bioChoosingMenu.gameObject.SetActive(true);
        yield return new WaitUntil(() => bioSelected);
        bioChoosingMenu.gameObject.SetActive(false);
    }

    private void PopulateBios()
    {
        foreach (Transform item in biosContainer.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (CharacterBio bio in player.GetPossibleBios())
        {
            UIBio newBio = Instantiate(uiBioPrefab, biosContainer);
            newBio.SetupBio(bio);
        }
    }
    public void SetSelectedBio(CharacterBio bio)
    {
        player.SetSelectedBio(bio);
        bioSelected = true;
    }
    #endregion
    #region SwipeGame
    IEnumerator SwipeGame()
    {
        yield return new WaitForEndOfFrame();
        swipeGameMenu.gameObject.SetActive(true);
        PopulatePossibleCandidates();
        yield return new WaitUntil(() => MatchIsResolved());
    }

    private void PopulatePossibleCandidates()
    {
        foreach (Transform item in characterPanel.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (AIBio bio in possibleCandidates)
        {
            CharacterBioUI newBio = Instantiate(characterBioPrefab, characterPanel);
            newBio.Setup(bio);
            newBio.gameObject.SetActive(false);
            candidates.Add(newBio.gameObject);
        }
    }

    private bool MatchIsResolved()
    {
        return false;
    }


    #endregion
}
