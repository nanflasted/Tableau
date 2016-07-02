using UnityEngine;
using System.Collections;

namespace Tableau.Util
{
    public class UnityHololensUtility
    {

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
}