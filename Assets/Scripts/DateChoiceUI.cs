using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateChoiceUI : MonoBehaviour
{
    [SerializeField] Conversant dateConversant = null;

    public void SelectDate()
    {
        GetComponentInChildren<Button>().interactable = false;
        FindObjectOfType<DialogueManager>().StartDate(dateConversant);
    }
}
