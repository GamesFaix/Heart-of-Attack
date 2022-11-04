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
        public static Token Kabutomachine(object source)
        {
            return new Unit(
                source, 
                Species.Kabutomachine, 
                Plane.Air,
                UnitRank.King,
                new Unit.StatSheetArgs(75, 4, 3),
                new Tuple[]
                {
                    Std.Dart(5),
                    Std.Focus(),
                    Std.Strike(16),
                    Std.Create(Price.Cheap, Species.Katandroid),
                    Std.Create(new Price(2,1), Species.Carapace),
                    Std.Create(new Price(2,2), Species.Mawth)
                },
                new Ledger()
                {
                    {A.Teleport, new AbilityArgs(new Price(1, 1),
                        Arg.Filter(RF.Filter0, FilterBuilders.Friendly + FilterBuilders.Unit), 
                        Arg.Stat(RS.Range0, new Flex(0, 5)), 
                        Arg.Stat(RS.Range1, new Flex(0, 5)))}
                    //Ability.GammaBurst(u),
                },
                Species.SiliconHeart);
        }

        public static Token CarapaceInvader(object source)
        {
            return new Unit(
                source,
                Species.Carapace,
                Plane.Ground,
                UnitRank.Medium,
                new Unit.StatSheetArgs(35, 4, 2, 2, 5, Unit.StatSheet.BoostDefense),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Shock(u),
                    //Ability.Discharge(u)
                }); 
			/*u.Notes = () =>
            {
                return "All non-Carapace neighboring teammates add Carapace's Defense.";
            };*/
        }

        public static Token Katandroid(object source)
        {
            return new Unit(
                source, 
                Species.Katandroid, 
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(25, 5),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Strike(8)
                },
                new Ledger()
                {
                    {A.Sprint, new AbilityArgs(Price.Free, 
                        Arg.Stat(RS.Range0, new Flex(0, 0)), 
                        Arg.Stat(RS.Boost, new Scalar(1)))}
                    //Ability.LaserSpin(u)
                });
        }

        public static Token Mawth(object source)
        {
            return new Unit(
                source,
                Species.Mawth,
                Plane.Air,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(55, 3),
                new Tuple[]
                {
                    Std.Dart(4),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.LaserShot(u),
                    //Ability.Bombard(u)
                });
        }

        public static Token Silicon(object source)
        {
            return new Obstacle(
                source, 
                Species.SiliconHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }
    }



}