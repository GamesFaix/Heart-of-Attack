using System.Collections.Generic;

namespace HOA{
	public class Metaterrainean : Unit {
		public Metaterrainean(Source s, bool template=false){
			NewLabel(EToken.META, s, false, template);
			BuildTrample();
			OnDeath = EToken.ROCK;
			
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
			AddAim(new Aim(EAim.NEIGHBOR, EClass.DEST));
			
			name = "Consume Terrain";
			desc = "Destroy neighboring non-Remains destructible.\n"+actor+" gains 12 health.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			Token t = (Token)targets[0];
			t.Die(new Source(actor));
			actor.AddStat(new Source(actor), EStat.HP, 12);
			actor.SpriteEffect(EEffect.STATUP);
		}
	}
}