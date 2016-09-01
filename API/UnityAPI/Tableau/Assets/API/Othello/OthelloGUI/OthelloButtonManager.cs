using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OthelloButtonManager : MonoBehaviour {

	public void OnStartGame()
    {
        
        SceneManager.LoadScene("RPSGame");
    }

    public void OnOptionsMenu()
    {
        //Load Option Scene
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnGazeEnter(GameObject o)
    {
        o.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    public void OnGazeExit(GameObject o)
    {
        o.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    public void OnGazeExitChosen(GameObject o)
    {
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
