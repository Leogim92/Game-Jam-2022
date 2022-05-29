using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuation : MonoBehaviour
{
    public enum PointTarget
    {
        None,
        Hughman,
        DalaiKaren,
        LovecraftThingie,
        Otis,
        Lorena
    }

    public void RevealDate()
    {
        //Reveal Date
    }
    public void ShowLastScene()
    {
        LoadingManager.LoadLastScene();
    }
}
