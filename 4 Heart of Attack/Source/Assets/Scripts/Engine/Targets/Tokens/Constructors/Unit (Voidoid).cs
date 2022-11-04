using UnityEngine; 

namespace HOA { 

    public partial class Unit {

        public static Unit Gatecreeper(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Gatecreeper, "Gatecreeper", false, template);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;

            u.ScaleLarge();
            u.Health = new Health(u, 30);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Burrow(u),
				Ability.Recycle(u, new Price(0,1)),
				Ability.Feast(u)
			});
			u.Arsenal.Sort();
            return u;
		}	

        public static Unit Necrochancellor(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Necro, "Necrochancellor", false, template);
            u.Plane = Plane.Ethereal;
            u.OnDeath = Species.None;
            u.ScaleMedium();
            u.Health = new Health(u, 30, 5);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Defile(u),
				Ability.TouchOfDeath(u)
			});
            u.Arsenal.Sort();
            return u;
		}	

        public static Unit Recyclops(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Recyclops, "Recyclops", false, template);
            u.Plane = Plane.Ground;
            u.Body.Destructible = true;
            u.Body.Corpse = true;
            u.ScaleSmall();
            u.Health = new Health(u, 15);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 2),
				Ability.Rage(u, 12),
				Ability.Burst(u),
				Ability.Cannibalize(u)
			});
            u.Arsenal.Sort();
            return u;
		}	
    }
}
