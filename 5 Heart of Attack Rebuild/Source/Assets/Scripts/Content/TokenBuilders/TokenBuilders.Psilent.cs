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
        public static Unit DreamReaver(object source)
        {
            return new Unit(
                source,
                Species.DreamReaver,
                Plane.Air,
                UnitRank.King,
                new Unit.StatSheetArgs(75, 3, 3, 2),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Create(Price.Cheap, Species.PrismGuard),
                    Std.Create(new Price(1,2), Species.Priest)
                },
                new Ledger()
                {
    				//Ability.PsiBeam(u),
	    			//Ability.Dislocate(u),
				    //Ability.CreateAren(u),
                },
                Species.GlassHeart);
        }
       
        public static Unit ArenaNonSensus(object source)
        {
            return new Unit(
                source, 
                Species.Arena, 
                Plane.Ethereal,
                UnitRank.Medium,
                new Unit.StatSheetArgs(55, 2, 2, 3),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.MneumonicPlague (u),
				    //Ability.Oasis (u)
                },
                Species.None);
        }

        public static Unit PriestOfNaja(object source)
        {
            return new Unit(
                source, 
                Species.Priest, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(50, 4),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Strike(15)
                }, 
                new Ledger()
                {
                    //Ability.Shove(u)
                });
        }

        public static Unit PrismGuard(object source)
        {
            return new Unit(
                source, 
                Species.PrismGuard, 
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(15, 3, 2, 0, -100, null, Unit.StatSheet.DamageDodgeHalf),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus(),
                    Std.Strike(8)
                },
                new Ledger()
                {
                    //Ability.Refract(u)
                });
        }

        public static Obstacle Glass(object source)
        {
            return new Obstacle(
                source, 
                Species.GlassHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }
    }

}