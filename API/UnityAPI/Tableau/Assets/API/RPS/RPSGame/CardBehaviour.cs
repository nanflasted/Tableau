using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Tableau;
using Tableau.Time;
using Tableau.Util;

public class CardBehaviour : Piece {

    //mod this script
    public AudioSource onSelectSound;

    void Awake()
    {
        GrantCtrl();
    }

    public void GrantCtrl()
    {
        UnityEvent tapEvent = new UnityEvent();
        tapEvent.AddListener(OnTap);
        UnityEvent gazeEnterEvent = new UnityEvent();
        gazeEnterEvent.AddListener(OnGazeEnter);
        UnityEvent gazeExitEvent = new UnityEvent();
        gazeEnterEvent.AddListener(OnGazeExit);
        EventManager.instance.AddEvent(TableauEventTypes.Tap, prefab, tapEvent);
        EventManager.instance.AddEvent(TableauEventTypes.GazeEnter, prefab, gazeEnterEvent);
        EventManager.instance.AddEvent(TableauEventTypes.GazeExit, prefab, gazeExitEvent);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        foreach (MeshRenderer m in gameObject.GetComponentsInChildren<MeshRenderer>()) m.enabled = true;
    }

    public void FreezeCtrl()
    {
        EventManager.instance.RemoveAllObjectTypeEvents(TableauEventTypes.Tap, prefab);
        EventManager.instance.RemoveAllObjectTypeEvents(TableauEventTypes.GazeEnter, prefab);
        EventManager.instance.RemoveAllObjectTypeEvents(TableauEventTypes.GazeExit, prefab);
    }

    public void OnTap()
    {
        PlayAnimation(0);
        PlaySound(0);
        RPSGameManager.Instance.playerCard = ID;
    }

    public void OnGazeEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.grey;
    }

    public void OnGazeExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void PlayAnimation(int n)
    {
        StartCoroutine("CardAnimation"+n.ToString());
    }

    public override void PlaySound(int n)
    {
        onSelectSound.Play();
    }

    //mod this method to make an animation for cards when it gets selected
    IEnumerator CardAnimation0()
    {
        Material cardmat = gameObject.GetComponent<Renderer>().material;
        for (int i = 0; i < 3; i++)
        {
            cardmat.color = Color.blue;
            yield return new WaitForSeconds(0.1f);
            cardmat.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
