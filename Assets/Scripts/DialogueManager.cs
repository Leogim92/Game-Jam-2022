using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    enum GameState
    {
        Intro,
        PosCharSelection,
        SpeedDating,
        PreBossFight
    }

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


    GameState gameState;
    Conversant currentConversant;

    int currentDialogueIndex = 0;
    int currentDialogueSegmentIndex = 0;

    bool inResponse;
    private void Awake()
    {
        gameState = GameState.Intro;
        currentConversant = deathConversantIntro;
    }

    private void Start()
    {
        UpdateDialogueText();
    }

    public void NextButton()
    {
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

    private void HandleEndConversation()
    {
        if(gameState == GameState.Intro)
        {
            dialogueBox.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(true);
        }
        else if(gameState == GameState.PosCharSelection)
        {
            dialogueBox.gameObject.SetActive(false);
            dateSelectionScreen.gameObject.SetActive(true);
            gameState = GameState.SpeedDating;
        }
        else if(gameState == GameState.SpeedDating)
        {
            foreach (Button button in dateSelectionScreen.GetComponentsInChildren<Button>())
            {
                if (button.interactable)
                {
                    dialogueBox.gameObject.SetActive(false);
                    dateSelectionScreen.gameObject.SetActive(true);
                    return;
                }
            }
            gameState = GameState.PreBossFight;
        }
        else if(gameState == GameState.PreBossFight)
        {
            Debug.Log("Implement death talk before boss fight");
        }
    }

    private void UpdateDialogueText()
    {
        dialogueText.text = currentConversant.GetDialogue(currentDialogueIndex).GetDialogueSegment(currentDialogueSegmentIndex).text;
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
    public void StartDate(Conversant newDate)
    {
        currentConversant = newDate;
        dateSelectionScreen.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(true);

        currentDialogueIndex = 0;
        currentDialogueSegmentIndex = 0;
        UpdateDialogueText();
    }
}
