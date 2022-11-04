using System;
using System.Collections.Generic;
using HOA.To;

namespace HOA
{

    public partial class Unit
    {
        public static Unit DreamReaver(object source)
        {
            Unit u = new Unit(source, Species.DreamReaver, Rank.King, Species.GlassHeart);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 75, 2);
            u.watch = new Watch(u, 3);
            u.wallet = new Wallet(u, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.PsiBeam(u),
				Ability.Dislocate(u),
				Ability.Create(u, Price.Cheap, Species.PrismGuard),
				Ability.CreateAren(u),
				Ability.Create(u, new Price(1,2), Species.Priest)
			});
             * */
            u.arsenal.Sort();
            return u;
        }
       
        public static Unit ArenaNonSensus(object source)
        {
            Unit u = new Unit(source, Species.Arena, Rank.Medium, Species.None);
            u.body = new Body(u, Plane.Ethereal);
            u.vitality = new Vitality(u, 55, 3);
            u.watch = new Watch(u, 2);
           /* u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.MneumonicPlague (u),
				Ability.Oasis (u)
			});*/
            u.arsenal.Sort();
            //u.Notes = () => { return "EXXXtremely buggy."; };
            return u;
        }

        public static Unit PriestOfNaja(object source)
        {
            Unit u = new Unit(source, Species.Priest, Rank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 50, 2);
            u.watch = new Watch(u, 4);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Strike(u, 15),
				Ability.Shove(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit PrismGuard(object source)
        {
            Unit u = new Unit(source, Species.PrismGuard, Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = Vitality.DodgeHalf(u, 15);
            u.watch = new Watch(u, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Strike(u, 8),
				Ability.Refract(u)
			});*/
            u.arsenal.Sort();
            return u;
        }
    }

    public partial class Obstacle
    {
        public static Obstacle Glass(object source)
        {
            Obstacle o = new Obstacle(source, Species.GlassHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }
    }

}