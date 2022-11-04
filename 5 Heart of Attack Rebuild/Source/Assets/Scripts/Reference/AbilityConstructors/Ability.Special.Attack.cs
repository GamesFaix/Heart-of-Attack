using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability Feast(Unit parent)
        { return LeechArc(parent, "Feast", Rank.Attack, Price.Cheap, new Range(2, 3), 12); }

        public static Ability Feed(Unit parent)
        { return LeechNeighbor(parent, 5); }

        public static Ability Fling(Unit parent)
        { return AttackArc(parent, "Fling", Rank.Attack, new Price(1, 1), new Range(0, 3), 16); }

        public static Ability Maul(Unit parent)
        { return AttackNeighbor(parent, "Maul", Rank.Attack, new Price(0, 1), 12); }

        public static Ability Sooth(Unit parent)
        {
            return HealNeighbor(parent, "Sooth", new Price(1, 1),
              Filter.Unit + Filter.Owner(parent.Owner, true) + Filter.identity(parent, false),
              10);
        }

    }
}