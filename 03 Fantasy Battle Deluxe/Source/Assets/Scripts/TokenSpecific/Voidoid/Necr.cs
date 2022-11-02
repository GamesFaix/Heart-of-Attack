
namespace HOA{
	public class Necrochancellor : Unit {
		public Necrochancellor(Source s, bool template=false){
			NewLabel(TTYPE.NECR, s, false, template);
			BuildEth();
			
			NewHealth(30,5);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ANecrTeleport(new Price(0,1), this, 5));
			arsenal.Add(new ANecrTouch(Price.Cheap, this, Aim.Melee(), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ANecrTouch : Action {
		
		int damage;
		
		public ANecrTouch (Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Touch of Death";
			desc = "Do "+d+" damage to target unit.\nIf target has less than 10 health after damage is dealt, destroy target.\nIf target is destroyed and is not an Attack King, it leaves no remains and you may place a Corpse in any cell.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RNecrTouch(new Source(actor), default(Token), damage));
			}
		}
	}

	public class ANecrTeleport : Action {
		Aim aim2;
		
		public ANecrTeleport (Price p, Unit u, int range) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.REM, range);
			aim2 = new Aim(AIMTYPE.ARC, TARGET.CELL, CTAR.MOVE, range);
			
			name = "Defile";
			desc = "Move target remains to target cell.\n"+aim2.ToString();
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RTeleport(new Source(actor), default(Token), aim2));
			}
		}
	}

	public class RNecrTouch : RInstanceSelect {
		public int magnitude;	
		public RNecrTouch (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				int oldHP = u.HP;
				int def = u.DEF;
				
				int damage = magnitude - def;
				if (oldHP - damage < 10) {damage = oldHP;}
				if (damage >= oldHP) {
					u.Die(source, false, true);
					Reset();
					Aim aim = new Aim(AIMTYPE.FREE, TARGET.CELL, CTAR.CREATE);
					Legalizer.Find(source.Token, aim, TemplateFactory.Template(TTYPE.CORP));
					GUISelectors.DoWithCell (new RCreate(source, TTYPE.CORP, default(Cell)));
				}
				else {
					u.Damage(source, magnitude);
					u.SpriteEffect(EFFECT.DMG);
				}
			}
			//Reset();
		}
	}
}