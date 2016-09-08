using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameCursor : MonoBehaviour {

    public GameObject cursorObject;

    private MeshRenderer mr;
    private Transform cur;
    private Material mat;

	// Use this for initialization
	void Start () {
        mr = this.gameObject.GetComponentInChildren<MeshRenderer>();
        cur = this.transform;
        mat = cursorObject.GetComponent<Renderer>().material;
       
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 headPos = Camera.main.transform.position;
        Vector3 headRot = Camera.main.transform.forward;


        // Cast a ray from current gaze position and direction onto UI
        // and record the hit info.
        RaycastHit hitInfo;
        
        if (Physics.Raycast(headPos,headRot,out hitInfo))
        {
            //Debug.Log("hit");

            // Calculate the position of the cursor based on hit info
            Vector3 postmp = hitInfo.point;
            //postmp.z -= cur.localScale.z / 2;
            
            // Place the cursor
            cur.position = postmp;
            cur.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            // Nicely change color of the cursor every frame
            float hue, sat, val;
            Color.RGBToHSV(mat.color, out hue, out sat, out val);
            hue = (hue + 0.005f > 1) ? hue + 0.005f - 1f : hue + 0.005f;
            mat.SetColor("_Color", Color.HSVToRGB(hue, sat, val));

            // Paint the cursor on the screen
            mr.enabled = true;

            
        }
        else
        {
            // If the gaze didn't hit the UI, don't show the cursor
            mr.enabled = false;
        }

	}
}
