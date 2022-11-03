using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace HOA {
	
	
	public abstract class Action{
		protected int weight;
		public int Weight {get {return weight;} }

		protected string name;
		protected string desc;
		
		protected List<Aim> aim = new List<Aim>();
		public List<Aim> Aim {get {return aim;} }
		public void AddAim (Aim a) {aim.Add(a);}
		public virtual void DrawAim (int n, Panel p) {aim[n].Draw(p);}

		protected Price price = new Price(1,0);
		
		public string Name {get {return name;} }
		public string Desc () {return desc;}

		public virtual void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			DrawAim(0, new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
		}

		public void DrawPrice (Panel p) {price.Draw(p);}

		protected bool used = false;
		public void Reset () {used = false;}

		public bool Playable {
			get {
				if (!used && !Restrict() && actor.CanAfford(price) && !EffectQueue.Processing && actor == TurnQueue.Top) {
					return true;
				}
				return false;
			}
		}

		protected bool multiAim = false;
		public bool MultiAim { get{return multiAim;} }

		protected Unit actor;
		public Unit Actor {get {return actor;} }
		protected Token childTemplate = default(Token);
		public Token ChildTemplate {get {return childTemplate;} }
		
		public abstract void Execute (List<ITarget> targets);

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
