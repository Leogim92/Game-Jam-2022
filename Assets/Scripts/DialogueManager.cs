using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    enum GameState
    {
        Intro,
        SpeedDating,
    }

    [SerializeField] Transform dialogueBox = null;
    [SerializeField] TextMeshProUGUI dialogueText = null;
    [Space]
    [SerializeField] Transform choiceBox = null;
    [SerializeField] PlayerChoiceUI playerChoicePrefab = null;
    [Space]
    [Header("Intro")]
    [SerializeField] Conversant deathConversant = null;
    [SerializeField] Transform characterSelectionScreen = null;

    GameState gameState;
    Conversant currentConversant;

    int currentDialogueIndex = 0;
    int currentDialogueSegmentIndex = 0;

    bool inResponse;
    private void Awake()
    {
        gameState = GameState.Intro;
        currentConversant = deathConversant;
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

    }
}
