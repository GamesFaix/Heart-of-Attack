using System;

namespace HOA 
{
	/// <summary> Takes no args, returns a string.</summary>
	public delegate string Description();

    /// <summary> Creates StringMaker delegates.</summary>
    public static class Scribe 
    {
        /// <summary> Returns Description that returns string.Format(message, items). </summary>
        public static Description Write (string message, params object[] items)
        { 
            return () => { return string.Format(message, items); }; 
        }

        /// <summary>Returns plain Description that just returns message.</summary>
        public static Description Write (string message = "Default description.") 
        { 
            return () => { return message; }; 
        }
    }

}