using UnityEngine;
using System.Collections;

public class RPS_Player : MonoBehaviour {

    public string pName;
    public bool isAI = false;
    public RPS_Card rockCard, paperCard, scissorCard;
    private RPS_Card selectedCard;
    

    //losses and ties require different win handling
    private int num_wins, num_losses, num_ties;


    public void selectCard(RPS_Card sCard) //called from the card
    { //so only the former selection's deselect method needs to be called
        if(selectedCard!=null)
            selectedCard.deselect();
        selectedCard = sCard;
    }

    public RPS_Card getCard()
    {
        return selectedCard;
    }

    public void checkCam(Camera aCam)
    {
        Camera tempCam = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        if(tempCam != aCam)
        {
            tempCam.enabled = false;
        }
    }

	// Use this for initialization
	void Start () {
        if (isAI)
        {
            switch(Random.Range(0, 2))
            {
                case 0:
                    rockCard.select();
                    break;
                case 1:
                    paperCard.select();
                    break;
                default:
                    scissorCard.select();
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
