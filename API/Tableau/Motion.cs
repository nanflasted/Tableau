using System;
using System.Collections;
using UnityEngine;

namespace Tableau {
	public abstract class Motion {
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

		private Motion(Zone[] initiator, Zone target) {
			this.initiators = initiators;
			this.targets = ToSingletonArray(target);
		}

		// Factories
		public static Motion Make(Zone initiator, Zone target) {
			if (initiator == null || target == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			return new Motion(initiator, target);
		}

		public static Motion Make(Zone[] initiators, Zone[] targets) {
			if (initiators == null || targets == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			if (getNumOccupants(initiators) == 0 || getNumOccupants(targets) == 0)
				throw new MotionInitException("Attempted to make motion with empty set of initiators/targets");
			return new Motion(initiators, targets);
		}

		public static Motion Make(Zone initiator, Zone[] targets) {
			if (initiator == null || targets == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			if (getNumOccupants(targets) == 0)
				throw new MotionInitException("Attempted to make motion with empty set of targets");
			return new Motion(initiator, targets);
		}

		public static Motion Make(Zone[] initiators, Zone target) {
			if (initiators == null || target == null)
				throw new MotionInitException("Attempted to make motion with null initiator/target");
			if (getNumOccupants(initiators) == 0)
				throw new MotionInitException("Attempted to make motion with empty set of initiators");
			return new Motion(initiators, target);
		}

		// Getters/setters
		public Zone[] getInitiators() { return initiators; }
		public Zone[] getTargets() { return targets; }

		protected internal void setInitiators(Zone initiator) {
			if (initiator == null)
				throw new MotionChangeException("Tried to set initiator to null zone");
			initiators = ToSingletonArray(initiator);
		}

		protected internal void setInitiators(Zone[] initiators) {
			if (initiators == null)
				throw new MotionChangeException("Tried to set initiator to null zones");
			if (getNumOccupants(initiators) == 0)
				throw new MotionChangeException("Tried to set initiator to empty set of zones");
			this.initiators = initiators;
		}

		protected internal void setTargets(Zone target) {
			if (target == null)
				throw new MotionChangeException("Tried to set target to null zone");
			targets = ToSingletonArray(target);
		}

		protected internal void setTargets(Zone[] targets) {
			if (targets == null)
				throw new MotionChangeException("Tried to set target to null zones");
			if (getNumOccupants(targets) == 0)
				throw new MotionChangeException("Tried to set target to empty set of zones");
			this.targets = targets;
		}

		// Override pls
		public abstract void invoke();

		// Methods used for input validation
		private static int getNumOccupants(Zone[] zarr) {
			int num = 0, index = 0;
			while (index < zarr.length) {
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
		public MotionInitException(string s) {
			super(s);
		}
	}

	public class MotionChangeException : Exception {
		public MotionChangeException(string s) {
			super(s);
		}
	}
}