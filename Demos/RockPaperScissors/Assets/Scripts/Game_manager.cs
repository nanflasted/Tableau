using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.Collections;

public class Game_manager : MonoBehaviour {
    //private int numPlayers = 2;
    public RPS_Player player1, player2; 
    //public RPS_Player[] playerRA;   //possibility for more players 
    public Text timeText, winScreen;
    public Camera activeCam;

    private bool canRestart = false;

    
	// Use this for initialization
	void Start () {
        setActiveCam();
        timeText.text = "Ready?\t On \'GO!\'.";
        winScreen.enabled = false;
        canRestart = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (canRestart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                EditorSceneManager.LoadScene(0);
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
        else
        {
            switch ((int)Time.timeSinceLevelLoad)
            {
                case 2:
                    timeText.text = "Rock...";
                    break;
                case 3:
                    timeText.text = "Rock...Paper...";
                    break;
                case 4:
                    timeText.text = "Rock...Paper...Scissors...";
                    break;
                case 5:
                    timeText.text = "Rock...Paper...Scissors...GO!";
                    RPS_Player winP = find_winner();

                    string victorText;
                    if (winP == null)
                        victorText = "Tie\n\n";
                    else
                        victorText = winP.pName + " Wins!\n\n"; ;




                    //different for players>2
                    victorText += "Player 1: " + player1.getCard().value() + "\nPlayer 2: " + player2.getCard().value();

                    victorText += "\n\n\'Enter\' to replay\n\'Escape\' to quit";

                    //EditorUtility.DisplayDialog("Test!", victorText, "OK");

                    winScreen.text = victorText;
                    winScreen.enabled = true;

                    canRestart = true;

                    break;
                default:
                    break;
            }
        }
	}

    void setActiveCam()
    {
        //change for players>2
        player1.checkCam(activeCam);
        player2.checkCam(activeCam);
    }

    RPS_Player find_winner()
    {
        /*RPS_Card[] chosenCards = new RPS_Card[numPlayers];
        for(int i = 0; i < numPlayers; i++)
        {
            chosenCards[i] = players[i].getCard();
        } *///for >2 players

        //EditorUtility.DisplayDialog("Test!", "Player 1: " + player1.getCard().value() + "\nPlayer 2: " + player2.getCard().value(), "OK");

        RPS_Card c1 = player1.getCard();
        RPS_Card c2 = player2.getCard();
        if (c1 == null)
        {
            if (c2 == null)
                return null;
            return player2;
        }
        if (c2 == null)
            return player1;
        if (c1.value().Equals(c2.value()))
            return null;
        if (c1.beatsCard(c2))
            return player1;

        

        //switch (c1.beatsCard(c2))
        //{

        //}

        return player2;
    }
}
