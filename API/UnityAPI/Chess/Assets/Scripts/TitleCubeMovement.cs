using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleCubeMovement : MonoBehaviour {

    public float speed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //rotating the cube in different directions
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.Rotate(Vector3.right, speed * Time.deltaTime);

        //enter the game screen
        if (Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene(1);
        }
        //exit the game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
