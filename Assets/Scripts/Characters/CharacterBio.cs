using UnityEngine;

[CreateAssetMenu(fileName = "Character Bio", menuName = "Characters/Character Bio")]
public class CharacterBio : ScriptableObject
{
    [SerializeField] string characterName = null;
    [SerializeField] string causeOfDeath = null;
    [SerializeField, TextArea] string characterDescription = null;

    public string CharacterName => characterName;
    public string CauseOfDeath => causeOfDeath;
    public string CharacterDescription => characterDescription;
}
