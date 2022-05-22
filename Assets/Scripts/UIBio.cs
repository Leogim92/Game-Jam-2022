using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBio : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bioName = null;
    [SerializeField] TextMeshProUGUI bioCauseOfDeath = null;
    [SerializeField] TextMeshProUGUI bioDescription = null;

    CharacterBio characterBio;

    public static event Action<CharacterBio> OnBioSelected;
    public void SetupBio(CharacterBio bio)
    {
        bioName.text = bio.CharacterName;
        bioCauseOfDeath.text = bio.CauseOfDeath;
        bioDescription.text = bio.CharacterDescription;

        characterBio = bio;
    }
    public void SelectBio()
    {
        OnBioSelected.Invoke(characterBio);
    }
}
