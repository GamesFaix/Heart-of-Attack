using HOA.Resources;

namespace HOA.Ab
{

	public partial class Effect {

        public static Effect AddStat(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Add Stat", args);
            e.action = (a) => {
                args.unit.StatAdd(e, args.stat, args.value);
                AVEffect.Stat(args.value >= 0).Play(args.unit);
            };
            return e;
        }

        public static Effect SetStat(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Stat", args);
            e.action = (a) =>
            {
                args.unit.StatAdd(e, args.stat, args.value);
                AVEffect.Stat(args.value >= 0).Play(args.unit);
            };
            return e;
        }

        public static Effect SetOwner(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Owner", args);
            e.action = (a) =>
            {
                args.token.Owner = args.player;
                AVEffect.Owner.Play(args.token);
                Log.Game("{0} acquired {1}", args.player, args.token);
            };
            return e;
        }

        public static Effect SetPlane(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Plane", args);
            e.action = (a) => { args.token.SetPlane(e, args.plane); };
            return e;
        }

        public static Effect SetDest(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Destructible", args);
            e.action = (a) => 
            { 
                args.token.SetFlags(e, To.TokenFlags.Destructible, args.option);
                AVEffect.Stat(!args.option).Play(args.token);
            };
            return e;
        }

        public static Effect SetTrample(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Trample", args);
            e.action = (a) =>
            {
                args.token.SetFlags(e, To.TokenFlags.Trample, args.option);
                AVEffect.Stat(args.option).Play(args.token);
            };
            return e;
        }

        public static Effect Learn(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Learn", args);
            e.action = (a) =>
            {
                args.unit.arsenal.Add(args.ability);
                AVEffect.StatUp.Play(args.unit);
            };
            return e;
        }

        public static Effect Forget(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Forget", args);
            e.action = (a) =>
            {
                args.unit.arsenal.Remove(args.ability);
                AVEffect.StatDown.Play(args.unit);
            };
            return e;
        }

        public static Effect AddTimer(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Add Timer", args);
            e.action = (a) =>
            {
                args.unit.timers.Add(To.Timer.Bombing(e, args.unit));
            };
            return e;
        }

        public static Effect RemoveTimer(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Remove Timer", args);
            e.action = (a) =>
            {
                Log.Debug("Not implemented.");
            };
            return e;
        

        }
    }
}