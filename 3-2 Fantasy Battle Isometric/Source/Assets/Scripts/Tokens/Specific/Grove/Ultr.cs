using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Ultratherium : Unit {
		public Ultratherium(Source s, bool template=false){
			id = new ID(this, EToken.ULTR, s, true, template);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			type.Add(EType.KING);
			onDeath = EToken.HFIR;

			ScaleJumbo();
			NewHealth(80);
			NewWatch(2);
			NewWallet(3);
			arsenal.Add(new AMovePath(this, 3));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 16));
			arsenal.Add(new AUltrThrow(new Price(1,1), this, 3, 16));
			arsenal.Add(new AUltrBlast(this));
			arsenal.Add(new ACreate(Price.Cheap, this, EToken.GRIZ));
			arsenal.Add(new ACreate(new Price(1,1), this, EToken.TALO));
			arsenal.Add(new AUltrCreateMeta(new Price(1,2), this, EToken.META));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	

	public class AUltrThrow : Action {
		int damage;
		Aim aim2;
		
		public AUltrThrow (Price p, Unit u, int range, int dmg) {
			weight = 4;
			actor = u;
			price = p;
			AddAim(new Aim(ETraj.NEIGHBOR, EType.DEST));
			AddAim(HOA.Aim.Arc(range));
			damage = dmg;
			
			name = "Throw Terrain";
			desc = "Destroy target non-Remains destructible.\n"+aim[1].ToString()+"\nDo "+damage+" damage to target unit.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EKill (new Source(actor), (Token)targets[0]));
			EffectQueue.Add(new EDamage(new Source(actor), (Unit)targets[1], damage));
			Targeter.Reset();
		}
	}

	public class AUltrCreateMeta : Action {
		
		Cell cell;
		EToken child;
		Token chiTemplate;
		
		public AUltrCreateMeta (Price p, Unit par, EToken chi) {
			weight = 5;
			price = p;
			actor = par;
			AddAim(new Aim (ETraj.NEIGHBOR, EType.DEST));
			
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = chiTemplate.ID.Name;
			desc = "Replace target non-remains destructible with "+name+".";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EReplace(new Source(actor), (Token)targets[0], child));
			Targeter.Reset();
		}
	}

	public class AUltrBlast : Action {
		
		int damage;
		
		public AUltrBlast (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Shoot(2));
			damage = 20;
			
			name = "Ice Blast";
			desc = "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(actor), u, damage));

			u.AddStat (new Source(actor), EStat.IN, -2);
			u.timers.Add(new TBlast(u, actor));
			Targeter.Reset();
		}
	}
	public class TBlast : Timer {
		Token source;

		public TBlast (Unit par, Token s) {
			parent = par;
			source = s;

			turns = 2;
			
			name = "Ice Blasted";
			desc = parent.ToString()+" Initiative -2 for 2 turns.";
			
		}
		
		public override void Activate () {
			parent.AddStat(new Source(source), EStat.IN, 2);
		}
	}
}