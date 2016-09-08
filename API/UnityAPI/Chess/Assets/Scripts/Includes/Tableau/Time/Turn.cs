using UnityEngine;
using System;
using System.Collections.Generic;

namespace Tableau.Time
{
	public class Turn
	{
		public string name = "";
		public int ID = 0;
		public Character owner;
		public Queue<Action> events;
		public List<Phase> phases;
		private int current = 0;

        public Turn()
        {

        }

		public Turn(string name, int ID)
		{
			this.name = name;
			this.ID = ID;
			events = new Queue<Action>();
			phases = new List<Phase>();
		}
			
		public Turn(string name, int ID, Character owner)
		{
			this.name = name;
			this.ID = ID;
			this.owner = owner;
			events = new Queue<Action>();
			phases = new List<Phase>();
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
			while (events.Count > 0)
			{
				events.Dequeue().Invoke();
			}
		}						
			
		public virtual int currentPhase()
		{
			return current;
		}

		public virtual int nextPhase()
		{
			if (current+1>=phases.Count) return -1;
			current++;
			return 0;
		}
	}
}
