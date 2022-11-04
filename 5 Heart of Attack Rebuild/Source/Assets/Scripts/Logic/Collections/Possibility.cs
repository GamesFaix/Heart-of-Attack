using System;

namespace HOA.Collections
{
    /// <summary>
    /// Pairing of a possible outcome and a weight factor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Possibility<T>
    {
        /// <summary>
        /// A possible outcome
        /// </summary>
        public T Item { get; private set; }
        /// <summary>
        /// Weights are compared by a Distribution
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Assigns parameters.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="frequency">Throws exception if negative.</param>
        public Possibility(T item, int weight)
        {
            if (weight < 0)
                throw new Exception("Possibility must have positive weight.");
            Item = item;
            Weight = weight;
        }

        public override string ToString()
        {
            return "P(" + Item + ": " + Weight + ")";
        }
    }
}
