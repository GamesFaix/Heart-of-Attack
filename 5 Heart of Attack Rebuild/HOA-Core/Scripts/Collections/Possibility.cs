using System;

namespace HOA.Collections
{
    /// <summary> Pairing of a possible outcome and a weight factor. </summary>
    public struct Possibility<T>
    {
        /// <summary>  A possible outcome </summary>
        public readonly T item;
        /// <summary> Weights are compared by a Distribution </summary>
        public readonly int weight;

        /// <summary> Assigns parameters.  </summary>
        /// <param name="frequency">Throws exception if negative.</param>
        public Possibility(T item, int weight)
        {
            if (weight < 0)
                throw new ArgumentOutOfRangeException("Possibility must have positive weight.");
            this.item = item;
            this.weight = weight; 
        }

        public override string ToString()
        {
            return String.Format("P({0}: {1})", item, weight);
        }
    }
}
