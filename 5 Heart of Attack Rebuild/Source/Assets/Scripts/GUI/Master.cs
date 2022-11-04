using System;
using System.Collections.Generic;
using UnityEngine; 

namespace HOA.GUI
{ 
    /// <summary>
    /// Coordinates all other GUI pages.
    /// </summary>
    public class Master : MonoBehaviour
	{
        /// <summary>
        /// Standard style for all GUI elements.
        /// </summary>
        public static GUIStyle style { get; private set; }

        void Awake()
        {
            style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 20;
        }

	}

}
