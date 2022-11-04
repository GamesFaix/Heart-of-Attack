using UnityEngine; 

namespace HOA { 

    public partial class Stat 
    {
    
        public static Stat Energy (Unit parent, int max) 
        {
            return new Stat("Energy", parent, Stats.Energy, 0, 0, max);
		}

        public static Stat Focus (Unit parent)
        {
            return new Stat("Focus", parent, Stats.Focus, 0, 0, 100);
        }

        public static Stat FocusAddsInitiative (Unit parent)
        {
            Stat s = Focus(parent);
            s.Add = (source, n, log) =>
            {
                s.Current += n;
                parent.AddStat(source, Stats.Initiative, n, log);
                s.Clamp();
                string sign = s.Sign(n);
                if (log) { GameLog.Out(source + ": " + parent + " " + sign + n + s.Name + ". " + 
                    s.Name + " = " + s.Current); }
                return s.Current;
            };
            return s;
        }

        public static Stat FocusAddsDefense(Unit parent, int cap)
        {
            Stat s = new Stat("Focus", parent, Stats.Focus, 0, 0, 100, cap);
            s.Add = (source, n, log) =>
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
                parent.AddStat(source, Stats.Defense, defChange, log);

                s.Current += n;
                s.Clamp();
                string sign = s.Sign(n);
                if (log) GameLog.Out(source + ": " + parent + " " + sign + n + s.Name + ". " + s.Name + " = " + s.Current);
                return s.Current;
            };
            return s;
        }

        public static Stat Defense (Unit parent, int normal, int max=100) 
        {
            return new Stat("Defense", parent, Stats.Defense, normal, 0, max);
        }

        public static Stat DefenseBonus (Unit parent, int normal)
        {
            Stat s = Defense(parent, normal);
            s.Modified = () => { return 1; };
            return s;
        }

        public static Stat Health(Unit parent, int normal)
        {
            Stat s = new Stat("Health", parent, Stats.Health, normal, 0, normal);
            s.Modified = () =>
            {
                int comparison = s.Current.CompareTo(s.Max);
                if (s.debuff) comparison *= (-1);
                return comparison;
            };
            s.MaxModified = () =>
            {
                int comparison = s.Max.CompareTo(s.Normal);
                if (s.debuff) comparison *= (-1);
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
