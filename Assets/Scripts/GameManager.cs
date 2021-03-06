using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Intro,
        PosCharSelection,
        SpeedDating,
        DeathIntermission,
        PreBossFight,
        PosBossFight,
        DateReveal
    }

    [SerializeField] Image background = null;
    [SerializeField] TextMeshProUGUI talkerName = null;
    [SerializeField] Transform dialogueBox = null;
    [SerializeField] TextMeshProUGUI dialogueText = null;
    [Space]
    [SerializeField] Transform choiceBox = null;
    [SerializeField] PlayerChoiceUI playerChoicePrefab = null;
    [Space]
    [Header("Intro")]
    [SerializeField] Conversant deathConversantIntro = null;
    [SerializeField] Conversant deathConversantAfterCharSelect = null;
    [SerializeField] Transform characterSelectionScreen = null;

    [Space]
    [Header("Speed Dating")]
    [SerializeField] Transform dateSelectionScreen = null;

    [Space]
    [Header("Boss Fight")]
    [SerializeField] Conversant deathConversantPreFight = null;
    [SerializeField] Conversant deathConversantPosFight = null;
    [SerializeField] Transform arena = null;
    [SerializeField] Transform talkArea = null;

    [Space]
    [Header("Date Reveal")]
    [SerializeField] Conversant deathRevealDate = null;

    GameState gameState;
    Conversant currentConversant;
    Conversant deathIntermission;

    AudioManager audioManager;

    int currentDialogueIndex = 0;
    int currentDialogueSegmentIndex = 0;

    bool inResponse;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameState = GameState.Intro;
        currentConversant = deathConversantIntro;
    }

    IEnumerator Start()
    {
        dialogueBox.gameObject.SetActive(false);
        yield return LoadingManager.FadeIn();
        dialogueBox.gameObject.SetActive(true);
        UpdateDialogueText();
    }

    public void NextButton()
    {
        audioManager.PlayButtonSound();
        if (inResponse)
        {
            if (currentConversant.HasMoreDialogue(currentDialogueIndex))
            {
                currentDialogueSegmentIndex = 0;
                currentDialogueIndex++;
                UpdateDialogueText();
            }
            inResponse = false;
            return;
        }
        if(currentConversant.GetDialogue(currentDialogueIndex).HasNextSegment(currentDialogueSegmentIndex))
        {
            currentDialogueSegmentIndex++;
            UpdateDialogueText();
        }
        else
        {
            if(currentConversant.GetDialogue(currentDialogueIndex).GetPlayerChoices() != null)
            {
                dialogueBox.gameObject.SetActive(false);
                choiceBox.gameObject.SetActive(true);

                PlayerChoices choices = currentConversant.GetDialogue(currentDialogueIndex).GetPlayerChoices();
                foreach (Transform item in choiceBox)
                {
                    Destroy(item.gameObject);
                }
                InstantiateChoice(choices.choice1);
                InstantiateChoice(choices.choice2);
                InstantiateChoice(choices.choice3);
            }
            else
            {
                HandleEndConversation();
            }
        }
    }
    public void UpdateBackground(Sprite sprite)
    {
        background.sprite = sprite;
    }
    private void HandleEndConversation()
    {
        if (gameState == GameState.Intro)
        {
            dialogueBox.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(true);
        }
        else if (gameState == GameState.PosCharSelection)
        {
            dialogueBox.gameObject.SetActive(false);
            dateSelectionScreen.gameObject.SetActive(true);
            gameState = GameState.SpeedDating;
        }
        else if (gameState == GameState.SpeedDating)
        {
            StartCoroutine(DeathIntermission());
            gameState = GameState.DeathIntermission;
        }
        else if (gameState == GameState.DeathIntermission)
        {
            foreach (Button button in dateSelectionScreen.GetComponentsInChildren<Button>())
            {
                if (button.interactable)
                {
                    dialogueBox.gameObject.SetActive(false);
                    dateSelectionScreen.gameObject.SetActive(true);
                    gameState = GameState.SpeedDating;
                    return;
                }
            }

            currentDialogueIndex = 0;
            currentDialogueSegmentIndex = 0;
            currentConversant = deathConversantPreFight;
            UpdateDialogueText();
            gameState = GameState.PreBossFight;
        }
        else if (gameState == GameState.PreBossFight)
        {
            StartFight();
        }
        else if(gameState == GameState.PosBossFight)
        {
            UpdateBackground(FindObjectOfType<Score>().RevealDate());
            currentDialogueIndex = 0;
            currentDialogueSegmentIndex = 0;
            currentConversant = deathRevealDate;
            UpdateDialogueText();
            gameState = GameState.DateReveal;
        }
        else if(gameState == GameState.DateReveal)
        {
            LoadingManager.LoadLastScene();
        }
    }

    private void UpdateDialogueText()
    {
        Dialogue.DialogueSegment dialogueSegment = currentConversant.GetDialogue(currentDialogueIndex).GetDialogueSegment(currentDialogueSegmentIndex);
        dialogueText.text = dialogueSegment.text;

        dialogueSegment.onDialogueSegmentUpdate?.Invoke();
        

        if (dialogueSegment.isPlayer)
        {
            talkerName.text = "Player";
        }
        else
        {
            talkerName.text = currentConversant.ConversantName;
        }
    }
    private void InstantiateChoice(Choice choice)
    {
        PlayerChoiceUI playerChoice = Instantiate(playerChoicePrefab, choiceBox);
        playerChoice.SetupPlayerChoice(choice);
    }
    public void SetResponseText(string responseText)
    {
        dialogueBox.gameObject.SetActive(true);
        choiceBox.gameObject.SetActive(false);
        inResponse = true;
        dialogueText.text = responseText;
    }
    public void SelectCharacter()
    {
        gameState = GameState.PosCharSelection;
        currentConversant = deathConversantAfterCharSelect;
        characterSelectionScreen.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(true);

        currentDialogueIndex = 0;
        currentDialogueSegmentIndex = 0;
        UpdateDialogueText();
    }
    public void StartDate(Conversant newDate, Conversant deathIntermission)
    {
        StartCoroutine(DateStart(newDate, deathIntermission));
    }
    private IEnumerator DateStart(Conversant newDate, Conversant deathIntermission)
    {
        dateSelectionScreen.gameObject.SetActive(false);
        yield return LoadingManager.FadeOut();
        audioManager.SetMusic(AudioManager.Music.Bar);

        currentConversant = newDate;
        this.deathIntermission = deathIntermission;
        currentDialogueIndex = 0;
        currentDialogueSegmentIndex = 0;
        UpdateBackground(currentConversant.ConversationBackground);
        yield return LoadingManager.FadeIn();

        dialogueBox.gameObject.SetActive(true);
        UpdateDialogueText();
    }

    IEnumerator DeathIntermission()
    {
        dialogueBox.gameObject.SetActive(false);

        yield return LoadingManager.FadeOut();
        audioManager.SetMusic(AudioManager.Music.Death);
        currentDialogueIndex = 0;
        currentDialogueSegmentIndex = 0;
        currentConversant = deathIntermission;
        UpdateBackground(currentConversant.ConversationBackground);
        yield return LoadingManager.FadeIn();

        dialogueBox.gameObject.SetActive(true);
        UpdateDialogueText();
    }
    private void StartFight()
    {
        StartCoroutine(FightStarter());
        StartCoroutine(StartFightMusic());
    }
    private IEnumerator FightStarter()
    {
        yield return LoadingManager.FadeOut();
        talkArea.gameObject.SetActive(false);
        arena.gameObject.SetActive(true);
        yield return LoadingManager.FadeIn();
    }
    private IEnumerator StartFightMusic()
    {
        audioManager.StopBackgroundMusic();
        audioManager.PlayFightIntro(out float duration);
        yield return new WaitForSeconds(duration);
        audioManager.SetMusic(AudioManager.Music.Fight);
    }
    public void RevealDate()
    {
        StartCoroutine(DateRevealer());
    }
    private IEnumerator DateRevealer()
    {
        dialogueBox.gameObject.SetActive(false);
        yield return LoadingManager.FadeOut();
        arena.gameObject.SetActive(false);
        talkArea.gameObject.SetActive(true);

        currentDialogueIndex = 0;
        currentDialogueSegmentIndex = 0;
        currentConversant = deathConversantPosFight;
        UpdateDialogueText();
        gameState = GameState.PosBossFight;

        yield return LoadingManager.FadeIn();
        dialogueBox.gameObject.SetActive(true);
    }
}
