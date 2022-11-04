using HOA.Resources;
using HOA.Args;
using Token = HOA.Tokens.Token;
using Unit = HOA.Tokens.Unit;
using Cell = HOA.Board.Cell;
using Direction = HOA.Board.Direction;
using Session = HOA.Sessions.Session;
using HOA.Collections;
using Farg = HOA.Args.Arg;

namespace HOA.Effects
{

    public partial class Effect
    {
        public static Effect Knockback(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Knockback", args);
            e.action = (a) =>
            {
                sbyte maxCells = a[RN.MaxCells];
                sbyte dmgPerCell = a[RN.Damage1];
                Token user = e.source.Last<Token>();
                Token damaged = a[RT.Damaged] as Token;
                Cell userCell = user.cell;
                Cell start = a[RT.Start] as Cell;
                
                int2 dir = Direction.FromCells(userCell, start);

                Set<IEntity> line = new Set<IEntity>();

                for (int i = 0; i < maxCells; i++)
                {
                    index2 index = start.Index + dir;
                    Cell next;
                    if (Session.Active.board.HasCell(index, out next))
                    {
                        line.Add(next);
                        start = next;
                    }
                }

                sbyte totalDamage = 0;
                sbyte totalCells = 0;
                foreach (Cell c in line)
                {
                    if (!damaged.CanEnter(c) || c.CanStop(damaged))
                        break;
                    else
                    {
                        EffectQueue.Add(Effect.Move(e.source, new EffectArgs(
                            Arg.Target(RT.Mover, damaged),
                            Arg.Target(RT.Destination, c))));
                        totalDamage += dmgPerCell;
                        totalCells++;
                    } 
                }

               
                if (totalCells == 0)
                    Log.Game("{0} attempted to knock {1} back, "
                    + "but there was something in the way.", user, damaged);
                else
                {
                    string log = string.Format(
                        "{0} knocked {1} back {2} cells", user, damaged, totalCells);
                    if (totalDamage > 0)
                    {
                        EffectQueue.Add(Effect.Damage(e.source, new EffectArgs(
                            Arg.Target(RT.Damaged, damaged),
                            Farg.Num(RN.Damage, totalDamage))));
                        log += string.Format(", dealing {0} damage.", totalDamage);
                    }
                    else 
                        log += ".";
                    Log.Game(log);
                }
            };
            return e;
        }

        public static Effect Miss(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Miss", args);
            e.action = (a) => { AVEffect.Miss.Play(a[RT.Damaged]); };
            return e;
        }

        public static Effect Stick(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Stick", args);
            e.action = (a) =>
            {
                Unit u = a[RT.Unit] as Unit;

                Abilities.Ability move = u.arsenal.Move.ability;
                if (move != null)
                {
                    e.source.Last<Token>().trackList.Add(u, move.Aims[0].range);
                    move.Aims[0].range = Range.sb(0, 1);
                    AVEffect.Stick.Play(u);
                }
            };
            return e;
        }

    }
}