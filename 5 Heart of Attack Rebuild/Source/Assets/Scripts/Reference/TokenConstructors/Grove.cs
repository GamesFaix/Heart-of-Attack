using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ab;
using HOA.Ref;

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
            u.Learn(Abilities.Move, new AbilityArgs(u, Price.Cheap, Range.b(0,3)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new AbilityArgs(u, Price.Cheap, 16));
            u.Learn(Abilities.Create, new AbilityArgs(u, Price.Cheap, Species.Grizzly));
            u.Learn(Abilities.Create, new AbilityArgs(u, new Price(1, 1), Species.TalonedScout));
            u.Learn(Abilities.Transmute, new AbilityArgs(u, new Price(1, 2), Filter.DestNotCorpse, Species.Metaterrainean));
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
            u.Learn(Abilities.Move, new AbilityArgs(u, Price.Cheap, Range.b(0,3)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new AbilityArgs(u, Price.Cheap, 9));
            u.Learn(Abilities.Create, new AbilityArgs(u, new Price(0, 1), Species.Tree));
            u.Learn(Abilities.Heal, new AbilityArgs(u, new Price(1, 1), 
                Filter.Owner(u.Owner, true) + Filter.identity(u, false), 7));
            return u;
        }

        public static Unit Metaterrainean(object source)
        {
            Unit u = new Unit(source, Species.Metaterrainean, To.Rank.Heavy, Species.Rock);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 50);
            u.watch = new Watch(u, 1);
            u.Learn(Abilities.Move, new AbilityArgs(u, Price.Cheap, Range.b(0,2)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new AbilityArgs(u, Price.Cheap, 20));
           	//Ability.Engorge(u)
            return u;
        }

        public static Unit TalonedScout(object source)
        {
            Unit u = new Unit(source, Species.TalonedScout, To.Rank.Medium);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 35);
            u.watch = new Watch(u, 4);
            u.Learn(Abilities.Move, new AbilityArgs(u, Price.Cheap, Range.b(0, 6)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new AbilityArgs(u, Price.Cheap, 12));
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