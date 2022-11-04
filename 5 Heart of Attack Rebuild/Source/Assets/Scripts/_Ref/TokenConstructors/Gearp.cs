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
        public static Unit Kabutomachine(object source)
        {
            Unit u = new Unit(source, Species.Kabutomachine, UnitRank.King, Species.SiliconHeart);
            u.body = new Body(u, Plane.Air);
            u.stats = Tokens.StatSheet.King(u, 75, 4);
            u.LearnDart(5);
            u.LearnFocus();
            u.LearnStrike(16);
            u.Learn(Content.Abilities.Teleport, new AbilityArgs(u, new Price(1, 1),
                Arg.Filter(FF.Filter0, Filter.Owner(u.Owner, true) + Filter.Unit), 
                Arg.Stat(FS.Range0, new Flex(0, 5)), 
                Arg.Stat(FS.Range1, new Flex(0, 5))));
            //Ability.GammaBurst(u),
            u.LearnCreate(Price.Cheap, Species.Katandroid);
            u.LearnCreate(new Price(2, 1), Species.Carapace);
            u.LearnCreate(new Price(2, 2), Species.Mawth);
            return u;
        }

        public static Unit CarapaceInvader(object source)
        {
            Unit u = new Unit(source, Species.Carapace, UnitRank.Medium);
            u.body = new Body(u, Plane.Ground);//Sensor.Carapace);
            u.stats = Tokens.StatSheet.FocusSideEffectsDefenseCap(u, 35, 4, 2, 5, (n) => { u.StatAdd(u, FS.Defense, n); });
            u.LearnMove(3);
            u.LearnFocus();
            //Ability.Shock(u),
			//Ability.Discharge(u)
			/*u.Notes = () =>
            {
                return "All non-Carapace neighboring teammates add Carapace's Defense.";
            };*/
            return u;
        }

        public static Unit Katandroid(object source)
        {
            Unit u = new Unit(source, Species.Katandroid, UnitRank.Light);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 25, 5);
            u.LearnMove(4);
            u.LearnFocus();
            u.LearnStrike(8);
            u.Learn(Content.Abilities.Sprint, new AbilityArgs(u, Price.Free, 
                Arg.Stat(FS.Range0, new Flex(0, 0)), 
                Arg.Stat(FS.Boost, new Scalar(1))));
			//Ability.LaserSpin(u)
            return u;
        }

        public static Unit Mawth(object source)
        {
            Unit u = new Unit(source, Species.Mawth, UnitRank.Heavy);
            u.body = new Body(u, Plane.Air);
            u.stats = new Tokens.StatSheet(u, 55, 3);
            u.LearnDart(4);
            u.LearnFocus();
           	//Ability.LaserShot(u),
			//Ability.Bombard(u)
            return u;
        }
    }

    public partial class Obstacle
    {
        public static Obstacle Silicon(object source)
        {
            Obstacle o = new Obstacle(source, Species.SiliconHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }
    }



}