using System;
using System.Collections.Generic;

namespace HOA 
{
	public static class Functionizer 
    {
        /// <summary>Feed it X, get back a function that returns X.</summary>
        public static Func<T> Return<T>(T item) { return () => { return item; }; }

        public static Func<TResult> Close<T, TResult>(Func<T, TResult> f, T arg)
        { return () => { return f(arg); }; }

        public static Func<TResult> Close<T1, T2, TResult>(Func<T1, T2, TResult> f, T1 arg1, T2 arg2)
        { return () => { return f(arg1, arg2); }; }

        public static Func<TResult> Close<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> f, T1 arg1, T2 arg2, T3 arg3)
        { return () => { return f(arg1, arg2, arg3); }; }

        public static Action Close<T>(Action<T> f, T arg)
        { return () => f(arg); }

        public static Action Close<T1, T2>(Action<T1, T2> f, T1 arg1, T2 arg2)
        { return () => f(arg1, arg2); }

        public static Action Close<T1, T2, T3>(Action<T1, T2, T3> f, T1 arg1, T2 arg2, T3 arg3)
        { return () => f(arg1, arg2, arg3); }
	}
}