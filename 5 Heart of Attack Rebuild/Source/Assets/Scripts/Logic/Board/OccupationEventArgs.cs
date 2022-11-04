using System;

namespace HOA.Board
{

    /// <summary>
    /// Arguments for Cell enter or exit events.
    /// </summary>
    public class OccupationEventArgs : EventArgs
    {
        /// <summary> Cell being entered or exited. </summary>
        public Cell Cell { get; private set; }
        /// <summary> Token entering or exiting. </summary>
        public Tokens.Token Token { get; private set; }
        /// <summary> True if event is Enter event. </summary>
        public bool Enter { get; private set; }
        /// <summary> True if event is Exit event. </summary>
        public bool Exit { get { return !Enter; } }

        /// <summary>  Assigns parameters to fields. </summary>
        /// <param name="cell">Cell being entered or exited.</param>
        /// <param name="token">Token entering or exiting.</param>
        /// <param name="enter">Is token entering?</param>
        public OccupationEventArgs(Cell cell, Tokens.Token token, bool enter)
        {
            Cell = cell;
            Token = token;
            Enter = enter;
        }
    }
}