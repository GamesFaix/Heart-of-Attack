using System;

namespace HOA.Abilities
{

    public static class AbilityProcessor
    {
        static Ability pending;
        static Set<IEntity> options, selection;
        public static NestedList<IEntity> targets { get; private set; }
        static bool cancel;
        //static float waitTime;

        public static void Load()
        {
            GUI.AbilityRequester.AbilityRequestEvent += OnAbilityRequest;
            //waitTime = 2000;
            Debug.Log("AbilityProcessor subscribed to AbilityRequester.AbilityReqeustEvent.");
        }

        static void Reset()
        {
            pending = null;
            options = null;
            selection = null;
            targets = null;
            cancel = false;
            Session.Active.ClearLegal();
        }

        static void OnAbilityRequest(object sender, AbilityRequestEventArgs args)
        {
            pending = args.ability;
            string msg;
            if (!pending.Usable(out msg))
            { 
                Debug.Log(msg);
                Reset();
            }

            pending.Adjust();
            targets = new NestedList<IEntity>();
            foreach (AimStage stage in pending.Aims)
            {
                options = stage.FindOptions();
                TargetSelectionRequestPublish(sender, options, stage.selectionCount);
                selection = WaitForSelection(options, stage.selectionCount);
                //...wait...
                if (selection != null && selection.Count > 0)
                    targets.AddToEnd(selection);
                else
                {
                    Debug.Log("No legal targets.");
                    Reset();
                    return;
                }
            }
            pending.Execute(targets);
        }
        
        public static event EventHandler<TargetSelectionRequestEventArgs> TargetSelectionRequestEvent;

        public static void TargetSelectionRequestPublish(object sender, Set<IEntity> options, Range selectionCount)
        {
            if (TargetSelectionRequestEvent != null)
            {
                Debug.Log("{0} requests {1} target(s) be selected, out of {2} possible.", sender, selectionCount, options.Count); 
                TargetSelectionRequestEvent(sender,
                    new TargetSelectionRequestEventArgs(options, selectionCount));
            }
        }

        static Set<IEntity> WaitForSelection(Set<IEntity> options, Range selectionCount)
        {
            Debug.Log("Waiting for {0} targets to be chosen.", selectionCount);
            float start = (float)Time.time;
            //while (Time.Since(start) < waitTime)
            for (short i=0; i<short.MaxValue; i++)
            {
                Debug.Log("{0}ms since wait start.", Time.Since(start));
                if (cancel)
                {
                    Reset();
                    break;
                }
                
                if (selection != null)
                    if (selectionCount.Contains(selection.Count))
                    {
                        Debug.Log("Selection accepted.");
                        return selection;
                    }
                    else
                    {
                        Debug.Log("Invalid number of targets selected.");
                        cancel = true;
                    }
            }
            Debug.Log("Wait over.");
            return null;
        }

        public static void OnTargetSelection(object sender, TargetSelectionEventArgs args)
        {
            cancel = args.Cancel;
            foreach (IEntity e in args.Selection)
                if (!options.Contains(e))
                {
                    Debug.Log("Illegal selection!");
                    return;
                }
            selection = args.Selection;
        }
    }

    
}