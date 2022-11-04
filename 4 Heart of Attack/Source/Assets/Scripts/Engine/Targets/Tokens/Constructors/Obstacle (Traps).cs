using UnityEngine; 

namespace HOA { 

    public partial class Obstacle
    {
        public static Obstacle BombingRange(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.BombingRange, "Bombing Range", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.BombingRange);
            o.Neutralize();
            o.Notes = () =>
            {
                return "\nIf any unit shares " + o.ID.Name + "'s Cell, " +
                "\n10 explosive damage is dealt in " + o.ID.Name + "'s cell at the end of that unit's turn.";
            };
            return o;
        }

        public static Obstacle Curse(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Curse, "Curse", false, template);
			o.Plane = Plane.Sunken;
            o.Body = new BodySensor9(o, Sensor.Curse);
            o.Neutralize();
            o.Notes = () =>
            {
                return "Units take 2 damage upon entering " + o.ID.Name + "'s cell " +
                "or a neighboring cell." +
                "\nUnits sharing " + o.ID.Name + "'s cell or in a neighboring cell " +
                "take 2 damage at the end of their turn.";
             };
            return o;
		}
    
        public static Obstacle Exhaust(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Exhaust, "Exhaust Vent", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.Exhaust);
            o.Neutralize();
            o.Notes = () =>
            {
                return "Ground and Flying units must stop on " + o.ID.Name + "." +
                "\nGround and Flying Units take 5 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround and Flying Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };
            return o;
		}

        public static Obstacle Ice(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Ice, "Ice", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.Ice);
            o.Neutralize();
            o.Notes = () =>
            {
                return "Ground Units moving into " + o.ID.Name + "'s Cell " +
                    "have a 25% of turning " + o.ID.Name + " into Water.";
            };
            return o;
		}

        public static Obstacle Lava(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.Lava, "Lava", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.Lava);
            o.Neutralize();
            o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units take 7 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 7 damage at the end of their turn.";
            };
            return o;
		}

        public static Obstacle TimeSink(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.TimeSink, "Time Sink", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.TimeSink);
            o.Neutralize();
            o.WatchList = new WatchList();
            o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have -2 Initiative."; };
            return o;
		}

        public static Obstacle TimeWell(Source s, bool template=false)
        {
            Obstacle o = new Obstacle(s, Species.TimeWell, "Time Well", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.TimeWell);
            o.Neutralize();
            o.WatchList = new WatchList();
            o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have +2 Initiative."; };
            return o;
		}

        public static Obstacle Water(Source s, bool template = false)
        {
            Obstacle o = new Obstacle(s, Species.Water, "Water", false, template);
            o.Plane = Plane.Sunken;
            o.Body = new BodySensor1(o, Sensor.Water);
            o.Neutralize();
            o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };
            return o;
		}
    
    }
}
