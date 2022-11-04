using System;
using System.Collections.Generic;
using HOA.Resources;

namespace HOA.Abilities 
{
	
    public partial class Effect
    {
        
        public static Effect Create(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Create", user, args);
            e.Process = () =>
            {
                Token newToken = Session.Active.Create(user.ToTokenCreator(), args.species, args.cell);
                AVEffect.Birth.Play(newToken);
            };            
            return e;
        }
         
        public static Effect DestroyCleanUp(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Destroy Clean Up", user, args);
            e.Process = () => { args.token.Destroy(e, true); };
            return e;
        }

        public static Effect DestroyObstacle(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Destroy Obstacle", user, args);
            e.Process = () =>
            {
                AVEffect.Destruct.Play(args.token);
                if (e.Sequence == null)
                    EffectQueue.Add(Effect.DestroyCleanUp(e.User, args));
                else
                    e.Sequence.AddToList(1, Effect.DestroyCleanUp(e.User, args));
            };
            return e;
        }

        public static Effect DestroyUnit(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Destroy Unit", user, args);
            e.Process = () =>
            {
                AVEffect.Death.Play(args.token);
                if (e.Sequence == null)
                    EffectQueue.Add(Effect.DestroyCleanUp(e.User, args));
                else
                    e.Sequence.AddToList(1, Effect.DestroyCleanUp(e.User, args));
            };
            return e;
        }

        public static Effect Detonate2(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Detonate2", user, args);
            e.Process = () =>
            {
                AVEffect.Detonate.Play(args.token);
                e.Sequence.AddToList(1, Effect.DestroyCleanUp(user, args));
            };
            return e;
        }

        public static Effect GetHeart(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Get Heart", user, args);
            e.Process = () =>
            {
                EffectSet effects = new EffectSet();
                foreach (Token t in args.token.Owner.Tokens)
                    effects.Add(Effect.SetOwner(user, new EffectArgs(t)));
                AVEffect.GetHeart.Play(args.token);
                Debug.Log("{0} acquired the {1}", user.ToTokenCreator(), args.token);
                effects.Add(Effect.DestroyCleanUp(user, args));
                EffectQueue.Add(effects);
            };
            return e;
        }

        public static Effect Replace(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Replace", user, args);
            e.Process = () =>
            {
                Token token = args.token;
                Cell cell = token.Cell;
                token.Destroy(e, false);
                Session.Active.Create(user.ToTokenCreator(), args.species, cell);
            };
            return e;
        }
	}
}