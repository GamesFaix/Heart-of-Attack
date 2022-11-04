using System;
using System.Collections.Generic;

namespace HOA.Tokens
{

    public partial class Obstacle
    {
        public static Obstacle Pylon(object source)
        {
            Obstacle o = new Obstacle(source, Species.Pylon);
            o.body = new Body(o, Plane.Tall);
            return o;
        }

        public static Obstacle Pyramid(object source)
        {
            Obstacle o = new Obstacle(source, Species.Pyramid);
            o.body = new Body(o, Plane.Tall);
            return o;
        }

        public static Obstacle Antenna(object source)
        {
            Obstacle o = new Obstacle(source, Species.Antenna);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Corpse(object source)
        {
            Obstacle o = new Obstacle(source, Species.Corpse);
            o.body = new Body(o, Plane.Ground, (TokenFlags.Destructible | TokenFlags.Corpse));
            return o;
        }

        public static Obstacle Cottage(object source)
        {
            Obstacle o = new Obstacle(source, Species.Cottage);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle House(object source)
        {
            Obstacle o = new Obstacle(source, Species.House);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Rampart(object source)
        {
            Obstacle o = new Obstacle(source, Species.Rampart);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Rock(object source)
        {
            Obstacle o = new Obstacle(source, Species.Rock);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Temple(object source)
        {
            Obstacle o = new Obstacle(source, Species.Temple);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Tree(object source)
        {
            Obstacle o = new Obstacle(source, Species.Tree);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }
        public static Obstacle Tree2(object source)
        {
            Obstacle o = new Obstacle(source, Species.Tree2);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }
        public static Obstacle Tree3(object source)
        {
            Obstacle o = new Obstacle(source, Species.Tree3);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }
        public static Obstacle Tree4(object source)
        {
            Obstacle o = new Obstacle(source, Species.Tree4);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle BombingRange(object source)
        {
            Obstacle o = new Obstacle(source, Species.BombingRange);
            o.body = new Body(o, Plane.Sunken);//Sensor.BombingRange);

            /*o.Notes = () =>
            {
                return "\nIf any unit shares " + o.ID.Name + "'s Cell, " +
                "\n10 explosive damage is dealt in " + o.ID.Name + "'s cell at the end of that unit's turn.";
            };
             * */
            return o;
        }

        public static Obstacle Curse(object source)
        {
            Obstacle o = new Obstacle(source, Species.Curse);
            o.body = new Body(o, Plane.Sunken);//Sensor.Curse);
            /*o.Notes = () =>
            {
                return "Units take 2 damage upon entering " + o.ID.Name + "'s cell " +
                "or a neighboring cell." +
                "\nUnits sharing " + o.ID.Name + "'s cell or in a neighboring cell " +
                "take 2 damage at the end of their turn.";
            };*/
            return o;
        }

    }

}