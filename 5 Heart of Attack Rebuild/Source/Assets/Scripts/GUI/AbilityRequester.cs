using System;
using HOA.Abilities;

namespace HOA.GUI
{
    public static class AbilityRequester
    {

        public static event EventHandler<AbilityRequestEventArgs> AbilityRequestEvent;

        public static void AbilityRequestPublish(object source, Ability ability, AbilityArgs args)
        {
            if (source == null || ability == null || args == null)
                throw new ArgumentNullException();
            if (AbilityRequestEvent != null)
            {
                Debug.Log("{0} requests to {1}.", source, ability);
                AbilityRequestEvent(source, new AbilityRequestEventArgs(ability, args));                
            }
        }

        public static void AbilityRequestCancel(object source)
        {
            if (AbilityRequestEvent != null)
            {
                Debug.Log("{0} cancelled ability request.", source);
                AbilityRequestEvent(null, new AbilityRequestEventArgs(null, null, true));
            }
        }

    }
}