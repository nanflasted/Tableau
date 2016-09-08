using UnityEngine;
using System;
using System.Collections;

namespace Tableau
{
	public class Piece : Character
	{
		//removed due to attribute list addition: public string type;
		//TODO public List</*unityAnimationType*/> animations;
		//TODO public List</*unitySoundType*/> sounds;	
		//TODO decide whether to access this way OR just use Unity getComponent(): public Player owner;
		//TODO decide whether to access this way OR just use Unity getComponent(): public Zone location;
		public GameObject prefab;
        public Character owner;
        public Zone residence;

		public virtual GameObject Spawn()
		{
            return Spawn(Vector3.zero, Quaternion.identity);
		}
		
		public virtual GameObject Spawn(Vector3 pos, Quaternion rot)
		{
			return (GameObject)Instantiate(prefab,pos,rot);
			//play spawn animations
			//play sound
		}
				

		public virtual GameObject Spawn(Zone z)
		{
            AddResidence(z);
            return (GameObject)Instantiate(prefab, z.gameObject.transform.localPosition, Quaternion.identity);
			//play spawn animation
			//play sound
		}

		public virtual void Destruct()
		{
            Destroy(gameObject);
		}

		public virtual void AddResidence(Zone z)
		{
            residence = z;
		}

        public virtual void MoveResidence(Zone target, Motion effect)
        {
            residence = target;
            effect.Invoke();
        }

		public virtual void PlayAnimation(int n)
		{
		}

		public virtual void PlaySound(int n)
		{
		}
		
	}
}
