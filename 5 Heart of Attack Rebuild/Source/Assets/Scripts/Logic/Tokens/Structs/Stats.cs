using System;

namespace HOA.Tokens
{ 
    /// <summary>
    /// Simple enum of Unit stats. None = 0.
    /// </summary>
    public enum Stats : byte
	{
        None = 0,
        Health,
        Defense,
        Initiative,
        Energy,
        Focus
	}

}
