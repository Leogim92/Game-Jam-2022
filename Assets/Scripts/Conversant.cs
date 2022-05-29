using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversant : MonoBehaviour
{
    [SerializeField] string conversantName = null;
    [SerializeField] Sprite conversationBackground = null;
    [SerializeField] AudioClip speechSound = null;
    [SerializeField] List<Dialogue> dialogues = null;

    public string ConversantName => conversantName;
    public AudioClip SpeechSound => speechSound;
    public Sprite ConversationBackground => conversationBackground;


    public Dialogue GetDialogue(int index)
    {
        return dialogues[index];
    }
    public bool HasMoreDialogue(int index)
    {
        return dialogues.Count - 1 > index;
    }
}
