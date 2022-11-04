using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ab;
using HOA.Ref;
using HOA.Stats;
using Args = HOA.Ab.AbilityArgs;
using HOA.Fargo;

namespace HOA
{

    public partial class Unit
    {

        public static Unit Ultratherium(object source)
        {
            Unit u = new Unit(source, Species.Ultratherium, To.Rank.King, Species.FirHeart);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 80);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            u.LearnMove(4);
            u.LearnFocus();
            u.LearnStrike(16);
            u.LearnCreate(Price.Cheap, Species.Grizzly);
            u.LearnCreate(new Price(1,1), Species.TalonedScout);
            u.Learn(Abilities.Transmute, new Args(u, new Price(1, 2), 
                Arg.Filter(FF.Filter0, Filter.DestNotCorpse), 
                Species.Metaterrainean));
            //Ability.ThrowTerrain(u),
	        //Ability.IceBlast(u),
			return u;
        }

        public static Unit GrizzlyElder(object source)
        {
            Unit u = new Unit(source, Species.Grizzly, To.Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 25);
            u.watch = new Watch(u, 3);
            u.LearnMove(3);
            u.LearnFocus();
            u.LearnStrike(9);
            u.Learn(Abilities.Heal, new Args(u, new Price(1, 1),
                Arg.Filter(FF.Filter0, Filter.Owner(u.Owner, true) + Filter.identity(u, false)), 
                Arg.Stat(FS.Damage, Scalar.Dam(u, 7))));
            u.LearnCreate(new Price(0, 1), Species.Tree);
            return u;
        }

        public static Unit Metaterrainean(object source)
        {
            Unit u = new Unit(source, Species.Metaterrainean, To.Rank.Heavy, Species.Rock);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 50);
            u.watch = new Watch(u, 1);
            u.LearnMove(2);
            u.LearnFocus();
            u.LearnStrike(20); 
           	//Ability.Engorge(u)
            return u;
        }

        public static Unit TalonedScout(object source)
        {
            Unit u = new Unit(source, Species.TalonedScout, To.Rank.Medium);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 35);
            u.watch = new Watch(u, 4);
            u.LearnMove(6);
            u.LearnFocus();
            u.LearnStrike(12);
           	//Ability.ArcticGust(u)
            return u;
        }
    }

    public partial class Obstacle
    {
        public static Obstacle Fir(object source)
        {
            Obstacle o = new Obstacle(source, Species.FirHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }
    }

}