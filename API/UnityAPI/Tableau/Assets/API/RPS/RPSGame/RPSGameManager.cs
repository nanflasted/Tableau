using UnityEngine;
using System.Collections;

/*TODO:
    -Freeze and un-freeze player

 
     
*/

public class RPSGameManager : MonoBehaviour {
    //UI here or separate manager?

    public HUDManager headsUp;

    //Cards information

    /*
    * Rock = 0
    * Paper = 1
    * Scissor = 2
    */
    public int playerCard
    {
        get
        {
            return playerCard;
        }
        set
        {
            playerCard = value;
            StopCoroutine(PlayerSelect());
            StartCoroutine(PlayerSelect());
        }
    }

    private int AICard;

    //End Card Info


    //Player information
    //added to keep track of win info
    private int numWins
    {
        get
        {
            return numWins;
        }
        set
        {
            numWins += value;
        }
    }
    
    private bool wonRound
    {
        get
        {
            return wonRound;
        }
        set
        {
            wonRound = value;
        }
    }
    private bool tiedRound
    {
        get
        {
            return tiedRound;
        }
        set
        {
            tiedRound = value;
        }
    }
    //End Player Info


    //Singleton management
    private static RPSGameManager gameManagerInstance;

    public static RPSGameManager Instance
    {
        get
        {
            if (!gameManagerInstance)
            {
                gameManagerInstance = FindObjectOfType(typeof(RPSGameManager)) as RPSGameManager;
                if (!gameManagerInstance)
                {
                    Debug.LogError("No EventManager found in the scene");
                }
                else
                {
                    gameManagerInstance.Instantiate();
                }
            }
            return gameManagerInstance;
        }
    }

    //Initialize if needed, with this method
    void Instantiate()
    {
        
    }

    //End Singleton Management




    // process, in this method, what happens after the player selects a card
	IEnumerator PlayerSelect()
    {
        //TODO freeze player control
        Freeze();
        //TODO animations
        StartCoroutine(PostSelectAnimation());
        //TODO make method to generate the card for AI
        AISelect();        
        //TODO make method to judge
        JudgeResult();
        //TODO make method to display result
        DisplayResults();
        //TODO give selection to restart game or exit
        DisplayMenu();
        yield return null;
    }

    IEnumerator PostSelectAnimation()
    {
        yield return null;
    }

    private void Freeze()
    {
    }

    private void AISelect()
    {
        AICard = Random.Range(0, 2);
    }

    private void JudgeResult()
    {
        int winVal = 0, loseVal=0;
        switch (playerCard)
        {
            case 0:
                winVal = 2;
                loseVal = 1;
                break;
            case 1:
                winVal = 0;
                loseVal = 2;
                break;
            case 2:
                winVal = 1;
                loseVal = 0;
                break;
            default:
                break;
        }
        if (AICard == winVal)
        {
            wonRound = true;
            numWins += 1;
        }
        else if (AICard == loseVal)
        {
            wonRound = false;
            tiedRound = false;
        }
        else
        {
            wonRound = false;
            tiedRound = true;
        }
    }

    private void DisplayResults()
    {
        if (wonRound)
        {
            headsUp.win(numWins);
        } else if (tiedRound)
        {
            headsUp.tie();
        }
        else
        {
            headsUp.lose();
        }
    }

    private void DisplayMenu()
    {
        headsUp.disableHUD();
        //switch scene to original menu
        //unfreeze player
    }
    //End processing player card selection


}
