using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tableau.Util {
	public class AttributeDict {
		private Dictionary<string, string> dict = new Dictionary<>();

		public void Add(string attribute, string value) {
			if (attribute == null)
				throw new ArgumentNullException("Tried to add null-name attribute");
			dict.Item[attribute] = value;
		}

		public void AddAll(AttributeDict otherDict) {
			foreach (KeyValuePair<string, string> kvp in otherDict.dict) {
				Add(kvp.Key, kvp.Value);
			}
		}

		public string Get(string attribute) {
			string val = null;
			dict.TryGetValue(attribute, out val);
			return val;
		}

		public void Clear() {
			dict.Clear();
		}

		public bool HasAttribute(string attribute) {
			return dict.ContainsKey(attribute);
		}

		public bool Remove(string attribute) {
			return dict.Remove(attribute);
		}

		public AttributeDict Copy() {
			AttributeDict newDict = new AttributeDict();
			newDict.AddAll(this);
			return newDict;
		}
	}
}