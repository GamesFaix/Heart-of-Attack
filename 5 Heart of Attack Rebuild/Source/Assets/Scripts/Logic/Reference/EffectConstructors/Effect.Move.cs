using HOA.Resources;
using HOA.Tokens;

namespace HOA.Abilities
{
    public partial class Effect
    {

		public static Effect BurrowStart(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Burrow Start", user, args);
            e.action = (a) =>
            {
                Token t = args.token;
                Cell c = args.cell;
                Cell oldCell = t.Cell;
                AVEffect.Burrow.Play(t);
                EffectQueue.Add(Effect.BurrowFinish(user, args));
                Debug.Log("{0} burrowed from {1} to {2}.", t, oldCell, c);
            };
            return e;
        }
        public static Effect BurrowFinish(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Burrow Finish", user, args);
            e.action = (a) =>
            {
                args.token.Enter(args.cell);
                AVEffect.Burrow.Play(args.token);
            };
            return e;
        }

        public static Effect Move(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Move", user, args);
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
        
        public static Effect Swap(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Swap", user, args);
            e.action = (a) =>
            {
                args.tokens[0].Swap(args.tokens[1]);
                Debug.Log("{0} swapped places with {1}.", args.tokens[0], args.tokens[1]);
            };
            return e;
        }

        public static Effect TeleportStart(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Teleport Start", user, args);
            e.action = (a) =>
            {
                Token t = args.token;
                Cell c = args.cell;
                Cell oldCell = t.Cell;
                AVEffect.Teleport.Play(t);
                EffectQueue.Add(Effect.TeleportFinish(user, args));

                Debug.Log("{0} teleported {1} from {2} to {3}.", user, t, oldCell, c);
            };
            return e;
        }
        public static Effect TeleportFinish(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Teleport Finish", user, args);
            e.action = (a) =>
            {
                args.token.Enter(args.cell);
                AVEffect.Teleport.Play(args.token);
            };
            return e;
        }

    }
}
