using UnityEngine;
using UnityEditor;
using System.Collections;

public class RPS_Card : MonoBehaviour {
    public string cardType; //rock, paper, scissor
    private bool isSelected;
    public RPS_Player ownPlayer;
    public Material selectedMat, deselectedMat;
    //private GameObject cardBody;

	// Use this for initialization
	void Start () {
        isSelected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {

        }
            //onMouseDown();
	}

    public void select()
    {
        Renderer draw = GetComponent<Renderer>();
        draw.material = selectedMat;
        isSelected = true;
        ownPlayer.selectCard(this);
    }
    public void deselect()
    {
        Renderer draw = GetComponent<Renderer>();
        draw.material = deselectedMat;
        isSelected = false;
    }

    void OnMouseDown()
    {
        if (!isSelected)
            select();
    }


    public bool beatsCard(RPS_Card other)      //API function to be extended per game
    {
        if(other == null)
        {
            return true;
        }

        string cVal = other.value();    //General API function 

        //bool retVal=false;

        if (cardType.Equals("rock"))
        {
            return cVal.Equals("scissors");
        }
        else if (cardType.Equals("scissors"))
        {
            return cVal.Equals("paper");
        }
        return cVal.Equals("rock");
    }

    public string value()
    {
        return cardType;
    }
}
