using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{

    public partial class Unit
    {
        public static Unit Monolith(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Monolith, Rank.King, Species.BloodHeart);
            u.body = new Body(u, Plane.Tall);
            u.vitality = new Vitality(u, 100);
            u.watch = new Watch(u, 2);
            u.wallet = new Wallet(u, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Rage(u, 20),
				Ability.DeathField(u),
				Ability.BloodAltar(u),
				Ability.Create(u, new Price(1,0), Species.Recyclops),
				Ability.Recycle(u, new Price(1,0)),
				Ability.Create(u, new Price(2,1), Species.Necro),
				Ability.CreateArc(u, new Price(1,2), Species.Gatecreeper, 3,3)
			});
             * */
            u.arsenal.Sort();
            return u;
        }
        
        public static Unit Gatecreeper(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Gatecreeper, Rank.Heavy);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 4);
            /*
             * u.arsenal.Add({
			//	Ability.Burrow(u),
				//Ability.Recycle(u, new Price(0,1)),
			//	Ability.Feast(u)
			});
             * */
            u.arsenal.Sort();
            
            return u;
        }

        public static Unit Necrochancellor(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Necro, Rank.Medium, Species.None);
            u.body = new Body(u, Plane.Ethereal);
            u.vitality = new Vitality(u, 30, 5);
            u.watch = new Watch(u, 3);
           /*
            * u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Defile(u),
				Ability.TouchOfDeath(u)
			});
            * */
            u.arsenal.Sort();
            
            return u;
        }

        public static Unit Recyclops(ITokenCreator creator)
        {
            Unit u = new Unit(creator, Species.Recyclops, Rank.Light);
            u.body = new Body(u, Plane.Ground, (TokenFlags.Destructible | TokenFlags.Corpse));
            u.vitality = new Vitality(u, 15);
            u.watch = new Watch(u, 4);
            /*
             * u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 2),
				Ability.Rage(u, 12),
				Ability.Burst(u),
				Ability.Cannibalize(u)
			});
             * */
            u.arsenal.Sort();
            
            return u;
        }

        
    }

    public partial class Obstacle
    {
        public static Obstacle BloodHeart (ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.BloodHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }
    }

}