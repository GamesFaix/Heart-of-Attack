using HOA.Resources;
using HOA.Tokens;

namespace HOA.Abilities
{
    public partial class Effect
    {

		public static Effect BurrowStart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Burrow Start", args);
            e.action = (a) =>
            {
                Token t = args.token;
                Cell c = args.cell;
                Cell oldCell = t.Cell;
                AVEffect.Burrow.Play(t);
                EffectQueue.Add(Effect.BurrowFinish(source, args));
                Debug.Log("{0} burrowed from {1} to {2}.", t, oldCell, c);
            };
            return e;
        }
        public static Effect BurrowFinish(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Burrow Finish", args);
            e.action = (a) =>
            {
                args.token.Enter(args.cell);
                AVEffect.Burrow.Play(args.token);
            };
            return e;
        }

        public static Effect Move(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Move", args);
            e.action = (a) =>
            {
                Token t = args.token;
                Cell oldCell = t.Cell;
                t.Enter(args.cell);
                Cell newCell = t.Cell;
                if (t.plane.ContainsAny(Plane.Ground)) 
                    AVEffect.Walk.Play(t);
                else if (t.plane.ContainsAny(Plane.Air)) 
                    AVEffect.Fly.Play(t);
                Debug.Log("{0} moved from {1} to {2}.", t, oldCell, newCell);
            };
            return e;
        }
        
        public static Effect Swap(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Swap", args);
            e.action = (a) =>
            {
                args.tokens[0].Swap(args.tokens[1]);
                Debug.Log("{0} swapped places with {1}.", args.tokens[0], args.tokens[1]);
            };
            return e;
        }

        public static Effect TeleportStart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Teleport Start", args);
            e.action = (a) =>
            {
                Token t = args.token;
                Cell c = args.cell;
                Cell oldCell = t.Cell;
                AVEffect.Teleport.Play(t);
                EffectQueue.Add(Effect.TeleportFinish(source, args));

                Debug.Log("{0} teleported {1} from {2} to {3}.", e.source.Last<Token>(), t, oldCell, c);
            };
            return e;
        }
        public static Effect TeleportFinish(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Teleport Finish", args);
            e.action = (a) =>
            {
                args.token.Enter(args.cell);
                AVEffect.Teleport.Play(args.token);
            };
            return e;
        }

    }
}
