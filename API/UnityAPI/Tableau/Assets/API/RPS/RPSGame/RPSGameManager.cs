using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private int pCard;
    public int playerCard
    {
        get
        {
            return pCard;
        }
        set
        {
            pCard = value;
            StopAllCoroutines();
            StartCoroutine(PlayerSelect());
        }
    }

    private int AICard;

    //End Card Info


    //Player information
    //added to keep track of win info
    private int numWins;
    private bool wonRound;
    private bool tiedRound;
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
        
        Freeze();
        //TODO animations
        yield return StartCoroutine(PostSelectAnimation());
        //TODO make method to generate the card for AI
        AISelect();
        //TODO make method to judge
        JudgeResult();
        //TODO make method to display result
        DisplayResults();
        yield return null;
    }

    IEnumerator PostSelectAnimation()
    {
        yield return new WaitForSeconds(1);
        foreach (CardBehaviour c in FindObjectsOfType<CardBehaviour>())
        {
            c.gameObject.GetComponent<MeshRenderer>().enabled = false;
            foreach (MeshRenderer m in c.gameObject.GetComponentsInChildren<MeshRenderer>()) m.enabled = false;
        }
    }

    private void Freeze()
    {
        foreach(CardBehaviour c in FindObjectsOfType<CardBehaviour>())
        {
            c.FreezeCtrl();
        }
    }

    private void AISelect()
    {
        AICard = Random.Range(0, 3);
    }

    private void JudgeResult()
    {
        int winVal = 0, loseVal = 0;
        switch (pCard)
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

    //End processing player card selection


}
