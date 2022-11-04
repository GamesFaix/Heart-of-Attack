using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{

    public partial class Unit
    {
        public static Unit Gargoliath(object source)
        {
            Unit u = new Unit(source, Species.Gargoliath, Rank.King, Species.StoneHeart);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 75);
            u.watch = new Watch(u, 3);
            u.wallet = new Wallet(u, 3);
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
            Unit u = new Unit(source, Species.Ashes, Rank.Minor, Species.None);
            u.body = new Body(u, Plane.Ground, TokenFlags.Destructible);
            u.vitality = new Vitality(u, 15);
            u.watch = new Watch(u, 5);
            //u.Arsenal.Add(Ability.Arise(u));
            u.arsenal.Sort();
            return u;
        }

        public static Unit BatteringRambuchet(object source)
        {
            Unit u = new Unit(source, Species.Rambuchet, Rank.Heavy);
            u.body = new Body(u, Plane.Ground, TokenFlags.Trample);
            u.vitality = new Vitality(u, 65);
            u.watch = new Watch(u, 1);
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
            Unit u = new Unit(source, Species.Conflagragon, Rank.Medium, Species.Ashes);
            u.body = new Body(u, Plane.Air);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 4);
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
            Unit u = new Unit(source, Species.Rook, Rank.Minor, Species.Rock);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 20, 3);
            u.watch = new Watch(u, 3);
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
            Unit u = new Unit(source, Species.Smashbuckler, Rank.Light);
            u.body = new Body(u, Plane.Ground);
            u.vitality = new Vitality(u, 30);
            u.watch = new Watch(u, 3);
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