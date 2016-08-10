using UnityEngine;
using System.Collections;
using Tableau;
using System;
using System.Runtime.Serialization;

public class OthelloPieceBehaviour : Piece {


    public OthelloPieceBehaviour(Player owner)
    {
        this.owner = owner;
    }

    public void Put(OthelloZoneBehaviour z, bool turn)
    {
        if (!OthelloGameManager.Instance.PutPieceCheck(z,turn)) { return; }
        base.Spawn(z.transform.position, Quaternion.identity);
        z.AddPiece(this);
    }
}