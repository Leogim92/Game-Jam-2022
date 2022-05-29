using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue 
{
    [System.Serializable]
    public class DialogueSegment
    {
        [TextArea] public string text;
        public bool isPlayer;
        public UnityEvent onDialogueSegmentUpdate = null;
    }

    [SerializeField] List<DialogueSegment> dialogueSegments = null;
    [SerializeField] PlayerChoices playerChoices = null;
    public DialogueSegment GetDialogueSegment(int index)
    {
        return dialogueSegments[index];
    }
    public bool HasNextSegment(int currentSegmentIndex)
    {
        return dialogueSegments.Count - 1 > currentSegmentIndex;
    }
    public PlayerChoices GetPlayerChoices()
    {
        return playerChoices;
    }
}
