using System;
using System.Collections.Generic;
using HOA.Resources;
using HOA.Fargo;
using Token = HOA.Tokens.Token;
using Cell = HOA.Board.Cell;
using Session = HOA.Sessions.Session;

namespace HOA.Ef
{
	
    public partial class Effect
    {

        public static Effect Create(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Create", args);
            e.action = (a) =>
            {
                Token newToken = Session.Active.Create(e, args.species, a[FT.Location] as Cell);
                AVEffect.Birth.Play(newToken);
                Log.Game("{0} created {1} in {2}.", source, newToken, newToken.Cell);
                if (a[FE.Birth] != null)
                    Queue.Interrupt(a[FE.Birth](source, new EffectArgs(
                        Arg.Target(FT.User, newToken))));
            };
            return e;

        }

        public static Effect DestroyCleanUp(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Clean Up", args);
            e.action = (a) => { (a[FT.Token] as Token).Destroy(e, true); };
            return e;
        }

        public static Effect DestroyObstacle(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Obstacle", args);
            e.action = (a) =>
            {
                AVEffect.Destruct.Play(a[FT.Token]);
                if (e.Sequence == null)
                    Queue.Add(Effect.DestroyCleanUp(e.source, args));
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
                AVEffect.Death.Play(a[FT.Token]);
                if (e.Sequence == null)
                    Queue.Add(Effect.DestroyCleanUp(e.source, args));
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
                AVEffect.Detonate.Play(a[FT.Token]);
                e.Sequence.AddToList(1, Effect.DestroyCleanUp(source, args));
            };
            return e;
        }

        public static Effect GetHeart(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Get Heart", args);
            e.action = (a) =>
            {
                Set effects = new Set();
                Set<IEntity> team = (a[FT.Token] as Token).Owner.Tokens;
                foreach (Token t in team)
                    effects.Add(Effect.SetOwner(source, new EffectArgs(
                        Arg.Target(FT.Token, t))));
                AVEffect.GetHeart.Play(a[FT.Token]);
                Log.Game("{0} acquired the {1}", e.source.Last<Player>(), a[FT.Token]);
                effects.Add(Effect.DestroyCleanUp(e.source, args));
                Queue.Add(effects);
            };
            return e;
        }

        public static Effect Replace(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Replace", args);
            e.action = (a) =>
            {
                Token token = a[FT.Token] as Token;
                Cell cell = token.Cell;
                token.Destroy(e, false);
                Token newToken = Session.Active.Create(e.source, args.species, cell);
                if (a[FE.Birth] != null)
                    Queue.Interrupt(a[FE.Birth](source, new EffectArgs(
                        Arg.Target(FT.Token, newToken))));
            };
            return e;
        }
	}
}