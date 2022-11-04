using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace HOA {
	public enum TargetClasses : byte { Cell, Token, Unit, Ob, King, Heart, Tram, Dest, Corpse }

	public struct TargetClass : IEnumerable, IEquatable<TargetClass> {
        
        private bool[] targetClasses;
        public static int Count { get { return (Enum.GetNames(typeof(TargetClasses))).Length; } }

        #region //Constructors

        private TargetClass (TargetClasses tc) : this() {
            targetClasses = new bool[Count];
            targetClasses[(byte)tc] = true;
        }

        public static TargetClass None () 
        {
            return new TargetClass(TargetClasses.Cell) - TargetClasses.Cell;
        }
        public static TargetClass Cell () { return new TargetClass(TargetClasses.Cell); }
        public static TargetClass Token () { return new TargetClass(TargetClasses.Token); }
        public static TargetClass Unit () { return Token() + TargetClasses.Unit; }
        public static TargetClass Obstacle() { return Token() + TargetClasses.Ob; }
        public static TargetClass DestOb() { return Obstacle() + TargetClasses.Dest; }
        public static TargetClass DestUnit() { return Unit() + TargetClasses.Dest; }
       
        #endregion

        public bool this[TargetClasses tc] 
        { 
            get { return targetClasses[(byte)tc]; }
        }

        private TargetClass Set(TargetClasses c, bool b)
        {
            TargetClass copy = this;
            copy.targetClasses[(byte)c] = b;
            return copy;
        }

        public static TargetClass operator +(TargetClass a, TargetClasses b) { return a.Set(b, true); }

        public static TargetClass operator -(TargetClass a, TargetClasses b) { return a.Set(b, false); }

        #region //Enumeration

        private TargetClasses[] TrueTargetClasses
        {
            get
            {
                List<TargetClasses> trueTargetClasses = new List<TargetClasses>();
                for (byte i = 0; i < Count; i++)
                    if (targetClasses[i]) trueTargetClasses.Add((TargetClasses)i);
                return trueTargetClasses.ToArray();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator)GetEnumerator(); }
        
        public TargetClassEnumerator GetEnumerator() { return new TargetClassEnumerator(TrueTargetClasses); }
        
        public class TargetClassEnumerator : IEnumerator<TargetClasses>
        {
            TargetClasses[] targetClasses;
            int index;
            public TargetClassEnumerator(TargetClasses[] targetClasses)
            {
                this.targetClasses = targetClasses;
                Reset();
            }
            public bool MoveNext()
            {
                index++;
                return (index < targetClasses.Length);
            }
            public void Reset() { index = -1; }

            object IEnumerator.Current { get { return Current; } }
            public TargetClasses Current
            {
                get
                {
                    try { return targetClasses[index]; }
                    catch (IndexOutOfRangeException) { throw new InvalidOperationException(); }
                }
            }
            public void Dispose() { }
        }
        
        #endregion

        #region //IEquatable

        public bool Equals(TargetClass other)
        {
            for (byte i = 0; i < Count; i++)
                if (targetClasses[i] != other.targetClasses[i]) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is TargetClass && ((TargetClass)obj).Equals(this)) return true;
            return false;
        }

        public static bool operator ==(TargetClass a, TargetClass b) { return a.Equals(b); }
        public static bool operator !=(TargetClass a, TargetClass b) { return !(a.Equals(b)); }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (TargetClasses targetClass in this)
            {
                byte power = (byte)targetClass;
                hash += (int)(Math.Pow(2, power));
            }
            return hash;
        }

        #endregion

        public void Display (Panel p) {
			Rect box;
			int classes = Count - 1;

			for (int i=0; i<classes; i++) {
				if (this[(TargetClasses)i]) {
					box = p.Box(p.LineH);
					if (GUI.Button(box, "")) {
						//if (GUIInspector.RightClick) {
							TipInspector.Inspect(Tip.Special((TargetClasses)i));
						//}
					}
					GUI.Box(box, Icons.TargetClass((TargetClasses)i), p.s);
					p.NudgeX();
				}
			}
		}
	}
}