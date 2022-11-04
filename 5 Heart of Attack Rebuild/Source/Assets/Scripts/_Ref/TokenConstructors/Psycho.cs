using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Unit
    {
        public static Unit BlackWinnow(object source)
        {
            Unit u = new Unit(source, Species.BlackWinnow, UnitRank.King, Species.SilkHeart);
            u.body = new Body(u, Plane.Ground);
            u.stats = Tokens.StatSheet.King(u, 75, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Sting(u, 15),
				Ability.CreateLich(u),
				Ability.WebShot(u)
			});*/
            u.arsenal.Sort();
            return u;


        }
        
        public static Unit Beesassin(object source)
        {
            Unit u = new Unit(source, Species.Beesassin, UnitRank.Light);
            u.body = new Body(u, Plane.Air);
            u.stats = new Tokens.StatSheet(u, 25, 5);
           // u.timers.Add(Timer.Corrosion(new Source(u), u, 12));
            /*u.Arsenal.Add(new Ability[]{
				Ability.Dart(u, 5),
				Ability.Sting(u, 8),
				Ability.FatalBlow(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit Lichenthrope(object source)
        {
            Unit u = new Unit(source, Species.Lichenthrope, UnitRank.Minor, Species.None);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 15, 5);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 0),
				Ability.Feed(u),
				Ability.Evolve(u, Price.Cheap, Species.Beesassin),
				Ability.Evolve(u, new Price(1,2), Species.Mycolonist),
				Ability.Evolve(u, new Price(1,3), Species.ManTrap)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit MartianManTrap(object source)
        {
            Unit u = new Unit(source, Species.ManTrap, UnitRank.Heavy, Species.Tree);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.stats = new Tokens.StatSheet(u, 70, 4);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Creep(u),
				Ability.Grow(u),
				Ability.Strike(u, 12),
				Ability.VineWhip(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit Mycolonist(object source)
        {
            Unit u = new Unit(source, Species.Mycolonist, UnitRank.Medium);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 40, 2);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 2),
				Ability.Sporatic(u),
				Ability.Donate(u),
				Ability.Seed(u)
			});*/
            u.arsenal.Sort();
            return u;
        }
    }

    public partial class Obstacle
    {
        public static Obstacle Silk(object source)
        {
            Obstacle o = new Obstacle(source, Species.SilkHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }

        public static Obstacle Web(object source)
        {
            Obstacle o = new Obstacle(source, Species.Web);
            o.body = new Body(o, Plane.Sunken, TokenFlags.Destructible); //Sensor.Web);
            /*o.Notes = () =>
            {
                return "Ground and Air units may not move through " + o.ID.Name + "." +
                "\nUnits sharing " + o.ID.Name + "'s Cell have a Move Range of 1.";
            };
             * */
            return o;
        }
    }

}