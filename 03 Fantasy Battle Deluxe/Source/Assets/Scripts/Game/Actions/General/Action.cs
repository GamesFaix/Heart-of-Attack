using System.Collections.Generic;
using System.Collections;
using System;

namespace HOA {
	
	
	public abstract class Action {
		protected int weight;
		public int Weight {get {return weight;} }

		protected string name;
		protected string desc;
		
		protected List<Aim> aim = new List<Aim>();
		public List<Aim> Aim {get {return aim;} }
		public void AddAim (Aim a) {aim.Add(a);}
		public void DrawAim (int n, Panel p) {aim[n].Draw(p);}

		protected Price price = new Price(1,0);
		
		public string Name {get {return name;} }
		public string Desc () {return desc;}
		public void DrawPrice (Panel p) {price.Draw(p);}

		protected bool used = false;
		public void Reset () {used = false;}
		
		protected Unit actor;
		public Unit Actor {get {return actor;} }
		protected Token childTemplate = default(Token);
		public Token ChildTemplate {get {return childTemplate;} }
		
		public abstract void Execute (List<ITargetable> targets);

		public virtual bool Legal () {
			if (actor.CanAfford(price) 
			    && !used
			    && !Restrict()) {
				return true;
			}
			return false;
		}
		public virtual bool Restrict () {
			return false;
		}
		public virtual void Adjust () {}
		public virtual void UnAdjust () {}

		public void Charge () {
			used = true;
			actor.Charge(price);
		}
	}
}
