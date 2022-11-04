using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA.Abilities
{

    public static class AbilityProcessor
    {
        static Ability pending;
        static EntitySet selection;
        static bool cancel;
        
        static AbilityProcessor()
        {
            EntitySelectionRequestEvent += GUI.TargetSelector.OnEntitySelectionRequest;
            GUI.TargetSelector.EntitySelectionEvent += OnEntitySelection;
            Reset();
        }

        public static void Process(Ability a)
        {
            if (!Legal(a))
                return;
            Reset();
            pending = a;
            pending.Adjust();
            NestedList<IEntity> targets = FindTargets(a);
            if (targets == null)
                Reset();
            else
                a.Execute(targets);
        }

        static bool Legal(Ability a)
        {
            string message;
            bool b = a.Usable(out message);
            if (!b)
                Debug.Log(message);
            return b;
        }

        static void Reset()
        {
            if (pending != null)
            {
                pending.Unadjust();
                pending = null;
            }
            Session.Active.ClearLegal();
            selection = null;
            cancel = false;
        }

        static NestedList<IEntity> FindTargets(Ability a)
        {
            NestedList<IEntity> targets = new NestedList<IEntity>();

            foreach (Aim aim in a.Aims)
            {
                targets.AddToEnd(new EntitySet());
                EntitySet options = aim.Pattern(aim.Args());
                if (aim.autoSelect)
                    for (int i = 0;
                        i < options.Count && i <= aim.selectionCount.max;
                        i++)
                        targets.AddToLast(options[i]);
                else
                {
                    selection = null;
                    EntitySelectionRequestPublish(options, aim.selectionCount);
                    selection = WaitForSelection(aim);
                    if (selection == null)
                    {
                        targets = null;
                        break;
                    }
                    else
                        targets.AddToLast(selection);
                }
            }
            return targets;
        }

       
        static EntitySet WaitForSelection(Aim aim)
        {
            Debug.Log("Waiting for " + aim.selectionCount + " targets to be chosen."); 
            float start = (float)Time.time;
            while (Time.Since(start) < 10000 )
            {
                if (cancel)
                    return null;
                if (selection != null)
                {
                    if (!aim.selectionCount.Contains(selection.Count))
                    {
                        Debug.Log("Invalid number of entities selected.");
                        selection = null;
                    }
                    else
                        return selection;
                }
            }
            return null;
        }
            

        public static event EventHandler<EntitySelectionRequestEventArgs> EntitySelectionRequestEvent;

        public static void EntitySelectionRequestPublish(EntitySet options, Range selectionCount)
        {
            if (EntitySelectionRequestEvent != null)
            {
                EntitySelectionRequestEvent(null,
                    new EntitySelectionRequestEventArgs(options, selectionCount));
                Debug.Log("Unfinished code: EntitySelectionRequest sender null.");
            }
        }

        public static void OnEntitySelection(object sender, GUI.EntitySelectionEventArgs args)
        {
            selection = args.Selection;
            cancel = args.Cancel;
        }
    }

    public class EntitySelectionRequestEventArgs : EventArgs
    {
        public EntitySet Options {get; private set;}
        public Range SelectionCount {get; private set;}

        public EntitySelectionRequestEventArgs(EntitySet options, Range selectionCount)
        {
            if (options == null || options.Count < 1 
                || selectionCount.max < 1)
                throw new ArgumentNullException();
            Options = options;
            SelectionCount = selectionCount;
        }
    }

}