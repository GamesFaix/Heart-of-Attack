using UnityEngine; 

namespace HOA { 

    public partial class Obstacle
    {
        private void BuildDestructibleStandard()
        {
            Body.Destructible = true;
            ScaleMedium();
            Neutralize();
        }
        
        public static Obstacle Antenna(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Antenna, "Antenna", false, template);
            o.BuildDestructibleStandard();
            return o;
        }
        
        public static Obstacle Corpse(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Corpse, "Corpse", false, template);
            o.BuildDestructibleStandard();
            o.Body.Corpse = true;
            return o;
        }
        
        public static Obstacle Cottage(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Cottage, "Cottage", false, template);
            o.BuildDestructibleStandard();
            return o;
        }
        
        public static Obstacle House(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.House, "House", false, template);
            o.BuildDestructibleStandard();
            return o;
        }
        
        public static Obstacle Rampart(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Rampart, "Rampart", false, template);
            o.BuildDestructibleStandard();
            return o;
        }
        
        public static Obstacle Rock(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Rock, "Rock", false, template);
			o.BuildDestructibleStandard();
            return o;
		}
        
        public static Obstacle Temple(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Temple, "Temple", false, template);
            o.BuildDestructibleStandard();
            return o;
        }
        
        public static Obstacle Tree(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Tree, "Tree", false, template);
            o.BuildDestructibleStandard();
            return o;
		}
        public static Obstacle Tree2(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Tree2, "Tree2", false, template);
            o.BuildDestructibleStandard();
            return o;
		}
        public static Obstacle Tree3(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Tree3, "Tree3", false, template);
            o.BuildDestructibleStandard();
            return o;
		}
        public static Obstacle Tree4(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Tree4, "Tree4", false, template);
            o.BuildDestructibleStandard();
            return o;
		}
        
    }
}
