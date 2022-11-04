using System;

namespace HOA 
{
	
    public static class Math 
    {
        public static int Round(float f) { return (int)System.Math.Round(f); }

        public static int Ceil(float f) { return (int)System.Math.Ceiling(f); }

        public static int Floor(float f) { return (int)System.Math.Floor(f); }

        public static int Abs(int i) { return (int)System.Math.Abs(i); }

        public static int Min(int i, int j) { return System.Math.Min(i, j); }

        public static int Max(int i, int j) { return System.Math.Max(i, j); }
	}
}