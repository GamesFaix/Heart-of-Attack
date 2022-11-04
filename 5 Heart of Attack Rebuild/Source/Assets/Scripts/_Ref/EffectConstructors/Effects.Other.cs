using HOA.Resources;
using HOA.Fargo;
using Token = HOA.Tokens.Token;
using Unit = HOA.Tokens.Unit;
using Cell = HOA.Board.Cell;
using Direction = HOA.Board.Direction;
using Session = HOA.Sessions.Session;

namespace HOA.Ef
{

    public partial class Effect
    {
        public static Effect Knockback(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Knockback", args);
            e.action = (a) =>
            {
                sbyte maxCells = a[FN.MaxCells];
                sbyte dmgPerCell = a[FN.Damage1];
                Token user = e.source.Last<Token>();
                Token damaged = a[FT.Damaged] as Token;
                Cell userCell = user.Cell;
                Cell start = a[FT.Start] as Cell;
                
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
                        Queue.Add(Effect.Move(e.source, new EffectArgs(
                            Arg.Target(FT.Mover, damaged),
                            Arg.Target(FT.Destination, c))));
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
                        Queue.Add(Effect.Damage(e.source, new EffectArgs(
                            Arg.Target(FT.Damaged, damaged),
                            Arg.Num(FN.Damage, totalDamage))));
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
            e.action = (a) => { AVEffect.Miss.Play(a[FT.Damaged]); };
            return e;
        }

        public static Effect Stick(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Stick", args);
            e.action = (a) =>
            {
                Unit u = a[FT.Unit] as Unit;

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