using System.Collections.Generic;
using System;
using HOA.Collections;

namespace HOA
{

    public class EntitySet : ListSet<IEntity>, IEnumerable<IEntity>
    {
        public EntitySet() { list = new List<IEntity>(); }

        public EntitySet(IEntity t)
        {
            list = new List<IEntity>();
            list.Add(t);
        }

        public EntitySet(IEnumerable<IEntity> collection)
        {
            list = new List<IEntity>(collection);
        }

        public void Add(CellSet cells)
        {
            foreach (Cell c in cells)
                Add(c as IEntity);
        }

        public void Add(TokenSet tokens)
        {
            foreach (Token t in tokens)
                Add(t as IEntity);
        }

        public EntitySet Copy()
        {
            EntitySet copy = new EntitySet();
            foreach (IEntity t in this) 
                copy.Add(t);
            return copy;
        }

        private EntitySet Filter(EntityFilter filter)
        {
            EntitySet rejected = Copy();
            EntitySet accepted = new EntitySet();

            foreach (Predicate<IEntity> test in filter.Tests)
            {
                foreach (IEntity t in rejected)
                    if (test(t))
                    {
                        accepted.Add(t);
                        rejected.Mark(t);
                    }
                rejected.RemoveMarked();
            }
            return accepted;
        }

        public static EntitySet operator -(EntitySet set, EntityFilter filter) { return set.Filter(filter); }

        public void Legalize(bool b = true)
        {
            foreach (IEntity t in this)
                t.Legal = b;
        }

        public static EntitySet operator +(EntitySet set1, EntitySet set2)
        {
            EntitySet set = new EntitySet(set1);
            set.Add(set2);
            return set;
        }

    }
}
