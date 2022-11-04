namespace HOA
{
    /// <summary>
    /// Wrapper for UnityEngine.Debug to eliminate extra Using directives.
    /// </summary>
    public static class Debug
    {
        /// <summary> Forwards message to UnityEngine.Debug.Log </summary>
        public static void Log(string message) { UnityEngine.Debug.Log(message); }

        /// <summary> 
        /// Processes message with String.Format, 
        /// then forwards to UnityEngine.Debug.Log.
        /// </summary>
        public static void Log(string message, params object[] objects)
        {
            Log(string.Format(message, objects));
        }
    }
}
