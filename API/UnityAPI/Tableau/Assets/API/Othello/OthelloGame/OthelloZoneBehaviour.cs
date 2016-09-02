using UnityEngine;
using UnityEngine.Events;
using System.Collections;

using Tableau;

public class OthelloZoneBehaviour : PieceZone {


    private OthelloPieceBehaviour piece;
    public UnityEvent onTapEvent,gazeEnterEvent,gazeExitEvent;


    public int i, j, k;

    public void Initialize(int i, int j, int k)
    {
        this.i = i;
        this.j = j;
        this.k = k;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void AddPiece(OthelloPieceBehaviour p)
    {
        piece = p;
    }
    public override bool IsEmpty()
    {
        return (piece == null);
    }
    public new OthelloPieceBehaviour GetPiece()
    {
        return piece;
    }

    public void setObjRef(Object o)
    {
        prefab = (GameObject)o;
    }

    public void AddEventsToManager()
    {
        onTapEvent = new UnityEvent();
        gazeEnterEvent = new UnityEvent();
        gazeExitEvent = new UnityEvent();
        onTapEvent.AddListener(OnTap);
        gazeEnterEvent.AddListener(GazeEnter);
        gazeExitEvent.AddListener(GazeExit);
        EventManager.instance.AddEvent(Tableau.Util.TableauEventTypes.Tap, gameObject, onTapEvent);
        EventManager.instance.AddEvent(Tableau.Util.TableauEventTypes.GazeEnter, gameObject, gazeEnterEvent);
        EventManager.instance.AddEvent(Tableau.Util.TableauEventTypes.GazeExit, gameObject, gazeExitEvent);
    }

    public void RemoveEventsFromManager()
    {
        EventManager.instance.RemoveEvent(Tableau.Util.TableauEventTypes.Tap, gameObject, onTapEvent);
        EventManager.instance.RemoveEvent(Tableau.Util.TableauEventTypes.GazeEnter, gameObject, gazeEnterEvent);
        EventManager.instance.RemoveEvent(Tableau.Util.TableauEventTypes.GazeExit, gameObject, gazeExitEvent);
    }

    public void OnTap()
    {
        if (!OthelloGameManager.Instance.PutPieceCheck(this, OthelloGameManager.Instance.GetCurrentTurn)) { Debug.Log("illegal");OthelloHUDManager.Prompt("Illegal Move!"); return; }
        OthelloGameManager.Instance.PutPiece(this, OthelloGameManager.Instance.GetCurrentTurn);
    }

    public void GazeEnter()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = Color.yellow;
    }

    public void GazeExit()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = Color.grey;
    }
}
