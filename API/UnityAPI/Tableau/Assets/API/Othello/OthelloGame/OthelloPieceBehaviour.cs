using UnityEngine;
using System.Collections;
using Tableau;
using System;
using System.Runtime.Serialization;

public class OthelloPieceBehaviour : Piece {

    private bool color; //black = false, white = true;

    public bool Color
    {
        get
        {
            return color;
        }
    }

    private Player otherPlayer;

    public OthelloPieceBehaviour(Player owner, Player otherPlayer)
    {
        this.owner = owner;
        this.otherPlayer = otherPlayer;
    }

    public OthelloPieceBehaviour(bool color)
    {
        this.color = color;
    }

    public bool Put(OthelloZoneBehaviour z, bool turn)
    {
        if (!OthelloGameManager.Instance.PutPieceCheck(z,turn)) { /* TODO: add illegal motion visuals */return false; }
        base.Spawn(z.transform.position, Quaternion.identity);
        z.AddPiece(this);
        OthelloGameManager.Instance.PutPiece(z, turn);
        return true;
    }

    public void Flip()
    {
        color = !color;
        if (!owner || !otherPlayer) return;
        Player tmp = (Player)owner;
        owner = otherPlayer;
        otherPlayer = tmp;        
    }
}