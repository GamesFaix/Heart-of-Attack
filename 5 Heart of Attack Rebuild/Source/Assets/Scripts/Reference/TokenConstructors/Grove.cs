﻿using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Abilities;

namespace HOA
{

    public partial class Unit
    {

        public static Unit Ultratherium(object source)
        {
            Unit u = new Unit(source, Species.Ultratherium, Tokens.Rank.King, Species.FirHeart);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 80);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            u.arsenal.Add(
				Ability.Move(u, 3),
                Ability.Focus(u),
				Ability.Strike(u, 16),
//				Ability.ThrowTerrain(u),
	//			Ability.IceBlast(u),
				Ability.Create(u, Price.Cheap, Species.Grizzly),
				Ability.Create(u, new Price(1,1), Species.TalonedScout),
				Ability.TransformOther(u, Scribe.Write("Animate Metaterrainean"), Abilities.Rank.Create,
                    new Price(1,2),  Species.Metaterrainean, Filter.DestNotCorpse)
			);
            return u;
        }

        public static Unit GrizzlyElder(object source)
        {
            Unit u = new Unit(source, Species.Grizzly, Tokens.Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 25);
            u.watch = new Watch(u, 3);
            u.arsenal.Add(
                Ability.Move(u, 3),
                Ability.Focus(u),
				Ability.Strike(u, 9),
				Ability.Create(u, new Price(0,1), Species.Tree),
				Ability.Sooth(u)
			);
            return u;
        }

        public static Unit Metaterrainean(object source)
        {
            Unit u = new Unit(source, Species.Metaterrainean, Tokens.Rank.Heavy, Species.Rock);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 50);
            u.watch = new Watch(u, 1);
            u.arsenal.Add(
                Ability.Move(u, 2),
                Ability.Focus(u),
				Ability.Strike(u, 20)//,
				//Ability.Engorge(u)
			);
            return u;
        }

        public static Unit TalonedScout(object source)
        {
            Unit u = new Unit(source, Species.TalonedScout, Tokens.Rank.Medium);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 35);
            u.watch = new Watch(u, 4);
            u.arsenal.Add(
                Ability.Move(u, 6),
                Ability.Focus(u),
				Ability.Strike(u, 12)//,
				//Ability.ArcticGust(u)
			);
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