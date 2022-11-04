using HOA.Resources;
using HOA.Tokens;
using System;

namespace HOA.Abilities
{

	public partial class Effect
	{
        public static Effect CorrodeInitial(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Corrode Initial", user, args);
            e.Process = () =>
            {
                int cor = Math.Floor(args.value * 0.5f);
                Unit u = args.unit;
                u.Damage(e, args.value);
                AVEffect.Corrode.Play(u);
                u.timers.Add(Timer.Corrosion(e, u, cor));
            };
            return e;
        }
        public static Effect CorrodeResidual(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Corrode Residual", user, args);
            e.Process = () =>
            {
                args.unit.StatAdd(e, Stats.Health, 0 - args.value);
                AVEffect.Corrode.Play(args.unit);
            };
            return e;
        }

        public static Effect Damage(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Damage", user, args);
            e.Process = () =>
            {
                Unit u = args.unit;
                if (u.Damage(e, args.value))
                    AVEffect.Damage.Play(u);
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }
        
        public static Effect Pierce(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Pierce", user, args);
            e.Process = () =>
            {
                args.unit.StatAdd(e, Stats.Health, 0 - args.value);
                AVEffect.Damage.Play(args.unit);
            };
            return e;
        }
        
        public static Effect Rage(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Rage", user, args);
            e.Process = () =>
            {
                Unit u = args.unit;
                if (u.Damage(e, args.value)) 
                    AVEffect.Damage.Play(u);
                else 
                    AVEffect.Miss.Play(u);
                e.userUnit.Damage(e, Math.Floor(args.value * 0.5f));
                AVEffect.Damage.Play(e.userUnit);
            };
            return e;
        }
        
        public static Effect Donate(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Donate", user, args);
            e.Process = () =>
            {
                Unit u = args.unit;
                int oldHP = u.Health;
                u.StatAdd(e, Stats.Health, args.value);
                AVEffect.StatUp.Play(u);
                int diff = u.Health - oldHP;
                e.userUnit.Damage(e, diff);
                AVEffect.StatDown.Play(e.userUnit);
            };
            return e;
        }
        
        public static Effect Leech(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Leech", user, args);
            e.Process = () =>
            {
                Unit u = args.unit;
                int oldHP = u.Health;
                if (u.Damage(e, args.value))
                {
                    AVEffect.Damage.Play(u);
                    int actualDmg = oldHP - u.Health ;
                    e.userUnit.StatAdd(e, Stats.Health, actualDmg);
                    AVEffect.StatUp.Play(e.userUnit);
                }
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }

        public static Effect ExplosionDummy(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Explosion Dummy", user, args);
            e.Process = () => { AVEffect.Explode.Play(args.token); };
            return e;
        }
        public static Effect ExplosionIndividual(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Explosion Individual", user, args);
            e.Process = () =>
            {
                Cell c = args.cell;

                Set<IEntity> Targets = c.occupants / Filter.UnitDest;
                if (args.option) 
                    Targets.Remove(e.userToken);

                foreach (Token t in Targets)
                {
                    if (t.destructible)
                    {
                        AVEffect.Explode.Play(t);
                        e.Sequence.AddToList(1, Effect.DestroyObstacle(user, new EffectArgs(t)));
                    }

                    else if (t is Unit)
                    {
                        Unit u = (Unit)t;
                        if (u.Damage(e, args.value)) 
                            AVEffect.Explode.Play(t);
                        else 
                            AVEffect.Miss.Play(t);
                    }
                }
            };
            return e;
        }
        
        public static Effect Shock(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Shock", user, args);
            e.Process = () =>
            {
                Unit u = args.unit;
                int dmg = args.values[0];
                int stun = args.values[1];
                u.Damage(e, dmg);
                u.timers.Add(Timer.Stunned(e, u, stun));
                AVEffect.Stun.Play(u);
            };
            return e;
        }
        public static Effect WaterLog(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("WaterLog", user, args);
            e.Process = () =>
            {
                args.unit.Damage(e, args.value);
                AVEffect.WaterLog.Play(args.unit);
            };
            return e;
        }

        public static Effect FireInitial(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Fire Initial", user, args);
            e.Process = () =>
            {
                EffectSet nextEffects = new EffectSet();
                Token t = args.token;

                if (t.destructible)
                {
                    nextEffects.Add(Effect.DestroyObstacle(user, args));
                    AVEffect.Fire.Play(t);
                }
                else if (t is Unit)
                {
                    (t as Unit).StatAdd(e, Stats.Health, 0 - args.value);
                    AVEffect.Fire.Play(t);
                }

                Set<IEntity> neighbors = t.NeighborsAndCellmates / Filter.UnitDest;

                int newDmg = Math.Floor(args.value * 0.5f);
                foreach (Token t2 in neighbors)
                    nextEffects.Add(Effect.FireSpread(user, new EffectArgs(t2, newDmg)));
                EffectQueue.Add(nextEffects);
            };
            return e;
        }

        public static Effect FireSpread(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Fire Spread", user, args);
            e.Process = () =>
            {
                Token t = args.token;
                if (t.destructible)
                {
                    AVEffect.Fire.Play(t);
                    EffectQueue.Add(Effect.DestroyObstacle(user, args));
                }

                else if (t is Unit)
                {
                    (t as Unit).StatAdd(e, Stats.Health, 0 - args.value);
                    AVEffect.Fire.Play(t);
                }
            };
            return e;
        }

        public static Effect LaserLine(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Laser Line", user, args);
            e.Process = () =>
            {
                Token t = args.token;
                Cell cell = t.Cell;
                int2 direction = Direction.FromCells(cell, e.userToken.Cell);

                Set<IEntity> cells = new Set<IEntity>(cell);
                bool stop = false;

                while (!stop)
                {
                    index2 nextIndex = cell.Index - direction;
                    if (Session.Active.board.HasCell(nextIndex, out cell)) 
                        cells.Add(cell);
                    else stop = true;
                }
                EffectQueue.Add(Effect.LaserInitial(user, new EffectArgs((Set<IEntity>)cells, args.values)));
            };
            return e;
        }
        public static Effect LaserInitial(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Laser Initial", user, args);
            e.Process = () =>
            {
                int currentDmg = args.values[0];
                int decayPercent = args.values[1];
                foreach (Cell cell in args.cells)
                {
                    Set<IEntity> units = cell.occupants / Filter.Unit;
                    foreach (Unit u in units)
                    {
                        if (u.Damage(e, currentDmg)) 
                            AVEffect.Laser.Play(u);
                        else 
                            AVEffect.Miss.Play(u);
                    }
                    Predicate<IEntity> f = Filter.Ob + Filter.Plane(Plane.Sunken, false);

                    if ((cell.occupants / f).Count > 0) 
                        return;
                    if (units.Count > 0)
                        currentDmg = Math.Floor(currentDmg * (decayPercent / 100));
                }
            };
            return e;
        }
        public static Effect LaserSpread(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Laser Spread", user, args);
            e.Process = () =>
            {
                if (args.unit.Damage(e, args.value)) 
                    AVEffect.Laser.Play(args.token);
                else 
                    AVEffect.Miss.Play(args.token);
            };
            return e;
        }
    
    }
}