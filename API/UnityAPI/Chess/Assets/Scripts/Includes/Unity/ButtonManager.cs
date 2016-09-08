using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void OnStartGame()
    {
        Debug.Log("Start pressed");
        SceneManager.UnloadScene(SceneManager.GetActiveScene());
        //SceneManager.LoadScene("Game");
        //For testing purposes
        SceneManager.LoadScene("Test");
    }

    public void OnOption()
    {
        Debug.Log("Option pressed");
    }

    public void OnExit()
    {
        Debug.Log("Exit pressed");
        Application.Quit();
    }

    public void OnGazeEnter(GameObject o)
    {
        Debug.Log("Gaze Entered " + o.name);
        o.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    public void OnGazeExit(GameObject o)
    {
        Debug.Log("Gaze Exited " + o.name);
        o.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    /*
     * not working. There might be a way to do this without having to add a reference in the Editor
     * but will have to figure it out
    public void OnGazeEnter()
    {
        Debug.Log(GetComponentInParent<GameObject>().name);
        Debug.Log(gameObject.name);
    }
    */
}
