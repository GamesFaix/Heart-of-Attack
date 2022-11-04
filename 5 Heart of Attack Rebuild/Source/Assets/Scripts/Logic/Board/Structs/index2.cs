using System;
using UnityEngine;

namespace HOA
{
    /// <summary>
    /// Pair of int32's for two-dimensional colleciton indexing. 
    /// Cannot be negative.
    /// </summary>
    public struct index2 : IEquatable<index2>
    {
        private const int limit = 0;
        int2 inner;
        
        /// <summary>
        /// X component
        /// </summary>
        public int x {
            get {return inner.x;}
            set 
            {
                try
                {
                    inner.x = value;
                    if (inner.x < limit)
                        throw new OverflowException();
                }
                catch (OverflowException) 
                {
                    inner.x = limit; 
                }
            }
        }
        /// <summary>
        /// Y component
        /// </summary>
        public int y
        {
            get { return inner.y; }
            set
            {
                try 
                {
                    inner.y = value;
                    if (inner.y < limit)
                    throw new OverflowException();
                }
                catch(OverflowException)
                {
                    inner.y = limit;
                }
            }
        }
        /// <summary>
        /// Create new index2
        /// Throws exception if arguments less than 0.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public index2(int x, int y)
        {
            if (x < limit || y < limit)
                throw new ArgumentOutOfRangeException("index2 must be above " + limit +".");
            inner = new int2(x, y);
        }

        /// <summary>
        /// Returns index2(limit, limit), defaults to 0.
        /// </summary>
        public static index2 MinValue { get { return new index2(limit, limit); } }
        /// <summary>
        /// Returns index2 (int32.Max, int32.Max)
        /// </summary>
        public static index2 MaxValue { get { return new index2(int.MaxValue, int.MaxValue); } }

        /// <summary>
        /// Create new int2 from x and y components.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static explicit operator int2(index2 a) { return new int2(a.x, a.y); }
        /// <summary>
        /// Create new index2 from x and y components.
        /// Constructor will throw exception if arguments below 0.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static explicit operator index2(int2 a) { return new index2(a.x, a.y); }

        /// <summary>
        /// Component-wise addition
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static index2 operator +(index2 a, int2 b) { return (index2)((int2)a + b); }
        /// <summary>
        /// Component-wise addition
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static index2 operator +(int2 a, index2 b) { return b + a; }
        /// <summary>
        /// Component-wise subtraction
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static index2 operator -(index2 a, int2 b) { return a + (-b); }

        #region IEquatable

        /// <summary>
        /// Component-wise value equality check
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(index2 other) { return (x == other.x && y == other.y); }

        /// <summary>
        /// Returns true if object are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) { return (obj is index2 && Equals((int2)obj)); }

        /// <summary>
        /// Returns unique identifier.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (x > short.MaxValue || x < short.MinValue
             || y > short.MaxValue || y < short.MinValue)
                throw new OverflowException(
                    "Hash code algorithm does not work with numbers outside the range of int16.");
            short x2 = (short)x;
            short y2 = (short)y;
            int x3 = (int)x2;
            int y3 = (int)(y2 << 16);

            return (x3 & y3);
        }

        /// <summary>
        /// Are both index2's equal?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(index2 a, index2 b) { return a.Equals(b); }
        /// <summary>
        /// Are both index2's unequal?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(index2 a, index2 b) { return !a.Equals(b); }

        #endregion

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }
}
