using System;
using System.Collections.Generic;
using HOA.Tokens;

using Ledger = HOA.Collections.Ledger<HOA.Abilities.Ability, HOA.Abilities.AbilityArgs>;
using A = HOA.Content.Abilities;
using Tuple = HOA.Abilities.AbilityTuple;
using Std = HOA.Content.AbilityMacros;

namespace HOA.Content
{

    public partial class TokenBuilders
    {
        public static Token BlackWinnow(object source)
        {
            return new Unit(
                source, 
                Species.BlackWinnow, 
                Plane.Ground,
                UnitRank.King, 
                new Unit.StatSheetArgs(75, 3, 3),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Sting(u, 15),
				    //Ability.CreateLich(u),
				    //Ability.WebShot(u)
                },
                Species.SilkHeart);
        }

        public static Token Beesassin(object source)
        {
            return new Unit(
                source, 
                Species.Beesassin, 
                Plane.Air,
                UnitRank.Light,
                new Unit.StatSheetArgs(25, 5),
                new Tuple[]
                {
                    Std.Dart(5),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Sting(u, 8),
				    //Ability.FatalBlow(u)
                });
           // u.timers.Add(Timer.Corrosion(new Source(u), u, 12));
        }

        public static Token Lichenthrope(object source)
        {
            return new Unit(
                source, 
                Species.Lichenthrope, 
                Plane.Ground,
                UnitRank.Minor,
                new Unit.StatSheetArgs(15, 5),
                new Tuple[]
                {
                    Std.Move(0),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Feed(u),
				    //Ability.Evolve(u, Price.Cheap, Species.Beesassin),
				    //Ability.Evolve(u, new Price(1,2), Species.Mycolonist),
				    //Ability.Evolve(u, new Price(1,3), Species.ManTrap)
                },
                Species.None);
        }

        public static Token MartianManTrap(object source)
        {
            return new Unit(
                source, 
                Species.ManTrap, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(70, 4),
                new Tuple[]
                {
                    Std.Strike(12)
                },
                new Ledger()
                {
                    //Ability.Creep(u),
				    //Ability.Grow(u),
                    //Ability.VineWhip(u)
                },
                TokenFlags.Trample,
                Species.Tree);
        }

        public static Token Mycolonist(object source)
        {
            return new Unit(
                source, 
                Species.Mycolonist, 
                Plane.Ground,
                UnitRank.Medium,
                new Unit.StatSheetArgs(40, 2),
                new Tuple[]
                {
                    Std.Move(2),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Donate(u),
				    //Ability.Seed(u)
                });
        }

        public static Token Silk(object source)
        {
            return new Obstacle(
                source, 
                Species.SilkHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }

        public static Token Web(object source)
        {
            return new Obstacle(
                source, 
                Species.Web,
                Plane.Sunken,
                TokenFlags.Destructible);
    //Sensor.Web);
            /*o.Notes = () =>
            {
                return "Ground and Air units may not move through " + o.ID.Name + "." +
                "\nUnits sharing " + o.ID.Name + "'s Cell have a Move Range of 1.";
            };
             * */
        }
    }

}