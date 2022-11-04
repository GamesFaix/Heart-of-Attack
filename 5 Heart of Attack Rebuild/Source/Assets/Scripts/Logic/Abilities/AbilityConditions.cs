using System;
using System.Collections.Generic;
using HOA.Tokens;
using Session = HOA.Sessions.Session;

namespace HOA.Abilities 
{

    public delegate bool AbilityCondition(AbilityClosure ac, out string message);

    public static class AbilityConditions
    {
        public static bool UserIsTop(this AbilityClosure ac, out string message)
        {
            bool b = (ac.source.Last<Unit>() == Session.Active.Queue.Top);
            message = (b ? "" : "It is not currently " + ac.source + "'s turn.");
            return b;
        }

        public static bool UserInQueue(this AbilityClosure ac, out string message)
        {
            bool b = (Session.Active.Queue.Contains(ac.sourceUnit));
            message = (b ? "" : ac.source + " is not in the TurnQueue.");
            return b;
        }

        public static bool Unused(this AbilityClosure ac, out string message)
        {
            message = (!ac.usedThisTurn ? "" : ac.name + " has already been used this turn.");
            return !ac.usedThisTurn;
        }

        public static bool Affordable(this AbilityClosure ac, out string message)
        {
            Unit u;
            bool b = (ac.source.Last<Unit>(out u) && u.CanAfford(ac.price));
            message = (b ? "" : ac.source + " cannot afford " + ac.name + ".");
            return b;
        }
        public static bool AlreadyProcessing(this AbilityClosure ac, out string message)
        {
            message = (!Effects.EffectQueue.Active ? "" : "Another action is currently in progress.");
            return !Effects.EffectQueue.Active;
        }

        public static bool AirClear(this AbilityClosure ac, out string message)
        {
            bool b = (ac.sourceToken.cell.occupants
                / Filter.Plane(Plane.Air, true))
                .Count < 1;
            message = (b ? "" : "A token occupies the required air space.");
            return b;
        }

        public static bool GroundClear(this AbilityClosure ac, out string message)
        {
            bool b = (ac.sourceToken.cell.occupants
                / Filter.Plane(Plane.Ground, true))
                .Count < 1;
            message = (b ? "" : "A token occupies the required ground space.");
            return b;
        }
    }
}