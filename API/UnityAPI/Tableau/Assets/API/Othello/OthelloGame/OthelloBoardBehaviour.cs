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

    new OthelloZoneBehaviour[,,] zones;

    public OthelloZoneBehaviour GetZone(int i, int j, int k)
    {
        return zones[i, j, k];
    }

    private void ConstructBoard(int size, float zoneScale = 0.1f)
    {
        this.size = size;
        zones = new OthelloZoneBehaviour[size,size,size];
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k =0; k < size; k++)
                {
                    zones[i, j, k] = new OthelloZoneBehaviour(i,j,k);
                }
        this.zoneScale = zoneScale;
    }

    private Vector3 CalculatePosition(int i, int j, int k)
    {
        float x = (i - (size - 1) / 2f) * zoneScale;
        float y = (j - (size - 1) / 2f) * zoneScale;
        float z = (k - (size - 1) / 2f) * zoneScale;
        return new Vector3(x, y, z);
    }

    private void DrawZones()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                {
                    Instantiate(zones[i,j,k].prefab, CalculatePosition(i,j,k), Quaternion.identity);
                }
    }


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
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                {
                    StartCoroutine(MoveZone(i, j, k, factor));
                }
    }

    void Start()
    {
        ConstructBoard(OthelloGameManager.Instance.SizeOption);
        foreach (OthelloZoneBehaviour z in zones)
        {
            z.AddEventsToManager();
        }
    }


}
