using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ref;
using HOA.Ab;
using HOA.Stats;
using Args = HOA.Ab.AbilityArgs;
using HOA.Fargo;

namespace HOA 
{

    public partial class Unit
    {
        public static Unit Kabutomachine(object source)
        {
            Unit u = new Unit(source, Species.Kabutomachine, To.Rank.King, Species.SiliconHeart);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 75);
            u.watch = new Watch(u, 4);
            u.LearnDart(5);
            u.LearnFocus();
            u.LearnStrike(16);
            u.Learn(Abilities.Teleport, new Args(u, new Price(1, 1),
                Arg.Filter(FF.Filter0, Filter.Owner(u.Owner, true) + Filter.Unit), 
                Arg.Stat(FS.Range0, Flex.Rng(u, 5)), 
                Arg.Stat(FS.Range1, Flex.Rng(u, 5))));
            //Ability.GammaBurst(u),
            u.LearnCreate(Price.Cheap, Species.Katandroid);
            u.LearnCreate(new Price(2, 1), Species.Carapace);
            u.LearnCreate(new Price(2, 2), Species.Mawth);
            return u;
        }

        public static Unit CarapaceInvader(object source)
        {
            Unit u = new Unit(source, Species.Carapace, To.Rank.Medium);
            u.body = new Body(u, Plane.Ground);//Sensor.Carapace);
            u.vitality = Vitality.DefenseCap(u, 35, 2, 5);
            u.watch = new Watch(u, 4);
            u.wallet = Wallet.DefenseBoost(u, 2);
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
            Unit u = new Unit(source, Species.Katandroid, To.Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 25);
            u.watch = new Watch(u, 5);
            u.LearnMove(4);
            u.LearnFocus();
            u.LearnStrike(8);
            u.Learn(Abilities.Sprint, new Args(u, Price.Free, 
                Arg.Stat(FS.Range0, Flex.Rng(u, 0)), 
                Arg.Stat(FS.Boost, Scalar.Boost(u, 1))));
			//Ability.LaserSpin(u)
            return u;
        }

        public static Unit Mawth(object source)
        {
            Unit u = new Unit(source, Species.Mawth, To.Rank.Heavy);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 3);
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