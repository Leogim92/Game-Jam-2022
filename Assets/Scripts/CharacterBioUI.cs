using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBioUI : MonoBehaviour
{
    [SerializeField] Image photo = null;
    [SerializeField] TextMeshProUGUI characterName = null;
    [SerializeField] TextMeshProUGUI characterDescription = null;
    [SerializeField] TextMeshProUGUI characterLikes = null;
    [SerializeField] TextMeshProUGUI characterDislikes = null;
    [SerializeField] TextMeshProUGUI characterCauseOfDeath = null;

    public void Setup(AIBio bio)
    {
        photo.sprite = bio.Photo;
        characterName.text = bio.CharacterName;
        characterDescription.text = bio.CharacterDescription;
        characterLikes.text = bio.CharacterLikes;
        characterDislikes.text = bio.CharacterDislikes;
        characterCauseOfDeath.text = bio.CauseOfDeath;
    }

}
