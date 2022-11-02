
namespace HOA{
	public class Gargoliath : Unit {
		public Gargoliath(Source s, bool template=false){
			NewLabel(TTYPE.GARG, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = TTYPE.HSTO;
			
			NewHealth(75);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 18));
			arsenal.Add(new AGargLand(this));
			arsenal.Add(new AGargRook(new Price(1,1), this));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.SMAS));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.CONF));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.BATT));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	

	public class AGargLand : Action {
		public AGargLand (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, CTAR.NA);
			
			name = "Land";
			desc = "Becomes trampling ground unit. \nMove range -2 \nDefense +2\nForget 'Create Rook' \nLearn 'Tail Whip'";
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
			desc = "Becomes air unit. \nMove range +2\nDefense -2\nForget 'Tail Whip'\nLearn 'Create Rook'";
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
	
	public class AGargTailWhip : Action {
		int damage;
		
		public AGargTailWhip (Price p, Unit u, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			aim = new Aim(AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
			damage = d;
			
			name = "Tail Whip";
			desc = "Do "+d+" damage to all neighboring units.";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup targets = actor.Neighbors(false);
				targets = targets.FilterUnit;
				foreach (Token t in targets) {
					Unit u = (Unit)t;
					InputBuffer.Submit(new RDamage(new Source(actor), u, damage));
				}
			}
		}
	}

	public class AGargRook : Action {
		
		Token template;
		
		public AGargRook (Price p, Unit par) {
			weight = 5;
			actor = par;
			template = TemplateFactory.Template(TTYPE.ROOK);
			price = p;
			
			aim = new Aim(AIMTYPE.SELF, TARGET.CELL, CTAR.CREATE);
			
			name = template.Name;
			desc = "Create "+name+" in "+actor+"'s cell.";
		}
		
		public override void Perform () {
			Cell c = actor.Cell;
			if (Charge() && !c.Occupied(PLANE.GND)) {
				TokenFactory.Add(TTYPE.ROOK, new Source(actor), c);
			}
		}
	}
	
}
