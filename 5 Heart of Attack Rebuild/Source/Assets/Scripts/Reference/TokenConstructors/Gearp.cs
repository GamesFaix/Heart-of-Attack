using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA 
{

    public partial class Unit
    {
        public static Unit Kabutomachine(object source)
        {
            Unit u = new Unit(source, Species.Kabutomachine, Rank.King, Species.SiliconHeart);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 75);
            u.watch = new Watch(u, 4);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Dart(u, 5),
				Ability.Strike(u, 16),
				Ability.Warp(u),
				Ability.GammaBurst(u),
				Ability.Create(u, Price.Cheap, Species.Katandroid),
				Ability.Create(u, new Price(2,1), Species.Carapace),
				Ability.Create(u, new Price(2,2), Species.Mawth)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit CarapaceInvader(object source)
        {
            Unit u = new Unit(source, Species.Carapace, Rank.Medium);
            u.body = new Body(u, Plane.Ground);//Sensor.Carapace);
            u.vitality = Vitality.DefenseCap(u, 35, 2, 5);
            u.watch = new Watch(u, 4);
            u.wallet = Wallet.DefenseBoost(u, 2, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Shock(u),
				Ability.Discharge(u)
			});*/
            u.arsenal.Sort();
            /*u.Notes = () =>
            {
                return "All non-Carapace neighboring teammates add Carapace's Defense.";
            };*/
            return u;
        }

        public static Unit Katandroid(object source)
        {
            Unit u = new Unit(source, Species.Katandroid, Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 25);
            u.watch = new Watch(u, 5);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Strike(u, 8),
				Ability.Sprint(u),
				Ability.LaserSpin(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit Mawth(object source)
        {
            Unit u = new Unit(source, Species.Mawth, Rank.Heavy);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 55);
            u.watch = new Watch(u, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Dart(u, 4),
				Ability.LaserShot(u),
				Ability.Bombard(u)
			});*/
            u.arsenal.Sort();
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