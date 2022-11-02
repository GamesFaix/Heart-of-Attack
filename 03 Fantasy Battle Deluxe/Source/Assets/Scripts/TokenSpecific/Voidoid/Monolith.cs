using UnityEngine;

namespace HOA{
	public class Monolith : Unit {
		public Monolith(Source s, bool template=false){
			NewLabel(TTYPE.MONO, s, true, template);
			sprite = new HOA.Sprite(this);
			BuildTall();
			AddKing();
			OnDeath = TTYPE.HBLO;
			
			NewHealth(100);
			NewWatch(2);
			
			NewArsenal();
			arsenal.Add(new AFocus(this));
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new ARage(Price.Cheap, this, Aim.Melee(), 20));
			
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 2);
			arsenal.Add(new AMonoFlame(new Price(2,2), this, fireAim, 20));
			
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.RECY));
			arsenal.Add(new AMonoReanimate(Price.Cheap, this, TTYPE.RECY));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.NECR));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.MOUT));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AMonoFlame : Action {
		int damage;
		
		public AMonoFlame (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			actor = u;
			
			aim = a;
			damage = d;
			
			name = "Eternal Flame";
			desc = "Do "+d+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamageFirMax(new Source(actor), default(Token), damage));
			}
		}
	}

	public class AMonoReanimate : Action {
		
		Cell cell;
		TTYPE child;
		Token chiTemplate;
		
		public AMonoReanimate (Price p, Unit par, TTYPE chi) {
			weight = 5;
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.REM);
			
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = "Reanimate "+chiTemplate.Name;
			desc = "Replace target remains with "+name+".";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReplace(new Source(actor), default(Token), child));
			}
		}
	}

	public class RDamageFirMax : RInstanceSelect {
		public int magnitude;	
		public RDamageFirMax (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			TokenGroup affected = new TokenGroup(source.Token);
			TokenGroup thisRad = new TokenGroup(instance);
			TokenGroup nextRad = new TokenGroup();
			
			int dmg = magnitude;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.SpriteEffect(EFFECT.FIRE);
						InputBuffer.Submit(new RDamageDest(source, next, dmg));
						
						foreach (Token t in next.Neighbors(true)) {
							nextRad.Add(t);
						}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new TokenGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
				
			}
			Reset();
		}
	}
}