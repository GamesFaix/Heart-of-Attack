using System;

namespace HOA.GUI
{
    public static class TargetSelector
    {

        public static void OnEntitySelectionRequest(object sender, Abilities.EntitySelectionRequestEventArgs args)
        {
            Debug.Log("Not implemented.");
        }

        public static event EventHandler<EntitySelectionEventArgs> EntitySelectionEvent;

        public static void EntitySelectionPublish(EntitySet selection)
        {
            if (selection == null || selection.Count < 1)
                throw new ArgumentNullException();
            if (EntitySelectionEvent != null)
            {
                EntitySelectionEvent(null, new EntitySelectionEventArgs(selection));
                Debug.Log("Unfinished code: EntitySelectionEvent sender null.");
            }
        }

        public static void EntitySelectionCancel()
        {
            if (EntitySelectionEvent != null)
            {
                EntitySelectionEvent(null, new EntitySelectionEventArgs(null, true));
                Debug.Log("Entity selection cancelled.");
            }
        }

    }

    public class EntitySelectionEventArgs : EventArgs
    {
        public EntitySet Selection { get; private set; }
        public bool Cancel { get; private set; }

        public EntitySelectionEventArgs(EntitySet selection, bool cancel = false)
        {
            Selection = selection;
            Cancel = cancel;
        }
    }
}