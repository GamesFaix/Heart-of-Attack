using UnityEngine; 

namespace HOA { 

    public partial class Obstacle 
    {
        public static Obstacle Aperture(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Aperture, "Aperture", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodyAper(o);
            o.Notes = () => { return "0% Functional"; };
            o.WatchList = new WatchList();
            return o;
        }
    
        public static Obstacle Mine(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Mine, "Mine", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor9(o, Sensor.Mine);
            o.Body.Destructible = true;
            o.Notes = () => 
            { 
                return "If any Token enters Mine's Cell or a neighboring Cell, destroy Mine.\n"+
                "When Mine is destroyed, do 10 damage to all units in its cell. \n"+
                "All units in neighboring cells take 50% damage (rounded down). \n"+
                "Damage continues to spread outward with 50% reduction until 1. \n"+
                "Destroy all destructible tokens that would take damage.";
            };
            o.Destroy = (source, Corpse, log) =>
            {
                o.DefaultDestroy(source, Corpse, log);
                EffectQueue.Interrupt(Effect.ExplosionSequence(new Source(o), o.Body.Cell, 12, false));
            };
            return o;
		}

        public static Obstacle Web(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Web, "Web", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.Web);
            o.Body.Destructible = true;
            o.Neutralize();
            o.WatchList = new WatchList();
            o.Notes = () =>
            {
                return "Ground and Air units may not move through " + o.ID.Name + "." +
                "\nUnits sharing " + o.ID.Name + "'s Cell have a Move Range of 1.";
            };
            return o;
        }

    }
}
