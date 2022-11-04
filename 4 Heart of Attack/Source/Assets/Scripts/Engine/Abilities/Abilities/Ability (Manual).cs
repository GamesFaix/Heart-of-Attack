using UnityEngine; 
namespace HOA { 

    public partial class Ability
    {
        public static Ability ManualAdd(Stats stat, int change)
        {
            Ability a = new Ability(null, "Manual Add Stat: " + stat + "" + change, 0, Price.Free, 0, change);
            a.manualFree = true;
            a.multiTarget = true;
            a.Desc = () => { return "Increase/Descrease stat of up to 10 units."; };
            a.Stat = stat;
            a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));
            for (int i = 2; i <= 10; i++)
                a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));

            a.PreEffects = () => { };
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(
                        Effect.AddStat(new Source(Roster.Neutral), (Unit)target, a.Stat, a.Modifier));
            };

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }

        public static Ability ManualCreate(Unit parent, EToken tokenType)
        {
            Ability a = new Ability(parent, "", 5, Price.Free, tokenType);
            a.multiTarget = true;

            a.Template = TokenFactory.Template(a.TokenType);
            a.Name = "Manual Create " + a.Template.ID.Name;
            a.Desc = () => { return "Create " + a.Template.ID.Name + " in upto 10 cells."; };
            a.Aims.Add(Aim.Free(TargetFilter.Cell, EPurp.CREATE));
            for (int i = 2; i <= 10; i++)
                a.Aims.Add(Aim.Free(TargetFilter.Cell, EPurp.CREATE));
            a.PreEffects = () => { };
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)target, a.TokenType));
            };

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }
       
        public static Ability ManualDestroy()
        {
            Ability a = new Ability(null, "Manual Destroy", 0, Price.Free);
            a.manualFree = true;
            a.multiTarget = true;
            a.Desc = () => { return "Kill up to 10 tokens."; };
            a.Aims.Add(Aim.Free(TargetFilter.Token, EPurp.ATTACK));
            for (int i = 2; i <= 10; i++)
                a.Aims.Add(Aim.Free(TargetFilter.Token, EPurp.ATTACK));
            a.PreEffects = () => { };
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                {
                    if (target is Unit)
                        EffectQueue.Add(Effect.DestroyUnit(new Source(Roster.Neutral), (Token)target));
                    else
                        EffectQueue.Add(Effect.DestroyObstacle(new Source(Roster.Neutral), (Token)target));
                }
            };

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }
       
        public static Ability ManualEnd()
        {
            Ability a = new Ability(null, "Manual End Turn", 0, Price.Free);
            a.manualFree = true;
            a.Desc = () => { return "End current turn."; };
            a.Aims.Add(Aim.Self());
            a.PreEffects = () => { };
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Advance(new Source(Roster.Neutral), true));

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }

        public static Ability ManualMove()
        {
            Ability a = new Ability(null, "Manual Move", 0, Price.Free);
            a.manualFree = true;
            a.teleport = true;
            a.Desc = () => { return "Move target token to target cell."; };
            a.Aims.Add(Aim.Free(TargetFilter.Token, EPurp.ATTACK));
            a.Aims.Add(Aim.Free(TargetFilter.Cell, EPurp.MOVE));
            a.PreEffects = () => { };
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.TeleportStart(new Source(Roster.Neutral), (Token)targets[0], (Cell)targets[1]));

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }

        public static Ability ManualOwner(Player owner)
        {
            Ability a = new Ability(null, "Manual Set Owner: " + owner, 0, Price.Free);

            a.manualFree = true;
            a.multiTarget = true;

            a.Desc = () => { return "Set owner of up to 10 units."; };
            a.Aims.Add(Aim.Free(TargetFilter.Token, EPurp.ATTACK));
            for (int i = 2; i <= 10; i++)
                a.Aims.Add(Aim.Free(TargetFilter.Token, EPurp.ATTACK));
            a.PreEffects = () => { };
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.SetOwner(new Source(Roster.Neutral), (Token)target, owner));
            };

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }

        public static Ability ManualSet(Stats stat, int newValue)
        {
            Ability a = new Ability(null, "Manual Set Stat: " + stat + "=" + newValue, 0, Price.Free, 0, newValue);
            a.manualFree = true;
            a.multiTarget = true;
            a.Desc = () => { return "Set stat of up to 10 units."; };
            a.Stat = stat;
            a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));
            for (int i = 2; i <= 10; i++)
                a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));
            a.PreEffects = () => { };
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.SetStat(new Source(Roster.Neutral), (Unit)target, a.Stat, a.Modifier));
            };

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }

        public static Ability ManualShift(int change)
        {
            Ability a = new Ability(null, "Manual Shift: " + change, 0, Price.Free, 0, change);
            a.multiTarget = true;
            a.manualFree = true;
            a.Desc = () => { return "Move up to 10 units in the queue."; };
            a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));
            for (int i = 2; i <= 10; i++)
                a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));

            a.PreEffects = () => { };
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Shift(new Source(Roster.Neutral), (Unit)target, a.Modifier));
            };

            a.Legal = delegate(out string message)
            {
                message = a.Name + " currently legal.";
                if (EffectQueue.Processing)
                {
                    message = "Another action is currently in progress.";
                    return false;
                }
                return true;
            };
            return a;
        }
        



    }
}
