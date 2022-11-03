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
			
			NewArsenal();
			arsenal.Add(new AFocus(this));
			arsenal.Add(new AMovePath(this, 4));
			arsenal.Add(new ARage(Price.Cheap, this, Aim.Melee(), 20));
			
			//arsenal.Add(new AMonoFlame(this));
			arsenal.Add(new AMonoField(this));
			arsenal.Add(new AMonoAltar(this));

			arsenal.Add(new ACreate(new Price(1,0), this, EToken.RECY));
			arsenal.Add(new AMonoReanimate(new Price(1,0), this));
			arsenal.Add(new ACreate(new Price(2,1), this, EToken.NECR));

			Aim aim = new Aim(EAim.ARC, EType.CELL, EPurpose.CREATE, 3,3);
			arsenal.Add(new ACreate(new Price(1,2), this, EToken.MOUT, aim));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	/*
	public class AMonoFlame : Action {
		int damage;
		
		public AMonoFlame (Unit u) {
			weight = 4;
			price = new Price(1,2);
			actor = u;
			
			AddAim(new Aim (EAim.LINE, new List<EType> {EType.UNIT, EType.DEST}, 2));
			damage = 20;
			
			name = "Eternal Flame";
			desc = "Do "+damage+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Token tar = (Token)targets[0];

			TokenGroup affected = new TokenGroup(actor);
			TokenGroup thisRad = new TokenGroup(tar);
			TokenGroup nextRad = new TokenGroup();
			
			int dmg = damage;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.Display.Effect(EEffect.FIRE);
						AEffects.DamageDest(new Source(actor), next, dmg);
						
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

	public class AMonoReanimate : Action {

		public AMonoReanimate (Price p, Unit par) {
			weight = 5;
			price = p;
			actor = par;
			AddAim(new Aim (EAim.NEIGHBOR, EType.REM));

			//Token childTemplate = TemplateFactory.Template(EToken.RECY);
			
			name = "Recycle";
			desc = "Replace target remains with "+name+".";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EReplace(new Source(actor), (Token)targets[0], EToken.RECY));
			Targeter.Reset();
		}
	}

	public class AMonoField : Action {
		
		int range;
		int damage;
		
		public AMonoField (Unit u) {
			weight = 4;
			price = new Price(1,1);
			actor = u;
			AddAim(HOA.Aim.Self);
			damage = 5;
			range = 2;

			name = "Death Field";
			desc = "Do "+damage+" damage to all units within "+range+" cells of "+actor.ID.Name+". \n"+actor.ID.Name+" gains Health equal to damage successfully dealt.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			CellGroup zone = Zone(actor, range);
			TokenGroup affected = zone.Occupants.OnlyClass(EType.UNIT);
			affected.Remove(actor);

			foreach (Unit u in affected) {
				EffectQueue.Add(new ELeech(new Source(actor), u, damage));
			}
			Targeter.Reset();
		}

		CellGroup Zone (Token actor, int range) {
			Cell start = actor.Body.Cell;

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

	public class AMonoAltar : Action {
		
		public AMonoAltar (Unit par) {
			weight = 4;
			price = new Price(1,0);
			actor = par;

			Aim newAim = new Aim (EAim.NEIGHBOR, EType.UNIT);
			newAim.TeamOnly = true;
			AddAim(newAim);
			
			name = "Blood Altar ";
			desc = "Destroy neighboring teammate.\nInitiative +4 for next 2 turns.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EKill(new Source(actor), (Token)targets[0]));

			actor.AddStat (new Source(actor), EStat.IN, 4);
			actor.timers.Add(new TAltar(actor));
			Targeter.Reset();
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