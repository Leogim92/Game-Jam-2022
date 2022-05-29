using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Choices", menuName = "New PlayerChoices")]
public class PlayerChoices : ScriptableObject
{
    public Choice choice1 = null;
    public Choice choice2 = null;
    public Choice choice3 = null;

}
[System.Serializable]
public class Choice
{
    [TextArea] public string choice = null;
    [TextArea] public string response = null;
    public Pontuation.PointTarget pointTarget = Pontuation.PointTarget.None;
}
