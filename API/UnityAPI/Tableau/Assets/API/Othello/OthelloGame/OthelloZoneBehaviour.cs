using UnityEngine;
using System.Collections;

using Tableau;

public class OthelloZoneBehaviour : PieceZone {

    public int[] coord = new int[3];

    public OthelloZoneBehaviour(int i, int j, int k)
    {
        coord[0] = i;
        coord[1] = j;
        coord[2] = k;
    }

}
