using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Bio", menuName = "Characters/AI Bio")]
public class AIBio : CharacterBio
{
    [SerializeField] Sprite photo = null;
    [SerializeField] List<CharacterBio> blacklistedBios = null;


    public bool IsBioBlacklisted(CharacterBio bio)
    {
        return blacklistedBios.Contains(bio);
    }
}

//[System.Serializable]
//public class Conversation
//{
    
//}
