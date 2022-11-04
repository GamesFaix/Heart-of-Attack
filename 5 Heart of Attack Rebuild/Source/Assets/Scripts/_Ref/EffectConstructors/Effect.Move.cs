using HOA.Resources;
using HOA.To;
using HOA.Fargo;

namespace HOA.Ef
{
    public partial class Effect
    {

        public static Effect BurrowStart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Burrow Start", args);
            e.action = (a) =>
            {
                Cell oldCell = (a[FT.Mover] as Token).Cell;
                AVEffect.Burrow.Play(a[FT.Mover]);
                Queue.Add(Effect.BurrowFinish(source, a));
                Log.Game("{0} burrowed from {1} to {2}.", a[FT.Mover], oldCell, a[FT.Destination]);
            };
            return e;
        }
        public static Effect BurrowFinish(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Burrow Finish", args);
            e.action = (a) =>
            {
                (a[FT.Mover] as Token).Enter(a[FT.Destination] as Cell);
                AVEffect.Burrow.Play(a[FT.Mover]);
            };
            return e;
        }

        public static Effect Move(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Move", args);
            e.action = (a) =>
            {
                Token mover = a[FT.Mover] as Token;
                Cell oldCell = mover.Cell;
                mover.Enter(a[FT.Destination] as Cell);
                Cell newCell = mover.Cell;
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
                Token t1 = a[FT.Token] as Token;
                Token t2 = a[FT.Token1] as Token;
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
                Token mover = a[FT.Mover] as Token;
                Cell c = a[FT.Destination] as Cell;
                Cell oldCell = mover.Cell;
                AVEffect.Teleport.Play(mover);
                Queue.Add(Effect.TeleportFinish(source, a));

                Log.Game("{0} teleported {1} from {2} to {3}.", e.source.Last<Token>(), mover, oldCell, c);
            };
            return e;
        }
        public static Effect TeleportFinish(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Teleport Finish", args);
            e.action = (a) =>
            {
                Token mover = a[FT.Mover] as Token;
                Cell c = a[FT.Destination] as Cell;
                mover.Enter(c);
                AVEffect.Teleport.Play(mover);
            };
            return e;
        }

    }
}
