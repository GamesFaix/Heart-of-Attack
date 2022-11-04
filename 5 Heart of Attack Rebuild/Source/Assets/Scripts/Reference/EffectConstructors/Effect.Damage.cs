using HOA.Resources;
using HOA.To;
using System;

namespace HOA.Ef
{

	public partial class Effect
	{
        public static Effect CorrodeInitial(object source, Args args)
        {
            Effect e = new Effect(source, "Corrode Initial", args);
            e.action = (a) => 
            {
                int cor = Math.Floor(a.value * 0.5f);
                Unit u = a.unit;
                u.Damage(e, a.value);
                AVEffect.Corrode.Play(u);
                u.timers.Add(Timer.Corrosion(e, u, cor));
            };
            return e;
        }
        public static Effect CorrodeResidual(object source, Args args)
        {
            Effect e = new Effect(source, "Corrode Residual", args);
            e.action = (a) =>
            {
                args.unit.StatAdd(e, Stats.Health, 0 - args.value);
                AVEffect.Corrode.Play(args.unit);
            };
            return e;
        }

        public static Effect Damage(object source, Args args)
        {
            Effect e = new Effect(source, "Damage", args);
            e.action = (a) =>
            {
                Unit u = args.unit;
                if (u.Damage(e, args.value))
                    AVEffect.Damage.Play(u);
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }

        public static Effect Pierce(object source, Args args)
        {
            Effect e = new Effect(source, "Pierce", args);
            e.action = (a) =>
            {
                args.unit.StatAdd(e, Stats.Health, 0 - args.value);
                AVEffect.Damage.Play(args.unit);
            };
            return e;
        }

        public static Effect Rage(object source, Args args)
        {
            Effect e = new Effect(source, "Rage", args);
            e.action = (a) =>
            {
                Unit u = args.unit;
                if (u.Damage(e, args.value)) 
                    AVEffect.Damage.Play(u);
                else 
                    AVEffect.Miss.Play(u);
                (e.source.Last<Unit>()).Damage(e, Math.Floor(args.value * 0.5f));
                AVEffect.Damage.Play(e.source.Last<Unit>());
            };
            return e;
        }

        public static Effect Donate(object source, Args args)
        {
            Effect e = new Effect(source, "Donate", args);
            e.action = (a) =>
            {
                Unit u = args.unit;
                int oldHP = u.Health;
                u.StatAdd(e, Stats.Health, args.value);
                AVEffect.StatUp.Play(u);
                int diff = u.Health - oldHP;
                (e.source.Last<Unit>()).Damage(e, diff);
                AVEffect.StatDown.Play(e.source.Last<Unit>());
            };
            return e;
        }

        public static Effect Leech(object source, Args args)
        {
            Effect e = new Effect(source, "Leech", args);
            e.action = (a) =>
            {
                Unit u = args.unit;
                int oldHP = u.Health;
                if (u.Damage(e, args.value))
                {
                    AVEffect.Damage.Play(u);
                    int actualDmg = oldHP - u.Health ;
                    (e.source.Last<Unit>()).StatAdd(e, Stats.Health, actualDmg);
                    AVEffect.StatUp.Play(e.source.Last<Unit>());
                }
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }

        public static Effect ExplosionDummy(object source, Args args)
        {
            Effect e = new Effect(source, "Explosion Dummy", args);
            e.action = (a) => { AVEffect.Explode.Play(args.token); };
            return e;
        }
        public static Effect ExplosionIndividual(object source, Args args)
        {
            Effect e = new Effect(source, "Explosion Individual", args);
            e.action = (a) =>
            {
                Cell c = args.cell;

                Set<IEntity> Targets = c.occupants / Filter.UnitDest;
                if (args.option) 
                    Targets.Remove(e.source.Last<Token>());

                foreach (Token t in Targets)
                {
                    if (t.destructible)
                    {
                        AVEffect.Explode.Play(t);
                        e.Sequence.AddToList(1, Effect.DestroyObstacle(source, new Args(t)));
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

        public static Effect Shock(object source, Args args)
        {
            Effect e = new Effect(source, "Shock", args);
            e.action = (a) =>
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
        public static Effect WaterLog(object source, Args args)
        {
            Effect e = new Effect(source, "WaterLog", args);
            e.action = (a) =>
            {
                args.unit.Damage(e, args.value);
                AVEffect.WaterLog.Play(args.unit);
            };
            return e;
        }

        public static Effect FireInitial(object source, Args args)
        {
            Effect e = new Effect(source, "Fire Initial", args);
            e.action = (a) =>
            {
                Set nextEffects = new Set();
                Token t = args.token;

                if (t.destructible)
                {
                    nextEffects.Add(Effect.DestroyObstacle(source, args));
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
                    nextEffects.Add(Effect.FireSpread(source, new Args(t2, newDmg)));
                Queue.Add(nextEffects);
            };
            return e;
        }

        public static Effect FireSpread(object source, Args args)
        {
            Effect e = new Effect(source, "Fire Spread", args);
            e.action = (a) =>
            {
                Token t = args.token;
                if (t.destructible)
                {
                    AVEffect.Fire.Play(t);
                    Queue.Add(Effect.DestroyObstacle(source, args));
                }

                else if (t is Unit)
                {
                    (t as Unit).StatAdd(e, Stats.Health, 0 - args.value);
                    AVEffect.Fire.Play(t);
                }
            };
            return e;
        }

        public static Effect LaserLine(object source, Args args)
        {
            Effect e = new Effect(source, "Laser Line", args);
            e.action = (a) =>
            {
                Token t = args.token;
                Cell cell = t.Cell;
                int2 direction = Direction.FromCells(cell, e.source.Last<Token>().Cell);

                Set<IEntity> cells = new Set<IEntity>(cell);
                bool stop = false;

                while (!stop)
                {
                    index2 nextIndex = cell.Index - direction;
                    if (Session.Active.board.HasCell(nextIndex, out cell)) 
                        cells.Add(cell);
                    else stop = true;
                }
                Queue.Add(Effect.LaserInitial(source, new Args((Set<IEntity>)cells, args.values)));
            };
            return e;
        }
        public static Effect LaserInitial(object source, Args args)
        {
            Effect e = new Effect(source, "Laser Initial", args);
            e.action = (a) =>
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
        public static Effect LaserSpread(object source, Args args)
        {
            Effect e = new Effect(source, "Laser Spread", args);
            e.action = (a) =>
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