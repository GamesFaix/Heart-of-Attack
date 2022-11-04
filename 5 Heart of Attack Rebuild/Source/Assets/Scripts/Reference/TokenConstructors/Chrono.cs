using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{
	
    public partial class Unit 
    {
        public static Unit OldThreeHands(object source)
        {
            Unit u = new Unit(source, Species.OldThreeHands, Rank.King, Species.BrassHeart);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 85, 2);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            /*u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 2), 
				Ability.Lob(u, 3, 15),
				Ability.HourSaviour(u),
				Ability.MinuteWaltz(u),
				Ability.SecondInCommand(u),
				Ability.Create(u, Price.Cheap, Species.RevolvingTom),
				Ability.Create(u, new Price(2,0), Species.Piecemaker),
				Ability.Create(u, new Price(2,1), Species.Reprospector)
			});*/
            u.arsenal.Sort();
            return u;
        }
        
        public static Unit Piecemaker(object source)
        {
            Unit u = new Unit(source, Species.Piecemaker, Rank.Medium);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 35, 3);
            u.watch = new Watch(u, 1);
            /*u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 4),
				Ability.Strike(u, 10),
				Ability.CreateArc(u, new Price(1,1), Species.Aperture, 2),
				Ability.Repair(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit Reprospector(object source)
        {
            Unit u = new Unit(source, Species.Reprospector, Rank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 2);
            /*u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 4),
				Ability.TimeMine(u),
				Ability.TimeSlam(u),
				Ability.TimeBomb(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit RevolvingTom(object source)
        {
            Unit u = new Unit(source, Species.RevolvingTom, Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 4);
            /*u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 3),
				Ability.Shoot(u, 2, 8),
				Ability.Quickdraw(u)
			});*/
            u.arsenal.Sort();
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