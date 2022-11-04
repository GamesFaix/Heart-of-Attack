using System.Collections.Generic;

namespace HOA.Collections
{
    /// <summary> Extension methods for generic List.  </summary>
    public static class ListExtensionMethods
    {
        /// <summary>  Get random item from list. </summary>
        /// <param name="list">This</param>
        /// <returns>Random item</returns>
        public static T Random<T>(this List<T> list)
        {
            T item = default(T);
            if (list.Count > 0)
            {
                int index = HOA.Random.Range(0, list.Count - 1);
                item = list[index];
            }
            return item;
        }

        /// <summary> Randomizes order of List.  Destructive. </summary>
        /// <param name="list">This</param>
        /// <returns>Same list as entered, but destructively reordered.</returns>
        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> old = list;

            List<T> shuffled = new List<T>();
            while (old.Count > 0)
            {
                int rand = HOA.Random.Range(0, old.Count - 1);
                shuffled.Add(old[rand]);
                old.Remove(old[rand]);
            }
            list = shuffled;
            return list;
        }

        /// <summary>  Add a collection of items to list. </summary>
        public static void Add<T>(this List<T> list, IEnumerable<T> collection)
        {
            foreach (T item in collection)
                list.Add(item);
        }



    }
}
