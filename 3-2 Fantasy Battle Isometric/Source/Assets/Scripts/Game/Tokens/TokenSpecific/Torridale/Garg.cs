using System.Collections.Generic;

namespace HOA{
	public class Gargoliath : Unit {
		public Gargoliath(Source s, bool template=false){
			id = new ID(this, EToken.GARG, s, true, template);
			plane = Plane.Air;
			type.Add(EClass.KING);
			onDeath = EToken.HSTO;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3);
			
			arsenal.Add(new AMovePath(this, 4));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 18));
			arsenal.Add(new AGargLand(this));
			arsenal.Add(new AGargPetrify(this));
			arsenal.Add(new AGargRook(new Price(1,1), this));
			arsenal.Add(new ACreate(Price.Cheap, this, EToken.SMAS));
			arsenal.Add(new ACreate(new Price(1,1), this, EToken.CONF));
			arsenal.Add(new ACreate(new Price(2,2), this, EToken.BATT));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	

	public class AGargLand : Action {
		public AGargLand (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Self());
			
			name = "Land";
			desc = "Becomes trampling ground unit. \nMove range -2 \nDefense +2\nForget 'Create Rook' \nLearn 'Tail Whip'";
		}

		public override bool Restrict () {
			if (!actor.Body.Cell.Contains(EPlane.GND)) {return false;}
			Token t;
			if (actor.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Type.Is(EClass.DEST)) {return false;}
			}
			return true;

		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			Token t;
			if (actor.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Type.Is(EClass.DEST)) {
					t.Die(new Source(actor));
				}
			}
			
			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.DEF, 2));
			actor.Plane.Set(EPlane.GND);
			actor.Type.Set(new List<EClass> {EClass.UNIT, EClass.KING, EClass.TRAM});
			foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {	if (a is AGargLand) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {	if (a is AGargRook) {actor.Arsenal().Remove(a);} }
			actor.Arsenal().Add(new AMove(actor, HOA.Aim.MovePath(3)));
			actor.Arsenal().Add(new AGargFly(actor));
			actor.Arsenal().Add(new AGargTailWhip(new Price(1,1), actor,10));
			actor.Arsenal().Sort();
			
			actor.Display.Effect(EEffect.STATUP);
			Targeter.Reset();
		}
	}
	public class AGargFly : Action {
		public AGargFly (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Self());
			
			name = "Take Flight";
			desc = "Becomes air unit. \nMove range +2\nDefense -2\nForget 'Tail Whip'\nLearn 'Create Rook'";
		}
		public override bool Restrict () {
			if (actor.Body.Cell.Contains(EPlane.AIR)) {return true;}
			return false;
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EAddStat(new Source(actor), actor, EStat.DEF, -2));
			actor.Plane.Set(EPlane.AIR);
			actor.Type.Set(new List<EClass> {EClass.UNIT, EClass.KING, EClass.TRAM});
			foreach (Action a in actor.Arsenal()) {if (a is AMove) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {	if (a is AGargFly) {actor.Arsenal().Remove(a);} }
			foreach (Action a in actor.Arsenal()) {if (a is AGargTailWhip) {actor.Arsenal().Remove(a);} }
			actor.Arsenal().Add(new AMove(actor, HOA.Aim.MovePath(5)));
			actor.Arsenal().Add(new AGargLand(actor));
			actor.Arsenal().Add(new AGargRook(new Price(1,1),actor));
			actor.Arsenal().Sort();
			
			actor.Display.Effect(EEffect.STATUP);
			Targeter.Reset();
		}
	}
	
	public class AGargTailWhip : Action {
		int damage;
		
		public AGargTailWhip (Price p, Unit u, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			AddAim(HOA.Aim.Self());
			damage = d;
			
			name = "Tail Whip";
			desc = "Do "+d+" damage to all neighboring units.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			TokenGroup neighbors = actor.Body.Neighbors(false);
			neighbors = neighbors.OnlyType(EClass.UNIT);
			foreach (Token t in neighbors) {
				Unit u = (Unit)t;
				EffectQueue.Add(new EDamage(new Source(actor), u, damage));
			}
			Targeter.Reset();
		}
	}

	public class AGargRook : Action {
		
		Token template;
		
		public AGargRook (Price p, Unit par) {
			weight = 5;
			actor = par;
			template = TemplateFactory.Template(EToken.ROOK);
			price = p;
			
			AddAim(HOA.Aim.Self());
			
			name = template.ID.Name;
			desc = "Create "+name+" in "+actor+"'s cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			if (!actor.Body.Cell.Occupied(EPlane.GND)) {
				Charge();
				TokenFactory.Add(EToken.ROOK, new Source(actor), actor.Body.Cell);
			}
			Targeter.Reset();
		}
	}

	public class AGargPetrify : Action {
		
		int damage;
		
		public AGargPetrify (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Shoot(2));
			damage = 10;
			
			name = "Petrify";
			desc = "Target Unit takes "+damage+" damage and cannot move on its next turn.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(actor), u, damage));
			if ((u.Arsenal()[0]) is AMove) {
				AMove move = (AMove)u.Arsenal()[0];
				u.timers.Add(new TPetrify(u, actor, move));
				u.Arsenal().Remove(move);
			}
			Targeter.Reset();
		}
	}
	public class TPetrify : Timer {
		
		AMove move;

		public TPetrify (Unit par, Token s, AMove m) {
			parent = par;
			turns = 1;

			move = m;

			name = "Petrified";
			desc = parent.ToString()+" cannot move on its next turn.";
			
		}
		
		public override void Activate () {
			parent.Arsenal().Add(move);
			parent.Arsenal().Sort();
		}
	}





}
