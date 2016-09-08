using UnityEngine;
using System;
using System.Collections;

namespace Tableau
{
	using Util;
	public abstract class Character : MonoBehaviour {
		public int ID;
		public string Name;
		private AttributeDict attributes;

		public Character() {
			attributes = new AttributeDict();
		}

		public void AddAttribute(string attribute, string value) {
			attributes.Add(attribute, value);
		}

		public void AddAllAttributes(AttributeDict otherDict) {
			attributes.AddAll(otherDict);
		}

		public string GetAttribute(string attribute) {
			return attributes.Get(attribute);
		}

		public void ClearAttributes() {
			attributes.Clear();
		}

		public bool HasAttribute(string attribute) {
			return attributes.HasAttribute(attribute);
		}

		public bool RemoveAttribute(string attribute) {
			return attributes.Remove(attribute);
		}
	}
}