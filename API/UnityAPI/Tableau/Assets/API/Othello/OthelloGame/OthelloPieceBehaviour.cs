using UnityEngine;
using System.Collections;
using Tableau;
using System;
using System.Runtime.Serialization;

public class OthelloPieceBehaviour : Piece {

    private bool color; //black = false, white = true;

    public bool GetColor
    {
        get
        {
            return color;
        }        
        set
        {
            color = value;
        }
    }

    //obsolete
    private Player otherPlayer;
    //obsolete
    public OthelloPieceBehaviour(Player owner, Player otherPlayer)
    {
        this.owner = owner;
        this.otherPlayer = otherPlayer;
    }
    //obsolete
    public OthelloPieceBehaviour(bool color)
    {
        this.color = color;
        prefab = GameObject.Find("OthelloPiecePrefab");
        gameObject.GetComponent<Renderer>().material.color = color ? Color.white : new Color(0.5f, 0, 0.5f, 1);
    }

    public OthelloPieceBehaviour Put(OthelloZoneBehaviour z, bool turn)
    {
        prefab = GameObject.Find("OthelloPiecePrefab");
        color = turn;
        OthelloPieceBehaviour p = base.Spawn(z.transform.position, Quaternion.identity).GetComponent<OthelloPieceBehaviour>();
        p.color = turn;
        p.gameObject.transform.parent = z.transform;
        p.gameObject.GetComponent<Renderer>().material.color = color ? Color.white : new Color(0.5f, 0, 0.5f, 1);
        z.AddPiece(p);
        return p;
    }

    public void Flip()
    {
        color = !color;
        gameObject.GetComponent<Renderer>().material.color = color ? Color.white : new Color(0.5f, 0, 0.5f, 1);
        OthelloGameManager.Instance.White += color ? 1 : -1;
        OthelloGameManager.Instance.Black -= color ? 1 : -1;
        /*
        if (!owner || !otherPlayer) return;
        Player tmp = (Player)owner;
        owner = otherPlayer;
        otherPlayer = tmp;        
        */
    }
}