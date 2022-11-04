using System;

namespace HOA 
{ 
    [Flags]
    public enum TokenFlags : byte
	{
        None = 0,
        Destructible = 1,
        Trample = 2,
        Corpse = 4,
        Heart = 8
	}

    public static class TokenFlagsExtensionMethods
    {
        /// <summary>
        /// Does this TokenFlags contain any of the flags of the argument?
        /// </summary>
        /// <param name="p">This.</param>
        /// <param name="query"></param>
        /// <returns>True if any flags match.</returns>
        public static bool ContainsAny(this TokenFlags a, TokenFlags b)
        {
            return !((a & b) == TokenFlags.None);
        }


    }

}
