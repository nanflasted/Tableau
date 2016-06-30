using UnityEngine;
using System.Collections;

public class GameCursor : MonoBehaviour {
    private MeshRenderer mr;
    private Transform cur;
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

        if (Physics.Raycast(headPos,headRot,out hitInfo))
        {
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
