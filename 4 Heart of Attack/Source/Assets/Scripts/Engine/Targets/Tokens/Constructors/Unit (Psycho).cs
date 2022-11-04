using UnityEngine; 

namespace HOA { 

    public partial class Unit
    {
        public static Unit Beesassin(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Beesassin, "Beesassin", false, template);
            u.Plane = Plane.Air;
            u.ScaleSmall();
            u.Health = new Health(u, 25);
            u.Watch = new Watch(u, 5);
            u.timers.Add(Timer.Corrosion(new Source(u), u, 12));
            u.Arsenal.Add(new Ability[]{
				Ability.Dart(u, 5),
				Ability.Sting(u, 8),
				Ability.FatalBlow(u)
			});
            u.Arsenal.Sort();
            return u;
        }

        public static Unit Lichenthrope(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Lichenthrope, "Lichenthrope", false, template);
            u.Plane = Plane.Ground;
            u.OnDeath = Species.None;
            u.ScaleSmall();
            u.Health = new Health(u, 15);
            u.Watch = new Watch(u, 5);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 0),
				Ability.Feed(u),
				Ability.Evolve(u, Price.Cheap, Species.Beesassin),
				Ability.Evolve(u, new Price(1,2), Species.Mycolonist),
				Ability.Evolve(u, new Price(1,3), Species.ManTrap)
			});
            u.Arsenal.Sort();
            return u;
        }
        
        public static Unit MartianManTrap(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.ManTrap, "Martian Man Trap", false, template);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;

            u.ScaleLarge();
            u.Health = new Health(u, 70);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Remove("Focus");
            u.Arsenal.Add(new Ability[]{
				Ability.Creep(u),
				Ability.Grow(u),
				Ability.Strike(u, 12),
				Ability.VineWhip(u)
			});
            u.Arsenal.Sort();
            return u;
		}	

        public static Unit Mycolonist(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Mycolonist, "Mycolonist", false, template);
            u.Plane = Plane.Ground;
            u.ScaleMedium();
            u.Health = new Health(u, 40);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 2),
				Ability.Sporatic(u),
				Ability.Donate(u),
				Ability.Seed(u)
			});
            u.Arsenal.Sort();
            return u;
		}		

        
    }
}
