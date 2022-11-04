using UnityEngine; 

namespace HOA { 

    public partial class Unit 
    {
        public static Unit Demolitia(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Demolitia, "Demolitia", false, template);
            u.Plane = Plane.Ground;
            u.ScaleSmall();
            u.Health = new Health(u, 30);
            u.Watch = new Watch(u, 3);
            u.Wallet = new WalletDEF(u, 2, 4);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 3),
				Ability.ThrowGrenade(u),
				Ability.PlantGrenade(u)
			});
            u.Arsenal.Sort();
            return u;
        }

        public static Unit MeinSchutz(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.MeinSchutz,"Mein Schutz", false, template);
            u.Plane = Plane.Ground;
            u.ScaleMedium();
            u.Health = new Health(u, 40);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 5),
				Ability.Shoot(u, 2, 12),
				Ability.Create(u, new Price(0,1), Species.Mine),
				Ability.Detonate(u)
			});
            u.Arsenal.Sort();
            return u;
        }

        public static Unit Panopticannon(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Panopticannon, "Panopticannon", false, template);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;
            u.ScaleLarge();
            u.Health = new Health(u, 65);
            u.Watch = new Watch(u, 1);
            u.Wallet = new WalletDEF(u, 2, 2);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 1),
				Ability.Cannon(u, Price.Cheap, 12),
				Ability.Pierce(u, new Price(1,2), 20),
			});
            u.Arsenal.Sort();
            return u;
		}	
    }
}
