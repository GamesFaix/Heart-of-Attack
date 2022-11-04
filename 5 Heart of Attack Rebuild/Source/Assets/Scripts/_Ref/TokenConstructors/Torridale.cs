using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Unit
    {
        public static Unit Gargoliath(object source)
        {
            Unit u = new Unit(source, Species.Gargoliath, UnitRank.King, Species.StoneHeart);
            u.body = new Body(u, Plane.Air);
            u.stats = Tokens.StatSheet.King(u, 75, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Strike(u, 18),
				Ability.Land(u),
				Ability.Petrify(u),
				Ability.CreateRook(u),
				Ability.Create(u, Price.Cheap, Species.Smashbuckler),
				Ability.Create(u, new Price(1,1), Species.Conflagragon),
				Ability.Create(u, new Price(2,2), Species.Rambuchet)
			});
             * */
            u.arsenal.Sort();
            return u;
        }
        
        public static Unit Ashes(object source)
        {
            Unit u = new Unit(source, Species.Ashes, UnitRank.Minor, Species.None);
            u.body = new Body(u, Plane.Ground, TokenFlags.Destructible);
            u.stats = new Tokens.StatSheet(u, 15, 5);
            //u.Arsenal.Add(Ability.Arise(u));
            u.arsenal.Sort();
            return u;
        }

        public static Unit BatteringRambuchet(object source)
        {
            Unit u = new Unit(source, Species.Rambuchet, UnitRank.Heavy);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.stats = new Tokens.StatSheet(u, 65, 1);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 2),
				Ability.Strike(u, 16),
				Ability.Fling(u),
				Ability.Cocktail(u)
			});
             * */
            u.arsenal.Sort();
            return u;
        }

        public static Unit Conflagragon(object source)
        {
            Unit u = new Unit(source, Species.Conflagragon, UnitRank.Medium, Species.Ashes);
            u.body = new Body(u, Plane.Air);
            u.stats = new Tokens.StatSheet(u, 30, 4);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 6),
				Ability.Maul(u),
				Ability.FireBreath(u)
			});*/
            u.arsenal.Sort();
            return u;
        }

        public static Unit Rook(object source)
        {
            Unit u = new Unit(source, Species.Rook, UnitRank.Minor, Species.Rock);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 30, 3, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Rebuild(u),
				Ability.Volley(u)
			});
             * */
            u.arsenal.Sort();
            return u;
        }

        public static Unit Smashbuckler(object source)
        {
            Unit u = new Unit(source, Species.Smashbuckler, UnitRank.Light);
            u.body = new Body(u, Plane.Ground);
            u.stats = new Tokens.StatSheet(u, 30, 3);
            /*u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Flail(u),
				Ability.Slam(u)
			});
             * */
            u.arsenal.Sort();
            return u;
        }
    }

    public partial class Obstacle
    {
        public static Obstacle Stone(object source)
        {
            Obstacle o = new Obstacle(source, Species.StoneHeart);
            o.body = new Body(o, Plane.Tall, TokenFlags.Heart);
            return o;
        }
    }

}