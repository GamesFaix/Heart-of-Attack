using System;
using System.Collections.Generic;
using HOA.Resources;

namespace HOA.Ef
{
	
    public partial class Effect
    {

        public static Effect Create(object source, Args args)
        {
            Effect e = new Effect(source, "Create", args);
            e.action = (a) =>
            {
                Token newToken = Session.Active.Create(e, args.species, args.cell);
                AVEffect.Birth.Play(newToken);
                Log.Game("{0} created {1} in {2}.", source, newToken, newToken.Cell);
                if (args.builder != null)
                    Queue.Interrupt(args.builder(source, new Args(newToken)));
            };
            return e;

        }
         
        public static Effect DestroyCleanUp(object source, Args args)
        {
            Effect e = new Effect(source, "Destroy Clean Up", args);
            e.action = (a) => { args.token.Destroy(e, true); };
            return e;
        }

        public static Effect DestroyObstacle(object source, Args args)
        {
            Effect e = new Effect(source, "Destroy Obstacle", args);
            e.action = (a) =>
            {
                AVEffect.Destruct.Play(args.token);
                if (e.Sequence == null)
                    Queue.Add(Effect.DestroyCleanUp(e.source, args));
                else
                    e.Sequence.AddToList(1, Effect.DestroyCleanUp(e.source, args));
            };
            return e;
        }

        public static Effect DestroyUnit(object source, Args args)
        {
            Effect e = new Effect(source, "Destroy Unit", args);
            e.action = (a) =>
            {
                AVEffect.Death.Play(args.token);
                if (e.Sequence == null)
                    Queue.Add(Effect.DestroyCleanUp(e.source, args));
                else
                    e.Sequence.AddToList(1, Effect.DestroyCleanUp(e.source, args));
            };
            return e;
        }

        public static Effect Detonate2(object source, Args args)
        {
            Effect e = new Effect(source, "Detonate2", args);
            e.action = (a) =>
            {
                AVEffect.Detonate.Play(args.token);
                e.Sequence.AddToList(1, Effect.DestroyCleanUp(source, args));
            };
            return e;
        }

        public static Effect GetHeart(object source, Args args)
        {
            Effect e = new Effect(source, "Get Heart", args);
            e.action = (a) =>
            {
                Set effects = new Set();
                foreach (Token t in args.token.Owner.Tokens)
                    effects.Add(Effect.SetOwner(source, new Args(t)));
                AVEffect.GetHeart.Play(args.token);
                Log.Game("{0} acquired the {1}", e.source.Last<Player>(), args.token);
                effects.Add(Effect.DestroyCleanUp(e.source, args));
                Queue.Add(effects);
            };
            return e;
        }

        public static Effect Replace(object source, Args args)
        {
            Effect e = new Effect(source, "Replace", args);
            e.action = (a) =>
            {
                Token token = args.token;
                Cell cell = token.Cell;
                token.Destroy(e, false);
                Token newToken = Session.Active.Create(e.source, args.species, cell);
                if (args.builder != null)
                    Queue.Interrupt(args.builder(source, new Args(newToken)));
            };
            return e;
        }
	}
}