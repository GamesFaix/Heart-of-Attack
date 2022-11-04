using System;
using HOA.Ab;

namespace HOA.GUI
{
    public static class TargetSelector
    {
        public static void Load()
        {
            Ab.Processor.TargetSelectionRequestEvent += OnTargetSelectionRequest;
            Log.Start("TargetSelector subscribed to Ab.Processor.TargetSelectionRequestEvent.");
            TargetSelectionEvent += Ab.Processor.OnTargetSelection;
            Log.Start("Ab.Processor subscribed to TargetSelector.TargetSelectionEvent.");
        }


        public static void OnTargetSelectionRequest(object sender, TargetSelectionRequestEventArgs args)
        {
            Log.Debug("TargetSelector.OnTargetSelectionRequest temporarily short-circuited.");
            Set<IEntity> selection = new Set<IEntity>();
            for (int i = 0; i < args.selectionCount.max; i++)
                selection.Add(args.options[i]);
            TargetSelectionPublish(Source.Force, selection);
        }

        public static event EventHandler<TargetSelectionEventArgs> TargetSelectionEvent;

        public static void TargetSelectionPublish(object sender, Set<IEntity> selection)
        {
            if (selection == null || selection.Count < 1)
                throw new ArgumentNullException();
            if (TargetSelectionEvent != null)
            {
                Log.Game("{0} selects {1}.", sender, selection.ToStringLong());
                TargetSelectionEvent(sender, new TargetSelectionEventArgs(selection));
                
            }
        }

        public static void TargetSelectionCancel()
        {
            if (TargetSelectionEvent != null)
            {
                TargetSelectionEvent(null, new TargetSelectionEventArgs(null, true));
                Log.Game("Target selection cancelled.");
            }
        }

    }

    
}