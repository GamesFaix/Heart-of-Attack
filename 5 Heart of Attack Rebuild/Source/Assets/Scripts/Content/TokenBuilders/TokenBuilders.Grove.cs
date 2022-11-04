using System;
using System.Collections.Generic;
using HOA.Abilities;
using HOA.Content;
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

        public static Token Ultratherium(object source)
        {
            return new Unit(
                source, 
                Species.Ultratherium, 
                Plane.Ground,
                UnitRank.King,
                new Unit.StatSheetArgs(80, 2, 3),
                new Tuple[]
                {
                    Std.Move(4),
                    Std.Focus(),
                    Std.Strike(16),
                    Std.Create(Price.Cheap, Species.Grizzly),
                    Std.Create(new Price(1,1), Species.TalonedScout),
                },
                new Ledger()
                {
                    {A.Transmute, new AbilityArgs(new Price(1, 2), 
                        Arg.Filter(RF.Filter0, FilterBuilders.DestNotCorpse), 
                        Species.Metaterrainean)}
                    //Ability.ThrowTerrain(u),
	                //Ability.IceBlast(u),
			    },
                TokenFlags.Trample,
                Species.FirHeart);
        }

        public static Token GrizzlyElder(object source)
        {
            return new Unit(
                source,
                Species.Grizzly,
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(25, 3),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus(),
                    Std.Strike(9),
                    Std.Create(new Price(0,1), Species.Tree)
                },
                new Ledger()
                {
                    {A.Heal, new AbilityArgs(new Price(1, 1),
                        Arg.Filter(RF.Filter0, FilterBuilders.Friendly + FilterBuilders.ExceptSelf), 
                        Arg.Stat(RS.Damage, new Scalar(7)))}
                });
        }

        public static Token Metaterrainean(object source)
        {
            return new Unit(
                source, 
                Species.Metaterrainean, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(50, 1),
                new Tuple[]
                {
                    Std.Move(2),
                    Std.Focus(),
                    Std.Strike(20)
                },
                new Ledger()
                {
                    //Ability.Engorge(u)
                },
                TokenFlags.Trample,
                Species.Rock);
        }

        public static Token TalonedScout(object source)
        {
            return new Unit(
                source,
                Species.TalonedScout,
                Plane.Air,
                UnitRank.Medium,
                new Unit.StatSheetArgs(35, 4),
                new Tuple[]
                {
                    Std.Move(6),
                    Std.Focus(),
                    Std.Strike(12)
                },
                new Ledger()
                {
                    //Ability.ArcticGust(u)
                });
        }
   
        public static Token Fir(object source)
        {
            return new Obstacle(
                source, 
                Species.FirHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }
    }

}