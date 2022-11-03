using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Monolith : Unit {
		public Monolith(Source s, bool template=false){
			id = new ID(this, EToken.MONO, s, true, template);
			//sprite = new HOA.Sprite(this);
			plane = Plane.Tall;
			type.Add(EType.KING);
			onDeath = EToken.HBLO;
			ScaleTall();
			NewHealth(100);
			NewWatch(2);
			NewWallet(3);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new ARage(this, 20),
				new AMonoField(this),
				new AMonoAltar(this),
				new ACreate(this, new Price(1,0), EToken.RECY),
				new AMonoReanimate(this, new Price(1,0)),
				new ACreate(this, new Price(2,1), EToken.NECR),
				new ACreateArc(this, new Price(1,2), EToken.GATE, 3,3)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
	/*
	public class AMonoFlame : Task {
		int damage;
		
		public AMonoFlame (Unit u) {
			weight = 4;
			Price = new Price(1,2);
			Parent = u;
			
			AddAim(new Aim (ETraj.LINE, new List<EType> {EType.UNIT, EType.DEST}, 2));
			damage = 20;
			
			name = "Eternal Flame";
			desc = "Do "+damage+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (TargetGroup targets) {
			Charge();
			Token tar = (Token)targets[0];

			TokenGroup affected = new TokenGroup(Parent);
			TokenGroup thisRad = new TokenGroup(tar);
			TokenGroup nextRad = new TokenGroup();
			
			int dmg = damage;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.Display.Effect(EEffect.FIRE);
						AEffects.DamageDest(new Source(Parent), next, dmg);
						
						foreach (Token t in next.Neighbors(true)) {
							nextRad.Add(t);
						}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new TokenGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
				Targeter.Reset();
			}

		}
	}
	*/

	public class AMonoReanimate : Task {

		public override string Desc {get {return "Replace target remains with Recyclops.";} }

		public AMonoReanimate (Unit par, Price p) {
			Name = "Recycle Recyclops";
			Weight = 5;
			Price = p;
			Parent = par;
			AddAim(new Aim (ETraj.NEIGHBOR, EType.REM));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EReplace(new Source(Parent), (Token)targets[0], EToken.RECY));
		}
	}

	public class AMonoField : Task {
		int range = 2;
		int damage = 5;

		public override string Desc {get {return "Do "+damage+" damage to all units within "+range+" cells of "+Parent.ID.Name+". " +
				"\n"+Parent.ID.Name+" gains Health equal to damage successfully dealt.";} }

		public AMonoField (Unit u) {
			Parent = u;
			Name = "Death Field";
			Weight = 4;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			CellGroup zone = Zone(Parent, range);
			TokenGroup affected = zone.Occupants.OnlyType(EType.UNIT);
			affected.Remove(Parent);

			foreach (Unit u in affected) {
				EffectQueue.Add(new ELeech(new Source(Parent), u, damage));
			}
		}

		CellGroup Zone (Token Parent, int range) {
			Cell start = Parent.Body.Cell;

			int startX = start.X-range;
			int endX = start.X+range;
			int startY = start.Y-range;
			int endY = start.Y+range;

			CellGroup cells = new CellGroup();
			Cell cell;

			for (int i=startX; i<=endX; i++) {
				for (int j=startY; j<=endY; j++) {
					if (Board.HasCell(i,j, out cell)) {cells.Add(cell);}
				}
			}
			return cells;
		}

	}

	public class AMonoAltar : Task {

		public override string Desc {get {return "Destroy neighboring teammate." +
				"\nInitiative +4 for next 2 turns.";} }

		public AMonoAltar (Unit par) {
			Name = "Blood Altar ";
			Weight = 4;
			Price = new Price(1,0);
			Parent = par;
			Aim newAim = new Aim (ETraj.NEIGHBOR, EType.UNIT);
			newAim.TeamOnly = true;
			AddAim(newAim);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EKill(new Source(Parent), (Token)targets[0]));

			Parent.AddStat (new Source(Parent), EStat.IN, 4);
			Parent.timers.Add(new TAltar(Parent));
		}
	}
	public class TAltar : Timer {

		public TAltar (Unit par) {
			parent = par;

			turns = 2;
			
			name = "Blood Altaration";
			desc = parent.ToString()+" Initiative +4 for 2 turns.";
		}
		
		public override void Activate () {
			parent.AddStat(new Source(parent), EStat.IN, -4);
		}
	}
}