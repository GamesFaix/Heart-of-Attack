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
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new AStrike(this, 16),
				new AUltrThrow(this),
				new AUltrBlast(this),
				new ACreate(this, Price.Cheap, EToken.GRIZ),
				new ACreate(this, new Price(1,1), EToken.TALO),
				new AUltrCreateMeta(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	

	public class AUltrThrow : Task {

		public override string Desc {get {return "Destroy target non-Remains destructible." +
				"\n"+aim[1].ToString()+"" +
					"\nDo "+damage+" damage to target unit.";} } 

		int damage = 16;
		Aim aim2;
		
		public AUltrThrow (Unit parent) {
			AddAim(new Aim(ETraj.NEIGHBOR, EType.DEST));
			AddAim(HOA.Aim.Arc(3));

			Name = "Throw Terrain";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
		} 
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EKill (new Source(Parent), (Token)targets[0]));
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[1], damage));
		}
	}

	public class AUltrCreateMeta : Task {

		public override string Desc {get {return "Replace target non-remains destructible with Metaterrainean.";} }

		public AUltrCreateMeta (Unit parent) {
			Name = "Animate Metaterrainean";
			Weight = 5;
			Price = new Price(1,2);
			Parent = parent;
			AddAim(new Aim (ETraj.NEIGHBOR, EType.DEST));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EReplace(new Source(Parent), (Token)targets[0], EToken.META));
		}
	}

	public class AUltrBlast : Task {

		public override string Desc {get {return "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns.";} }

		int damage = 20;
		
		public AUltrBlast (Unit parent) {
			Name = "Ice Blast";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Shoot(2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(Parent), u, damage));

			u.AddStat (new Source(Parent), EStat.IN, -2);
			u.timers.Add(new TBlast(u, Parent));
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