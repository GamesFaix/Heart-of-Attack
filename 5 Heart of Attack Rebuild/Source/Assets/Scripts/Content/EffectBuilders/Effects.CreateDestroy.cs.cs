using System;
using System.Collections.Generic;
using HOA.Resources;
using HOA.Args;
using Token = HOA.Tokens.Token;
using Cell = HOA.Board.Cell;
using Session = HOA.Sessions.Session;
using HOA.Collections;

namespace HOA.Effects
{
	
    public partial class Effect
    {

        public static Effect Create(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Create", args);
            e.action = (a) =>
            {
                Token newToken = Session.Active.Create(e, args.species, a[RT.Location] as Cell);
                AVEffect.Birth.Play(newToken);
                Log.Game("{0} created {1} in {2}.", source, newToken, newToken.cell);
                if (a[RE.Birth] != null)
                    EffectQueue.Interrupt(a[RE.Birth](source, new EffectArgs(
                        Arg.Target(RT.User, newToken))));
            };
            return e;

        }

        public static Effect DestroyCleanUp(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Clean Up", args);
            e.action = (a) => { (a[RT.Token] as Token).Destroy(e, true); };
            return e;
        }

        public static Effect DestroyObstacle(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Destroy Obstacle", args);
            e.action = (a) =>
            {
                AVEffect.Destruct.Play(a[RT.Token]);
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
                AVEffect.Death.Play(a[RT.Token]);
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
                AVEffect.Detonate.Play(a[RT.Token]);
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
                Set<IEntity> team = (a[RT.Token] as Token).owner.Tokens;
                foreach (Token t in team)
                    effects.Add(Effect.SetOwner(source, new EffectArgs(
                        Arg.Target(RT.Token, t))));
                AVEffect.GetHeart.Play(a[RT.Token]);
                Log.Game("{0} acquired the {1}", e.source.Last<Player>(), a[RT.Token]);
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
                Token token = a[RT.Token] as Token;
                Cell cell = token.cell;
                token.Destroy(e, false);
                Token newToken = Session.Active.Create(e.source, args.species, cell);
                if (a[RE.Birth] != null)
                    EffectQueue.Interrupt(a[RE.Birth](source, new EffectArgs(
                        Arg.Target(RT.Token, newToken))));
            };
            return e;
        }
	}
}