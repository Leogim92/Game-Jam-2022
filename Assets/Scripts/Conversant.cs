using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversant : MonoBehaviour
{
    [SerializeField] AIBio aiBio = null;
    [SerializeField] List<Dialogue> dialogues = null;

    public AIBio AIBio => aiBio;

    public Dialogue GetDialogue(int index)
    {
        return dialogues[index];
    }
    public bool HasMoreDialogue(int index)
    {
        return dialogues.Count - 1 > index;
    }
}
