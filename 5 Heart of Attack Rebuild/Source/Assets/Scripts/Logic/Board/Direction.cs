using System;
using System.Collections;

namespace HOA
{
    /// <summary>
    /// Basically an enumerator of ordered pairs for iterating thru 8 planar directions.
    /// (Left, UpLeft, Up, UpRight, Rigth, DownRight, Down, DownLeft)
    /// </summary>
    public class Direction : IEnumerable
    {
        /// <summary>
        /// Returns direction between two cells,
        /// rounded to 45deg.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static int2 FromCells(Cell c1, Cell c2)
        {
            int2 diff = (int2)(c2.Index) - (int2)(c1.Index);
            int2 dir = new int2(0, 0);

            if (diff.x != 0) { dir.x = diff.x / (Math.Abs(diff.x)); }
            if (diff.y != 0) { dir.y = diff.y / (Math.Abs(diff.y)); }

            return dir;
        }

        /// <summary>
        /// (-1,0)
        /// </summary>
        public static int2 Left { get { return new int2(-1, 0); } }
        /// <summary>
        /// (-1,-1)
        /// </summary>
        public static int2 UpLeft { get { return new int2(-1, -1); } }
        /// <summary>
        /// (0,-1)
        /// </summary>
        public static int2 Up { get { return new int2(0, -1); } }
        /// <summary>
        /// (1,-1)
        /// </summary>
        public static int2 UpRight { get { return new int2(1, -1); } }
        /// <summary>
        /// (1,0)
        /// </summary>
        public static int2 Right { get { return new int2(1, 0); } }
        /// <summary>
        /// (1,1)
        /// </summary>
        public static int2 DownRight { get { return new int2(1, 1); } }
        /// <summary>
        /// (0,1)
        /// </summary>
        public static int2 Down { get { return new int2(0, 1); } }
        /// <summary>
        /// (-1,1)
        /// </summary>
        public static int2 DownLeft { get { return new int2(-1, 1); } }

        /// <summary>
        /// Returns array of directions, clockwise from Left
        /// </summary>
        public static int2[] Directions
        {
            get
            {
                return new int2[8] { Left, UpLeft, Up, UpRight, Right, DownRight, Down, DownLeft };
            }
        }

        /// <summary>
        /// Iterates thru directions, clockwise from Left
        /// </summary>
        /// <returns></returns>
        public static IEnumerator GetEnumerator()
        {
            for (byte i = 0; i < Directions.Length; i++)
            {
                yield return Directions[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    }
}