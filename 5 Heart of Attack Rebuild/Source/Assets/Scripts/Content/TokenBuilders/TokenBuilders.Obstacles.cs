using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Content
{

    public partial class TokenBuilders
    {
        public static Token Pylon(object source)
        {
            return new Obstacle(source, Species.Pylon, Plane.Tall);
        }

        public static Token Pyramid(object source)
        {
            return new Obstacle(source, Species.Pyramid, Plane.Tall);
        }

        public static Token Antenna(object source)
        {
            return new Obstacle(source, Species.Antenna, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token Corpse(object source)
        {
            return new Obstacle(source, Species.Corpse, Plane.Ground, (TokenFlags.Destructible | TokenFlags.Corpse));
        }

        public static Token Cottage(object source)
        {
            return new Obstacle(source, Species.Cottage, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token House(object source)
        {
            return new Obstacle(source, Species.House, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token Rampart(object source)
        {
            return new Obstacle(source, Species.Rampart, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token Rock(object source)
        {
            return new Obstacle(source, Species.Rock, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token Temple(object source)
        {
            return new Obstacle(source, Species.Temple, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token Tree(object source)
        {
            return new Obstacle(source, Species.Tree, Plane.Ground, TokenFlags.Destructible);
        }
        public static Token Tree2(object source)
        {
            return new Obstacle(source, Species.Tree2, Plane.Ground, TokenFlags.Destructible);
        }
        public static Token Tree3(object source)
        {
            return new Obstacle(source, Species.Tree3, Plane.Ground, TokenFlags.Destructible);
        }
        public static Token Tree4(object source)
        {
            return new Obstacle(source, Species.Tree4, Plane.Ground, TokenFlags.Destructible);
        }

        public static Token BombingRange(object source)
        {
            var o = new Obstacle(
                source, 
                Species.BombingRange,
                Plane.Sunken);
            //Sensor.BombingRange);

            /*o.Notes = () =>
            {
                return "\nIf any unit shares " + o.ID.Name + "'s Cell, " +
                "\n10 explosive damage is dealt in " + o.ID.Name + "'s cell at the end of that unit's turn.";
            };
             * */
            return o;
        }

        public static Token Curse(object source)
        {
            var o = new Obstacle(
                source, 
                Species.Curse,
                Plane.Sunken);
            //Sensor.Curse);
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