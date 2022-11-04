using UnityEngine; 

namespace HOA { 

    public partial class Obstacle
    {
        public static Obstacle Gap(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Gap, "Gap", false, template);
            o.Plane = Plane.HalfSunk;
            o.ScaleLarge();
            o.Neutralize();
            return o;
        }
       
        public static Obstacle Hill(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Hill, "Hill", false, template);
            o.ScaleLarge();
            o.Neutralize();
            return o;
        }
        
        public static Obstacle Mountain(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Mountain, "Mountain", false, template);
			o.Plane = Plane.Tall;
            o.ScaleTall();
            o.Neutralize();
            return o;
		}
        
        public static Obstacle Pylon(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Pylon, "Pylon", false, template);
            o.Plane = Plane.Tall;
            o.ScaleTall();
            o.Neutralize();
            return o;
        }
        
        public static Obstacle Pyramid(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Pyramid, "Pyramid", false, template);
            o.Plane = Plane.Tall;
            o.ScaleTall();
            o.Neutralize();
            return o;
		}
        
        
    }
}
