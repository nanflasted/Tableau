using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*TODO:
    -Link replay and quit buttons to their respective behaviors
    -Set buttons events such that they respond to selection


*/
public class HUDManager : MonoBehaviour {

    public Canvas hud_canvas;
    public Text mainDialog, winCounter;
    public GameObject replayButton, quitButton;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

  

    public void win(int numWins)
    {
        hud_canvas.enabled = true;

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

        mainDialog.text = "Round Over\nSo Close!";

        mainDialog.enabled = true;
        winCounter.enabled = true;

        replayButton.GetComponent<BoxCollider>().enabled = true;
        quitButton.GetComponent<BoxCollider>().enabled = true;
    }

    public void tie()
    {
        hud_canvas.enabled = true;

        mainDialog.text = "Round Over\nTie!";

        mainDialog.enabled = true;
        winCounter.enabled = true;

        replayButton.GetComponent<BoxCollider>().enabled = true;
        quitButton.GetComponent<BoxCollider>().enabled = true;
    }

    public void disableHUD()
    {
        hud_canvas.enabled = false;
        mainDialog.enabled = false;
        winCounter.enabled = false;

        replayButton.GetComponent<BoxCollider>().enabled = false;
        quitButton.GetComponent<BoxCollider>().enabled = false;
    }
    
}
