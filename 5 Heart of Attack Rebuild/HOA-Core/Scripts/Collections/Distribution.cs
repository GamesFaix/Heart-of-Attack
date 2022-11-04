using System;
using System.Collections.Generic;

namespace HOA.Collections
{
    /// <summary> List of items with weights, for weighted lottery systems. </summary>
    public class Distribution<T>
    {
        /// <summary> List of all possibilities. </summary>
        public List<Possibility<T>> Possibilities { get; private set; }

        /// <summary> Returns sum of the weights of all possibilities. </summary>
        public int TotalWeight
        {
            get
            {
                int sum = 0;
                foreach (Possibility<T> p in Possibilities)
                    sum += p.weight;
                return sum;
            }
        }

        /// <summary>  Create blank list of possibilities. </summary>
        public Distribution()
        {
            Possibilities = new List<Possibility<T>>();
        }
        
        /// <summary> Inner List.Add </summary>
        public void Add(Possibility<T> p) { Possibilities.Add(p); }
        /// <summary> If(Contains) Remove  </summary>
        /// <returns>True if found, false if never in list.</returns>
        public bool Remove(Possibility<T> p)
        {
            if (Possibilities.Contains(p))
            {
                Possibilities.Remove(p);
                return true;
            }
            return false;
        }

        /// <summary>  Returns a random outcome based on possibility weights. </summary>
        public T Random()
        {
            int random = HOA.Random.Range(1, TotalWeight);
            int sum = 0;
            foreach (Possibility<T> p in Possibilities)
            {
                sum += p.weight;
                if (random <= sum) 
                    return p.item;
            }
            throw new Exception("Distribution randomizer failure!");
        }
    }
}
