using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace HOA {

	public abstract class Task : IComparable<Task>{

		public string name {get; protected set;}
		public abstract string desc {get;}
		public byte weight {get; protected set;}
		public Price price {get; protected set;}
		public Unit parent {get; protected set;}
		public virtual Token template {get; protected set;}
		public AimSeq aims {get; protected set;}

		protected Task (Unit parent) {
			this.parent = parent;
			name = "";
			weight = 0;
			price = Price.Cheap;
			parent = null;
			template = null;
			aims = new AimSeq();
		}

		protected Task () {}


		public virtual void DrawAim (int n, Panel p) {aims[n].Draw(p);}

		public void Execute (TargetGroup targets) {
			ExecuteStart();
			ExecuteMain(targets);
			ExecuteFinish();
		}
		protected virtual void ExecuteStart () {Charge();}
		protected abstract void ExecuteMain (TargetGroup targets);
		protected virtual void ExecuteFinish () {Targeter.Reset();}

		public virtual void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), desc);	
		}

		public void DrawPrice (Panel p) {price.Draw(p);}

		public bool Used {get; protected set;}
		public void Reset () {Used = false;}

		public virtual bool Legal (out string message) {
			message = name+" currently legal.";
			if (parent != TurnQueue.Top) {
				message = "It is not currently "+parent+"'s turn.";
				return false;
			}
			if (Used) {
				message = name+" has already been used this turn.";
				return false;
			}
			if (!parent.Wallet.CanAfford(price)) {
				message = parent+" cannot afford "+name+".";
				return false;
			}
			if (Restrict()) {
				message = name+" currently illegal.";
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
			parent.Wallet.Charge(price);
		}

		public int CompareTo (Task other) {
			try {
				if (weight < other.weight) {return -1;}
				else if (weight > other.weight) {return 1;}
				else {
					int i = price.CompareTo(other.price);
					if (i != 0) {return i;}
					else {return (name.CompareTo(other.name));}
				}
			}
			catch {
				return 0;
			}
		}

		protected Source source {get {return new Source(parent);} }
	}
}