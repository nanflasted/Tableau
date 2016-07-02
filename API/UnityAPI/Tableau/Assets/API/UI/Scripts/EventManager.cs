using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Tableau.Util;

public class EventManager : MonoBehaviour {

    private static EventManager eventManager;

    private MultiMap<int, UnityEvent> tapDict;
    private GestureRecognizer recognizer;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("No EventManager found in the scene");
                }
                else
                {
                    eventManager.Instantiate();
                }
            }
            return eventManager;
        }
    }

    void Instantiate()
    {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += OnTap;
        tapDict = new MultiMap<int, UnityEvent>();
        recognizer.StartCapturingGestures();
    }

    public void AddTapEvent(GameObject o, UnityEvent e)
    {
        tapDict.Add(o.GetHashCode(), e);
    }

    public bool RemoveTapEvent(GameObject o, UnityEvent e)
    {
        return tapDict.Remove(o.GetHashCode(), e);
    }

    public bool RemoveAllTapEvents(GameObject o)
    {
        return tapDict.RemoveAll(o.GetHashCode());
    }

    private void OnTap(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(headRay, out hitInfo))
        { 
            int objectHit = hitInfo.collider.gameObject.GetHashCode();
            List<UnityEvent> objectOnTapEvents;
            if (tapDict.TryGetValue(objectHit,out objectOnTapEvents))
            {
                foreach(UnityEvent e in objectOnTapEvents)
                {
                    e.Invoke();
                }
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void OnDestroy()
    {
        recognizer.StopCapturingGestures();
    }
}
