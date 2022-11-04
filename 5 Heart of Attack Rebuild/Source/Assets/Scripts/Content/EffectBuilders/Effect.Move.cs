using HOA.Resources;
using HOA.Tokens;
using HOA.Args;
using Cell = HOA.Board.Cell;

namespace HOA.Effects
{
    public partial class Effect
    {

        public static Effect BurrowStart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Burrow Start", args);
            e.action = (a) =>
            {
                Cell oldCell = (a[RT.Mover] as Token).cell;
                AVEffect.Burrow.Play(a[RT.Mover]);
                EffectQueue.Add(Effect.BurrowFinish(source, a));
                Log.Game("{0} burrowed from {1} to {2}.", a[RT.Mover], oldCell, a[RT.Destination]);
            };
            return e;
        }
        public static Effect BurrowFinish(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Burrow Finish", args);
            e.action = (a) =>
            {
                (a[RT.Mover] as Token).Enter(a[RT.Destination] as Cell);
                AVEffect.Burrow.Play(a[RT.Mover]);
            };
            return e;
        }

        public static Effect Move(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Move", args);
            e.action = (a) =>
            {
                Token mover = a[RT.Mover] as Token;
                Cell oldCell = mover.cell;
                mover.Enter(a[RT.Destination] as Cell);
                Cell newCell = mover.cell;
                if (mover.plane.ContainsAny(Plane.Ground)) 
                    AVEffect.Walk.Play(mover);
                else if (mover.plane.ContainsAny(Plane.Air)) 
                    AVEffect.Fly.Play(mover);
                Log.Game("{0} moved from {1} to {2}.", mover, oldCell, newCell);
            };
            return e;
        }

        public static Effect Swap(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Swap", args);
            e.action = (a) =>
            {
                Token t1 = a[RT.Token] as Token;
                Token t2 = a[RT.Token1] as Token;
                t1.Swap(t2);
                Log.Game("{0} swapped places with {1}.", t1, t2);
            };
            return e;
        }

        public static Effect TeleportStart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Teleport Start", args);
            e.action = (a) =>
            {
                Token mover = a[RT.Mover] as Token;
                Cell c = a[RT.Destination] as Cell;
                Cell oldCell = mover.cell;
                AVEffect.Teleport.Play(mover);
                EffectQueue.Add(Effect.TeleportFinish(source, a));

                Log.Game("{0} teleported {1} from {2} to {3}.", e.source.Last<Token>(), mover, oldCell, c);
            };
            return e;
        }
        public static Effect TeleportFinish(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Teleport Finish", args);
            e.action = (a) =>
            {
                Token mover = a[RT.Mover] as Token;
                Cell c = a[RT.Destination] as Cell;
                mover.Enter(c);
                AVEffect.Teleport.Play(mover);
            };
            return e;
        }

    }
}
