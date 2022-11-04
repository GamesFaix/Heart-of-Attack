using System;
using System.Collections.Generic;
using HOA.Content;
using HOA.Abilities;
using HOA.Stats;
using HOA.Args;
using HOA.Tokens;

using Ledger = HOA.Collections.Ledger<HOA.Abilities.Ability, HOA.Abilities.AbilityArgs>;
using A = HOA.Content.Abilities;
using Tuple = HOA.Abilities.AbilityTuple;
using Std = HOA.Content.AbilityMacros;

namespace HOA.Content
{
	
    public partial class TokenBuilders 
    {
        public static Token OldThreeHands(object source)
        {
            return new Unit(
                source, 
                Species.OldThreeHands, 
                Plane.Ground, 
                UnitRank.King, 
                new Unit.StatSheetArgs(85, 2, 3, 2),
                new Tuple[]{
                    Std.Move(2), 
                    Std.Focus(), 
                    Std.Create(Price.Cheap, Species.RevolvingTom),
                    Std.Create(new Price(2,0), Species.Piecemaker),
                    Std.Create(new Price(2,1), Species.Reprospector)
                },
                new Ledger()
                {
                   {A.Lob, new AbilityArgs(Price.Cheap, 
                        Arg.Stat(RS.Range0, new Flex(0, 3)),
                        Arg.Stat(RS.Damage, new Scalar(15)))}
                   //Ability.HourSaviour(u),
                   //Ability.MinuteWaltz(u),
                   //Ability.SecondInCommand(u),
                },
                Species.BrassHeart);
        }

        public static Token Piecemaker(object source)
        {
            return new Unit(
                source,
                Species.Piecemaker,
                Plane.Ground,
                UnitRank.Medium,
                new Unit.StatSheetArgs(35, 1, 2, 3),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Strike(10)
                },
                new Ledger()
                {
                    {A.Summon, new AbilityArgs(new Price(1, 1), Species.Aperture, 
                        Arg.Stat(RS.Range0, new Flex(0, 2)))},
                    {A.Restore, new AbilityArgs(new Price(0, 2), 
                        Arg.Filter(RF.Filter0, FilterBuilders.Unit),
                        Arg.Stat(RS.Range0, new Flex(0, 2)), 
                        Arg.Stat(RS.Damage, new Scalar(10)))}
                });
        }

        public static Token Reprospector(object source)
        {
            return new Unit(
                source, 
                Species.Reprospector, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(55, 2),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.TimeMine(u),
                    //Ability.TimeSlam(u),
                    //Ability.TimeBomb(u)
                });
        }

        public static Token RevolvingTom(object source)
        {
            return new Unit(
                source,
                Species.RevolvingTom,
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(30, 4),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus(),
                    Std.Shoot(2, 8)
                },
                new Ledger()
                {
                    //Ability.Quickdraw(u)
                });
	    }

        public static Token Brass(object source)
        {
            return new Obstacle(
                source, 
                Species.BrassHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }

        public static Token Aperture(object source)
        {
            return new Obstacle(
                source, 
                Species.Aperture,
                Plane.Sunken);
            //Sensor.Aperture);
            //o.Notes = () => { return "0% Functional"; };
        }
    }

}