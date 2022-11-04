namespace HOA.Tokens
{

    public partial class Stat
    {

        public static Stat Energy(Unit parent, int max)
        {
            return new Stat("Energy", parent, Stats.Energy, 0, 0, max);
        }

        public static Stat Focus(Unit parent)
        {
            return new Stat("Focus", parent, Stats.Focus, 0, 0, 100);
        }

        public static Stat FocusAddsInitiative(Unit parent)
        {
            Stat s = Focus(parent);
            s.Add = (n) =>
            {
                s.Current += n;
                s.Parent.StatAdd(Force.Effect, Stats.Initiative, n);
                s.Clamp();
                return s.Current;
            };
            return s;
        }

        public static Stat FocusAddsDefense(Unit parent, int cap)
        {
            Stat s = new Stat("Focus", parent, Stats.Focus, 0, 0, 100, cap);
            s.Add = (n) =>
            {
                sbyte defChange = 0;
                if (n > 0)
                    for (sbyte i = 1; i <= n; i++)
                        if (s.Current + i <= s.Modifier)
                            defChange++;
                if (n < 0)
                    for (int i = 1; i <= (-n); i++)
                        if (s.Current - i < s.Modifier)
                            defChange--;
                s.Parent.StatAdd(Force.Effect, Stats.Defense, defChange);

                s.Current += n;
                s.Clamp();
                return s.Current;
            };
            return s;
        }

        public static Stat Defense(Unit parent, int normal, int max = 100)
        {
            return new Stat("Defense", parent, Stats.Defense, normal, 0, max);
        }

        public static Stat DefenseBonus(Unit parent, int normal)
        {
            Stat s = Defense(parent, normal);
            s.Mod = () => { return 1; };
            return s;
        }

        public static Stat Health(Unit parent, int normal)
        {
            Stat s = new Stat("Health", parent, Stats.Health, normal, 0, normal);
            s.Mod = () =>
            {
                int comparison = s.Current.CompareTo(s.Max);
                if (s.Debuff) comparison *= (-1);
                return comparison;
            };
            s.ModMax = () =>
            {
                int comparison = s.Max.CompareTo(s.Normal);
                if (s.Debuff) comparison *= (-1);
                return comparison;
            };
            return s;
        }

        public static Stat Initiative(Unit parent, int normal)
        {
            return new Stat("Initiative", parent, Stats.Initiative, normal, 1, 100);
        }

    }
}
