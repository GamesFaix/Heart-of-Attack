using System;
using System.Text;
using System.Collections.Generic;

namespace HOA 
{
	
    public static class StringBuilderExtensionMethods 
    {
        /// <summary>Append a formatted string.</summary>
        public static void Append(this StringBuilder s, string str, params object[] items)
        {
            s.Append(String.Format(str, items));
        }

        /// <summary>Remove n characters from the end.</summary>
        public static void Trim(this StringBuilder s, int n)
        {
            s.Remove(s.Length - n, n);
        }

	}
}