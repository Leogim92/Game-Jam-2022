using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] List<CharacterBio> possibleBios = null;

    CharacterBio selectedBio;

    public IEnumerable GetPossibleBios()
    {
        return possibleBios;
    }
    public void SetSelectedBio(CharacterBio bio)
    {
        selectedBio = bio;
    }
}
