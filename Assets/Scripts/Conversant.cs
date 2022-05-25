using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversant : MonoBehaviour
{
    [SerializeField] AIBio aiBio = null;

    public AIBio AIBio => aiBio;
}
