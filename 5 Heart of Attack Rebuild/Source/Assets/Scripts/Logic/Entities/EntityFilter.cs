using UnityEngine;
using System.Collections.Generic;
using System;

namespace HOA
{

    public partial class EntityFilter //: IEquatable<EntityFilter>
    {

        public List<Predicate<IEntity>> Tests { get; private set; }

        private EntityFilter()
        {
            Tests = new List<Predicate<IEntity>>();
        }
        public EntityFilter(Predicate<IEntity> test)
            : this()
        {
            Add(test);
        }

        public bool Test(IEntity t)
        {
            foreach (Predicate<IEntity> f in Tests)
                if (f(t) == false) return false;
            return true;
        }

        public void Add(Predicate<IEntity> test) { Tests.Add(test); }
        public void Remove(Predicate<IEntity> test) { Tests.Remove(test); }

        public static EntityFilter operator +(EntityFilter a, Predicate<IEntity> b) 
        { 
            a.Add(b); 
            return a; 
        }
        public static EntityFilter operator -(EntityFilter a, Predicate<IEntity> b) 
        { 
            a.Remove(b); 
            return a; 
        }

        /*

        public bool Equals(EntityFilter other)
        {
            if (Tests.Count != other.Tests.Count) 
                return false;
            for (int i = 0; i < Tests.Count; i++)
                if (Tests[i] != other.Tests[i]) 
                    return false;
            return true;
        }
        public override bool Equals(object other) 
        { 
            return (other is EntityFilter && ((EntityFilter)other).Equals(this)); 
        }
        public static bool operator ==(EntityFilter a, EntityFilter b) { return a.Equals(b); }
        public static bool operator !=(EntityFilter a, EntityFilter b) { return !(a.Equals(b)); }

         * */
         
        public bool Contains(Predicate<IEntity> test) { return Tests.Contains(test); }
    }
}
