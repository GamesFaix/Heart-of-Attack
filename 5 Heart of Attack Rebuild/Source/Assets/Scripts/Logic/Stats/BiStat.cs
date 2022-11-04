using System;

namespace HOA.Stats
{

    public abstract class BiStat : Stat
    {
        protected Element secondary;

        protected BiStat(Unit self, string name, sbyte normal, sbyte secondNormal)
            : base(self, name, normal)
        {
            secondary = new Element(secondNormal);
        }

        public override void Add(sbyte amount, byte index = 0)
        {
            switch (index)
            {
                case 0:
                    primary += amount;
                    break;
                case 1:
                    secondary += amount;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public override void Set(sbyte amount, byte index = 0)
        {
            switch (index)
            {
                case 0:
                    primary.Set(amount);
                    break;
                case 1:
                    secondary.Set(amount);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public override int Changed(byte index = 0)
        {
            switch (index)
            {
                case 0:
                    return primary.Changed();
                case 1:
                    return secondary.Changed();
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}