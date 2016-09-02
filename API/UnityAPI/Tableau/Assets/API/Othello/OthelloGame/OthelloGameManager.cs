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

    private int sizeOption = 4;
    public int SizeOption
    {
        get{ return sizeOption; }
        set{ sizeOption = value; }
    }

    public OthelloBoardBehaviour boardReference;

    private int whiteNum = 4, blackNum = 4;
    public int White
    {
        get { return whiteNum; }
        set
        {
            whiteNum = value;
            OthelloHUDManager.WhiteNum(value);
        }
    }
    public int Black
    {
        get { return blackNum; }
        set
        {
            blackNum = value;
            OthelloHUDManager.BlackNum(value);
        }
    }


    private bool turn = false; //false = black, true = white
    private bool playerTurn = false;
    public string PlayerTurnSelection
    {
        get { return playerTurn ? "white" : "black"; }
        set { turn = value.Equals("white"); }
    }
    public bool PlayerTurn
    {
        get { return playerTurn; }
    }

    public string GetCurrentTurnText
    {
        get { return turn? "white" : "black"; }
    }
    public bool GetCurrentTurn
    {
        get { return turn; }
    }

    void Start()
    {
        //sizeOption = SizeManager.gridSize;
        SizeManager sm = FindObjectOfType<SizeManager>();
        sizeOption = (!sm)?4:sm.gridSize;
        Debug.Log((!sm) ? "No SizeMgr found" : sm.gridSize.ToString());
        boardReference.ConstructBoard(sizeOption);
        playerTurn = Random.Range(0, 2) <= 1;
        InitializeGame();        
    }

    private HashSet<OthelloZoneBehaviour> possibleZones = new HashSet<OthelloZoneBehaviour>();

    private bool skipped = false;

    public void ChangeTurn()
    {
        turn = !turn;
        if (turn != playerTurn)
        {
            OthelloHUDManager.Prompt(GetCurrentTurnText + "AI's turn!");
            EventManager.instance.FreezeControl();
            AIPlay();
        }
        else
        {
            OthelloHUDManager.Prompt(GetCurrentTurnText + "Player's turn!");
            EventManager.instance.GrantControl();
        }
        foreach (OthelloZoneBehaviour z in possibleZones)
        {
            if (PutPieceCheck(z, turn)) return;
        }

        if (skipped)
        {
            Debug.Log("Ended?");
            EndGame();
        }
        else
        {
            skipped = true;
            OthelloHUDManager.Prompt("No valid move for " + GetCurrentTurnText + "player, turn skipped!");
            ChangeTurn();
        }
    }

    public bool ValidIndex(int i)
    {
        return (i >= 0) && (i < sizeOption);
    }

    private void InitializeGame()
    {
        White = 4;
        Black = 4;
        skipped = false;
        int m = SizeOption / 2;
        ForcePut(boardReference.GetZone(m - 1, m - 1, m - 1), false);
        ForcePut(boardReference.GetZone(m, m, m), true);
        ForcePut(boardReference.GetZone(m - 1, m , m), false);
        ForcePut(boardReference.GetZone(m, m - 1, m - 1), true);
        ForcePut(boardReference.GetZone(m, m, m - 1), false);
        ForcePut(boardReference.GetZone(m - 1, m - 1, m), true);
        ForcePut(boardReference.GetZone(m, m - 1, m), false);
        ForcePut(boardReference.GetZone(m - 1, m, m - 1), true);
        OthelloHUDManager.ClearDisplayOnStart();
        ExpandBoard();
        if (turn != playerTurn) { AIPlay(); }
    }

    private void EndGame()
    {
        boardReference.DestroyBoard();
        OthelloHUDManager.DisplayEndgame(White > Black);
    }

    public void ExpandBoard()
    {
        boardReference.ExpandBoard(4);
    }

    public void ContractBoard()
    {
        boardReference.ExpandBoard(0.25f);
    }

    //put piece ignoring rules other than the "no putting pieces in occupied zones" rule
    public bool ForcePut(OthelloZoneBehaviour z, bool turn)
    {
        if (!z.IsEmpty()) return false;
        OthelloPieceBehaviour newPiece = new GameObject().AddComponent<OthelloPieceBehaviour>();
        newPiece = newPiece.Put(z, turn);
        newPiece.gameObject.GetComponent<MeshRenderer>().enabled = true;
        foreach (OthelloZoneBehaviour a in boardReference.GetAdjacentZones(z))
        {
            if (a.IsEmpty()) possibleZones.Add(a);
        }
        possibleZones.Remove(z);
        return true;
    }

    public bool PutPieceCheck(OthelloZoneBehaviour z, bool turn)
    {
        if (!z.IsEmpty()) return false;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && j == 0 && k == 0) continue;
                    int zi = z.i, zj = z.j, zk = z.k;
                    int dist = 0;
                    OthelloPieceBehaviour currPiece;
                    while(
                            ValidIndex(zi + i) &&
                            ValidIndex(zj + j) &&
                            ValidIndex(zk + k)
                         )
                    {
                        zi += i; zj += j; zk += k; dist++;
                        currPiece = boardReference.GetZone(zi, zj, zk).GetPiece();
                        if (!currPiece) break;
                        if (currPiece.GetColor == turn && dist > 1)
                        {
                            return true;
                        }
                    }
                }
        return false;
    }

    public void PutPiece(OthelloZoneBehaviour z, bool turn)
    {
        OthelloPieceBehaviour newPiece = new GameObject().AddComponent<OthelloPieceBehaviour>();
        newPiece = newPiece.Put(z, turn);
        newPiece.gameObject.GetComponent<MeshRenderer>().enabled = true;
        if (turn) whiteNum += 1; else blackNum += 1;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && j == 0 && k == 0) continue;
                    int zi = z.i, zj = z.j, zk = z.k;
                    OthelloPieceBehaviour currPiece;
                    List<OthelloPieceBehaviour> toFlip = new List<OthelloPieceBehaviour>();
                    while (
                            ValidIndex(zi + i) &&
                            ValidIndex(zj + j) &&
                            ValidIndex(zk + k)
                         )
                    {
                        zi += i; zj += j; zk += k;
                        currPiece = boardReference.GetZone(zi, zj, zk).GetPiece();
                        if (!currPiece) break;
                        if (currPiece.GetColor == /*z.GetPiece().GetColor*/turn)
                        {
                            foreach (OthelloPieceBehaviour p in toFlip) { p.Flip(); }
                            break;
                        }
                        toFlip.Add(currPiece);
                    }
                }
        foreach (OthelloZoneBehaviour a in boardReference.GetAdjacentZones(z))
        {
            if (a.IsEmpty()) possibleZones.Add(a);
        }
        possibleZones.Remove(z);
        ChangeTurn();
    }

    private void AIPlay()
    {
        foreach(OthelloZoneBehaviour z in boardReference.GetAllZones())
        {
            if (possibleZones.Contains(z))
            {
                if (PutPieceCheck(z,turn))
                {
                    PutPiece(z, turn);
                    break;
                }
            }
        }
    }
    
   
    
}
