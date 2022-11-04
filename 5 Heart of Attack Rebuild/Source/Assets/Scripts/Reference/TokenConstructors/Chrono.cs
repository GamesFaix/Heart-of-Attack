using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ref;
using HOA.Ab;

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
            u.Learn(Abilities.Move, new Ab.Args(u, Price.Cheap, Range.b(0,2)));
            u.Learn(Abilities.Focus, new Ab.Args(u, Price.Cheap, 1));
            u.Learn(Abilities.Lob, new Ab.Args(u, Price.Cheap, Range.b(0, 3), 15));
            u.Learn(Abilities.Create, new Ab.Args(u, Price.Cheap, Species.RevolvingTom));
            u.Learn(Abilities.Create, new Ab.Args(u, new Price(2, 0), Species.Piecemaker));
            u.Learn(Abilities.Create, new Ab.Args(u, new Price(2, 1), Species.Reprospector));
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
            u.Learn(Abilities.Move, new Ab.Args(u, Price.Cheap, Range.b(0, 4)));
            u.Learn(Abilities.Focus, new Ab.Args(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new Ab.Args(u, Price.Cheap, 10));
            u.Learn(Abilities.Summon, new Ab.Args(u, new Price(1, 1), Range.b(0, 2), Species.Aperture));
            u.Learn(Abilities.Restore, new Ab.Args(u, new Price(0, 2), Range.b(0, 2), Filter.Unit, 10));
            return u;
        }

        public static Unit Reprospector(object source)
        {
            Unit u = new Unit(source, Species.Reprospector, To.Rank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 2);
            u.Learn(Abilities.Move, new Ab.Args(u, Price.Cheap, Range.b(0,3)));
            u.Learn(Abilities.Focus, new Ab.Args(u, Price.Cheap, 1));
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
            u.Learn(Abilities.Move, new Ab.Args(u, Price.Cheap, Range.b(0,3)));
            u.Learn(Abilities.Focus, new Ab.Args(u, Price.Cheap, 1));
            u.Learn(Abilities.Shoot, new Ab.Args(u, Price.Cheap, Range.b(0,2), 8));
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