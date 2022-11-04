using System;
using System.Collections.Generic;
using HOA.Content;
using HOA.Abilities;
using HOA.Stats;
using HOA.Fargo;

namespace HOA.Tokens
{
	
    public partial class Unit 
    {
        public static Unit OldThreeHands(object source)
        {
            Unit u = new Unit(source, Species.OldThreeHands, UnitRank.King, Species.BrassHeart);
            u.body = new Body(u, Plane.Ground);
            u.stats = Tokens.StatSheet.King(u, 85, 2, 2);
            u.LearnMove(2);
            u.LearnFocus();
            u.Learn(Content.Abilities.Lob, new AbilityArgs(u, Price.Cheap, 
                Arg.Stat(FS.Range0, new Flex(0, 3)),
                Arg.Stat(FS.Damage, new Scalar(15))));
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
            Unit u = new Unit(source, Species.Piecemaker, UnitRank.Medium);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 35, 1, 3);
            u.LearnMove(4);
            u.LearnFocus();
            u.LearnStrike(10);
            u.Learn(Content.Abilities.Summon, new AbilityArgs(u, new Price(1, 1), Species.Aperture, 
                Arg.Stat(FS.Range0, new Flex(0, 2))));
            u.Learn(Content.Abilities.Restore, new AbilityArgs(u, new Price(0, 2), 
                Arg.Filter(FF.Filter0, Filter.Unit),
                Arg.Stat(FS.Range0, new Flex(0, 2)), 
                Arg.Stat(FS.Damage, new Scalar(10))));
            return u;
        }

        public static Unit Reprospector(object source)
        {
            Unit u = new Unit(source, Species.Reprospector, UnitRank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 55, 2);
            u.LearnMove(3);
            u.LearnFocus();
            //Ability.TimeMine(u),
            //Ability.TimeSlam(u),
            //Ability.TimeBomb(u)
            return u;
        }

        public static Unit RevolvingTom(object source)
        {
            Unit u = new Unit(source, Species.RevolvingTom, UnitRank.Light);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 30, 4);
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