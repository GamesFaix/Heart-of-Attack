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
        public static Unit Gargoliath(object source)
        {
            return new Unit(
                source, 
                Species.Gargoliath, 
                Plane.Air,
                UnitRank.King,
                new Unit.StatSheetArgs(75, 3, 3),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Strike(18),
                    Std.Create(new Price(1,0), Species.Smashbuckler),
                    Std.Create(new Price(1,1), Species.Conflagragon),
                    Std.Create(new Price(2,2), Species.Rambuchet)
                },
                new Ledger()
                {
                    //Ability.Land(u),
				    //Ability.Petrify(u),
				    //Ability.CreateRook(u),
				},
                Species.StoneHeart);
        }
        
        public static Unit Ashes(object source)
        {
            return new Unit(
                source, 
                Species.Ashes, 
                Plane.Ground,
                UnitRank.Minor,
                new Unit.StatSheetArgs(15, 5),
                new Tuple[0] { },
                new Ledger()
                {
                    //Ability.Arise(u)
                }, 
                TokenFlags.Destructible,
                Species.None);
        }

        public static Unit BatteringRambuchet(object source)
        {
            return new Unit(
                source, 
                Species.Rambuchet, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(65, 1),
                new Tuple[]
                {
                    Std.Move(2),
                    Std.Focus(),
                    Std.Strike(16)
                },
                new Ledger()
                {
                    //Ability.Fling(u),
				    //Ability.Cocktail(u)
                },
                TokenFlags.Trample);
        }

        public static Unit Conflagragon(object source)
        {
            return new Unit(
                source, 
                Species.Conflagragon, 
                Plane.Air,
                UnitRank.Medium,
                new Unit.StatSheetArgs(30, 4),
                new Tuple[]
                {
                    Std.Move(6),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Maul(u),
				    //Ability.FireBreath(u)
                },
                Species.Ashes);
        }

        public static Unit Rook(object source)
        {
            return new Unit(
                source, 
                Species.Rook, 
                Plane.Ground,
                UnitRank.Minor,
                new Unit.StatSheetArgs(30, 3, 3),
                new Tuple[]
                {
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Rebuild(u),
				    //Ability.Volley(u)
                },
                Species.Rock);
        }

        public static Unit Smashbuckler(object source)
        {
            return new Unit(
                source, 
                Species.Smashbuckler, 
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(30, 3),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Flail(u),
				    //Ability.Slam(u)
                });
        }

        public static Token Stone(object source)
        {
            return new Obstacle(
                source, 
                Species.StoneHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }
    }

}