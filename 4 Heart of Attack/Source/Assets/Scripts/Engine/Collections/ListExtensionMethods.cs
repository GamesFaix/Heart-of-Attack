using System.Collections.Generic;
using UnityEngine;

namespace HOA { 

    public static class ListExtensionMethods
    {
        public static T Random<T>(this List<T> list)
        {
            T item = default(T);
            if (list.Count > 0)
            {
                int index = (int)Mathf.Round(UnityEngine.Random.Range(0, list.Count - 1));
                item = list[index];
            }
            return item;
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> old = list;

            List<T> shuffled = new List<T>();
            while (old.Count > 0)
            {
                int rand = (int)Mathf.Round(UnityEngine.Random.Range(0, old.Count-1));
                shuffled.Add(old[rand]);
                old.Remove(old[rand]);
            }
            list = shuffled;
            return list;
        }

        public static void Add<T>(this List<T> list, IEnumerable<T> collection)
        {
            foreach (T item in collection)
                list.Add(item);
        }

    }
}
