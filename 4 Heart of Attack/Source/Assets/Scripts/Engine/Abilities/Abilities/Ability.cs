using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace HOA {

	public partial class Ability : IInspectable, IComparable<Ability>{
        public bool manualFree, multiMove, multiTarget, recursiveMove, teleport;


        public Unit Parent { get; private set; }
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public Price Price { get; private set; }
		
        public Func<string> Desc;
        public Species Species { get; private set; }
        public virtual Token Template { get; private set; }
        public int Damage { get; private set; }
        public int Modifier { get; private set; }
        public Stats Stat { get; private set; }

        public delegate bool LegalTest(out string message);
        public LegalTest Legal;
        public List<Aim> Aims { get; private set; }
        public bool Used { get; private set; }

        protected Action PreEffects;
        protected Action<TargetGroup> MainEffects;
        protected Action PostEffects;
        
        protected Func<bool> Restrict;
        public Action Adjust;
        public Action Unadjust;

        public Action<Panel> DrawAims { get; private set; }
        public Action<Panel> DrawSpecial { get; private set; }
        
        private Ability(
            Unit parent, 
            string name, 
            int weight, 
            Price price, 
            int damage = 0, 
            int modifier = 0,
            Species species = Species.None)
        {
            Parent = parent;
            Name = name;
            Weight = weight;
            Price = price;

            Desc = () => { return "Default Ability description."; };
            Species = species;
            Template = null;
            Damage = damage;
            Modifier = modifier;

            Used = false;
            Legal = DefaultLegalTest;
            Aims = new List<Aim>();

            PreEffects = () => { Charge(); };
            MainEffects = Targets => { };
            PostEffects = () => Targeter.Reset();

            Restrict = () => { return false; };
            Adjust = () => { };
            Unadjust = () => { };

            DrawAims = (p) =>
            {
                foreach (Aim aim in Aims)
                    aim.Draw(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            DrawSpecial = (p) => { GUI.Label(p.LineBox, Desc()); };
                
        }


        private Ability(Unit parent, string name, int weight, Price price, Species species)
            : this(parent, name, weight, price, 0, 0, species) { }



		
		
		public void Execute (TargetGroup Targets) {
			PreEffects();
			MainEffects(Targets);
			PostEffects();
		}

        public void Draw(Panel p) { InspectorInfo.Ability(this, p); }

		public void Reset () {Used = false;}

		public void Charge () {
			Used = true;
			Parent.Wallet.Charge(Price);
		}

		public int CompareTo (Ability other) {
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

        public bool DefaultLegalTest(out string message)
        {
            message = Name + " currently legal.";
            if (Parent != TurnQueue.Top)
            {
                message = "It is not currently " + Parent + "'s turn.";
                return false;
            }
            if (Used)
            {
                message = Name + " has already been used this turn.";
                return false;
            }
            if (!Parent.Wallet.CanAfford(Price))
            {
                message = Parent + " cannot afford " + Name + ".";
                return false;
            }
            if (Restrict())
            {
                message = Name + " currently illegal.";
                return false;
            }
            if (EffectQueue.Processing)
            {
                message = "Another action is currently in progress.";
                return false;
            }
            return true;
        }

	}
}