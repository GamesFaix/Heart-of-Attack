using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Unit
    {
        public static Unit DreamReaver(object source)
        {
            Unit u = new Unit(source, Species.DreamReaver, UnitRank.King, Species.GlassHeart);
            u.body = new Body(u, Plane.Air);
            u.stats = Tokens.StatSheet.King(u, 75, 3, 2);
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
            Unit u = new Unit(source, Species.Arena, UnitRank.Medium, Species.None);
            u.body = new Body(u, Plane.Ethereal);
            u.stats = new Tokens.StatSheet(u, 55, 2, 3);
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
            Unit u = new Unit(source, Species.Priest, UnitRank.Heavy);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 50, 4);
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
            Unit u = new Unit(source, Species.PrismGuard, UnitRank.Light);
            u.body = new Body(u, Plane.Ground);
            u.stats = Tokens.StatSheet.HalfDodge(u, 15, 3);
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