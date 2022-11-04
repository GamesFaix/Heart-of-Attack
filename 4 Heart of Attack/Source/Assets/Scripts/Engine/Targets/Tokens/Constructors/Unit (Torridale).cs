using UnityEngine; 

namespace HOA { 

    public partial class Unit
    {
        public static Unit Ashes(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Ashes, "Ashes", false, template);
            u.Plane = Plane.Ground;
            u.Body.Destructible = true;
            u.OnDeath = Species.None;
            u.ScaleSmall();
            u.Health = new Health(u, 15);
            u.Watch = new Watch(u, 5);
            u.Arsenal.Remove("Focus");
            u.Arsenal.Add(Ability.Arise(u));
            u.Arsenal.Sort();
            return u;
        }
        
        public static Unit BatteringRambuchet(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Rambuchet, "Battering Rambuchet", false, template);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;
            u.ScaleLarge();
            u.Health = new Health(u, 65);
            u.Watch = new Watch(u, 1);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 2),
				Ability.Strike(u, 16),
				Ability.Fling(u),
				Ability.Cocktail(u)
			});
            u.Arsenal.Sort();
            return u;
		}

        public static Unit Conflagragon(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Conflagragon, "Conflagragon", false, template);
            u.Plane = Plane.Air;
            u.OnDeath = Species.Ashes;
            u.ScaleMedium();
            u.Health = new Health(u, 30);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 6),
				Ability.Maul(u),
				Ability.FireBreath(u)
			});
            u.Arsenal.Sort();
            return u;
		}

        public static Unit Rook(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Rook, "Rook", false, template);
            u.Plane = Plane.Ground;
            u.OnDeath = Species.Rock;
            u.ScaleMedium();
            u.Health = new Health(u, 20, 3);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Rebuild(u),
				Ability.Volley(u)
			});
            u.Arsenal.Sort();
            return u;
        }	

        public static Unit Smashbuckler(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Smashbuckler, "Smashbuckler", false, template);
            u.Plane = Plane.Ground;
            u.ScaleSmall();
            u.Health = new Health(u, 30);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Flail(u),
				Ability.Slam(u)
			});
            u.Arsenal.Sort();
            return u;
		}

        
    }
}
