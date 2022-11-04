using System;
using System.Collections.Generic;
using HOA.Stats;
using HOA.Tokens;
using Ledger = HOA.Collections.Ledger<HOA.Abilities.Ability, HOA.Abilities.AbilityArgs>;
using A = HOA.Content.Abilities;
using Tuple = HOA.Abilities.AbilityTuple;
using Std = HOA.Content.AbilityMacros;

namespace HOA.Content
{

    public partial class TokenBuilders
    {
        public static Token Decimatrix(object source)
        {
            return new Unit(
                source, 
                Species.Decimatrix,
                Plane.Ground,
                UnitRank.King,
                new Unit.StatSheetArgs(85, 2, 3),
                new Tuple[]
                {
                    Std.Focus(),
                    Std.Shoot(3, 15),
                    Std.Create(Price.Cheap, Species.Demolitia),
                    Std.Create(new Price(1,1), Species.MeinSchutz),
                    Std.Create(new Price(2,2), Species.Panopticannon)
                },
                new Ledger()
                {
                    //Ability.Tread(u),
                    //Ability.Pierce(u, new Price(1,1), 15),
				    //Ability.Mortar(u),
				    //new ADeciFortify(u),
                },
                TokenFlags.Trample,
                Species.SteelHeart);
        }

        public static Token Demolitia(object source)
        {
            return new Unit(
                source, 
                Species.Demolitia,
                Plane.Ground,
                UnitRank.Light,
                new Unit.StatSheetArgs(30, 3, 2, 0, -100, Unit.StatSheet.BoostDefense),
                new Tuple[]
                {
                    Std.Move(3),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.ThrowGrenade(u),
				    //Ability.PlantGrenade(u)
                });
        }

        public static Token MeinSchutz(object source)
        {
            return new Unit(
                source, 
                Species.MeinSchutz, 
                Plane.Ground,
                UnitRank.Medium,
                new Unit.StatSheetArgs(40, 4),
                new Tuple[]
                {
                    Std.Move(5),
                    Std.Focus(),
                    Std.Shoot(2, 12),
                    Std.Create(new Price(0,1), Species.Mine)
                },
                new Ledger()
                {
                    //Ability.Detonate(u)
                });
        }

        public static Token Panopticannon(object source)
        {
            return new Unit(
                source, 
                Species.Panopticannon, 
                Plane.Ground,
                UnitRank.Heavy,
                new Unit.StatSheetArgs(65, 1, 2, 0, -100, Unit.StatSheet.BoostDefense),
                new Tuple[]
                {
                    Std.Move(1),
                    Std.Focus()
                },
                new Ledger()
                {
                    //Ability.Cannon(u, Price.Cheap, 12),
				    //Ability.Pierce(u, new Price(1,2), 20),
                },
                TokenFlags.Trample);
        }	

        public static Token SteelHeart(object source)
        {
            return new Obstacle(
                source, 
                Species.SteelHeart,
                Plane.Tall,
                TokenFlags.Heart);
        }

        public static Token Mine(object source)
        {
            return new Obstacle(
                source, 
                Species.Mine,
                Plane.Sunken,
                TokenFlags.Destructible);
            //, Sensor.Mine);
            /*o.notes = () =>
            {
                return "If any Token enters Mine's Cell or a neighboring Cell, destroy Mine.\n" +
                "When Mine is destroyed, do 10 damage to all units in its cell. \n" +
                "All units in neighboring cells take 50% damage (rounded down). \n" +
                "Damage continues to spread outward with 50% reduction until 1. \n" +
                "Destroy all destructible tokens that would take damage.";
            };
            o.Destroy = (source, Corpse, log) =>
            {
                o.DefaultDestroy(source, Corpse, log);
                EffectQueue.Interrupt(Effect.ExplosionSequence(new Source(o), o.Body.Cell, 12, false));
            };
             * */
        }
    }

}