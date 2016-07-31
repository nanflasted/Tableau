using UnityEngine;
using System.Collections;

public class RPSGameManager : MonoBehaviour {
   
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
    }

    private void JudgeResult()
    {
    }

    private void DisplayResults()
    {
    }

    private void DisplayMenu()
    {
    }
    //End processing player card selection


}
