using HOA.Tokens;
using System;

namespace HOA.Abilities
{

    public partial class Ability
    {
        public static Ability Creep(Unit parent)
        { return MoveFocusBoost(parent, "Creep", new Range(0, 1), 1); }

        public static Ability Tread(Unit parent)
        { return MoveFocusBoost(parent, "Tread", new Range(0, 3), -1); }

        public static Ability Defile(Unit parent)
        {
            return Teleport(parent, "Defile", new Price(0,1), 
                Filter.Corpse, new Range(0,5), new Range(0,5));
        }
        public static Ability Dislocate(Unit parent)
        {
            return Teleport(parent, "Dislocate", new Price(1, 1),
                Filter.Owner(parent.Owner, false) + Filter.Unit + Filter.Rank(Tokens.Rank.King, false),
                new Range(0, 5), new Range(0, 5));
        }


        public static Ability Rebuild(Unit parent)
        { return Move(parent, "Rebuild", new Price(0,2), new Range(0, 2)); }


        public static Ability Sprint(Unit parent)
        {
            Ability a = MoveFocusBoost(parent, "Sprint", new Range(0, 0), 1, Rank.Special);
            a.desc = Scribe.Write("Move {0} to target cell. (+{1} range per Focus.)\nLose all Focus.", a.sourceToken, a.values[2]);
            a.MainEffects = Targets =>
            {
                foreach (Set<IEntity> s in Targets)
                    EffectQueue.Add(Effect.Move(a, new EffectArgs(a.sourceToken, s[0] as Cell)));
                EffectQueue.Add(Effect.SetStat(a, new EffectArgs(a.sourceUnit, 0, Stats.Focus)));
            };
            return a;
        }

        public static Ability Burrow(Unit parent)
        {
            Ability a = new Ability(parent, new AbilityArgs("Burrow", Rank.Move, Price.Cheap));
            a.desc = Scribe.Write("Move {0} to Target cell.", a.sourceToken);
            a.Aims += AimStage.MoveArc(a.Aims, new Range(0, 3));
            a.MainEffects = t =>
                EffectQueue.Add(Effect.BurrowStart(a, new EffectArgs(a.sourceToken, (Cell)t[0][0])));
            return a;
        }
    }
}