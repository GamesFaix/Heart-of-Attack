namespace HOA
{
    /// <summary>
    /// Wrapper for UnityEngine.Debug to eliminate extra Using directives.
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// UnityEngine.Debug.Log
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str) { UnityEngine.Debug.Log(str); }
    }
}
