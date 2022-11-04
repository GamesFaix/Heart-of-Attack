using System;

namespace HOA.Abilities
{
    internal class AbilityTuple
    {
        internal readonly Ability ability;
        internal readonly AbilityArgs args;

        internal AbilityTuple(Ability ability, AbilityArgs args)
        {
            if (ability == null || args == null)
                throw new ArgumentNullException();
            this.ability = ability;
            this.args = args;
        }
    }
}