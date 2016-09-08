using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tableau.Util
{
    public class MultiMap<Tkey,Tvalue>: Dictionary<Tkey,List<Tvalue>>
    {
        public MultiMap() : base()
        {
            
        }

        public void Add(Tkey key, Tvalue value)
        {
            List<Tvalue> values;
            if (TryGetValue(key, out values))
            {
                values.Add(value);
            }
            else
            {
                values = new List<Tvalue>();
                values.Add(value);
                Add(key, values);
            }
        }

        public void AddAll(Tkey key, ICollection<Tvalue> value)
        {
            List<Tvalue> values;
            if (TryGetValue(key, out values))
            {
                values.AddRange(value);
            }
            else
            {
                values = new List<Tvalue>();
                values.AddRange(value);
                Add(key, values);
            }
        }

        public bool Remove(Tkey key, Tvalue value)
        {
            List<Tvalue> values;
            return (TryGetValue(key, out values)) ? values.Remove(value) : false;
        }

        public bool RemoveSome(Tkey key, ICollection<Tvalue> value)
        {
            List<Tvalue> values;
            return (TryGetValue(key, out values)) ? (values.RemoveAll(value.Contains) != 0) : false;
        }

        public bool RemoveAll(Tkey key)
        {
            return Remove(key);
        }

        public bool Contains(Tkey key, Tvalue value)
        {
            List<Tvalue> values;
            return (TryGetValue(key, out values)) ? values.Contains(value) : false;
        }

        public bool ContainsValue(Tvalue value)
        {
            foreach(List<Tvalue> values in Values)
            {
                if (values.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }

    }
}

