using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HOA {
    public enum Planes : byte { Sunken, Ground, Air, Ethereal }

	public struct Plane : IEnumerable<Planes>, IEquatable<Plane>, IInspectable
    {

        private bool[] planes;
        public static int Count { get { return (Enum.GetNames(typeof(Planes))).Length; } }

        #region //Constructors

        private Plane(bool[] planes)
        {
            this.planes = new bool[4];
            planes.CopyTo(this.planes,0);
        }

        public static Plane Sunken       { get { return new Plane(new bool[4] { true, false, false, false}); } }
        public static Plane HalfSunk     { get { return new Plane(new bool[4] { true, true, false, false}); } }
        public static Plane Ground       { get { return new Plane(new bool[4] { false, true, false, false}); } }
        public static Plane Tall         { get { return new Plane(new bool[4] { false, true, true, false}); } }
        public static Plane Air          { get { return new Plane(new bool[4] { false, false, true, false}); } }
        public static Plane Ethereal     { get { return new Plane(new bool[4] { false, false, false, true }); } }
        public static Plane GroundAirEth { get { return new Plane(new bool[4] { false, true, true, true }); } }
        public static Plane All          { get { return new Plane(new bool[4] { true, true, true, true }); } }
        public static Plane None         { get { return new Plane(new bool[4] { false, false, false, false }); } }

        #endregion

        public bool this[Planes p] { get { return planes[(byte)p]; } }

        #region //IEquatable

        public bool Equals(Plane other)
        {
            for (byte i = 0; i < planes.Length; i++)
            {
                if (planes[i] != other.planes[i]) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Plane && ((Plane)obj).Equals(this)) { return true;}
            return false;
        }

        public static bool operator == (Plane a, Plane b) {return a.Equals(b);}
        public static bool operator != (Plane a, Plane b) {return !(a.Equals(b));}

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (Planes plane in this)
            {
                byte power = (byte)plane;
                hash += (int)(Math.Pow(2, power));
            }
            return hash;
        }

        #endregion

        #region//IEnumerator

        private List<Planes> TruePlanes()
        {
            List<Planes> truePlanes = new List<Planes>();
            for (byte i = 0; i < Count; i++)
            {
                if (planes[i]) truePlanes.Add((Planes)i);
            }
            return truePlanes;
        }

        public IEnumerator<Planes> GetEnumerator()
        {
            List<Planes> truePlanes = TruePlanes();
            for (byte i = 0; i < truePlanes.Count; i++) 
            { 
                yield return truePlanes[i]; 
            }
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion

        public void Draw(Panel p) { InspectorInfo.Plane(this, p); }
	}
}