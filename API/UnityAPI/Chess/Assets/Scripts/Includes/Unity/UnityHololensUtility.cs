using UnityEngine;
using System.Collections;

namespace Tableau.Util
{
    public class UnityHololensUtility
    {

        public static bool getGaze(Camera head, out RaycastHit gaze)
        {
            return (Physics.Raycast(head.transform.position, head.transform.forward, out gaze));
        }


        public static bool isGazed(RaycastHit hitInfo, GameObject target)
        {
            return hitInfo.collider.gameObject.Equals(target);
        }

        public static bool isGazed(Camera head, GameObject target)
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(head.transform.position, head.transform.forward, out hitInfo))
            {
                return hitInfo.collider.gameObject.Equals(target);
            }

            return false;
        }

        
    }
    public enum TableauEventTypes
    {
        Tap, Hold, GazeEnter, GazeExit, Drag
    }
}