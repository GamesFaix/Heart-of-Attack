using UnityEngine; 

namespace HOA { 

    public partial class Unit 
    {
        public static Unit ArenaNonSensus(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.Arena, "Arena Non Sensus", false, template);

            u.Plane = Plane.Ethereal;
            u.Body = new BodyAren(u, template);

            u.OnDeath = Species.None;
            u.ScaleMedium();
            u.Health = new Health(u, 55, 3);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.MneumonicPlague (u),
				Ability.Oasis (u)
			});
            u.Arsenal.Sort();
            u.Notes = () => { return "EXXXtremely buggy."; };
            return u;
        }

        public static Unit PriestOfNaja(Source s, bool template=false)
        {
            Unit u = new Unit(s, Species.Priest, "Priset of Naja", false, template);
            u.Plane = Plane.Ground;
            u.ScaleLarge();
            u.Health = new Health(u, 50, 2);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Strike(u, 15),
				Ability.Shove(u)
			});
            u.Arsenal.Sort();
            return u;
		}		

        public static Unit PrismGuard(Source s, bool template = false)
        {
            Unit u = new Unit(s, Species.PrismGuard, "Prism Guard", false, template);
            u.Plane = Plane.Ground;
            u.ScaleSmall();
            u.Health = new HealthHalfDodge(u, 15);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Strike(u, 8),
				Ability.Refract(u)
			});
            u.Arsenal.Sort();
            return u;
		}
		
    		/*"Actions Targetting "+ID.Name+" have a 50% chance of missing.";
		}
		/*
		public override bool Select (Source s) {
			int flip = DiceCoin.Throw(s, EDice.COIN);
			Debug.Log("coin result"+flip);
			if (flip == 1) {
				Display.Effect(AVEffects.HEADS);
				return true;
	//			GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to Target "+ToString()+" and missed.");
				EffectQueue.Add(new ETails(new Source(this), this));
				return false;
			}
		}*/
    }
}
