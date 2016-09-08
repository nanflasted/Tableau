using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tableau.Time
{
	public class Phase
	{
		public string name;
		public int ID;
		public Character owner;
		public Queue<Action> events;
			
		public Phase(string name, int ID)
		{
			this.name = name;
			events = new Queue<Action>();
		}	

		public Phase(string name, int ID, Character owner)
		{
			this.name = name;
			this.owner = owner;
			events = new Queue<Action>();
		}
			
		
		public virtual void addEvent(Action a)
		{
			events.Enqueue(a);
		}

		public virtual void executeEvent()
		{
			events.Dequeue().Invoke();
		}

		public virtual void executeAll()
		{
			while(events.Count > 0)
			{
				events.Dequeue().Invoke();
			}
		}
	}
}
