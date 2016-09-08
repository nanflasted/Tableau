using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LeaveGameScript : MonoBehaviour
{ 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //exit the game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}