using UnityEngine;

namespace HOA{
	public class Ultratherium : Unit {
		public Ultratherium(Source s, bool template=false){
			NewLabel(TTYPE.ULTR, s, true, template);
			BuildTrample();
			AddKing();
			OnDeath = TTYPE.HFIR;
			
			NewHealth(80);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 16));
			arsenal.Add(new AUltrThrow(new Price(1,1), this, 3, 20));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.GRIZ));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.TALO));
			arsenal.Add(new AUltrCreateMeta(new Price(1,2), this, TTYPE.META));
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
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DEST);
			aim2 = Aim.Shoot(range);
			damage = dmg;
			
			name = "Throw Terrain";
			desc = "Destroy target non-Remains destructible.\n"+aim2.ToString()+"\nDo "+damage+" damage to target unit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RUltrThrow(new Source(actor), default(Token), aim2, damage));
			}
		}
	}

	public class AUltrCreateMeta : Action {
		
		Cell cell;
		TTYPE child;
		Token chiTemplate;
		
		public AUltrCreateMeta (Price p, Unit par, TTYPE chi) {
			weight = 5;
			price = p;
			actor = par;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DEST);
			
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = chiTemplate.Name;
			desc = "Replace target non-remains destructible with "+name+".";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReplace(new Source(actor), default(Token), child));
			}
		}
	}

	public class RUltrThrow : RInstanceSelect{
		Aim aim2; int damage;
		
		public RUltrThrow (Source s, Token t, Aim a2, int dmg) {source = s; instance = t; aim2 = a2; damage = dmg;}
		
		public override void Grant () {
			Debug.Log("killing destructible");
			instance.Die(source);
			Debug.Log("legalizing tokens for attack ("+aim2.ToString()+")");
			Reset();
			Legalizer.Find(source.Token, aim2);
			
			GUISelectors.DoWithInstance(new RDamage (source, default(Token), damage));
		}
	}
}