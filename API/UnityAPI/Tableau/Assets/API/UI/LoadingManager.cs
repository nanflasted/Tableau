using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour {

    public RectTransform backgroundImage;
    public RectTransform title;
    public AudioSource backgroundMusic;
	// Use this for initialization
	void Start () {
	    /*if (backgroundImage!=null)
        {
            
            backgroundImage.sizeDelta = new Vector2(Screen.width, Screen.height);
       
        }*/
        if (backgroundMusic.clip!=null)
        {
            backgroundMusic.Play();
        }
	}
	
}
