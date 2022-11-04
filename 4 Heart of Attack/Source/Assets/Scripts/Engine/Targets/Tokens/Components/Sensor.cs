using UnityEngine;
using System;

namespace HOA {
	
	public partial class Sensor : TokenComponent, IInspectable
    {
        #region //Properties
        
        public string Name { get; private set; }
        public Func<string> Desc { get; private set; }
        public Cell Cell { get; private set; }
        public Plane PlanesToStop { get; private set; }
        public Func<Token, bool> TriggerTest { get; private set; }

        public Action<Token> SNCE { get; private set; } // SNCE = Self eNter Cellmate Effects
        public Action<Token> ONE { get; private set; }  // ONE = Other eNter Effects
        public Action<Token> SXCE { get; private set; } // SXCE = Self eXit Cellmate Effects
        public Action<Token> OXE { get; private set; }  // OXE = Other eXit Effects

        #endregion

        #region //Constructors

        private Sensor(Token parent, Cell cell) : base (parent)
        {
            Name = "Default Sensor name";
            Desc = () => { return "Default Sensor description"; };
            Cell = cell;
            TriggerTest = NothingTrigger;
            PlanesToStop = Plane.None;
            SNCE = NoEffects;
            ONE = NoEffects;
            SXCE = NoEffects;
            OXE = NoEffects;
        }

        #endregion

        #region //TriggerTests
        
        public static bool GroundTokenTrigger(Token t) { return (t.Plane[Planes.Ground]); }
        public static bool GroundUnitTrigger(Token t) { return (t.Plane[Planes.Ground] && (t is Unit)); }
        public static bool TallUnitTrigger(Token t)
        {
            return ((t is Unit) && (t.Plane[Planes.Ground] || t.Plane[Planes.Air]));
        }
        public static bool UnitTrigger(Token t) { return (t is Unit); }
        public static bool EverythingTrigger(Token t) { return true; }
        public static bool NothingTrigger(Token t) { return false; }

        #endregion

        #region //Trigger Methods

        public void Enter(Cell c)
        {
            Cell = c;
            Stop(c);
            foreach (Token t in c.Occupants) { if (TriggerTest(t)) SNCE(t); }
        }
        public void Exit()
        {
            ReleaseStop(Cell);
            foreach (Token t in Cell.Occupants) { if (TriggerTest(t)) SXCE(t); }
        }
        public void OtherEnter(Token t) { if (TriggerTest(t)) ONE(t); }
        public void OtherExit(Token t) { if (TriggerTest(t)) OXE(t); }

        #endregion

        public void RemoveTimer(Token token, string timerName)
        {
            Unit u;
            if (token.IsUnit(out u))
            {
                for (int i = u.timers.Count - 1; i >= 0; i--)
                {
                    Timer timer = u.timers[i];
                    if (timer.Name == timerName) { u.timers.Remove(timer); }
                }
            }
        }
        public void NoEffects(Token token) { }

        protected void Stop (Cell cell) {
			if (PlanesToStop != Plane.None) {
				foreach (Planes plane in PlanesToStop) {
					cell.SetStop(plane, true);
				}
			}
		}
		protected void ReleaseStop (Cell cell) {
			if (PlanesToStop != Plane.None) {
				foreach (Planes plane in PlanesToStop) {
					cell.SetStop(plane, false);
				}
			}
		}

        public void Delete () {
			Exit();
			Cell.RemoveSensor(this);
		}

        public override string ToString() {return Name + ", " + Desc();}

        public override void Draw(Panel p) { InspectorInfo.Sensor(this, p); }
	}
}