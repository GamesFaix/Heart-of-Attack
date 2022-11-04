using System;
using System.Collections.Generic;

namespace HOA 
{
	public static class Comb 
    {
        /// <summary>K-Combinator: Feed it X, get back a function that returns X.</summary>
        public static Func<T> K<T>(T item) { return () => { return item; }; }


	}
}