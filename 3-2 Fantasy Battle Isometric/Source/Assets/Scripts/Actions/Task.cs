using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace HOA {

	public abstract class Task : IComparable<Task>{

		public string Name {get; protected set;}
		public abstract string Desc {get;}
		public int Weight {get; protected set;}
		public Price Price {get; protected set;}
		public Unit Parent {get; protected set;}
		public virtual Token Template {get; protected set;}

		public List<Aim> Aim {get; protected set;}
		protected void NewAim (Aim aim) {Aim = new List<Aim>{aim};}
		public virtual void DrawAim (int n, Panel p) {Aim[n].Draw(p);}

		public void Execute (TargetGroup targets) {
			ExecuteStart();
			ExecuteMain(targets);
			ExecuteFinish();
		}
		protected virtual void ExecuteStart () {Charge();}
		protected abstract void ExecuteMain (TargetGroup targets);
		protected virtual void ExecuteFinish () {Targeter.Reset();}

		public virtual void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(p.LinePanel);
			DrawAim(0, p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallBox(descH), Desc);	
		}

		public void DrawPrice (Panel p) {Price.Draw(p);}

		protected bool used = false;
		public void Reset () {used = false;}

		public virtual bool Legal {
			get {
				if (used) {return false;}
				if (Restrict()) {return false;}
				if (!Parent.CanAfford(Price)) {return false;}
				if (EffectQueue.Processing) {return false;}
				if (Parent != TurnQueue.Top) {return false;}
				return true;
			}
		}

		public virtual bool Restrict () {return false;}

		public virtual void Adjust () {}
		public virtual void UnAdjust () {}

		public void Charge () {
			used = true;
			Parent.Charge(Price);
		}

		public int CompareTo (Task other) {
			try {
				if (Weight < other.Weight) {return -1;}
				else if (Weight > other.Weight) {return 1;}
				else if (Price != null && other.Price !=null) {
					int i = Price.CompareTo(other.Price);
					if (i != 0) {return i;}
					else {return (Name.CompareTo(other.Name));}
				}
				else {throw new Exception ("Task.CompareTo: Null price");}
			}
			catch {
				return 0;
			}
		}
	}
}