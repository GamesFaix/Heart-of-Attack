using System;
using System.Collections.Generic;
using HOA.Abilities;
using HOA.Content;
using HOA.Stats;
using HOA.Fargo;

namespace HOA.Tokens
{

    public partial class Unit
    {

        public static Unit Ultratherium(object source)
        {
            Unit u = new Unit(source, Species.Ultratherium, UnitRank.King, Species.FirHeart);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.stats = Tokens.StatSheet.King(u, 80, 2);
            u.LearnMove(4);
            u.LearnFocus();
            u.LearnStrike(16);
            u.LearnCreate(Price.Cheap, Species.Grizzly);
            u.LearnCreate(new Price(1,1), Species.TalonedScout);
            u.Learn(Content.Abilities.Transmute, new AbilityArgs(u, new Price(1, 2), 
                Arg.Filter(FF.Filter0, Filter.DestNotCorpse), 
                Species.Metaterrainean));
            //Ability.ThrowTerrain(u),
	        //Ability.IceBlast(u),
			return u;
        }

        public static Unit GrizzlyElder(object source)
        {
            Unit u = new Unit(source, Species.Grizzly, UnitRank.Light);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 25, 3);
            u.LearnMove(3);
            u.LearnFocus();
            u.LearnStrike(9);
            u.Learn(Content.Abilities.Heal, new AbilityArgs(u, new Price(1, 1),
                Arg.Filter(FF.Filter0, Filter.Owner(u.Owner, true) + Filter.identity(u, false)), 
                Arg.Stat(FS.Damage, new Scalar(7))));
            u.LearnCreate(new Price(0, 1), Species.Tree);
            return u;
        }

        public static Unit Metaterrainean(object source)
        {
            Unit u = new Unit(source, Species.Metaterrainean, UnitRank.Heavy, Species.Rock);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.stats = new Tokens.StatSheet(u, 50, 1);
            u.LearnMove(2);
            u.LearnFocus();
            u.LearnStrike(20); 
           	//Ability.Engorge(u)
            return u;
        }

        public static Unit TalonedScout(object source)
        {
            Unit u = new Unit(source, Species.TalonedScout, UnitRank.Medium);
            u.body = new Body(u, Plane.Air);
            u.stats = new Tokens.StatSheet(u, 35, 4);
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