using HOA.Resources;

namespace HOA.Abilities
{

    public partial class Effect
    {
        public static Effect Knockback(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Knockback", user, args);
            e.action = (a) =>
            {
                int maxCells = args.values[0];
                int dmgPerCell = args.values[1];

                Cell userCell = e.userToken.Cell;
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
                        EffectQueue.Add(Effect.Move(user, new EffectArgs(args.token, c)));
                        totalDamage += dmgPerCell;
                        totalCells++;
                    } 
                }

                if (totalCells == 0)
                    Debug.Log("{0} attempted to knock {1} back, "
                    + "but there was something in the way.", e.userToken, args.token);
                else
                {
                    string log = string.Format(
                        "{0} knocked {1} back {2} cells", e.userToken, args.token, totalCells);
                    if (totalDamage > 0)
                    {
                        EffectQueue.Add(Effect.Damage(user, new EffectArgs(args.token, totalDamage)));
                        log += string.Format(", dealing {0} damage.", totalDamage);
                    }
                    else 
                        log += ".";
                    Debug.Log(log);
                }
            };
            return e;
        }
        
        public static Effect Miss(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Miss", user, args);
            e.action = (a) => { AVEffect.Miss.Play(args.token); };
            return e;
        }
        
        public static Effect Stick(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Stick", user, args);
            e.action = (a) =>
            {
                Ability move = args.unit.arsenal.Move;
                if (move != null)
                {
                    e.userToken.trackList.Add(args.token, move.Aims[0].Args.range);
                    move.Aims[0].Args.range = new Range(0,1);
                    AVEffect.Stick.Play(args.token);
                }
            };
            return e;
        }

    }
}