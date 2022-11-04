using System;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Abilities;

namespace HOA
{
	
    public partial class Unit 
    {
        public static Unit OldThreeHands(object source)
        {
            Unit u = new Unit(source, Species.OldThreeHands, Tokens.Rank.King, Species.BrassHeart);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 85, 2);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            u.arsenal.Add(
                Ability.Move(u, 2),
                Ability.Focus(u),
                Ability.AttackArc(u, "Lob", Abilities.Rank.Attack, Price.Cheap, new Range(0,3), 15),
                //Ability.HourSaviour(u),
                //Ability.MinuteWaltz(u),
                //Ability.SecondInCommand(u),
                Ability.Create(u, Price.Cheap, Species.RevolvingTom),
                Ability.Create(u, new Price(2, 0), Species.Piecemaker),
                Ability.Create(u, new Price(2, 1), Species.Reprospector)
            );
            return u;
        }
        
        public static Unit Piecemaker(object source)
        {
            Unit u = new Unit(source, Species.Piecemaker, Tokens.Rank.Medium);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 35, 3);
            u.watch = new Watch(u, 1);
            u.arsenal.Add(
				Ability.Move(u, 4),
                Ability.Focus(u),
				Ability.Strike(u, 10),
				Ability.CreateArc(u, new Price(1,1), Species.Aperture, new Range(0,2)),
				Ability.HealArc(u, "Repair", new Price(0, 2), Filter.Unit, new Range(0, 2), 10)
			);
            return u;
        }

        public static Unit Reprospector(object source)
        {
            Unit u = new Unit(source, Species.Reprospector, Tokens.Rank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 2);
            u.arsenal.Add(
                Ability.Move(u, 4),
                Ability.Focus(u)//, 
                //Ability.TimeMine(u),
                //Ability.TimeSlam(u),
                //Ability.TimeBomb(u)
            );
            return u;
        }

        public static Unit RevolvingTom(object source)
        {
            Unit u = new Unit(source, Species.RevolvingTom, Tokens.Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 4);
            u.arsenal.Add(
				Ability.Move(u, 3),
				Ability.Focus(u),
                Ability.Shoot(u, 2, 8)//,
				//Ability.Quickdraw(u)
			);
            return u;
        }
	}

    public partial class Obstacle
    {
        public static Obstacle Brass(object source)
        {
            Obstacle o = new Obstacle(source, Species.BrassHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }

        public static Obstacle Aperture(object source)
        {
            Obstacle o = new Obstacle(source, Species.Aperture);
            o.body = new Body(o, Plane.Sunken);//Sensor.Aperture);
            /*
            o.Notes = () => { return "0% Functional"; };
            */
            return o;
        }
    }

}