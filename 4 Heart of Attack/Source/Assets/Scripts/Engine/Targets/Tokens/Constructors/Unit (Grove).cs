using UnityEngine; 

namespace HOA { 

    public partial class Unit 
    {
        public static Unit GrizzlyElder(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Grizzly, "Grizzly Elder", false, template);
            u.Plane = Plane.Ground;
            u.ScaleSmall();
            u.Health = new Health(u, 25);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Strike(u, 9),
				Ability.Create(u, new Price(0,1), Species.Tree),
				Ability.Sooth(u)
			});
            u.Arsenal.Sort();
            return u;
        }
     
        public static Unit Metaterrainean(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Metaterrainean, "Metaterrainean", false, template);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;
            u.OnDeath = Species.Rock;
            u.ScaleLarge();
            u.Health = new Health(u, 50);
            u.Watch = new Watch(u, 1);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 2),
				Ability.Strike(u, 20),
				Ability.Engorge(u)
			});
            u.Arsenal.Sort();
            return u;
		}		

        public static Unit TalonedScout(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.TalonedScout,"Taloned Scout", false, template);
            u.Plane = Plane.Air;
            u.ScaleMedium();
            u.Health = new Health(u, 35);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 6),
				Ability.Strike(u, 12),
				Ability.ArcticGust(u)
			});
            u.Arsenal.Sort();
            return u;
		}
    }
}
