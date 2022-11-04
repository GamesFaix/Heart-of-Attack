using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{

    public partial class Obstacle
    {
        public static Obstacle Pylon(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Pylon);
            o.body = new Body(o, Plane.Tall);
            return o;
        }

        public static Obstacle Pyramid(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Pyramid);
            o.body = new Body(o, Plane.Tall);
            return o;
        }

        public static Obstacle Antenna(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Antenna);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Corpse(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Corpse);
            o.body = new Body(o, Plane.Ground, (TokenFlags.Destructible | TokenFlags.Corpse));
            return o;
        }

        public static Obstacle Cottage(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Cottage);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle House(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.House);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Rampart(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Rampart);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Rock(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Rock);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Temple(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Temple);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle Tree(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Tree);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }
        public static Obstacle Tree2(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Tree2);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }
        public static Obstacle Tree3(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Tree3);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }
        public static Obstacle Tree4(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Tree4);
            o.body = new Body(o, Plane.Ground, TokenFlags.Destructible);
            return o;
        }

        public static Obstacle BombingRange(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.BombingRange);
            o.body = new Body(o, Plane.Sunken);//Sensor.BombingRange);

            /*o.Notes = () =>
            {
                return "\nIf any unit shares " + o.ID.Name + "'s Cell, " +
                "\n10 explosive damage is dealt in " + o.ID.Name + "'s cell at the end of that unit's turn.";
            };
             * */
            return o;
        }

        public static Obstacle Curse(ITokenCreator creator)
        {
            Obstacle o = new Obstacle(creator, Species.Curse);
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