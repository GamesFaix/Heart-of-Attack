using UnityEngine;

namespace HOA.Resources
{

    public partial class AVEffect
    {
        static AVEffect advance = new AVEffect("Advance");
        public static AVEffect Advance { get { return advance; } }

        static AVEffect birth = new AVEffect("Birth");
        public static AVEffect Birth { get { return birth; } }

        static AVEffect burrow = new AVEffect("Burrow");
        public static AVEffect Burrow { get { return burrow; } }

        static AVEffect corrode = new AVEffect("Corrode");
        public static AVEffect Corrode { get { return corrode; } }

        static AVEffect damage = new AVEffect("Damage");
        public static AVEffect Damage { get { return damage; } }

        static AVEffect death = new AVEffect("Death");
        public static AVEffect Death { get { return death; } }

        static AVEffect destruct = new AVEffect("Destruct");
        public static AVEffect Destruct { get { return destruct; } }

        static AVEffect detonate = new AVEffect("Detonate");
        public static AVEffect Detonate { get { return detonate; } }

        static AVEffect explode = new AVEffect("Explode");
        public static AVEffect Explode { get { return explode; } }

        static AVEffect fire = new AVEffect("Fire");
        public static AVEffect Fire { get { return fire; } }

        static AVEffect fly = new AVEffect("Fly");
        public static AVEffect Fly { get { return fly; } }

        static AVEffect getHeart = new AVEffect("GetHeart");
        public static AVEffect GetHeart { get { return getHeart; } }

        static AVEffect highlight = new AVEffect("Highlight");
        public static AVEffect Highlight { get { return highlight; } }

        static AVEffect incinerate = new AVEffect("Incinerate");
        public static AVEffect Incinerate { get { return incinerate; } }

        static AVEffect laser = new AVEffect("Laser");
        public static AVEffect Laser { get { return laser; } }

        static AVEffect miss = new AVEffect("Miss");
        public static AVEffect Miss { get { return miss; } }

        static AVEffect owner = new AVEffect("Owner");
        public static AVEffect Owner { get { return owner; } }

        static AVEffect shuffle = new AVEffect("Shuffle");
        public static AVEffect Shuffle { get { return shuffle; } }

        static AVEffect statDown = new AVEffect("StatDown");
        public static AVEffect StatDown { get { return statDown; } }

        static AVEffect statUp = new AVEffect("StatUp");
        public static AVEffect StatUp { get { return statUp; } }

        public static AVEffect Stat(bool statUp) { return (statUp ? StatUp : StatDown); }

        static AVEffect stick = new AVEffect("Stick");
        public static AVEffect Stick { get { return stick; } }

        static AVEffect stun = new AVEffect("Stun");
        public static AVEffect Stun { get { return stun; } }

        static AVEffect teleport = new AVEffect("Teleport");
        public static AVEffect Teleport { get { return teleport; } }

        static AVEffect walk = new AVEffect("Walk");
        public static AVEffect Walk { get { return walk; } }

        static AVEffect waterLog = new AVEffect("WaterLog");
        public static AVEffect WaterLog { get { return waterLog; } }
	}
}