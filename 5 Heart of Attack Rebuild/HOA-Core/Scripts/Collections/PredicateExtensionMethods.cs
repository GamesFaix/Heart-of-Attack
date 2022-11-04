using System;
using System.Collections.Generic;

namespace HOA.Collections
{
	
    public static class PredicateExtensionMethods
    {
        #region Multicast checks

        /// <summary>Iterates thru each predicate in multicast.
        /// Return false at first false result.</summary>
        public static bool AllTrue<T>(this Predicate<T> multicast, T item)
        { return All(multicast, item, true); }

        /// <summary>Iterates thru each predicate in multicast.
        /// Return false at first true result.</summary>
        public static bool AllFalse<T>(this Predicate<T> multicast, T item)
        { return All(multicast, item, false); }

        /// <summary>Iterates thru each predicate in mutlicast.
        /// Returns true at first true result.</summary>
        public static bool AnyTrue<T>(this Predicate<T> multicast, T item) { return !AllFalse(multicast, item); }

        /// <summary>Iterates thru each predicate in mutlicast.
        /// Returns false at first false result.</summary>
        public static bool AnyFalse<T>(this Predicate<T> multicast, T item) { return !AllTrue(multicast, item); }

        static bool All<T>(Predicate<T> multicast, T item, bool b)
        {
            foreach (Predicate<T> p in multicast.GetInvocationList())
                if (p(item) != b)
                    return false;
            return true;
        }

        #endregion

        #region Conversion

        public static Func<T, bool> ToFunc<T>(this Predicate<T> p)
        { return (t) => { return p(t); }; }

        public static Predicate<T> ToPredicate<T>(this Func<T, bool> f)
        { return (t) => { return f(t); }; }

        #endregion
    }
}