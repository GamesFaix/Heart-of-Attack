using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{


    public partial class Terrain
    {
        public static Terrain Gap(object source)
        {
            Terrain o = new Terrain(source, Species.Gap);
            o.body = new Body(o, (Plane.Terrain | Plane.Sunken));
            return o;
        }

        public static Terrain Hill(object source)
        {
            Terrain o = new Terrain(source, Species.Hill);
            o.body = new Body(o, (Plane.Terrain | Plane.Sunken | Plane.Ground));
            return o;
        }

        public static Terrain Mountain(object source)
        {
            Terrain o = new Terrain(source, Species.Mountain);
            o.body = new Body(o, (Plane.Terrain | Plane.Sunken | Plane.Ground | Plane.Air));
            return o;
        }

        public static Terrain Exhaust(object source)
        {
            Terrain o = new Terrain(source, Species.Exhaust);
            o.body = new Body(o, Plane.Terrain);
/*            o.Notes = () =>
            {
                return "Ground and Flying units must stop on " + o.ID.Name + "." +
                "\nGround and Flying Units take 5 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround and Flying Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };*/
            return o;
        }

        public static Terrain Ice(object source)
        {
            Terrain o = new Terrain(source, Species.Ice);
            o.body = new Body(o, Plane.Terrain);//Sensor.Ice);
/*            o.Notes = () =>
            {
                return "Ground Units moving into " + o.ID.Name + "'s Cell " +
                    "have a 25% of turning " + o.ID.Name + " into Water.";
            };*/
            return o;
        }

        public static Terrain Lava(object source)
        {
            Terrain o = new Terrain(source, Species.Lava);
            o.body = new Body(o, Plane.Terrain);//Sensor.Lava);

            /*o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units take 7 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 7 damage at the end of their turn.";
            };*/
            return o;
        }

        public static Terrain Quicksand(object source)
        {
            Terrain o = new Terrain(source, Species.Quicksand);
            o.body = new Body(o, Plane.Terrain);//Sensor.Quicksand);
            return o;
        }

        public static Terrain Sand(object source)
        {
            Terrain o = new Terrain(source, Species.Sand);
            o.body = new Body(o, Plane.Terrain);
            return o;
        }

        public static Terrain TimeSink(object source)
        {
            Terrain o = new Terrain(source, Species.TimeSink);
            o.body = new Body(o, Plane.Terrain);//Sensor.TimeSink);
            //o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have -2 Initiative."; };
            return o;
        }

        public static Terrain TimeWell(object source)
        {
            Terrain o = new Terrain(source, Species.TimeWell);
            o.body = new Body(o, Plane.Terrain);//Sensor.TimeWell);
            //o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have +2 Initiative."; };
            return o;
        }

        public static Terrain Water(object source)
        {
            Terrain o = new Terrain(source, Species.Water);
            o.body = new Body(o, Plane.Terrain);//Sensor.Water);
            /*o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };*/
            return o;
        }
    }

}