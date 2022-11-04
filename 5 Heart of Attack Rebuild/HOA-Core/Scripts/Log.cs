using System;
using System.Collections.Generic;

namespace HOA
{

    [Flags]
    public enum Logs : byte { None = 0, Start = 1, Debug = 2, Session = 4, Game = 8, All = (Start|Debug|Session|Game) }

    public static class LogsExtensionMethods
    {
        public static bool Contains(this Logs logs, Logs other)
        {
            return (logs & other) != Logs.None;
        }
    }

    public static class Log
    {
        #region Properties

        static List<string> startLog, sessionLog, gameLog, debugLog;

        static Logs toPublish;

        #endregion

        #region Setup

        public static void Load()
        {
            toPublish =
                Logs.All;
                //Logs.Start;
                //Logs.Session;
                //Logs.Game;

            startLog = new List<string>();
            startLog.Add("StartLog created.");

            debugLog = new List<string>();
            debugLog.Add("DebugLog created.");
        }

        public static void NewSession()
        {
            sessionLog = new List<string>();
            sessionLog.Add("SessionLog created.");

            gameLog = new List<string>();
            gameLog.Add("GameLog created.");
        }

        #endregion

        static void Add(string message, List<string> log, Logs logs)
        {
            if (log == null)
                throw new Exception("Log null.");
            log.Add(message);
            if (toPublish.Contains(logs))
                Forward(message);
        }

        static void Forward(string message) { UnityLog.Debug(message); }

        public static void Start(string message) { Add(message, startLog, Logs.Start); }
        public static void Start(string message, params object[] items) { Start(string.Format(message, items)); }

        public static void Debug(string message) { Add(message, debugLog, Logs.Debug); }
        public static void Debug(string message, params object[] items) { Debug(string.Format(message, items)); }

        public static void Session(string message) { Add(message, sessionLog, Logs.Session); }
        public static void Session(string message, params object[] items) { Session(string.Format(message, items)); }

        public static void Game(string message) { Add(message, gameLog, Logs.Game); }
        public static void Game(string message, params object[] items) { Game(string.Format(message, items)); }
    }
}
