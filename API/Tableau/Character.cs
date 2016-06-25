using UnityEngine;
using System;
using System.Collections;

namespace Tableau
{
	public abstract class Character : GameObject {
		public int id;
		public string name;
		public AttributeDict attributes;
	}
}