using System;
using System.Collections;
using UnityEngine;

namespace Tableau {
	public /*abstract*/ class Motion {
		private Zone[] initiators, targets;

		// Constructors (all private, called by factories)
		private Motion(Zone initiator, Zone target) {
			this.initiators = ToSingletonArray(initiator);
			this.targets = ToSingletonArray(target);
		}

		private Motion(Zone[] initiators, Zone[] targets) {
			this.initiators = initiators;
			this.targets = targets;
		}

		private Motion(Zone initiator, Zone[] targets) {
			this.initiators = ToSingletonArray(initiator);
			this.targets = targets;
		}

		private Motion(Zone[] initiators, Zone target) {
			this.initiators = initiators;
			this.targets = ToSingletonArray(target);
		}

		// Factories
		public static Motion Make(Zone initiator, Zone target) {
			// Check input
			if (initiator == null || target == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			
			// If input is good...
			return new Motion(initiator, target);
		}

		public static Motion Make(Zone[] initiators, Zone[] targets) {
			// Check input
			if (initiators == null || targets == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			if (GetNumOccupants(initiators) == 0 || GetNumOccupants(targets) == 0)
				throw new MotionInitException("Attempted to make motion with empty set of initiators/targets");
			
			// If input is good...
			return new Motion(initiators, targets);
		}

		public static Motion Make(Zone initiator, Zone[] targets) {
			// Check input
			if (initiator == null || targets == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			if (GetNumOccupants(targets) == 0)
				throw new MotionInitException("Attempted to make motion with empty set of targets");
			
			// If input is good...
			return new Motion(initiator, targets);
		}

		public static Motion Make(Zone[] initiators, Zone target) {
			// Check input
			if (initiators == null || target == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			if (GetNumOccupants(initiators) == 0)
				throw new MotionInitException("Attempted to make motion with empty set of initiators");
			
			// If input is good...
			return new Motion(initiators, target);
		}

		// Getters/Setters
		public Zone[] GetInitiators() { return initiators; }
		public Zone[] GetTargets() { return targets; }

		protected internal void SetInitiators(Zone initiator) {
			// Check input
			if (initiator == null)
				throw new MotionChangeException("Tried to set initiator to null zone");
			
			// If input is good...
			initiators = ToSingletonArray(initiator);
		}

		protected internal void SetInitiators(Zone[] initiators) {
			// Check input
			if (initiators == null)
				throw new MotionChangeException("Tried to set initiator to null zones");
			if (GetNumOccupants(initiators) == 0)
				throw new MotionChangeException("Tried to set initiator to empty set of zones");
			
			// If input is good...
			this.initiators = initiators;
		}

		protected internal void SetTargets(Zone target) {
			// Check input
			if (target == null)
				throw new MotionChangeException("Tried to set target to null zone");
			
			// If input is good...
			targets = ToSingletonArray(target);
		}

		protected internal void SetTargets(Zone[] targets) {
			// Check input
			if (targets == null)
				throw new MotionChangeException("Tried to set target to null zones");
			if (GetNumOccupants(targets) == 0)
				throw new MotionChangeException("Tried to set target to empty set of zones");

			// If input is good...
			this.targets = targets;
		}

        // Override pls
        public virtual void Invoke() { }

		// Methods used for input validation
		private static int GetNumOccupants(Zone[] zarr) {
			int num = 0, index = 0;
			while (index < zarr.Length) {
				if (zarr[index] != null)
					num++;
			}
			return num;
		}

		private static Zone[] ToSingletonArray(Zone z) {
			Zone[] arr = new Zone[1];
			arr[0] = z;
			return arr;
		}
	}
	
	public class MotionInitException : Exception {
		public MotionInitException(string s) : base(s) {
			
		}
	}

	public class MotionChangeException : Exception {
		public MotionChangeException(string s) : base(s) {
			
		}
	}
}