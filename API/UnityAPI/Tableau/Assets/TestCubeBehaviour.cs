using UnityEngine;
using System.Collections;

public class TestCubeBehaviour : MonoBehaviour {
    int i = 0;
    public GameObject cube;
	// Use this for initialization
	void Start () {
        Debug.Log(cube.name);
        Debug.Log(cube.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        i++;
        float newX = i/100f;
        cube.transform.position = new Vector3(newX,cube.transform.position.y, cube.transform.position.z);
        Debug.Log(cube.transform.position);
        Debug.Log(newX);
	}
}
