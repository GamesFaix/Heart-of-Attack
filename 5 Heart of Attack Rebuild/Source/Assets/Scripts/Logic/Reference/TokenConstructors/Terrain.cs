using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{


    public partial class Terrain
    {
        public static Terrain Gap(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Gap);
            o.body = new Body(o, (Plane.Terrain | Plane.Sunken));
            return o;
        }

        public static Terrain Hill(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Hill);
            o.body = new Body(o, (Plane.Terrain | Plane.Sunken | Plane.Ground));
            return o;
        }

        public static Terrain Mountain(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Mountain);
            o.body = new Body(o, (Plane.Terrain | Plane.Sunken | Plane.Ground | Plane.Air));
            return o;
        }

        public static Terrain Exhaust(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Exhaust);
            o.body = new Body(o, Plane.Terrain);
/*            o.Notes = () =>
            {
                return "Ground and Flying units must stop on " + o.ID.Name + "." +
                "\nGround and Flying Units take 5 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround and Flying Units sharing " + o.ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };*/
            return o;
        }

        public static Terrain Ice(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Ice);
            o.body = new Body(o, Plane.Terrain);//Sensor.Ice);
/*            o.Notes = () =>
            {
                return "Ground Units moving into " + o.ID.Name + "'s Cell " +
                    "have a 25% of turning " + o.ID.Name + " into Water.";
            };*/
            return o;
        }

        public static Terrain Lava(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Lava);
            o.body = new Body(o, Plane.Terrain);//Sensor.Lava);

            /*o.Notes = () =>
            {
                return "Ground units must stop on " + o.ID.Name + "." +
                "\nGround Units take 7 damage upon entering " + o.ID.Name + "'s Cell." +
                "\nGround Units sharing " + o.ID.Name + "'s Cell take 7 damage at the end of their turn.";
            };*/
            return o;
        }

        public static Terrain Quicksand(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Quicksand);
            o.body = new Body(o, Plane.Terrain);//Sensor.Quicksand);
            return o;
        }

        public static Terrain Sand(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Sand);
            o.body = new Body(o, Plane.Terrain);
            return o;
        }

        public static Terrain TimeSink(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.TimeSink);
            o.body = new Body(o, Plane.Terrain);//Sensor.TimeSink);
            //o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have -2 Initiative."; };
            return o;
        }

        public static Terrain TimeWell(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.TimeWell);
            o.body = new Body(o, Plane.Terrain);//Sensor.TimeWell);
            //o.Notes = () => { return "Units sharing " + o.ID.Name + "'s Cell have +2 Initiative."; };
            return o;
        }

        public static Terrain Water(ITokenCreator creator)
        {
            Terrain o = new Terrain(creator, Species.Water);
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