using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.VR.WSA.Input;
using Tableau.Util;
using System;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {

    public UnityEvent OnTap, OnGazeEnter, OnGazeExit;
    
    void OnEnable()
    {
        if (OnTap != null)
        {
            EventManager.instance.AddEvent(TableauEventTypes.Tap, gameObject, OnTap);
        }
        if (OnGazeEnter != null)
        {
            EventManager.instance.AddEvent(TableauEventTypes.GazeEnter, gameObject, OnGazeEnter);
        }
        if (OnGazeExit != null)
        {
            EventManager.instance.AddEvent(TableauEventTypes.GazeExit, gameObject, OnGazeExit);
        }
    }
}
