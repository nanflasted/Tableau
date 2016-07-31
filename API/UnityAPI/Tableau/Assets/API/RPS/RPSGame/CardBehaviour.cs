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
        UnityEvent tapEvent = new UnityEvent();
        tapEvent.AddListener(OnTap);
        EventManager.instance.AddEvent(TableauEventTypes.Tap, prefab, tapEvent);   
    }

    public void OnTap()
    {
        PlayAnimation(0);
        PlaySound(0);
        RPSGameManager.Instance.playerCard = ID;
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
        yield return null;
    }
}
