using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Unit
    {
        public static Unit Monolith(object source)
        {
            Unit u = new Unit(source, Species.Monolith, UnitRank.King, Species.BloodHeart);
            u.body = new Body(u, Plane.Tall);
            u.stats = Tokens.StatSheet.King(u, 100, 2);
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
        
        public static Unit Gatecreeper(object source)
        {
            Unit u = new Unit(source, Species.Gatecreeper, UnitRank.Heavy);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.stats = new Tokens.StatSheet(u, 30, 4);
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

        public static Unit Necrochancellor(object source)
        {
            Unit u = new Unit(source, Species.Necro, UnitRank.Medium, Species.None);
            u.body = new Body(u, Plane.Ethereal);
            u.stats = new Tokens.StatSheet(u, 30, 3, 5);
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

        public static Unit Recyclops(object source)
        {
            Unit u = new Unit(source, Species.Recyclops, UnitRank.Light);
            u.body = new Body(u, Plane.Ground, (TokenFlags.Destructible | TokenFlags.Corpse));
            u.stats = new Tokens.StatSheet(u, 15, 4);
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
        public static Obstacle BloodHeart (object source)
        {
            Obstacle o = new Obstacle(source, Species.BloodHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }
    }

}