using UnityEngine;
using System.Collections;

public class SizeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}

    public int gridSize=4;

    
}
