using System;

namespace HOA.Ab.Aim
{

    public partial class Plan
    {
        public static Plan MovePath(Ability ability, Range<byte> range)
        {
            Plan plan = new Plan(ability);
            plan += Stage.Self(plan);
            if (range.max > 0)
                plan += Stage.MoveNeighbor(plan);
            for (int i = 1; i <= range.max; i++)
                plan += Stage.MoveNeighborFromLast(plan);
            for (int i = 0; i < plan.Count; i++)
            {
                if (i < range.min)
                    plan[i].selectionCount = Range.sb(1, 1);
                else
                    plan[i].selectionCount = Range.sb(0, 1);
            }
            return plan;
        }

        public static Plan Melee(Ability ability, Predicate<IEntity> filter)
        {
            Plan a = new Plan(ability);
            a += Stage.AttackNeighbor(a, filter);
            return a;
        }

        public static Plan Self(Ability ability)
        {
            Plan a = new Plan(ability);
            a += Stage.Self(a);
            return a;
        }
    }
}