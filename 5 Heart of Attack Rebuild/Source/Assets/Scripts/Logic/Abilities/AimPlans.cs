using System;

namespace HOA.Abilities
{

    public partial class AimPlan
    {
        public static AimPlan MovePath(Ability ability, Range<byte> range)
        {
            AimPlan plan = new AimPlan(ability);
            plan += AimStage.Self(plan);
            if (range.max > 0)
                plan += AimStage.MoveNeighbor(plan);
            for (int i = 1; i <= range.max; i++)
                plan += AimStage.MoveNeighborFromLast(plan);
            for (int i = 0; i < plan.Count; i++)
            {
                if (i < range.min)
                    plan[i].selectionCount = Range.sb(1, 1);
                else
                    plan[i].selectionCount = Range.sb(0, 1);
            }
            return plan;
        }

        public static AimPlan Melee(Ability ability, Predicate<IEntity> filter)
        {
            AimPlan a = new AimPlan(ability);
            a += AimStage.AttackNeighbor(a, filter);
            return a;
        }

        public static AimPlan Self(Ability ability)
        {
            AimPlan a = new AimPlan(ability);
            a += AimStage.Self(a);
            return a;
        }
    }
}