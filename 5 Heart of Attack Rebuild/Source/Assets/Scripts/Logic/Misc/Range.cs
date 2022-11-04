using System;
using System.Collections.Generic;

namespace HOA {

    /// <summary>Syntatic sugar to create generic Ranges of primitive types.</summary>
    public static class Range
    {
        public static Range<byte> b(byte min, byte max) { return new Range<byte>(min, max); }
        public static Range<sbyte> sb(sbyte min, sbyte max) { return new Range<sbyte>(min, max); }
        
        public static Range<short> s(short min, short max) { return new Range<short>(min, max); }
        public static Range<ushort> us(ushort min, ushort max) { return new Range<ushort>(min, max); }
        
        public static Range<int> i(int min, int max) { return new Range<int>(min, max); }
        public static Range<uint> ui(uint min, uint max) { return new Range<uint>(min, max); }

        public static Range<long> l(long min, long max) { return new Range<long>(min, max); }
        public static Range<ulong> ul(ulong min, ulong max) { return new Range<ulong>(min, max); }
        
        public static Range<float> f(float min, float max) { return new Range<float>(min, max); }
        public static Range<double> d(double min, double max) { return new Range<double>(min, max); }

        public static Range<decimal> m(decimal min, decimal max) { return new Range<decimal>(min, max); }
    }

    /// <summary> Min and max value always inclusive, 
    /// can test T to see if in range. </summary>
    public struct Range<T> : IEquatable<Range<T>>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public T min, max;

        public Range (T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        public bool Contains(T item)
        {
            return (item.CompareTo(min) >= 0 
                && item.CompareTo(max) <= 0);
        }

        public override string ToString() 
        { 
            return string.Format("{0}-to-{1}", min, max);
        }

        #region IEquatable

        public bool Equals(Range<T> other)
        {
            return min.Equals(other.min) 
                && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return (obj is Range<T> && ((Range<T>)obj).Equals(this));
        }

        public static bool operator ==(Range<T> r1, Range<T> r2) { return r1.Equals(r2); }
        public static bool operator !=(Range<T> r1, Range<T> r2) { return !r1.Equals(r2); }

        public override int GetHashCode()
        {
            Log.Debug("No custom implementation.");
            return base.GetHashCode();
        }

        #endregion

    }
}