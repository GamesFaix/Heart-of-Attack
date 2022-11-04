using UnityEngine;
using System;

namespace HOA {
	
	public partial class Sensor : TokenComponent, IInspectable
    {
        #region //Properties
        
        public string Name { get; private set; }
        public Func<string> Desc { get; private set; }
        
        public CellSet Subscriptions { get; private set; }
        
        public Plane PlanesToStop { get; private set; }
        public Func<Token, bool> TriggerTest { get; private set; }

        public Action<Cell> OnParentEnter { get; private set; }
        public Action<Cell> OnParentExit { get; private set; }
        public Action<Token> OnOtherEnter { get; private set; } 
        public Action<Token> OnOtherExit { get; private set; }

        #endregion

        private Sensor(Token parent) : base (parent)
        {
            Name = "Default Sensor name";
            Desc = () => { return "Default Sensor description"; };
            Subscriptions = new CellSet();
            TriggerTest = NothingTrigger;
            PlanesToStop = Plane.None;
            OnParentEnter = (c) => 
            {
                Subscribe(c);
                foreach (Token t in c.Occupants)
                    OnOtherEnter(t);
            };
            OnParentExit = (c) =>
            {
                foreach (Token t in c.Occupants)
                    OnOtherExit(t);
                Unsubscribe(c);
            };
            OnOtherEnter = (t) => { };
            OnOtherExit = (t) => { };
        }

        public void Subscribe(Cell c)
        {
            Subscriptions.Add(c);
            c.OccupationEvent += this.OccupationSubscribe;
            c.Subscribers.Add(this);
        }

        public void Unsubscribe(Cell c)
        {
            Subscriptions.Remove(c);
            c.OccupationEvent -= this.OccupationSubscribe;
            c.Subscribers.Remove(this);
        }

        void UnsubscribeAll()
        {
            for (int i = Subscriptions.Count; i >= 0; i--)
            {
                Cell c = Subscriptions[i];
                c.OccupationEvent -= this.OccupationSubscribe;
                c.Subscribers.Remove(this);
            }
            Subscriptions = new CellSet();
        }

        public void OccupationSubscribe(object sender, OccupationEventArgs args)
        {
            if (Subscriptions.Contains(args.Cell)
                && TriggerTest(args.Token))
            {
                if (args.Enter)
                    OnOtherEnter(args.Token);
                else
                    OnOtherExit(args.Token);
            }
        }


        #region //TriggerTests
        
        public static bool GroundTokenTrigger(Token t) { return (t.Plane.ContainsAny(Plane.Ground)); }
        public static bool GroundUnitTrigger(Token t) { return (t.Plane.ContainsAny(Plane.Ground) && (t is Unit)); }
        public static bool TallUnitTrigger(Token t)
        {
            return ((t is Unit) && t.Plane.ContainsAny(Plane.Tall));
        }
        public static bool UnitTrigger(Token t) { return (t is Unit); }
        public static bool EverythingTrigger(Token t) { return true; }
        public static bool NothingTrigger(Token t) { return false; }

        #endregion

        public void RemoveTimer(Token token, string timerName)
        {
            if (token is Unit)
            {
                Unit u = token as Unit;
                for (int i = u.timers.Count - 1; i >= 0; i--)
                {
                    Timer timer = u.timers[i];
                    if (timer.Name == timerName) { u.timers.Remove(timer); }
                }
            }
        }
        public void NoEffects(Token token) { }

        protected void Stop (Cell cell) {
            cell.Stop = cell.Stop | PlanesToStop;
		}
		protected void ReleaseStop () 
        {
            foreach (Cell c in Subscriptions)
               c.Stop = ~(c.Stop & PlanesToStop);
		}

        public override string ToString() {return Name + ", " + Desc();}

        public override void Draw(Panel p) { InspectorInfo.Sensor(this, p); }
	}
}