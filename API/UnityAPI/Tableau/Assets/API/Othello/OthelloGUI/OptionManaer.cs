using UnityEngine;
using System.Collections;

//TODO:  Change GUI to show whether options have been changed?
/*
 * Options:
 * Grid Size    4x4x4 | 6x6x6 | 8x8x8
 * Opponent     Player | AI
 * 
 */

public class OptionManaer : MonoBehaviour {

    public GameObject title_screen, option_screen;

    public GameObject[] title_buttons, option_buttons;

    private string def_options, current_options, temp_options;

    public SizeManager sm;
    
    public void OnOptionsEnter()
    {
        title_screen.SetActive(false);
        for (int i = 0; i < title_buttons.Length; i++)
        {
            title_buttons[i].SetActive(false);
        }

        option_screen.SetActive(true);
        for (int i = 0; i < option_buttons.Length; i++)
        {
            option_buttons[i].SetActive(true);
        }
    }

	public void OnOptionsSave()
    {
        current_options = temp_options;
        OnOptionsCancel();
    }

    public void OnOptionsDefault()
    {
        current_options = def_options;
        //TODO:     set GUI options to reflect update
    }

    public void OnOptionsCancel()
    {
        title_screen.SetActive(true);
        for(int i=0; i < title_buttons.Length; i++)
        {
            title_buttons[i].SetActive(true);
        }

        option_screen.SetActive(false);
        for (int i = 0; i < option_buttons.Length; i++)
        {
            option_buttons[i].SetActive(false);
        }
    }

    public void OnGridToggle()
    {
        //
        string temp = this.GetComponentInChildren<TextMesh>().text;
        sm.gridSize = temp[0] - '0';
    }

    public void OnOpponentToggle()
    {
        //get value of clicked object, change color of clicked button?

    }

}
