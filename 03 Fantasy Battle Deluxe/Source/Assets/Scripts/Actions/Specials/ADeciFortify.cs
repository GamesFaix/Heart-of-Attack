using HOA.Tokens;
using System.Collections;
using HOA.Map;

namespace HOA.Actions {
	public class ADeciFortify : Action {
		public ADeciFortify (Unit u) {
			weight = 4;
			
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Fortify";
			desc = "Health +10/10\nDefense + 1\nAttack range +1\nAttack damage +4\nForget 'Move'\nLearn 'Mortar'";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.MHP, 10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.HP, 10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.DEF, 1));
				foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {if (a is AAttack) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is ADeciFortify) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, Aim.Shoot(4), 22));
				actor.Arsenal().Add(new ADeciMortar(new Price(1,2), actor, 3, 5, 14));
				actor.Arsenal().Add(new ADeciMobilize(actor));
				actor.Arsenal().Sort();
				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}
	public class ADeciMobilize : Action {
		public ADeciMobilize (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Mobilize";
			desc = "Health -10/10\nDefense -1\nAttack range -1\nAttack damage -4\nLearn 'Move'\nForget 'Mortar'";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.MHP, -10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.HP, -10));
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.DEF, -1));
				foreach (Action a in actor.Arsenal()) {	if (a is AAttack) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) { if (a is ADeciMortar) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is ADeciMobilize) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AMove(actor, Aim.MovePath(2)));
				actor.Arsenal().Add(new AAttack("Shoot", Price.Cheap, actor, Aim.Shoot(3), 18));
				actor.Arsenal().Add(new ADeciFortify(actor));
				actor.Arsenal().Sort();

				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}
	
	
	
}
