using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Tableau;
using Tableau.Time;
using Tableau.Util;

public class CardBehaviour : Piece {

    //mod this script
    public AudioSource onSelectSound;

    void Start()
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
        PlayAnimation(1);
    }

    public void OnGazeExit()
    {
        PlayAnimation(2);
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

    IEnumerator CardAnimation1()
    {
        Material cardmat = gameObject.GetComponent<Renderer>().material;
        cardmat.color = Color.yellow;
        yield return null;
    }

    IEnumerator CardAnimation2()
    {
        Material cardmat = gameObject.GetComponent<Renderer>().material;
        cardmat.color = Color.white;
        yield return null;
    }
}
