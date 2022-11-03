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

		protected Task () {
			Name = "";
			Weight = 0;
			Price = Price.Free;
			Parent = null;
			Template = null;
		}

		public List<Aim> Aims {get; protected set;}
		protected void NewAim (Aim aim) {Aims = new List<Aim>{aim};}
		public virtual void DrawAim (int n, Panel p) {Aims[n].Draw(p);}

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
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}

		public void DrawPrice (Panel p) {Price.Draw(p);}

		public bool Used {get; protected set;}
		public void Reset () {Used = false;}

		public virtual bool Legal (out string message) {
			message = Name+" currently legal.";
			if (Parent != TurnQueue.Top) {
				message = "It is not currently "+Parent+"'s turn.";
				return false;
			}
			if (Used) {
				message = Name+" has already been used this turn.";
				return false;
			}
			if (!Parent.Wallet.CanAfford(Price)) {
				message = Parent+" cannot afford "+Name+".";
				return false;
			}
			if (Restrict()) {
				message = Name+" currently illegal.";
				return false;
			}
			if (EffectQueue.Processing) {
				message = "Another action is currently in progress.";
				return false;
			}
			return true;
		}

		public virtual bool Restrict () {return false;}

		public virtual void Adjust () {}
		public virtual void UnAdjust () {}

		public void Charge () {
			Used = true;
			Parent.Wallet.Charge(Price);
		}

		public int CompareTo (Task other) {
			try {
				if (Weight < other.Weight) {return -1;}
				else if (Weight > other.Weight) {return 1;}
				else {
					int i = Price.CompareTo(other.Price);
					if (i != 0) {return i;}
					else {return (Name.CompareTo(other.Name));}
				}
			}
			catch {
				return 0;
			}
		}
	}
}