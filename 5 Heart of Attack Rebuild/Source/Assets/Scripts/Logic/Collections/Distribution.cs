using System;
using System.Collections.Generic;

namespace HOA
{
    /// <summary>
    /// List of items with weights, for weighted lottery systems.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Distribution<T>
    {
        /// <summary>
        /// List of all possibilities.
        /// </summary>
        public List<Possibility<T>> Possibilities { get; private set; }

        /// <summary>
        /// Returns sum of the weights of all possibilities.
        /// </summary>
        public int TotalWeight
        {
            get
            {
                int sum = 0;
                foreach (Possibility<T> p in Possibilities)
                    sum += p.Weight;
                return sum;
            }
        }

        /// <summary>
        /// Create blank list of possibilities.
        /// </summary>
        public Distribution()
        {
            Possibilities = new List<Possibility<T>>();
        }
        
        /// <summary>
        /// Inner List.Add
        /// </summary>
        /// <param name="item"></param>
        public void Add(Possibility<T> p) { Possibilities.Add(p); }
        /// <summary>
        /// If(Contains) Remove
        /// </summary>
        /// <param name="item"></param>
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

        /// <summary>
        /// Returns a random outcome based on possibility weights.
        /// </summary>
        /// <returns></returns>
        public T Random()
        {
            int random = HOA.Random.Range(1, TotalWeight);
            int sum = 0;
            foreach (Possibility<T> p in Possibilities)
            {
                sum += p.Weight;
                if (random <= sum) 
                    return p.Item;
            }
            throw new Exception("Distribution randomizer failure!");
        }
    }
}
