using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*TODO:
    -Link replay and quit buttons to their respective behaviors
    -Set buttons events such that they respond to selection


*/
public class HUDManager : MonoBehaviour {

    public Canvas hud_canvas,hud_static_canvas;
    public Text mainDialog, winCounter;
    public GameObject replayButton, quitButton;
  
    void Awake()
    {
        disableHUD();
    }

    public void win(int numWins)
    {
        hud_canvas.enabled = true;
        hud_static_canvas.enabled = true;

        mainDialog.text = "Round Over\nCongrats!";
        winCounter.text = "Player Wins: " + numWins;

        mainDialog.enabled = true;
        winCounter.enabled = true;

        replayButton.GetComponent<BoxCollider>().enabled = true;
        quitButton.GetComponent<BoxCollider>().enabled = true;
    }

    public void lose()
    {
        hud_canvas.enabled = true;
        hud_static_canvas.enabled = true;
        mainDialog.text = "Round Over\nSo Close!";

        mainDialog.enabled = true;
        winCounter.enabled = false;

        replayButton.GetComponent<BoxCollider>().enabled = true;
        quitButton.GetComponent<BoxCollider>().enabled = true;
    }

    public void tie()
    {
        hud_canvas.enabled = true;
        hud_static_canvas.enabled = true;
        mainDialog.text = "Round Over\nTie!";

        mainDialog.enabled = true;
        winCounter.enabled = false;

        replayButton.GetComponent<BoxCollider>().enabled = true;
        quitButton.GetComponent<BoxCollider>().enabled = true;
    }

    public void disableHUD()
    {
        hud_canvas.enabled = false;
        hud_static_canvas.enabled = false;
        mainDialog.enabled = false;
        winCounter.enabled = false;

        replayButton.GetComponent<BoxCollider>().enabled = false;
        quitButton.GetComponent<BoxCollider>().enabled = false;
    }
    
    public void OnReplay()
    {
        disableHUD();
        foreach (CardBehaviour card in FindObjectsOfType<CardBehaviour>())
        {
            card.GrantCtrl();
        }
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("RPSGUI");
    }

    public void OnGazeEnter(Text t)
    {
        t.color = Color.white;
    }

    public void OnGazeExit(Text t)
    {
        t.color = Color.grey;
    }
}
