using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerChoiceUI : MonoBehaviour
{
    Choice choice;
    public void SetupPlayerChoice(Choice choice)
    {
        this.choice = choice;
        GetComponentInChildren<TextMeshProUGUI>().text = choice.choice;
    }
    public void SetChoiceResponse()
    {
        if(choice.pointTarget != Score.PointTarget.None)
        {
            FindObjectOfType<Score>().AddPoints(choice.pointTarget);
        }
        FindObjectOfType<GameManager>().SetResponseText(choice.response);
    }
}
