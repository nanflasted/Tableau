using UnityEngine;
using System.Collections;
using Tableau;
using System;
using System.Runtime.Serialization;

public class OthelloPieceBehaviour : Piece {

    public void Put(OthelloZoneBehaviour z,Tableau.Time.Turn turn)
    {
        if (!OthelloGameManager.PutPieceCheck(z,turn)) { return; }
        base.Spawn(z.transform.position, Quaternion.identity);
        z.AddPiece(this);
    }
}