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
		
		public virtual void spawn()
		{
			//acquire coordinates
			//spawn prefab
			//play spawn animation
			//sound
		}
		
		public virtual void spawn(Vector3 pos, Quaternion rot)
		{
			Instantiate(prefab,pos,rot);
			//play spawn animations
			//play sound
		}
				

		public virtual void spawn(Zone z)
		{
			//acquire coordinates from z
			//spawn prefab
			//play spawn animation
			//play sound
		}

		public virtual void destruct()
		{
		}

		public virtual void moveToZone(Zone z)
		{
			//visual effect triggered by a Motion or Motions
		}

		public virtual void playAnimation(int n)
		{
		}

		public virtual void playSound(int n)
		{
		}

		public virtual void triggerMotion(Motion m)
		{
		}

		
	}
}
