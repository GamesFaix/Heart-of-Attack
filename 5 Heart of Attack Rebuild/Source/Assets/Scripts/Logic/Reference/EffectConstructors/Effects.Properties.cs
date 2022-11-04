using HOA.Resources;

namespace HOA.Abilities
{

	public partial class Effect {

        public static Effect AddStat(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Add Stat", user, args);
            e.action = (a) => {
                args.unit.StatAdd(e, args.stat, args.value);
                AVEffect.Stat(args.value >= 0).Play(args.unit);
            };
            return e;
        }

        public static Effect SetStat(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Set Stat", user, args);
            e.action = (a) =>
            {
                args.unit.StatAdd(e, args.stat, args.value);
                AVEffect.Stat(args.value >= 0).Play(args.unit);
            };
            return e;
        }

        public static Effect SetOwner(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Set Owner", user, args);
            e.action = (a) =>
            {
                args.token.Owner = args.player;
                AVEffect.Owner.Play(args.token);
                Debug.Log("{0} acquired {1}", args.player, args.token);
            };
            return e;
        }

        public static Effect SetPlane(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Set Plane", user, args);
            e.action = (a) => { args.token.SetPlane(e, args.plane); };
            return e;
        }

        public static Effect SetDest(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Set Destructible", user, args);
            e.action = (a) => 
            { 
                args.token.SetFlags(e, Tokens.TokenFlags.Destructible, args.option);
                AVEffect.Stat(!args.option).Play(args.token);
            };
            return e;
        }

        public static Effect SetTrample(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Set Trample", user, args);
            e.action = (a) =>
            {
                args.token.SetFlags(e, Tokens.TokenFlags.Trample, args.option);
                AVEffect.Stat(args.option).Play(args.token);
            };
            return e;
        }

        public static Effect Learn(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Learn", user, args);
            e.action = (a) =>
            {
                args.unit.arsenal.Add(args.ability);
                AVEffect.StatUp.Play(args.unit);
            };
            return e;
        }

        public static Effect Forget(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Forget", user, args);
            e.action = (a) =>
            {
                args.unit.arsenal.Remove(args.ability);
                AVEffect.StatDown.Play(args.unit);
            };
            return e;
        }

        public static Effect AddTimer(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Add Timer", user, args);
            e.action = (a) =>
            {
                args.unit.timers.Add(Tokens.Timer.Bombing(e, args.unit));
            };
            return e;
        }

        public static Effect RemoveTimer(IEffectUser user, EffectArgs args)
        {
            Effect e = new Effect("Remove Timer", user, args);
            e.action = (a) =>
            {
                Debug.Log("Not implemented.");
            };
            return e;
        

        }
    }
}