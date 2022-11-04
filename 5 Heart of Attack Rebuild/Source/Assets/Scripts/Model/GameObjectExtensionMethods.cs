using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA
{
    public static class GameObjectExtensionMethods
    {
        /// <summary>
        /// Create instance at (0,0,0) in default orientation.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static GameObject InstantiateNowhere(this GameObject prefab)
        {
            return GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        /// <summary>
        /// Create instance at (0,0,0) in default orientation and attach to parent.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject InstantiateNowhereUnder(this GameObject prefab, GameObject parent)
        {
            GameObject g = InstantiateNowhere(prefab);
            g.transform.parent = parent.transform;
            return g;
        }

        /// <summary>
        /// Create instance at (0,0,0) in default orientation and name it.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject InstantiateNowhere(this GameObject prefab, string name)
        {
            GameObject g = InstantiateNowhere(prefab);
            g.name = name;
            return g;
        }

        /// <summary>
        /// Create instance at (0,0,0) in default orientation, attach to parent, and name it.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject InstantiateNowhereUnder(this GameObject prefab, GameObject parent, string name)
        {
            GameObject g = InstantiateNowhere(prefab);
            g.transform.parent = parent.transform;
            g.name = name;
            return g;
        }

    }
}
