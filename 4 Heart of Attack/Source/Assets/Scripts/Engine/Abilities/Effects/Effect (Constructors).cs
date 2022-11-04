using System;
using UnityEngine;

namespace HOA

{
    public partial class Effect
    {
        #region //Create & Destroy
        public static Effect Create(Source source, Cell Target, Species Species)
        {
            Effect e = new Effect("Create", source, Target, Species);
            e.Process = () =>
            {
                Token newToken = TokenFactory.Create(e.Source, e.Species, (Cell)e.Target);
                AVEffect.Birth.Play(newToken);
            };
            return e;
        }
        public static Effect DestroyCleanUp(Source source, Token Target)
        {
            Effect e = new Effect("Destroy Clean Up", source, Target);
            e.Process = () => { ((Token)e.Target).Destroy(e.Source, true, true); };
            return e;
        }
        public static Effect DestroyObstacle(Source source, Token Target)
        {
            Effect e = new Effect("Destroy Obstacle", source, Target);
            e.Process = () =>
            {
                AVEffect.Destruct.Play(e.Target);
                if (e.Source.Sequence == default(EffectSeq))
                    EffectQueue.Add(Effect.DestroyCleanUp(e.Source, (Token)e.Target));
                else
                    e.Source.Sequence.AddToNext(Effect.DestroyCleanUp(e.Source, (Token)e.Target));
            };
            return e;
        }
        public static Effect DestroyUnit(Source source, Token Target)
        {
            Effect e = new Effect("Destroy Unit", source, Target);
            e.Process = () =>
            {
                AVEffect.Death.Play(e.Target);
                if (e.Source.Sequence == default(EffectSeq))
                    EffectQueue.Add(Effect.DestroyCleanUp(e.Source, (Token)e.Target));
                else
                    e.Source.Sequence.AddToNext(Effect.DestroyCleanUp(e.Source, (Token)e.Target));
            };
            return e;
        }
        public static EffectSeq Detonate(Source source, Token Target)
        {
            return new Effects.Detonate(source, Target);
        }

        public static Effect Detonate2(Source source, Token Target)
        {
            Effect e = new Effect("Detonate2", source, Target);
            e.Process = () =>
            {
                AVEffect.Detonate.Play(e.Target);
                e.Source.Sequence.AddToNext(Effect.DestroyCleanUp(e.Source, (Token)e.Target));
            };
            return e;
        }
        public static Effect GetHeart(Source source, Token Target)
        {
            Effect e = new Effect("Get Heart", source, Target);
            e.Process = () =>
            {
                EffectSet effects = new EffectSet();
                foreach (Token token in ((Token)e.Target).Owner.Tokens)
                    effects.Add(Effect.SetOwner(e.Source, token, e.Source.Player));
                AVEffect.GetHeart.Play(e.Target);
                GameLog.Out(e.Source.Player.ToString() + " acquired the " + e.Target.ToString());
                effects.Add(Effect.DestroyCleanUp(e.Source, (Unit)e.Target));
                EffectQueue.Add(effects);
            };
            return e;
        }
        public static Effect Replace(Source source, Token Target, Species Species)
        {
            Effect e = new Effect("Replace", source, Target, Species);
            e.Process = () =>
            {
                Token token = (Token)e.Target;
                Cell cell = token.Body.Cell;
                token.Destroy(e.Source, false, false);
                TokenFactory.Create(e.Source, e.Species, cell, false);
            };
            return e;
        }
       
        #endregion

        #region //Queue
  
        public static Effect Advance(Source source, bool log)
        {
            Effect e = new Effect("Advance", source);
            e.Process = () =>
            {
                TurnQueue.Advance();
                if (e.Flag) 
                    GameLog.Out(e.Source + " ended the turn.");
                AVEffect.Advance.PlayNonLocal();
            };
            return e;
        }
        public static Effect Initialize(Source source)
        {
            Effect e = new Effect("Initialize", source);
            e.Process = () => { TurnQueue.Initialize(); };
            return e;
        }
        public static Effect Shift(Source source, Unit Target, int distance)
        {
            Effect e = new Effect("Shift", source, Target, distance);
            e.Process = () =>
            {
                Unit u = (Unit)e.Target;
                int d = e.Modifier;
                TurnQueue.Shift(u, d);
                if (d < 0)
                {
                    GameLog.Out(u + " shifted up " + d + " slot(s) in the Queue.");
                    AVEffect.StatUp.Play(u);
                }
                else if (d > 0)
                {
                    GameLog.Out(u + " shifted down " + d + " slot(s) in the Queue.");
                    AVEffect.StatDown.Play(u);
                }
                else
                {
                    GameLog.Out(u + " shifted down 0 slots in the Queue\n...or shifted up 0 slots.");
                }
            };
            return e;
        }
        public static Effect Shuffle(Source source)
        {
            Effect e = new Effect("Shuffle", source);
            e.Process = () =>
            {
                TurnQueue.Shuffle();
                GameLog.Out(e.Source + " shuffled the Queue.");
                AVEffect.Shuffle.PlayNonLocal();
            };
            return e;
        }

        #endregion


        #region //Movement
        
        public static Effect BurrowStart(Source source, Token mover, Cell destination)
        {
            Effect e = new Effect("Burrow Start", source, new Target[2] { mover, destination });
            e.Process = () =>
            {
                Token t = (Token)e.Targets[0];
                Cell c = (Cell)e.Targets[1];
                Cell oldCell = t.Body.Cell;
                AVEffect.Burrow.Play(t);
                EffectQueue.Add(Effect.BurrowFinish(e.Source, t, c));
                GameLog.Out(t + " burrowed from " + oldCell + " to " + c + ".");
            };
            return e;
        }
        public static Effect BurrowFinish(Source source, Token mover, Cell destination)
        {
            Effect e = new Effect("Burrow Finish", source, new Target[2] { mover, destination });
            e.Process = () =>
            {
                ((Token)e.Targets[0]).Body.Enter(((Cell)e.Targets[1]), false);
                AVEffect.Burrow.Play(e.Targets[0]);
            };
            return e;
        }
        public static Effect Move(Source source, Token mover, Cell destination)
        {
            Effect e = new Effect("Move", source, new Target[2] { mover, destination });
            e.Process = () =>
            {
                Token t = (Token)e.Targets[0];
                Cell oldCell = t.Body.Cell;
                //	Target.SpriteMove(cell);
                t.Body.Enter(((Cell)e.Targets[1]), true);
                Cell newCell = t.Body.Cell;
                if (t.Plane.ContainsAny(Plane.Ground)) 
                    AVEffect.Walk.Play(t);
                else if (t.Plane.ContainsAny(Plane.Air)) 
                    AVEffect.Fly.Play(t);
                GameLog.Out(e.Target + " moved from " + oldCell + " to " + newCell + ".");
            };
            return e;
        }
        public static Effect Swap(Source source, Token Target1, Token Target2)
        {
            Effect e = new Effect("Swap", source, new Target[2] { Target1, Target2 });
            e.Process = () =>
            {
                Token t1 = (Token)e.Targets[0];
                Token t2 = (Token)e.Targets[1];
                //	Target.SpriteMove(other.Cell);
                //	other.SpriteMove(Target.Cell);
                t1.Body.Swap(t2);
                GameLog.Out(t1 + " swapped places with " + t2 + ".");
            };
            return e;
        }
        public static Effect TeleportStart(Source source, Token mover, Cell destination)
        {
            Effect e = new Effect("Teleport Start", source, new Target[2] { mover, destination });
            e.Process = () =>
            {
                Token t = (Token)e.Targets[0];
                Cell c = (Cell)e.Targets[1];
                Cell oldCell = t.Body.Cell;
                AVEffect.Teleport.Play(t);
                EffectQueue.Add(Effect.TeleportFinish(e.Source, t, c));

                GameLog.Out(e.Source.Token + " teleported " + t + " from " + oldCell + " to " + c + ".");
            };
            return e;
        }
        public static Effect TeleportFinish(Source source, Token mover, Cell destination)
        {
            Effect e = new Effect("Teleport Finish", source, new Target[2] { mover, destination });
            e.Process = () =>
            {
                ((Token)e.Targets[0]).Body.Enter((Cell)e.Targets[1], false);
                AVEffect.Teleport.Play(e.Targets[0]);
            };
            return e;
        }

        #endregion

        #region //Damage
        
        public static Effect CorrodeInitial(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Corrode Initial", source, Target, damage);
            e.Process = () =>
            {
                int cor = (int)Mathf.Floor(e.Modifier * 0.5f);
                Unit u = (Unit)e.Target;
                u.Damage(e.Source, e.Modifier);
                AVEffect.Corrode.Play(e.Target);
                u.timers.Add(Timer.Corrosion(e.Source, u, cor));
            };
            return e;
        }
        public static Effect CorrodeResidual(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Corrode Residual", source, Target, damage);
            e.Process = () =>
            {
                ((Unit)e.Target).AddStat(e.Source, Stats.Health, 0 - e.Modifier);
                AVEffect.Corrode.Play(e.Target);
            };
            return e;
        }

        public static Effect Damage(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Damage", source, Target, damage);
            e.Process = () =>
            {
                if (((Unit)e.Target).Damage(e.Source, e.Modifier))
                    AVEffect.Damage.Play(e.Target);
                else AVEffect.Miss.Play(e.Target);
            };
            return e;
        }
        public static Effect Pierce(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Pierce", source, Target, damage);
            e.Process = () =>
            {
                ((Unit)e.Target).AddStat(e.Source, Stats.Health, 0 - e.Modifier);
                AVEffect.Damage.Play(e.Target);
            };
            return e;
        }
        public static Effect Rage(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Rage", source, Target, damage);
            e.Process = () =>
            {
                Unit u = (Unit)e.Target;
                if (u.Damage(e.Source, e.Modifier)) AVEffect.Damage.Play(u);
                else AVEffect.Miss.Play(u);
                Unit Parent = (Unit)e.Source.Token;
                Parent.Damage(e.Source, (int)Mathf.Floor(e.Modifier * 0.5f));
                AVEffect.Damage.Play(Parent);
            };
            return e;
        }
        public static Effect Donate(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Donate", source, Target, damage);
            e.Process = () =>
            {
                Unit u = (Unit)e.Target;
                int oldHP = u.HP;
                u.AddStat(e.Source, Stats.Health, e.Modifier);
                AVEffect.StatUp.Play(u);
                int diff = u.HP - oldHP;
                Unit Parent = (Unit)(e.Source.Token);
                Parent.Damage(e.Source, diff);
                AVEffect.StatDown.Play(Parent);
            };
            return e;
        }
        public static Effect Leech(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("Leech", source, Target, damage);
            e.Process = () =>
            {
                Unit u = (Unit)e.Target;
                int oldHP = u.HP;
                if (u.Damage(e.Source, e.Modifier))
                {
                    AVEffect.Damage.Play(u);
                    int actualDmg = oldHP - u.HP;
                    Unit Parent = (Unit)(e.Source.Token);
                    Parent.AddStat(e.Source, Stats.Health, actualDmg);
                    AVEffect.StatUp.Play(Parent);
                }
                else AVEffect.Miss.Play(u);
            };
            return e;
        }
        
        public static Effect ExplosionDummy(Source source, Cell Target)
        {
            Effect e = new Effect("Explosion Dummy", source, Target);
            e.Process = () => { AVEffect.Explode.Play(e.Target); };
            return e;
        }
        public static Effect ExplosionIndividual(Source source, Cell Target, int damage, bool selfImmune)
        {
            Effect e = new Effect("Explosion Individual", source, Target, damage, selfImmune);
            e.Process = () =>
            {
                Cell c = (Cell)e.Target;

                TokenSet Targets = c.Occupants - TargetFilter.UnitDest;
                if (e.Flag) Targets.Remove(e.Source.Token);

                foreach (Token t in Targets)
                {
                    if (t.Body.Destructible)
                    {
                        AVEffect.Explode.Play(t);
                        e.Source.Sequence.AddToNext(Effect.DestroyObstacle(e.Source, t));
                    }

                    else if (t is Unit)
                    {
                        Unit u = (Unit)t;
                        if (u.Damage(e.Source, e.Modifier)) AVEffect.Explode.Play(t);
                        else AVEffect.Miss.Play(t);
                    }
                }
            };
            return e;
        }
        public static EffectSeq ExplosionSequence(Source source, Cell Target, int damage, bool selfImmune)
        {
            return new Effects.Explosion(source, Target, damage, selfImmune);
        }

        public static Effect Shock(Source source, Unit Target, int damage, int stun)
        {
            Effect e = new Effect("Shock", source, Target, new int[2] { damage, stun });
            e.Process = () =>
            {
                Unit u = (Unit)e.Target;
                u.Damage(e.Source, e.Modifier);
                u.timers.Add(Timer.Stunned(e.Source, u, stun));
                AVEffect.Stun.Play(u);
            };
            return e;
        }
        public static Effect WaterLog(Source source, Unit Target, int damage)
        {
            Effect e = new Effect("WaterLog", source, Target, damage);
            e.Process = () =>
            {
                ((Unit)e.Target).Damage(e.Source, e.Modifier);
                AVEffect.WaterLog.Play(e.Target);
            };
            return e;
        }

        public static Effect FireInitial(Source source, Token Target, int damage)
        {
            Effect e = new Effect("Fire Initial", source, Target, damage);
            e.Process = () =>
            {
                EffectSet nextEffects = new EffectSet();
                Token t = (Token)e.Target;

                if (t.Body.Destructible)
                {
                    nextEffects.Add(Effect.DestroyObstacle(e.Source, t));
                    AVEffect.Fire.Play(t);
                }
                else if (t is Unit)
                {
                    ((Unit)t).AddStat(e.Source, Stats.Health, 0 - e.Modifier);
                    AVEffect.Fire.Play(t);
                }

                TokenSet neighbors = t.Body.NeighborsAndCellmates - TargetFilter.UnitDest;

                int newDmg = (int)Mathf.Floor(e.Modifier * 0.5f);
                foreach (Token t2 in neighbors)
                    nextEffects.Add(Effect.FireSpread(e.Source, t2, newDmg));
                EffectQueue.Add(nextEffects);
            };
            return e;
        }

        public static Effect FireSpread(Source source, Token Target, int damage)
        {
            Effect e = new Effect("Fire Spread", source, Target, damage);
            e.Process = () =>
            {
                Token t = (Token)e.Target;
                if (t.Body.Destructible)
                {
                    AVEffect.Fire.Play(t);
                    EffectQueue.Add(Effect.DestroyObstacle(e.Source, t));
                }

                else if (t is Unit)
                {
                    ((Unit)t).AddStat(e.Source, Stats.Health, 0 - e.Modifier);
                    AVEffect.Fire.Play(t);
                }
            };
            return e;
        }

        public static Effect LaserLine(Source source, Unit Target, int damage, int decayPercent)
        {
            Effect e = new Effect("Laser Line", source, Target, new int[2] { damage, decayPercent });
            e.Process = () =>
            {
                Token t = (Token)e.Target;
                Cell cell = t.Body.Cell;
                int2 direction = Direction.FromCells(cell, e.Source.Token.Body.Cell);

                CellSet cells = new CellSet(cell);
                bool stop = false;

                while (!stop)
                {
                    index2 nextIndex = cell.Index - direction;
                    if (Game.Board.HasCell(nextIndex, out cell)) cells.Add(cell);
                    else stop = true;
                }
                EffectQueue.Add(Effect.LaserInitial(e.Source, cells, e.Modifiers[0], e.Modifiers[1]));
            };
            return e;
        }
        public static Effect LaserInitial(Source source, CellSet Targets, int damage, int decayPercent)
        {
            Effect e = new Effect("Laser Initial", source, Targets.ToArray(), new int[2]{damage, decayPercent});
            e.Process = () =>
            {
                int currentDmg = e.Modifiers[0];
                foreach (Cell cell in e.Targets)
                {
                    TokenSet units = cell.Occupants - TargetFilter.Unit;
                    foreach (Unit u in units)
                    {
                        if (u.Damage(e.Source, currentDmg)) AVEffect.Laser.Play(u);
                        else AVEffect.Miss.Play(u);
                    }
                    TargetFilter f = TargetFilter.Ob + FilterTests.Plane(Plane.Sunken, false);

                    if ( (cell.Occupants - f).Count > 0 ) return;
                    if (units.Count > 0)
                        currentDmg = (int)Mathf.Floor(currentDmg * (decayPercent/100));
                }
            };
            return e;
        }
        public static Effect LaserSpread(Source source, Token Target, int damage)
        {
            Effect e = new Effect("Laser Spread", source, Target, damage);
            e.Process = () =>
            {
                if (((Unit)e.Target).Damage(e.Source, e.Modifier)) AVEffect.Laser.Play(e.Target);
                else AVEffect.Miss.Play(e.Target);
            };
            return e;
        }


        #endregion

        #region //Misc.

        public static Effect Knockback(Source source, Token Target, int range, int damage)
        {
            Effect e = new Effect("Knockback", source, Target, new int[2] { range, damage });
            e.Process = () =>
            {
                Token t = (Token)e.Target;
                Cell actorCell = e.Source.Token.Body.Cell;
                Cell start = t.Body.Cell;

                int2 dir = Direction.FromCells(actorCell, start);

                CellSet line = new CellSet();

                for (int i = 0; i < e.Modifiers[0]; i++)
                {
                    index2 index = start.Index + dir;
                    Cell next;
                    if (Game.Board.HasCell(index, out next))
                    {
                        line.Add(next);
                        start = next;
                    }
                }

                int totalDamage = 0;
                int totalCells = 0;
                foreach (Cell c in line)
                {
                    if (((Token)e.Target).Body.CanEnter(c))
                    {
                        EffectQueue.Add(Effect.Move(e.Source, t, c));
                        totalDamage += e.Modifiers[1];
                        totalCells++;
                    }
                    else { break; }
                    if (c.StopToken(t)) { break; }
                }

                string log = "";
                if (totalCells == 0) { log = e.Source.Token + " attempted to knock " + t + " back, but there was something in the way."; }
                else
                {
                    log = e.Source.Token + " knocked " + t + " back " + totalCells + " cells";
                    if (totalDamage > 0)
                    {
                        EffectQueue.Add(Effect.Damage(e.Source, (Unit)t, totalDamage));
                        log += ", dealing " + totalDamage + " damage.";
                    }
                    else { log += "."; }
                }
                GameLog.Out(log);
            };
            return e;
        }
        public static Effect Miss(Source source, Token Target)
        {
            Effect e = new Effect("Miss", source, Target);
            e.Process = () => { AVEffect.Miss.Play(e.Target); };
            return e;
        }
        public static Effect Stick(Source source, Unit Target) {
            Effect e = new Effect("Stick", source, Target);
            e.Process = () =>
            {
                Ability move = ((Unit)e.Target).Arsenal.Move;
                if (move != default(Ability))
                {
                    ((Token)e.Targets[1]).WatchList.Add(new WebRecord((Token)e.Target, move.Aims[0].Range));
                    move.Aims[0].Range = 1;
                    AVEffect.Stick.Play(e.Target);
                }
            };
            return e;
        }

        #endregion

        #region //Token Properties

        public static Effect AddStat(Source source, Unit target, Stats stat, int change)
        {
            Effect e = new Effect("Add Stat", source, target, stat, change);
            e.Process = () => { ((Unit)e.Target).SetStat(e.Source, e.Stat, e.Modifier); };
            return e;
        }
        public static Effect SetStat(Source source, Unit target, Stats stat, int change)
        {
            Effect e = new Effect("Set Stat", source, target, stat, change);
            e.Process = () =>
            {
                Unit u = (Unit)e.Target;
                if (e.Modifier > 0) AVEffect.StatUp.Play(u);
                if (e.Modifier < 0) AVEffect.StatDown.Play(u);
                u.AddStat(e.Source, e.Stat, e.Modifier);
            };
            return e;
        }

        public static Effect SetOwner(Source source, Token target, Player owner)
        {
            Effect e = new Effect("Set Owner", source, target, owner);
            e.Process = () =>
            {
                Token t = (Token)e.Target;
                t.Owner = e.Player;
                AVEffect.Owner.Play(t);
                GameLog.Out(e.Player + " acquired " + t);
            };
            return e;
        }

        public static Effect SetPlane(Source source, Token target, Plane plane)
        {
            Effect e = new Effect("Set Plane", source, target, plane);
            e.Process = () =>
            {
                target.Plane = plane;
                Cell c = target.Body.Cell;
                target.Body.Exit();
                target.Body.TryEnter(c, false);
            };
            return e;
        }

        public static Effect SetDest(Source source, Token target, bool destructible)
        {
            Effect e = new Effect("Set Destructible", source, target, destructible);
            e.Process = () =>
            {
                target.Body.Destructible = e.Flag;
            };
            return e;
        }

        public static Effect SetTrample(Source source, Token target, bool trample)
        {
            Effect e = new Effect("Set Trample", source, target, trample);
            e.Process = () =>
            {
                target.Body.Trample = e.Flag;
            };
            return e;
        }


        #endregion




    }
}