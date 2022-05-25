using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [System.Serializable]
    public class DialogueSegment
    {
        [TextArea] public string text;
        public bool isPlayer;
    }

    [SerializeField] List<DialogueSegment> dialogueSegments = null;

    public IEnumerable<DialogueSegment> GetDialogueSegments()
    {
        return dialogueSegments;
    }
}
