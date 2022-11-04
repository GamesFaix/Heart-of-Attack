using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ref;
using HOA.Ab;
using HOA.St;

namespace HOA
{
	
    public partial class Unit 
    {
        public static Unit OldThreeHands(object source)
        {
            Unit u = new Unit(source, Species.OldThreeHands, To.Rank.King, Species.BrassHeart);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 85, 2);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            u.LearnMove(2);
            u.LearnFocus();
            u.Learn(Abilities.Lob, new Ab.Args(u, Price.Cheap, Flex.Rng(u, 3), Scalar.Dam(u, 15)));
            u.LearnCreate(Price.Cheap, Species.RevolvingTom);
            u.LearnCreate(new Price(2,0), Species.Piecemaker);
            u.LearnCreate(new Price(2,1), Species.Reprospector); 
            //Ability.HourSaviour(u),
            //Ability.MinuteWaltz(u),
            //Ability.SecondInCommand(u),
            return u;
        }
        
        public static Unit Piecemaker(object source)
        {
            Unit u = new Unit(source, Species.Piecemaker, To.Rank.Medium);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 35, 3);
            u.watch = new Watch(u, 1);
            u.LearnMove(4);
            u.LearnFocus();
            u.LearnStrike(10);
            u.Learn(Abilities.Summon, new Ab.Args(u, new Price(1, 1), Species.Aperture, Flex.Rng(u, 2)));
            u.Learn(Abilities.Restore, new Ab.Args(u, new Price(0, 2), Filter.Unit, Flex.Rng(u, 2), Scalar.Dam(u, 10)));
            return u;
        }

        public static Unit Reprospector(object source)
        {
            Unit u = new Unit(source, Species.Reprospector, To.Rank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 2);
            u.LearnMove(3);
            u.LearnFocus();
            //Ability.TimeMine(u),
            //Ability.TimeSlam(u),
            //Ability.TimeBomb(u)
            return u;
        }

        public static Unit RevolvingTom(object source)
        {
            Unit u = new Unit(source, Species.RevolvingTom, To.Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 4);
            u.LearnMove(3);
            u.LearnFocus();
            u.LearnShoot(8, 2);
     		//Ability.Quickdraw(u)
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