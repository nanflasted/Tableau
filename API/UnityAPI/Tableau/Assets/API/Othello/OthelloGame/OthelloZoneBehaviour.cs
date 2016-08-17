using UnityEngine;
using UnityEngine.Events;
using System.Collections;

using Tableau;

public class OthelloZoneBehaviour : PieceZone {


    private OthelloPieceBehaviour piece;
    private UnityEvent onTapEvent,gazeEnterEvent,gazeExitEvent;

    public int[] coord = new int[3];

    public OthelloZoneBehaviour(int i, int j, int k)
    {
        coord[0] = i;
        coord[1] = j;
        coord[2] = k;
    }

    public new OthelloPieceBehaviour GetPiece()
    {
        return piece;
    }

    public void AddEventsToManager()
    {
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

    private void OnTap()
    {

    }

    private void GazeEnter()
    {

    }

    private void GazeExit()
    {

    }
}
