using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour {

    public Transform backgroundImage;
    public AudioSource backgroundMusic;
	// Use this for initialization
	void Start () {
        if (backgroundMusic.clip!=null)
        {
            backgroundMusic.Play();
        }
	}
	
}
