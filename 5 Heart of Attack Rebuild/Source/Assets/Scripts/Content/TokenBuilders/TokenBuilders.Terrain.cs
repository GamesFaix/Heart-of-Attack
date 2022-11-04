using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Content
{


    public partial class TokenBuilders
    {
        public static Token Gap(object source)
        {
            return new Terrain(
                source, 
                Species.Gap, 
                Plane.Terrain | Plane.Sunken);
        }

        public static Token Hill(object source)
        {
            return new Terrain(
                source, 
                Species.Hill, 
                Plane.Terrain | Plane.Sunken | Plane.Ground);
        }

        public static Token Mountain(object source)
        {
            return new Terrain(
                source, 
                Species.Mountain, 
                Plane.Terrain | Plane.Sunken | Plane.Ground | Plane.Air);
        }

        public static Token Exhaust(object source)
        {
            return new Terrain(
                source, 
                Species.Exhaust, 
                Plane.Terrain);
/*            o.Notes = () =>
            {
                return "Ground and Flying units must stop on " + o.ID.Name + "." +
                "\nGround and Flying Units take 5 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround and Flying Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };*/
        }

        public static Token Ice(object source)
        {
            return new Terrain(
                source, 
                Species.Ice,
                Plane.Terrain);
            //Sensor.Ice);
/*            o.Notes = () =>
            {
                return "Ground Units moving into " + o.ID.Name + "'s Cell " +
                    "have a 25% of turning " + o.ID.Name + " into Water.";
            };*/
        }

        public static Token Lava(object source)
        {
            return new Terrain(
                source, 
                Species.Lava, 
                Plane.Terrain);
            //Sensor.Lava);

            /*o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units take 7 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 7 damage at the end of their turn.";
            };*/
        }

        public static Token Quicksand(object source)
        {
            return new Terrain(
                source, 
                Species.Quicksand,
                Plane.Terrain);
            //Sensor.Quicksand);
        }

        public static Token Sand(object source)
        {
            return new Terrain(
                source, 
                Species.Sand,
                Plane.Terrain);
        }

        public static Token TimeSink(object source)
        {
            return new Terrain(
                source, 
                Species.TimeSink, 
                Plane.Terrain);
            //Sensor.TimeSink);
            //o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have -2 Initiative."; };
       }

        public static Token TimeWell(object source)
        {
            return new Terrain(
                source, 
                Species.TimeWell, 
                Plane.Terrain);
            //Sensor.TimeWell);
            //o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have +2 Initiative."; };
        }

        public static Token Water(object source)
        {
            return new Terrain(
                source, 
                Species.Water, 
                Plane.Terrain);
            //Sensor.Water);
            /*o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };*/
        }
    }

}