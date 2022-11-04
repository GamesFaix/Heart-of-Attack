using UnityEngine;
using System;

namespace HOA { 

    public partial class Timer {

        //This file is just for specific Timer constructors.

        public static Timer ActiveGrenade(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Active Grenade";
            t.Desc = () => { return t.Modifier.ToString() + " explosive damage at end of " + t.Parent + "'s next turn."; };
            t.Modifier = 10;
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => { EffectQueue.Add(Effect.ExplosionSequence(t.Source, t.Parent.Body.Cell, t.Modifier, false)); };
            return t;
        }

        public static Timer Altaration(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Blood Altaration";
            t.Desc = () => { return "Initiative +" + t.Modifier + ". (" + t.Turns + " turns left.)"; };
            t.Turns = 2;
            t.Modifier = 4;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => { ((Unit)t.Parent).AddStat(new Source(t.Parent), Stats.Initiative, (0 - t.Modifier)); };
            return t;
        }

        public static Timer ArcticGust(Source source, Unit parent, int modifier, Ability ability)
        {
            Timer t = new Timer(source, parent, modifier, ability);
            t.Name = "Arctic Gusted";
            t.Desc = () => { return "Move range " + (0 - t.Modifier).ToString() + ". (" + t.Turns + " turns left.)"; };
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                if (((Unit)t.Parent).Arsenal.Move != default(Ability))
                {
                    Ability move = ((Unit)t.Parent).Arsenal.Move;
                    Aim aim = move.Aims[0];
                    aim.Range += t.Modifier;
                }
            };
            return t;
        }

        public static Timer Bombing(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Targeted";
            t.Desc = () => { return t.Modifier.ToString() + " explosive damage at end of " + t.Parent + "'s turn."; };
            t.Turns = 1;
            t.Modifier = 10;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.ExplosionSequence(t.Source, t.Parent.Body.Cell, t.Modifier, false));
                t.Turns++;
            };
            return t;
        }

        public static Timer Corrosion(Source source, Unit parent, int damage)
        {
            Timer t = new Timer(source, parent, damage);
            t.Name = "Corrosion";
            t.Desc = () => { return t.Modifier + " damage at end of turn. (50% next turn.)"; };
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.CorrodeResidual(t.Source, ((Unit)t.Parent), t.Modifier));
                t.Modifier = (int)Math.Floor(t.Modifier * 0.5f);
                if (t.Modifier > 0) { t.Turns++; }
            };
            return t;

        }

        public static Timer Cursed(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Cursed";
            t.Desc = () => { return t.Modifier.ToString() + " damage at end of " + t.Parent + "'s turn."; };
            t.Turns = 1;
            t.Modifier = 2;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.Damage(t.Source, ((Unit)t.Parent), t.Modifier));
                t.Turns++;
            };
            return t;
        }

        public static Timer Exhaust(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Exhausted";
            t.Desc = () => { return t.Modifier.ToString() + " damage at end of " + t.Parent + "'s turn."; };
            t.Turns = 1;
            t.Modifier = 5;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.Damage(t.Source, ((Unit)t.Parent), t.Modifier));
                t.Turns++;
            };
            return t;
        }

        public static Timer IceBlast(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Ice Blasted";
            t.Desc = () => { return "Initiative " + t.Modifier + ". (" + t.Turns + " turns left.)"; };
            t.Turns = 2;
            t.Modifier = (-2);
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => { ((Unit)t.Parent).AddStat(t.Source, Stats.Initiative, (0 - t.Modifier)); };
            return t;
        }

        public static Timer Incineration(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Incinerating";
            t.Desc = () => { return t.Modifier.ToString() + " damage at end of " + t.Parent + "'s turn."; };
            t.Turns = 1;
            t.Modifier = 7;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.FireInitial(t.Source, t.Parent, t.Modifier));
                t.Turns++;
            };
            return t;
        }

        public static Timer Petrified(Source source, Unit parent, Ability ability)
        {
            Timer t = new Timer(source, parent, ability);
            t.Name = "Petrified";
            t.Desc = () => { return "Cannot move. (" + t.Turns + " turns left.)"; };
            t.Turns = 1;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                ((Unit)t.Parent).Arsenal.Add(t.Ability);
                ((Unit)t.Parent).Arsenal.Sort();
            };
            return t;
        }

        public static Timer Stunned(Source source, Unit parent, int turns)
        {
            Timer t = new Timer(source, parent, turns);
            t.Name = "Stunned";
            t.Desc = () => { return t.Parent.ID.FullName + " cannot advance in the Queue for " + t.Turns + " global turns."; };
            t.Turns = turns;
            t.Test = t.EveryTurnTest;
            t.Activate = () => { EffectQueue.Add(Effect.Shift(t.Source, t.Parent as Unit, 1)); };
            return t;
        }


        public static Timer TimeBomb(Source source, Unit parent, int modifier)
        {
            Timer t = new Timer(source, parent, modifier);
            t.Name = "Time Bombed";
            t.Desc = () => { return "Initiative -" + t.Modifier + ". (" + t.Turns + " turns left.)"; };
            t.Turns = 2;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => { ((Unit)t.Parent).AddStat(t.Source, Stats.Initiative, t.Modifier); };
            return t;
        }

        public static Timer TimeSlam(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Time Slammed";
            t.Desc = () => { return "Initiative " + t.Modifier + ". (" + t.Turns + " turns left.)"; };
            t.Turns = 2;
            t.Modifier = (-2);
            t.Test = t.ParentTurnEndTest;
            t.Activate = () => { ((Unit)t.Parent).AddStat(t.Source, Stats.Initiative, (0 - t.Modifier)); };
            return t;

        }

        public static Timer WaterLogged(Source source, Unit parent)
        {
            Timer t = new Timer(source, parent);
            t.Name = "Waterlogged";
            t.Desc = () => { return t.Modifier.ToString() + " damage at end of " + t.Parent + "'s turn."; };
            t.Turns = 1;
            t.Modifier = 5;
            t.Test = t.ParentTurnEndTest;
            t.Activate = () =>
            {
                EffectQueue.Add(Effect.WaterLog(t.Source, ((Unit)t.Parent), t.Modifier));
                t.Turns++;
            };
            return t;
        }
    }
}
