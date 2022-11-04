using HOA.Resources;

namespace HOA.Abilities
{

    public partial class Effect
    {
        public static Effect Knockback(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Knockback", args);
            e.action = (a) =>
            {
                int maxCells = args.values[0];
                int dmgPerCell = args.values[1];
                Token t = e.source.Last<Token>();
                Cell userCell = t.Cell;
                Cell start = args.cell;

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

                int totalDamage = 0;
                int totalCells = 0;
                foreach (Cell c in line)
                {
                    if (!args.token.CanEnter(c) || c.CanStop(args.token))
                        break;
                    else
                    {
                        EffectQueue.Add(Effect.Move(e.source, new EffectArgs(args.token, c)));
                        totalDamage += dmgPerCell;
                        totalCells++;
                    } 
                }

               
                if (totalCells == 0)
                    Debug.Log("{0} attempted to knock {1} back, "
                    + "but there was something in the way.", t, args.token);
                else
                {
                    string log = string.Format(
                        "{0} knocked {1} back {2} cells", t, args.token, totalCells);
                    if (totalDamage > 0)
                    {
                        EffectQueue.Add(Effect.Damage(e.source, new EffectArgs(args.token, totalDamage)));
                        log += string.Format(", dealing {0} damage.", totalDamage);
                    }
                    else 
                        log += ".";
                    Debug.Log(log);
                }
            };
            return e;
        }
        
        public static Effect Miss(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Miss", args);
            e.action = (a) => { AVEffect.Miss.Play(args.token); };
            return e;
        }
        
        public static Effect Stick(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Stick", args);
            e.action = (a) =>
            {
                Ability move = args.unit.arsenal.Move;
                if (move != null)
                {
                    e.source.Last<Token>().trackList.Add(args.token, move.Aims[0].range);
                    move.Aims[0].range = new Range(0,1);
                    AVEffect.Stick.Play(args.token);
                }
            };
            return e;
        }

    }
}