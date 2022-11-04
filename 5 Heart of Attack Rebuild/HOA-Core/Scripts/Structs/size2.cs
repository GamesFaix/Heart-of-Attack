using System;
using System.Collections;

namespace HOA
{
    /// <summary> Pair of int32's for measuring 2-dimensional collections.
    /// Components must be greater than 0. </summary>
    public struct size2 : IEnumerable, IEquatable<size2>
    {
        private const int limit = 1;
        int2 inner;

        /// <summary>  X component </summary>
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
        /// <summary> Y component  </summary>
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
        
        /// <summary> Create new size2.  Throws exception if components out of range.  </summary>
        public size2(int x, int y)
        {
            if (x < limit || y < limit)
                throw new ArgumentOutOfRangeException("size2 must be above " + limit +".");
            inner = new int2(x, y);
        }

        /// <summary> Returns size2(limit, limit).  Default = 1 </summary>
        public static size2 MinValue { get { return new size2(limit, limit); } }
        /// <summary> Returns size2(int32.Max, int32.Max) </summary>
        public static size2 MaxValue { get { return new size2(int.MaxValue, int.MaxValue); } }

        /// <summary> Creates new int2(x, y) </summary>
        public static explicit operator int2(size2 a) { return new int2(a.x, a.y); }
        /// <summary> Creates new size2.  Constructor throws exception if components out of range. </summary>
        public static explicit operator size2(int2 a) { return new size2(a.x, a.y); }

        /// <summary> Component-wise addition </summary>
        public static size2 operator +(size2 a, size2 b) { return new size2(a.x + b.x, a.y + b.y); }
        /// <summary> Component-wise addition </summary>
        public static size2 operator +(size2 a, int2 b) { return (size2)((int2)a + b); }
        /// <summary> Component-wise addition </summary>
        public static size2 operator +(int2 a, size2 b) { return b + a; }
        /// <summary> Add scalar to each component </summary>
        public static size2 operator +(size2 a, int b) { return new size2(a.x + b, a.y + b); }
        /// <summary> Add scalar to each component </summary>
        public static size2 operator +(int a, size2 b) { return b + a; }

        /// <summary> Component-wise subtraction </summary>
        public static size2 operator -(size2 a, size2 b) { return a - (int2)b; }
        /// <summary> Component-wise subtraction </summary>
        public static size2 operator -(size2 a, int2 b) { return a + (-b); }
        /// <summary> Subtract scalar from each component  </summary>
        public static size2 operator -(size2 a, int b) { return a + (-b); }


        /// <summary> Component-wise multiplication </summary>
        public static size2 operator *(size2 a, size2 b) { return new size2(a.x * b.x, a.y * b.y); }
        /// <summary> Component-wise multiplication </summary>
        public static size2 operator *(size2 a, int2 b) { return (size2)((int2)a * b); }
        /// <summary> Component-wise multiplication  </summary>
        public static size2 operator *(int2 b, size2 a) { return b * a; }
        /// <summary> Scalar multiplication </summary>
        public static size2 operator *(size2 a, int b) { return new size2(a.x * b, a.y * b); }
        /// <summary> Scalar multiplication </summary>
        public static size2 operator *(int a, size2 b) { return b * a; }

        /// <summary> Component-wise division.
        /// Throws exception if not evenly divisible.  </summary>
        public static size2 operator /(size2 a, size2 b) { return (size2)((int2)a / (int2)b); }

        /// <summary>  Product of compnents </summary>
        public int Product { get { return x * y; } }

        /// <summary> Checks if index would be available in a two-dimensional collection of this size. </summary>
        /// <returns>True if index x and y are less than size x and y.</returns>
        public bool Contains(index2 i) { return (i.x < x && i.y < y); }
        /// <summary> Checks if all components less than or equal to other size </summary>
        public bool SubsetOf(size2 s) { return (x <= s.x && y <= s.y); }
        /// <summary> Checks if all components greater than or equal to other size </summary>
        public bool SupersetOf(size2 s) { return (s.x <= x && s.y <= y); }

        /// <summary> Iterates through each index2 that would be available in a collection of this size. </summary>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    yield return new index2(i, j);
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #region IEquatable

        /// <summary>  Component-wise value equality check </summary>
        public bool Equals(size2 other) { return (x == other.x && y == other.y); }

        /// <summary> Returns true if object are equal. </summary>
        public override bool Equals(object obj) { return (obj is size2 && Equals((size2)obj)); }

        /// <summary> Returns unique identifier. </summary>
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

        /// <summary> Are both size2's equal? </summary>
        public static bool operator ==(size2 a, size2 b) { return a.Equals(b); }
        /// <summary>  Are both size2's unequal?  </summary>
        public static bool operator !=(size2 a, size2 b) { return !a.Equals(b); }

        #endregion

        public override string ToString() { return String.Format("({0}, {1})", x, y); }
    }
}
