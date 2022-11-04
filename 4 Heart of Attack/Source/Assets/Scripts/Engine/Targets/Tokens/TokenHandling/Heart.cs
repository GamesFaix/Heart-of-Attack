using UnityEngine; 

namespace HOA { 

    public class Heart : Obstacle{

        public Heart(Source source, Species species, string name, bool template = false)
            : base (source, species, name+" Heart of Attack", true, template)
        {
            Plane = Plane.Tall;
            Neutralize();
        }

        public static Heart Blood(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.BloodHeart, "Blood", template);
            return h;
        }

        public static Heart Brass(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.BrassHeart, "Brass", template);
            return h;
        }

        public static Heart Fir(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.FirHeart, "Fir", template);
            return h;
        }

        public static Heart Glass(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.GlassHeart, "Glass", template);
            return h;
        }

        public static Heart Silicon(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.SiliconHeart, "Silicon", template);
            return h;
        }

        public static Heart Silk(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.SilkHeart, "Silk", template);
            return h;
        }

        public static Heart Steel(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.SteelHeart, "Steel", template);
            return h;
        }

        public static Heart Stone(Source s, bool template = false)
        {
            Heart h = new Heart(s, Species.StoneHeart, "Stone", template);
            return h;
        }
    }
}
