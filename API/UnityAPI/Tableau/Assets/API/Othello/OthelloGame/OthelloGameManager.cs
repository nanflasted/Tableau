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

    private int whiteNum, blackNum;
    public int White
    {
        get { return whiteNum; }
        set
        {
            whiteNum = value;
            //TODO: change the number on HUD
        }
    }
    public int Black
    {
        get { return blackNum; }
        set
        {
            blackNum = value;
            //TODO: change the number on HUD
        }
    }


    private bool turn = false; //false = black, true = white
    private bool playerTurn;
    public string PlayerTurnSelection
    {
        get { return playerTurn ? "white" : "black"; }
        set { turn = value.Equals("white"); }
    }

    public string GetCurrentTurn
    {
        get { return turn? "white" : "black"; }
    }






    public void ChangeTurn()
    {
        turn = !turn;
        if (turn != playerTurn) EventManager.instance.FreezeControl(); else EventManager.instance.GrantControl();
        //TODO: Detect if there are legal moves for the current player
        //      If not, skip turn, display on HUD
        //      If skipped both turns, end game
    }

    public void InitializeGame()
    {
        White = 4;
        Black = 4;
        int m = SizeOption / 2;
        //TODO: add 8 pieces in the center
        // w  - b
        // | \  |\
        // b -b w- w
        //  \ |    |
        //    w  - b
    }

    public bool PutPieceCheck(OthelloZoneBehaviour z, bool turn)
    {
        if (!z.IsEmpty()) return false;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && j == 0 && k == 0) continue;
                    int zi = z.coord[0], zj = z.coord[1], zk = z.coord[2];
                    int dist = 0;
                    OthelloPieceBehaviour currPiece;
                    while(
                            zi>=0 && zi<=SizeOption && 
                            zj>=0 && zj<=SizeOption && 
                            zk>=0 && zk<=SizeOption                            
                         )
                    {
                        zi += i; zj += j; zk += k; dist++;
                        currPiece = boardReference.GetZone(zi, zj, zk).GetPiece();
                        if (!currPiece) break;
                        if (currPiece.Color == z.GetPiece().Color)
                        {
                            return dist > 1;
                        }
                    }
                }
        return false;
    }

    public void PutPiece(OthelloZoneBehaviour z, bool turn)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && j == 0 && k == 0) continue;
                    int zi = z.coord[0], zj = z.coord[1], zk = z.coord[2];
                    OthelloPieceBehaviour currPiece;
                    List<OthelloPieceBehaviour> toFlip = new List<OthelloPieceBehaviour>();
                    while (
                            zi > 0 && zi < SizeOption &&
                            zj > 0 && zj < SizeOption &&
                            zk > 0 && zk < SizeOption
                         )
                    {
                        zi += i; zj += j; zk += k;
                        currPiece = boardReference.GetZone(zi, zj, zk).GetPiece();
                        if (!currPiece) break;
                        if (currPiece.Color == z.GetPiece().Color)
                        {
                            foreach(OthelloPieceBehaviour p in toFlip) { p.Flip(); }
                            break;
                        }
                        toFlip.Add(currPiece);
                    }
                }
        //TODO: add tracker for number of black/white pieces on the board
        ChangeTurn();
    }
    
    void EndGame()
    {
        foreach(OthelloZoneBehaviour z in boardReference.zones)
        {
            z.RemoveEventsFromManager();
        }
        //TODO: add display of results to HUD
    }

}
