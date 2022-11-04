using System;
using System.Collections.Generic;
using HOA.Resources;

namespace HOA.Abilities 
{
	
    public partial class Effect
    {
        
        public static Effect Create(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Create", args);
            e.action = (a) =>
            {
                Token newToken = Session.Active.Create(e, args.species, args.cell);
                AVEffect.Birth.Play(newToken);
                if (args.effectConstructor != null)
                    EffectQueue.Interrupt(args.effectConstructor(source, new EffectArgs(newToken)));
            };
            return e;

        }
         
        public static Effect DestroyCleanUp(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Clean Up", args);
            e.action = (a) => { args.token.Destroy(e, true); };
            return e;
        }

        public static Effect DestroyObstacle(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Obstacle", args);
            e.action = (a) =>
            {
                AVEffect.Destruct.Play(args.token);
                if (e.Sequence == null)
                    EffectQueue.Add(Effect.DestroyCleanUp(e.source, args));
                else
                    e.Sequence.AddToList(1, Effect.DestroyCleanUp(e.source, args));
            };
            return e;
        }

        public static Effect DestroyUnit(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Unit", args);
            e.action = (a) =>
            {
                AVEffect.Death.Play(args.token);
                if (e.Sequence == null)
                    EffectQueue.Add(Effect.DestroyCleanUp(e.source, args));
                else
                    e.Sequence.AddToList(1, Effect.DestroyCleanUp(e.source, args));
            };
            return e;
        }

        public static Effect Detonate2(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Detonate2", args);
            e.action = (a) =>
            {
                AVEffect.Detonate.Play(args.token);
                e.Sequence.AddToList(1, Effect.DestroyCleanUp(source, args));
            };
            return e;
        }

        public static Effect GetHeart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Get Heart", args);
            e.action = (a) =>
            {
                EffectSet effects = new EffectSet();
                foreach (Token t in args.token.Owner.Tokens)
                    effects.Add(Effect.SetOwner(source, new EffectArgs(t)));
                AVEffect.GetHeart.Play(args.token);
                Debug.Log("{0} acquired the {1}", e.source.Last<Player>(), args.token);
                effects.Add(Effect.DestroyCleanUp(e.source, args));
                EffectQueue.Add(effects);
            };
            return e;
        }

        public static Effect Replace(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Replace", args);
            e.action = (a) =>
            {
                Token token = args.token;
                Cell cell = token.Cell;
                token.Destroy(e, false);
                Token newToken = Session.Active.Create(e.source, args.species, cell);
                if (args.effectConstructor != null)
                    EffectQueue.Interrupt(args.effectConstructor(source, new EffectArgs(newToken)));
            };
            return e;
        }
	}
}