
namespace HOA{
	public class Metaterrainean : Unit {
		public Metaterrainean(Source s, bool template=false){
			NewLabel(TTYPE.META, s, false, template);
			BuildTrample();
			OnDeath = TTYPE.ROCK;
			
			NewHealth(50);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 20));
			arsenal.Add(new AMetaConsume(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AMetaConsume : Action {
		
		public AMetaConsume (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DEST);
			
			name = "Consume Terrain";
			desc = "Destroy neighboring non-Remains destructible.\n"+actor+" gains 12 health.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RMetaConsume(new Source(actor), default(Token)));
			}
		}
	}

	public class RMetaConsume : RInstanceSelect{
		public RMetaConsume (Source s, Token t) {source = s; instance = t;}
		
		public override void Grant () {
			instance.Die(source);
			Unit u = (Unit)source.Token;
			u.AddStat(source, STAT.HP, 12);
			u.SpriteEffect(EFFECT.STATUP);
			Reset();
		}
	}
}