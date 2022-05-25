using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Bio", menuName = "Characters/AI Bio")]
public class AIBio : CharacterBio
{
    [SerializeField] Sprite photo = null;
    [SerializeField] string characterLikes = null;
    [SerializeField] string characterDislikes = null;

    public Sprite Photo => photo;
    public string CharacterLikes => characterLikes;
    public string CharacterDislikes => characterDislikes;

}
