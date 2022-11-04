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
        public static Unit Monolith(object source)
        {
            return new Unit(
                source, 
                Species.Monolith, 
                Plane.Tall,
                UnitRank.King,
                new Unit.StatSheetArgs(100, 2, 3),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Create(new Price(1,0), Species.Recyclops),
                    Std.Create(new Price(2,1), Species.Necro),
                },
                new Ledger()
                {
                    //Ability.Rage(u, 20),
				    //Ability.DeathField(u),
				    //Ability.BloodAltar(u),
                    //Ability.Recycle(u, new Price(1,0)),
                    //Ability.CreateArc(u, new Price(1,2), Species.Gatecreeper, 3,3)
                },
                Species.BloodHeart);
        }
        
        public static Unit Gatecreeper(object source)
        {
            return new Unit(
                source, 
                Species.Gatecreeper, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(30, 4),
                new Tuple[]
                {
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Burrow(u),
				    //Ability.Recycle(u, new Price(0,1)),
			        //Ability.Feast(u)
                },
                TokenFlags.Trample);
        }

        public static Unit Necrochancellor(object source)
        {
            return new Unit(
                source, 
                Species.Necro, 
                Plane.Ethereal,
                UnitRank.Medium,
                new Unit.StatSheetArgs(30, 3, 2, 5),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Defile(u),
				    //Ability.TouchOfDeath(u)
                },
                Species.None);
        }

        public static Unit Recyclops(object source)
        {
            return new Unit(
                source, 
                Species.Recyclops, 
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(15, 4),
                new Tuple[]
                {
                    Std.Move(2),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Rage(u, 12),
				    //Ability.Burst(u),
				    //Ability.Cannibalize(u)
                },
                TokenFlags.Destructible | TokenFlags.Corpse);
        }

        public static Obstacle BloodHeart (object source)
        {
            return new Obstacle(
                source, 
                Species.BloodHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }
    }

}