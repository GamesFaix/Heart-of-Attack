using UnityEngine; 

namespace HOA { 

    public partial class Unit 
    {
        public static Unit Piecemaker(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Piecemaker, "Piecemaker", false, template);
            u.Plane = Plane.Ground;
            u.ScaleMedium();
            u.Health = new Health(u, 35, 3);
            u.Watch = new Watch(u, 1);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 4),
				Ability.Strike(u, 10),
				Ability.CreateArc(u, new Price(1,1), Species.Aperture, 2),
				Ability.Repair(u)
			});
            u.Arsenal.Sort();
            return u;
        }
        
        public static Unit Reprospector(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Reprospector, "Reprospector", false, template);
            u.Plane = Plane.Ground;
            u.ScaleLarge();
            u.Health = new Health(u, 55);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 4),
				Ability.TimeMine(u),
				Ability.TimeSlam(u),
				Ability.TimeBomb(u)
			});
            u.Arsenal.Sort();
            return u;
		}

        public static Unit RevolvingTom(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.RevolvingTom, "Revolving Tom", false, template);
            u.Plane = Plane.Ground;
            u.ScaleSmall();
            u.Health = new Health(u, 30);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 3),
				Ability.Shoot(u, 2, 8),
				Ability.Quickdraw(u)
			});
            u.Arsenal.Sort();
            return u;
		}
    }
}
