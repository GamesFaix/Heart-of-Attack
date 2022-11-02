using HOA.Tokens;
using System.Collections;
using HOA.Map;

namespace HOA.Actions {
	public class AGargLand : Action {
		public AGargLand (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Land";
			desc = "Becomes trampling ground unit. \nMove range: 3 \nDefense +2\nForget 'Create Rook' \nLearn 'Tail Whip'";
		}

		public override void Perform () {
			Cell c = actor.Cell;
			if (Charge() && (!c.Contains(PLANE.GND) || c.Contains(SPECIAL.DEST))) {
				if (c.Contains(SPECIAL.DEST)) {
					Token dest = c.Occupant(PLANE.GND);
					dest.Die(new Source(actor));
				}

				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.DEF, 2));
				actor.SetPlane(PLANE.GND);
				actor.SetSpecial(new SPECIAL[2] {SPECIAL.KING, SPECIAL.TRAM});
				foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is AGargLand) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is AGargRook) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AMove(actor, Aim.MovePath(3)));
				actor.Arsenal().Add(new AGargFly(actor));
				actor.Arsenal().Add(new AGargTailWhip(new Price(1,1), actor,10));
				actor.Arsenal().Sort();

				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}
	public class AGargFly : Action {
		public AGargFly (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Take Flight";
			desc = "Becomes air unit. \nMove range: 5\nDefense -2\nForget 'Tail Whip'\nLearn 'Create Rook'";
		}

		public override void Perform () {
			Cell c = actor.Cell;
			if (Charge() && (!c.Contains(PLANE.AIR))) {
				InputBuffer.Submit(new RAddStat(new Source(actor), (Token)actor, STAT.DEF, -2));
				actor.SetPlane(PLANE.AIR);
				actor.SetSpecial(new SPECIAL[2] {SPECIAL.KING, SPECIAL.TRAM});
				foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {	if (a is AGargFly) {actor.Arsenal().Remove(a);} }
				foreach (Action a in actor.Arsenal()) {if (a is AGargTailWhip) {actor.Arsenal().Remove(a);} }
				actor.Arsenal().Add(new AMove(actor, Aim.MovePath(5)));
				actor.Arsenal().Add(new AGargLand(actor));
				actor.Arsenal().Add(new AGargRook(new Price(1,1),actor));
				actor.Arsenal().Sort();

				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
	}



}
