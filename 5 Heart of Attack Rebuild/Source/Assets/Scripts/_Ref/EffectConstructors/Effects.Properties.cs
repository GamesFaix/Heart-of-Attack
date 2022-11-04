using HOA.Resources;
using HOA.Fargo;
using Unit = HOA.Tokens.Unit;
using Token = HOA.Tokens.Token;

namespace HOA.Ef
{

	public partial class Effect {

        public static Effect AddStat(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Add Stat", args);
            e.action = (a) => 
            {
                Unit u = a[FT.Unit] as Unit;
                u.StatAdd(e, (FS)a[FN.Stat], a[FN.Damage], a[FO.Secondary]);
                AVEffect.Stat(a[FN.Damage] >= 0).Play(u);
            };
            return e;
        }

        public static Effect SetStat(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Stat", args);
            e.action = (a) =>
            {
                Unit u = a[FT.Unit] as Unit;
                u.StatAdd(e, (FS)a[FN.Stat], a[FN.Damage], a[FO.Secondary]);
                AVEffect.Stat(a[FN.Damage] >= 0).Play(u);
            };
            return e;
        }

        public static Effect SetOwner(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Owner", args);
            e.action = (a) =>
            {
                Token t = a[FT.Token] as Token; 
                t.Owner = a.player;
                AVEffect.Owner.Play(t);
                Log.Game("{0} acquired {1}", a.player, t);
            };
            return e;
        }

        public static Effect SetPlane(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Plane", args);
            e.action = (a) => 
            {
                Token t = a[FT.Token] as Token; 
                t.SetPlane(e, a.plane);
            };
            return e;
        }

        public static Effect SetDest(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Destructible", args);
            e.action = (a) => 
            {
                Token t = a[FT.Token] as Token; 
                t.SetFlags(e, Tokens.TokenFlags.Destructible, a[FO.Toggle]);
                AVEffect.Stat(!a[FO.Toggle]).Play(t);
            };
            return e;
        }

        public static Effect SetTrample(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Set Trample", args);
            e.action = (a) =>
            {
                Token t = a[FT.Token] as Token; 
                t.SetFlags(e, Tokens.TokenFlags.Trample, a[FO.Toggle]);
                AVEffect.Stat(a[FO.Toggle]).Play(t);
            };
            return e;
        }

        public static Effect Learn(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Learn", args);
            e.action = (a) =>
            {
                Unit u = a[FT.Unit] as Unit;
                u.arsenal.Add(args.ability);
                AVEffect.StatUp.Play(u);
            };
            return e;
        }

        public static Effect Forget(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Forget", args);
            e.action = (a) =>
            {
                Unit u = a[FT.Unit] as Unit;
                u.arsenal.Remove(a.ability);
                AVEffect.StatDown.Play(u);
            };
            return e;
        }

        public static Effect AddTimer(object source, EffectArgs args)
        {
            Effect e = new Effect(source, "Add Timer", args);
            e.action = (a) =>
            {
                //Unit u = a[FT.Unit] as Unit;
                Log.Debug("Not implemented.");
                //                u.timers.Add(Tokens.Timer.Bombing(e, u));
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