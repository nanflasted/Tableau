using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    //@deprecated
    //Unity doesn't implement scene loading with Application.LoadLevel anymore
    //const int gameSceneNumber = 2;

	public void onStartGame()
    {
        Debug.Log("Start pressed");
        SceneManager.UnloadScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Game");
    }

    public void onOption()
    {
        Debug.Log("Option pressed");
    }

    public void onExit()
    {
        Debug.Log("Exit pressed");
        Application.Quit();
    }
}
