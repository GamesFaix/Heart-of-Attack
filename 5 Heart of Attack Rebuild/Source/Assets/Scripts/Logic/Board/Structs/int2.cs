using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA
{
    /// <summary>
    /// Pair of int32's
    /// </summary>
    public struct int2 : IEquatable<int2>
    {
        /// <summary>
        /// X component
        /// </summary>
        public int x;
        /// <summary>
        /// Y component
        /// </summary>
        public int y;

        /// <summary>
        /// Creat new int2
        /// </summary>
        /// <param name="x">Assigns to x component</param>
        /// <param name="y">Assigns to y component</param>
		public int2 (int x, int y) {
			this.x = x;
			this.y = y;
		}

        /// <summary>
        /// Returns int2(int.Min, int.Min)
        /// </summary>
        public static int2 MinValue { get { return new int2(int.MinValue, int.MinValue); } }
        /// <summary>
        /// Returns int2(int.Max, int.Max)
        /// </summary>
        public static int2 MaxValue { get { return new int2(int.MaxValue, int.MaxValue); } }

        /// <summary>
        /// Component-wise addition
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator +(int2 a, int2 b) { return new int2(a.x + b.x, a.y + b.y); }
        /// <summary>
        /// Add scalar to both components
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator +(int2 a, int b) { return new int2(a.x + b, a.y + b); }
        /// <summary>
        /// Add Scalar to components
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator +(int a, int2 b) { return b + a; }

        /// <summary>
        /// Invert each component
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int2 operator -(int2 a) { return new int2(-a.x, -a.y); }
        /// <summary>
        /// Component-wise subtraction
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator -(int2 a, int2 b) { return a + (-b); }
        /// <summary>
        /// Subtract scalar from each component
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator -(int2 a, int b) { return a + (-b); }

        /// <summary>
        /// Component-wise multiplication
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator *(int2 a, int2 b) { return new int2(a.x * b.x, a.y * b.y); }
        /// <summary>
        /// Mulitply each component by scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator *(int2 a, int b) { return new int2(a.x * b, a.y * b); }
        /// <summary>
        /// Multiply each component by scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int2 operator *(int a, int2 b) { return b * a; }

        /// <summary>
        /// Divide each component by scalar.
        /// Throws exception if not evenly divisible.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
		public static int2 operator / (int2 a, int b) {
			if (a.x%b != 0  || a.y%b != 0) 
                throw new ArgumentOutOfRangeException("int2 cannot be divided unevenly");
			else if (b == 0)
                throw new DivideByZeroException();
            return new int2((int)(a.x / b), (int)(a.y / b));
		}
        /// <summary>
        /// Component-wise division.
        /// Throws exception if does not divide evenly.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
		public static int2 operator / (int2 a, int2 b) {
			if (a.x%b.x != 0 || a.y%b.y != 0) 
                throw new ArgumentOutOfRangeException("int2 cannot be divided unevenly.");
			else if (b.x==0 || b.y==0) 
                throw new DivideByZeroException();
            return new int2((int)(a.x / b.x), (int)(a.y / b.y));
		}

        /// <summary>
        /// Creates new int2 from absolute value of each component.
        /// </summary>
        public int2 Abs { get { return new int2(Math.Abs(x), Math.Abs(y)); } }
        /// <summary>
        /// Multiply components together.
        /// </summary>
        public int Product { get { return x * y; } }

        #region IEquatable

        /// <summary>
        /// Component-wise value equality check
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(int2 other) { return (x == other.x && y == other.y); }

        /// <summary>
        /// Returns true if object are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) { return (obj is int2 && Equals((int2)obj)); }

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
        /// Are both int2's equal?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(int2 a, int2 b) { return a.Equals(b); }
        /// <summary>
        /// Are both int2's unequal?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(int2 a, int2 b) { return !a.Equals(b); }

        #endregion

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }
}
