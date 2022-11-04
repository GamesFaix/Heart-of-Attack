using UnityEngine; 
using System.Collections.Generic;
using System;

namespace HOA {
   
    public partial class TargetFilter : IEquatable<TargetFilter>{
        
        public List<FilterTest> Tests { get; private set; }

        private TargetFilter()
        {
            Tests = new List<FilterTest>();
        }
        public TargetFilter(FilterTest test)
            : this()
        {
            Add(test);
        }

        public bool Test(Target t)
        {
            foreach (FilterTest f in Tests)
                if (f(t) == false) return false;
            return true;
        }

        public void Add(FilterTest test) { Tests.Add(test); }
        public void Remove(FilterTest test) { Tests.Remove(test); }

        public static TargetFilter operator +(TargetFilter a, FilterTest b) { a.Add(b); return a; }
        public static TargetFilter operator -(TargetFilter a, FilterTest b) { a.Remove(b); return a; }

        public bool Equals(TargetFilter other)
        {
            if (Tests.Count != other.Tests.Count) return false;
            for (int i = 0; i < Tests.Count; i++)
            {
                if (Tests[i] != other.Tests[i]) return false;
            }
            return true;
        }
        public override bool Equals(object other) { return (other is TargetFilter && ((TargetFilter)other).Equals(this)); }
        public static bool operator ==(TargetFilter a, TargetFilter b) { return a.Equals(b); }
        public static bool operator !=(TargetFilter a, TargetFilter b) { return !(a.Equals(b)); }

        public bool Contains(FilterTest test) { return Tests.Contains(test); }

        public void Display(Panel p)
        {/*
            Rect box;
            int classes = TargetClass.Count - 1;

            for (int i = 0; i < classes; i++)
            {
                if (this[(TargetClasses)i])
                {
                    box = p.Box(p.LineH);
                    if (GUI.Button(box, ""))
                    {
                        //if (GUIInspector.RightClick) {
                        TipInspector.Inspect(Tip.Special((TargetClasses)i));
                        //}
                    }
                    GUI.Box(box, Icons.TargetClass((TargetClasses)i), p.s);
                    p.NudgeX();
                }
           }*/
        }

        
    }
}
