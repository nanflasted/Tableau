using UnityEngine;
using System;
using System.Collections;

namespace Tableau
{
	public class Piece : Character
	{
		public string type;
		public int ID;
		//TODO public List</*unityAnimationType*/> animations;
		//TODO public List</*unitySoundType*/> sounds;	
		public Player owner;
		public GameObject prefab;	
		public Zone location;
		
		public virtual void spawn()
		{
			//acquire coordinates
			//spawn prefab
			//play spawn animation
			//sound
		}
		
		public virtual void spawn(Vector3 pos, Quarternion rot)
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
