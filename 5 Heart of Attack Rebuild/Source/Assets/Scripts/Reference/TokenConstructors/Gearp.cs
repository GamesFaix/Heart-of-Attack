using System;
using System.Collections.Generic;
using HOA.To;
using HOA.Ref;
using HOA.Ab;

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
            u.Learn(Abilities.Dart, new AbilityArgs(u, Price.Cheap, Range.b(0, 5)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new AbilityArgs(u, Price.Cheap, 16));
            u.Learn(Abilities.Teleport, new AbilityArgs(u, new Price(1,1), Range.b(0,5),
                Filter.Owner(u.Owner, true) + Filter.Unit, Range.b(0,5)));
            u.Learn(Abilities.Create, new AbilityArgs(u, Price.Cheap, Species.Katandroid));
            u.Learn(Abilities.Create, new AbilityArgs(u, new Price(2, 1), Species.Carapace));
            u.Learn(Abilities.Create, new AbilityArgs(u, new Price(2, 2), Species.Mawth));
            //Ability.GammaBurst(u),
            return u;
        }

        public static Unit CarapaceInvader(object source)
        {
            Unit u = new Unit(source, Species.Carapace, To.Rank.Medium);
            u.body = new Body(u, Plane.Ground);//Sensor.Carapace);
            u.vitality = Vitality.DefenseCap(u, 35, 2, 5);
            u.watch = new Watch(u, 4);
            u.wallet = Wallet.DefenseBoost(u, 2, 3);
            u.Learn(Abilities.Move, new AbilityArgs(u, Price.Cheap, Range.b(0, 3)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
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
            u.Learn(Abilities.Move, new AbilityArgs(u, Price.Cheap, Range.b(0, 4)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
            u.Learn(Abilities.Strike, new AbilityArgs(u, Price.Cheap, 8));
            u.Learn(Abilities.Sprint, new AbilityArgs(u, Price.Free, Range.b(0, 0), 1));
			//Ability.LaserSpin(u)
            return u;
        }

        public static Unit Mawth(object source)
        {
            Unit u = new Unit(source, Species.Mawth, To.Rank.Heavy);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 3);
			u.Learn(Abilities.Dart, new AbilityArgs(u, Price.Cheap, Range.b(0, 4)));
            u.Learn(Abilities.Focus, new AbilityArgs(u, Price.Cheap, 1));
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