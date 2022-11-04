using System;
using HOA.Abilities;

namespace HOA.GUI
{
    public static class AbilityRequester
    {

        public static event EventHandler<AbilityRequestEventArgs> AbilityRequestEvent;

        public static void AbilityRequestPublish(object source, AbilityClosure closure)
        {
            if (source == null || closure == null)
                throw new ArgumentNullException();
            if (AbilityRequestEvent != null)
            {
                Log.Debug("{0} requests to {1}.", source, closure);
                AbilityRequestEvent(source, new AbilityRequestEventArgs(closure));                
            }
        }

        public static void AbilityRequestCancel(object source)
        {
            if (AbilityRequestEvent != null)
            {
                Log.Debug("{0} cancelled ability request.", source);
                AbilityRequestEvent(null, new AbilityRequestEventArgs(null, true));
            }
        }

    }
}