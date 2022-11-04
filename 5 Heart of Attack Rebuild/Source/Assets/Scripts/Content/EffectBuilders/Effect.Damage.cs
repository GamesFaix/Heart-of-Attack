using HOA.Resources;
using HOA.Tokens;
using System;
using HOA.Args;
using Cell = HOA.Board.Cell;
using Direction = HOA.Board.Direction;
using Session = HOA.Sessions.Session;
using HOA.Collections;
using Farg = HOA.Args.Arg;

namespace HOA.Effects
{

	public partial class Effect
	{
        public static Effect CorrodeInitial(object source, EffectArgs args)
        {
            var e = new Effect(source, "Corrode Initial", args);
            e.action = (a) => 
            {
                sbyte cor = (sbyte)Math.Floor(a[RN.Damage] * 0.5f);
                Unit u = a[RT.Damaged] as Unit;
                u.Damage(e, a[RN.Damage]);
                AVEffect.Corrode.Play(u);
                u.timers.Add(Timer.Corrosion(e, u, cor));
            };
            return e;
        }
        public static Effect CorrodeResidual(object source, EffectArgs args)
        {
            var e = new Effect(source, "Corrode Residual", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit;
                u.StatAdd(e, RS.Health, (sbyte)(0 - a[RN.Damage]));
                AVEffect.Corrode.Play(u);
            };
            return e;
        }

        public static Effect Damage(object source, EffectArgs args)
        {
            var e = new Effect(source, "Damage", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit;
                if (u.Damage(e, a[RN.Damage]))
                    AVEffect.Damage.Play(u);
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }

        public static Effect Pierce(object source, EffectArgs args)
        {
            var e = new Effect(source, "Pierce", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit; 
                u.StatAdd(e, RS.Health, (sbyte)(0 - a[RN.Damage]));
                AVEffect.Damage.Play(u);
            };
            return e;
        }

        public static Effect Rage(object source, EffectArgs args)
        {
            var e = new Effect(source, "Rage", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit;
                if (u.Damage(e, a[RN.Damage])) 
                    AVEffect.Damage.Play(u);
                else 
                    AVEffect.Miss.Play(u);
                (e.source.Last<Unit>()).Damage(e, (sbyte)Math.Floor(a[RN.Damage] * 0.5f));
                AVEffect.Damage.Play(e.source.Last<Unit>());
            };
            return e;
        }

        public static Effect Donate(object source, EffectArgs args)
        {
            var e = new Effect(source, "Donate", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Unit] as Unit;
                sbyte oldHP = u.health;
                u.StatAdd(e, RS.Health, a[RN.Amount]);
                AVEffect.StatUp.Play(u);
                sbyte diff = (sbyte)(u.health - oldHP);
                (e.source.Last<Unit>()).Damage(e, diff);
                AVEffect.StatDown.Play(e.source.Last<Unit>());
            };
            return e;
        }

        public static Effect Leech(object source, EffectArgs args)
        {
            var e = new Effect(source, "Leech", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit;
                sbyte oldHP = u.health;
                if (u.Damage(e, args[RN.Damage]))
                {
                    AVEffect.Damage.Play(u);
                    sbyte actualDmg = (sbyte)(oldHP - u.health) ;
                    (e.source.Last<Unit>()).StatAdd(e, RS.Health, actualDmg);
                    AVEffect.StatUp.Play(e.source.Last<Unit>());
                }
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }

        public static Effect ExplosionDummy(object source, EffectArgs args)
        {
            var e = new Effect(source, "Explosion Dummy", args);
            e.action = (a) => { AVEffect.Explode.Play(args[RT.Token]); };
            return e;
        }
        public static Effect ExplosionIndividual(object source, EffectArgs args)
        {
            var e = new Effect(source, "Explosion Individual", args);
            e.action = (a) =>
            {
                Set<IEntity> Targets = 
                    (a[RT.Location] as Cell).occupants 
                    / Filter.UnitDest;
                if (a[RO.ExcludeSelf]) 
                    Targets.Remove(e.source.Last<Token>());

                foreach (Token t in Targets)
                {
                    if (t.destructible)
                    {
                        AVEffect.Explode.Play(t);
                        e.Sequence.AddToList(1, Effect.DestroyObstacle(source, new EffectArgs(
                            Arg.Target(RT.Token, t))));
                    }

                    else if (t is Unit)
                    {
                        Unit u = (Unit)t;
                        if (u.Damage(e, a[RN.Damage])) 
                            AVEffect.Explode.Play(t);
                        else 
                            AVEffect.Miss.Play(t);
                    }
                }
            };
            return e;
        }

        public static Effect Shock(object source, EffectArgs args)
        {
            var e = new Effect(source, "Shock", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit;
                u.Damage(e, a[RN.Damage]);
                u.timers.Add(Timer.Stunned(e, u, a[RN.Stun]));
                AVEffect.Stun.Play(u);
            };
            return e;
        }
        public static Effect WaterLog(object source, EffectArgs args)
        {
            var e = new Effect(source, "WaterLog", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit; 
                u.Damage(e, a[RN.Damage]);
                AVEffect.WaterLog.Play(u);
            };
            return e;
        }

        public static Effect FireInitial(object source, EffectArgs args)
        {
            var e = new Effect(source, "Fire Initial", args);
            e.action = (a) =>
            {
                var nextEffects = new EffectSet();
                Token t = a[RT.Damaged] as Token;

                if (t.destructible)
                {
                    nextEffects.Add(Effect.DestroyObstacle(source, args));
                    AVEffect.Fire.Play(t);
                }
                else if (t is Unit)
                {
                    (t as Unit).StatAdd(e, RS.Health, (sbyte)(0 - a[RN.Damage]));
                    AVEffect.Fire.Play(t);
                }

                Set<IEntity> neighbors = t.neighborsAndCellmates / Filter.UnitDest;

                sbyte newDmg = (sbyte)Math.Floor(a[RN.Decay] * 0.5f);
                foreach (Token t2 in neighbors)
                    nextEffects.Add(Effect.FireSpread(source, new EffectArgs(
                        Arg.Target(RT.Damaged, t2), 
                        Farg.Num(RN.Damage, newDmg))));
                EffectQueue.Add(nextEffects);
            };
            return e;
        }

        public static Effect FireSpread(object source, EffectArgs args)
        {
            var e = new Effect(source, "Fire Spread", args);
            e.action = (a) =>
            {
                Token t = a[RT.Damaged] as Token;
                if (t.destructible)
                {
                    AVEffect.Fire.Play(t);
                    EffectQueue.Add(Effect.DestroyObstacle(source, args));
                }

                else if (t is Unit)
                {
                    (t as Unit).StatAdd(e, RS.Health, (sbyte)(0 - a[RN.Damage]));
                    AVEffect.Fire.Play(t);
                }
            };
            return e;
        }

        public static Effect LaserLine(object source, EffectArgs args)
        {
            var e = new Effect(source, "Laser Line", args);
            e.action = (a) =>
            {
                Token t = a[RT.Damaged] as Token;
                Cell cell = t.cell;
                int2 direction = Direction.FromCells(cell, e.source.Last<Token>().cell);

                var cells = new Set<IEntity>(cell);
                bool stop = false;

                while (!stop)
                {
                    index2 nextIndex = cell.Index - direction;
                    if (Session.Active.board.HasCell(nextIndex, out cell)) 
                        cells.Add(cell);
                    else stop = true;
                }
                EffectQueue.Add(Effect.LaserInitial(source, new EffectArgs(
                    cells, 
                    Farg.Num(RN.Damage, a[RN.Damage]))));
            };
            return e;
        }
        public static Effect LaserInitial(object source, EffectArgs args)
        {
            var e = new Effect(source, "Laser Initial", args);
            e.action = (a) =>
            {
                sbyte currentDmg = a[RN.Damage];
                foreach (Cell cell in a.targetBatch)
                {
                    Set<IEntity> units = cell.occupants / Filter.Unit;
                    foreach (Unit u in units)
                    {
                        if (u.Damage(e, currentDmg)) 
                            AVEffect.Laser.Play(u);
                        else 
                            AVEffect.Miss.Play(u);
                    }
                    Predicate<IEntity> f = Filter.Ob  + Filter.Plane(Plane.Sunken, false);

                    if ((cell.occupants / f).Count > 0) 
                        return;
                    if (units.Count > 0)
                        currentDmg = (sbyte)Math.Floor(currentDmg * (a[RN.Decay] / 100));
                }
            };
            return e;
        }
        public static Effect LaserSpread(object source, EffectArgs args)
        {
            var e = new Effect(source, "Laser Spread", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Damaged] as Unit;
                if (u.Damage(e, a[RN.Damage])) 
                    AVEffect.Laser.Play(u);
                else 
                    AVEffect.Miss.Play(u);
            };
            return e;
        }
    
    }
}