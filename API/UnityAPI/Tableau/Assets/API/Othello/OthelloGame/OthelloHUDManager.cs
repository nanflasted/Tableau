using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OthelloHUDManager : MonoBehaviour {

	public static void DisplayEndgame(bool victor)
    {
        Prompt("Game Over!");
        GameObject.Find("EndgameDisplay").SetActive(true);
        GameObject.Find("EndgameDisplay").GetComponent<Text>().text = (victor ? "White" : "Black") + " victory!";
        GameObject.Find("ReplayButton").SetActive(true);
        GameObject.Find("QuitButton").SetActive(true);
    }

    public static void WhiteNum(int num)
    {
        GameObject.Find("WhiteCount").GetComponent<Text>().text = "White Pieces: "+num.ToString();
    }

    public static void BlackNum(int num)
    {
        GameObject.Find("BlackCount").GetComponent<Text>().text = "Black Pieces: " + num.ToString();
    }

    public static void ClearDisplayOnStart()
    {
        WhiteNum(4);
        BlackNum(4);
        GameObject.Find("ExpandContractButton").SetActive(true);
        GameObject.Find("EndgameDisplay").SetActive(false);
        GameObject.Find("ReplayButton").SetActive(false);
        GameObject.Find("QuitButton").SetActive(false);
        Prompt("Game Started!");
    }

    public static void Prompt(string ptext)
    {
        GameObject.Find("Prompt").GetComponent<Text>().text = ptext;
    }

    //TODO: make the texts
    public static void OnExpand()
    {
        GameObject.Find("ExpandButtonText").SetActive(false);
        GameObject.Find("ContractButtonText").SetActive(true);
    }

    public static void OnContract()
    {
        GameObject.Find("ExpandButtonText").SetActive(true);
        GameObject.Find("ContractButtonText").SetActive(false);
    }

    
}
