using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Tableau;
using Tableau.Util;

public class OthelloBoardBehaviour : Board {

    private int size;
    public int Size
    {
        get { return size; }
        //set { size = value; }
    }

    private float zoneScale;

    private new OthelloZoneBehaviour[,,] zones;

    public OthelloZoneBehaviour GetZone(int i, int j, int k)
    {
        return zones[i, j, k];
    }

    public List<OthelloZoneBehaviour> GetAllZones()
    {
        List<OthelloZoneBehaviour> res = new List<OthelloZoneBehaviour>();
        foreach (OthelloZoneBehaviour z in zones) res.Add(z);
        return res;
    }

    public void ConstructBoard(int size, float zoneScale = 0.15f)
    {
        this.size = size;
        zones = new OthelloZoneBehaviour[size,size,size];
        this.zoneScale = zoneScale;
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k =0; k < size; k++)
                {
                    GameObject z = (GameObject)Instantiate(GameObject.Find("OthelloZonePrefab"), CalculatePosition(i, j, k), Quaternion.identity);
                    z.transform.parent = GameObject.Find("Origin").transform;
                    zones[i, j, k] = z.GetComponent<OthelloZoneBehaviour>();
                    zones[i, j, k].Initialize(i, j, k);
                }
        GrantBoardControl();
    }

    public void DestroyBoard()
    {
        FreezeBoardControl();
        foreach (OthelloZoneBehaviour z in zones)
        {
            if (!z.IsEmpty()) Destroy(z.GetPiece());
            Destroy(z);
        }
    }

    private Vector3 CalculatePosition(int i, int j, int k)
    {
        float x = (i - (size - 1) / 2f) * zoneScale;
        float y = (j - (size - 1) / 2f) * zoneScale;
        float z = (k - (size - 1) / 2f) * zoneScale;
        return new Vector3(x, y, z);
    }

    /*
    private void DrawZones()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                {
                    
                }
    }
    */

    IEnumerator MoveZone(int i, int j, int k, float factor)
    {
        Transform zTr = zones[i, j, k].prefab.transform;
        Vector3 target = new Vector3(zTr.position.x,zTr.position.y,zTr.position.z);
        target.Scale(new Vector3(factor, factor, factor));
        while(Vector3.Distance(zTr.position,target)>0.005f)
        {
            zTr.position = Vector3.Lerp(zTr.position, target, 7f * Time.deltaTime);
            yield return null;
        }
    }

    public void ExpandBoard(float factor)
    {
        FreezeBoardControl();
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                {
                    StartCoroutine(MoveZone(i, j, k, factor));
                }
        GrantBoardControl();
    }

    public List<OthelloZoneBehaviour> GetAdjacentZones(OthelloZoneBehaviour z)
    {
        List<OthelloZoneBehaviour> res = new List<OthelloZoneBehaviour>();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && j == 0 && k == 0) continue;
                    if (OthelloGameManager.Instance.ValidIndex(z.i + i)&&
                        OthelloGameManager.Instance.ValidIndex(z.j + j)&&
                        OthelloGameManager.Instance.ValidIndex(z.k + k))
                    {
                        res.Add(zones[z.i+i,z.j+j,z.k+k]);
                    }
                }
        return res;
    }

    public void GrantBoardControl()
    {
        foreach (OthelloZoneBehaviour z in zones)
        {
            z.AddEventsToManager();
        }
    }

    public void FreezeBoardControl()
    {
        foreach (OthelloZoneBehaviour z in zones)
        {
            z.RemoveEventsFromManager();
        }
    }

}
