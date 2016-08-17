using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Tableau.Util;

public class EventManager : MonoBehaviour {

    // EventMap definition, and related methods
    public class EventMap : Dictionary<TableauEventTypes,MultiMap<int, UnityEvent>>
    {
        public EventMap() : base()
        {
            foreach(TableauEventTypes type in Enum.GetValues(typeof(TableauEventTypes)).Cast<TableauEventTypes>().ToList())
            {
                Add(type, new MultiMap<int, UnityEvent>());
            }
        }

        public bool TriggerAll(TableauEventTypes trigger, int key)
        {
            List<UnityEvent> toBeTriggered;
            if (this[trigger].TryGetValue(key, out toBeTriggered))
            {
                foreach(UnityEvent e in toBeTriggered)
                {
                    e.Invoke();
                }
                return true;

            }
            else
            {
                return false;
            }
        }
    }

    public void AddEvent(TableauEventTypes type, GameObject o, UnityEvent e)
    {
        eventMap[type].Add(o.GetInstanceID(), e);
    }

    public bool RemoveEvent(TableauEventTypes type, GameObject o, UnityEvent e)
    {
        return eventMap[type].Remove(o.GetInstanceID(), e);
    }

    public bool RemoveAllObjectTypeEvents(TableauEventTypes type, GameObject o)
    {
        return eventMap[type].RemoveAll(o.GetInstanceID());
    }

    /* public bool RemoveAllObjectEvents(GameObject o)
    {
        return eventMap[].RemoveAll(o.GetInstanceID());
    }*/

    // Declarations

    private static EventManager eventManager;
    private EventMap eventMap;
    private GestureRecognizer recognizer;
    private bool hasControl = false;
    private int lastGazedObject;
    private bool lastGazeHit = false;


    //Instance management: There should always be one and only one event manager on the scene


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

    void Start()
    {
        
    }

    void Instantiate()
    {
        //DontDestroyOnLoad(gameObject);
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += OnTap;
        eventMap = new EventMap();
        GrantControl();
    }

    public void GrantControl()
    {        
        recognizer.StartCapturingGestures();
        hasControl = true;
    }

    public void FreezeControl()
    {
        recognizer.StopCapturingGestures();
        hasControl = false;
    }

    // Tap event management, equivalent of PointClick

    private void OnTap(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(headRay, out hitInfo))
        {
            eventMap.TriggerAll(TableauEventTypes.Tap, hitInfo.collider.gameObject.GetInstanceID());
        }
    }

    // Gaze event management, equivalent of PointEnter/PointExit

    void Update()
    {
        if (hasControl)
        {
            RaycastHit gaze;

            // acquire gaze info, and if current gaze hit anything
            if (UnityHololensUtility.getGaze(Camera.main, out gaze))
            {
                int currentGazedObject = gaze.collider.gameObject.GetInstanceID();

                // if the gaze stayed on the same object, no hover events needs to be triggered again
                if ((currentGazedObject == lastGazedObject) && (lastGazeHit))
                {
                    return;
                }

                // otherwise trigger point exit events for last gazed object (if there is any), and substitute the gazed object info
                else
                {
                    if (lastGazeHit) eventMap.TriggerAll(TableauEventTypes.GazeExit, lastGazedObject);
                    lastGazedObject = currentGazedObject;
                    lastGazeHit = true;
                    eventMap.TriggerAll(TableauEventTypes.GazeEnter, lastGazedObject);
                }
            }

            // if current gaze doesn't hit anything
            else
            {
                if (lastGazeHit)
                {
                    eventMap.TriggerAll(TableauEventTypes.GazeExit, lastGazedObject);
                    lastGazeHit = false;
                }
            }
        }
    }


    void OnDestroy()
    {
        recognizer.StopCapturingGestures();
    }
}
