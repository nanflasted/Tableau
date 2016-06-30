using UnityEngine;
using System;
using System.Collections;

public class GameCursor : MonoBehaviour {
    private MeshRenderer mr;
    private Transform cur;
    // used for debugging purposes
    private int counter = 0;

	// Use this for initialization
	void Start () {
        mr = this.gameObject.GetComponentInChildren<MeshRenderer>();
        cur = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 headPos = Camera.main.transform.position;
        Vector3 headRot = Camera.main.transform.forward;
        RaycastHit hitInfo;
        /*
        counter++;
        counter %= 60;
        if (counter == 0)
        {
            Debug.Log(headPos);
            Debug.Log(headRot);
        }*/
        if (Physics.Raycast(headPos,headRot,out hitInfo))
        {
            Debug.Log("hit");
            mr.enabled = true;
            cur.position = hitInfo.point;
            cur.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            mr.enabled = false;
        }

	}
}
