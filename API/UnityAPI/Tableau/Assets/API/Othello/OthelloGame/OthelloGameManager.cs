using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tableau;
using Tableau.Time;

public class OthelloGameManager : MonoBehaviour {

    private static OthelloGameManager instance;
    public static OthelloGameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<OthelloGameManager>() as OthelloGameManager;
                if (!instance)
                {
                    Debug.LogError("No GM found in scene");
                }
                else
                {
                    instance.Instantiate();
                }
            }
            return instance;
        }
    }

    void Instantiate()
    {

    }

    private int sizeOption;
    public int SizeOption
    {
        get{ return sizeOption; }
        set{ sizeOption = value; }
    }

    public OthelloBoardBehaviour boardReference;

    private bool turn = false; //false = black, true = white
    public string GetTurn
    {
        get { return turn? "white" : "black"; }
    }

    public void ChangeTurn()
    {
        turn = !turn;
    }

    public bool PutPieceCheck(OthelloZoneBehaviour z, bool turn)
    {
        return true;//TODO implement rule check
    }


    

}
