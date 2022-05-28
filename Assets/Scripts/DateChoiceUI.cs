using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateChoiceUI : MonoBehaviour
{
    [SerializeField] Conversant dateConversant = null;
    [SerializeField] Conversant deathIntermission = null;

    public void SelectDate()
    {
        GetComponentInChildren<Button>().interactable = false;
        FindObjectOfType<GameManager>().StartDate(dateConversant, deathIntermission);
    }
}
