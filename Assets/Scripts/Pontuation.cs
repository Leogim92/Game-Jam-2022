using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuation : MonoBehaviour
{
    [SerializeField] Sprite hughman = null;
    [SerializeField] Sprite hipothetica = null;
    [SerializeField] Sprite lovecraftThingie = null;
    [SerializeField] Sprite otis = null;
    [SerializeField] Sprite lorena = null;

    int hughmanPoints;
    int hipotheticaPoints;
    int lovecraftThingiePoints;
    int otisPoints;
    int lorenaPoints;

    public enum PointTarget
    {
        None,
        Hughman,
        Hipothetica,
        LovecraftThingie,
        Otis,
        Lorena
    }
    public void AddPoints(PointTarget pointTarget)
    {
        switch (pointTarget)
        {
            case PointTarget.None:
                break;
            case PointTarget.Hughman:
                hughmanPoints++;
                break;
            case PointTarget.Hipothetica:
                hipotheticaPoints++;
                break;
            case PointTarget.LovecraftThingie:
                lovecraftThingiePoints++;
                break;
            case PointTarget.Otis:
                otisPoints++;
                break;
            case PointTarget.Lorena:
                lorenaPoints++;
                break;
            default:
                break;
        }
    }
    public void RevealDate(out string characterName, out Sprite background)
    {
        int dateWinner = Mathf.Max(hughmanPoints, hipotheticaPoints, lovecraftThingiePoints, otisPoints, lorenaPoints);
        if(dateWinner == hughmanPoints)
        {
            characterName = "Hughman McPerson";
            background = hughman;
        }
        else if (dateWinner == hipotheticaPoints)
        {
            characterName = "Hipothetica";
            background = hipothetica;
        }
        else if (dateWinner == lovecraftThingiePoints)
        {
            characterName = "Yaghrazulb’Nyovlatheth";
            background = lovecraftThingie;
        }
        else if (dateWinner == lovecraftThingiePoints)
        {
            characterName = "Yaghrazulb’Nyovlatheth";
            background = lovecraftThingie;
        }
        else if(dateWinner == otisPoints)
        {
            characterName = "Oontz Oontz Otis";
            background = otis;
        }
        else
        {
            characterName = "Lorena Leeches";
            background = lorena;
        }

    }
    public void ShowLastScene()
    {
        LoadingManager.LoadLastScene();
    }
}
