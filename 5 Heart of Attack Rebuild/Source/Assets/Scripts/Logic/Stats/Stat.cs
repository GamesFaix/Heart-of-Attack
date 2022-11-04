using System;

namespace HOA.Stats
{

    public abstract class Stat
    {
        public Unit self { get; protected set; }
        public string name { get; protected set; }

        protected Element primary;
        public sbyte Current { get { return primary.current; } }

        protected Stat(Unit self, string name, sbyte normal)
        {
            this.self = self;
            this.name = name;
            primary = new Element(normal);
        }

        public virtual void Add(sbyte amount, byte index = 0)
        {
            if (index > 0)
                throw new IndexOutOfRangeException();
            primary += amount;
        }
        public virtual void Set(sbyte amount, byte index = 0)
        {
            if (index > 0)
                throw new IndexOutOfRangeException();
            primary.Set(amount);
        }
        public virtual int Changed(byte index = 0)
        {
            if (index > 0)
                throw new IndexOutOfRangeException();
            return primary.Changed();
        }

        public static implicit operator sbyte(Stat s) { return  s.Current; }
        public override string ToString() { return primary.ToString(); }

        public static Stat operator +(Stat s, sbyte i) { s.Add(i); return s; }
    }
}