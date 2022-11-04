using UnityEngine; 

namespace HOA { 

    public partial class Unit 
    {
        public static Unit CarapaceInvader(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Carapace, "Carapace Invader", false, template);
            u.Plane = Plane.Ground;
            u.Body = new Body(u, Sensor.Carapace);
            u.ScaleMedium();
            u.Health = new HealthDEFCap(u, 35, 2, 5);
            u.Watch = new Watch(u, 4);
            u.Wallet = new WalletDEF(u, 2, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Shock(u),
				Ability.Discharge(u)
			});
            u.Arsenal.Sort();
            u.Notes = () =>
            {
                return "All non-Carapace neighboring teammates add Carapace's Defense.";
            };
            return u;
        }

        public static Unit Katandroid(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Katandroid, "Katandroid", false, template);
            u.Plane = Plane.Ground;
            u.ScaleSmall();
            u.Health = new Health(u, 25);
            u.Watch = new Watch(u, 5);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Strike(u, 8),
				Ability.Sprint(u),
				Ability.LaserSpin(u)
			});
            u.Arsenal.Sort();
            return u;
        }

        public static Unit Mawth (Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Mawth, "M.A.W.T.H.", false, template);
            u.Plane = Plane.Air;
            u.ScaleLarge();
            u.Health = new Health(u, 55);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Dart(u, 4),
				Ability.LaserShot(u),
				Ability.Bombard(u)
			});
            u.Arsenal.Sort();
            return u;
		}
    }
}
