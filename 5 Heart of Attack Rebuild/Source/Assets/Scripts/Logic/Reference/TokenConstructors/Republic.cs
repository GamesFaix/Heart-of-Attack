using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{

    public partial class Unit
    {
        public static Unit Decimatrix(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Decimatrix, Rank.King, Species.SteelHeart);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 85);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            /*
            u.Arsenal.Add(new Ability[]{
				Ability.Tread(u),
				Ability.Shoot(u, 3, 15),
				Ability.Pierce(u, new Price(1,1), 15),
				Ability.Mortar(u),
				//new ADeciFortify(u),
				Ability.Create(u, new Price(1,0), Species.Demolitia),
				Ability.Create(u, new Price(1,1), Species.MeinSchutz),
				Ability.Create(u, new Price(2,2), Species.Panopticannon)
			});
             * */
            u.arsenal.Sort();
            return u;
        }
        
        public static Unit Demolitia(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Demolitia, Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 3);
            u.wallet = Wallet.DefenseBoost(u, 2, 4);
            /*u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 3),
				Ability.ThrowGrenade(u),
				Ability.PlantGrenade(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit MeinSchutz(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.MeinSchutz, Rank.Medium);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 40);
            u.watch = new Watch(u, 4);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 5),
				Ability.Shoot(u, 2, 12),
				Ability.Create(u, new Price(0,1), Species.Mine),
				Ability.Detonate(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit Panopticannon(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Panopticannon, Rank.Heavy);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 65);
            u.watch = new Watch(u, 1);
            u.wallet = Wallet.DefenseBoost(u, 2, 2);
            /*u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 1),
				Ability.Cannon(u, Price.Cheap, 12),
				Ability.Pierce(u, new Price(1,2), 20),
			});*/
            u.arsenal.Sort();
            return u;
        }	
    }

    public partial class Obstacle
    {
        public static Obstacle SteelHeart(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.SteelHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }

        public static Obstacle Mine(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Mine);
            o.body = new Body(o, Plane.Sunken, TokenFlags.Destructible);//, Sensor.Mine);
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
            return o;
        }
    }

}