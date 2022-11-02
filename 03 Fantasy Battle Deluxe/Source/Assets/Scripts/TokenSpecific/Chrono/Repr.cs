
namespace HOA{
	public class Reprospector : Unit {
		public Reprospector(Source s, bool template=false){
			NewLabel(TTYPE.REPR, s, false, template);
			BuildGround();
			
			NewHealth(55);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new AGrenade("Grenade", new Price(1,1), this, 2, 10));
			arsenal.Add(new AReprMine(Price.Cheap, this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AReprMine : Action {
		
		public AReprMine (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DESTREM);
			
			name = "Time Mine";
			desc = "Destroy neighboring destructible.\nIf initative is less than 6, initiative +1.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReprMine(new Source(actor), default(Token)));
			}
		}
	}


	public class RReprMine : RInstanceSelect{
		public RReprMine (Source s, Token t) {source = s; instance = t;}
		
		public override void Grant () {
			instance.Die(source);
			Unit u = (Unit)source.Token;
			
			if (u.IN < 7) {
				u.AddStat(source, STAT.IN, 1);
				u.SpriteEffect(EFFECT.STATUP);
			}
			Reset();
		}
	}
}