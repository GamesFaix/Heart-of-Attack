using UnityEngine;
using System.Collections.Generic;

namespace HOA.Effects {

	public class Move : Effect {
		public override string ToString () {return "Effect - Move";}
		Token target; Cell cell;
		
		public Move (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Body.Cell;
		//	target.SpriteMove(cell);
			target.Body.MoveTo(cell);
			Cell newCell = target.Body.Cell;
			if (target.Plane.ground) {Mixer.Play(SoundLoader.Effect(EEffect.WALK));}
			else if (target.Plane.air) {Mixer.Play(SoundLoader.Effect(EEffect.FLY));}
			
			GameLog.Out (target+" moved from "+oldCell+" to "+newCell+".");
		}
	}	

	public class Burrow : Effect {
		public override string ToString () {return "Effect - Burrow";}
		Token target; Cell cell;
		
		public Burrow (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Body.Cell;
			
			target.Display.Effect(EEffect.BURROW);
			Mixer.Play(SoundLoader.Effect(EEffect.BURROW));
			EffectQueue.Add(new Burrow2(source, target, cell));
			
			
			GameLog.Out (target+" burrowed from "+oldCell+" to "+cell+".");
		}
	}	

	public class Burrow2 : Effect {
		public override string ToString () {return "Effect - Burrow2";}
		Token target; Cell cell;
		
		public Burrow2 (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			target.Body.Enter(cell);
			target.Display.Effect(EEffect.BURROW);
			Mixer.Play(SoundLoader.Effect(EEffect.BURROW));
		}
	}	

	public class Teleport : Effect {
		public override string ToString () {return "Effect - Teleport";}
		Token target; Cell cell;
		
		public Teleport (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Body.Cell;
			
			target.Display.Effect(EEffect.TELEPORT);
			Mixer.Play(SoundLoader.Effect(EEffect.TELEPORT));
			EffectQueue.Add(new Teleport2(source, target, cell));
			
			
			GameLog.Out (source.Token+" teleported "+target+" from "+oldCell+" to "+cell+".");
		}
	}	

	public class Teleport2 : Effect {
		public override string ToString () {return "Effect - Teleport2";}
		Token target; Cell cell;
		
		public Teleport2 (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			target.Body.Enter(cell);
			target.Display.Effect(EEffect.TELEPORT);
			Mixer.Play(SoundLoader.Effect(EEffect.TELEPORT));
		}
	}	

	public class Swap : Effect {
		public override string ToString () {return "Effect - Swap";}
		Token target; Token other;
		
		public Swap (Source s, Token t, Token t2) {
			source = s; target = t; other = t2;
		}
		public override void Process() {
		//	target.SpriteMove(other.Cell);
		//	other.SpriteMove(target.Cell);
			target.Body.Swap(other);
			GameLog.Out (target+" swapped places with "+other+".");
		}
	}	

	public class Knockback : Effect {
		public override string ToString () {return "Effect - Knockback";}
		Token target; int range; int damage;
		
		public Knockback (Source s, Token t, int r, int d=0) {
			source = s; target = t; range = r; damage = d;
		}
		public override void Process() {
			Cell actorCell = source.Token.Body.Cell;
			Cell start = target.Body.Cell;

			int2 dir = Direction.FromCells(actorCell, start);

			CellGroup line = new CellGroup();

			for (int i=0; i<range; i++) {
				index2 index = start.Index + dir;
				Cell next;
				if (Game.Board.HasCell(index, out next)) {
					line.Add(next);
					start = next;
				}
			}

			int totalDamage = 0;
			int totalCells = 0;
			foreach (Cell c in line) {
				if (target.Body.CanEnter(c)) {
					EffectQueue.Add(new Move(source, target, c));
					totalDamage += damage;
					totalCells++;
				}
				else {break;}
				if (c.StopToken(target)) {break;}
			}

			string log = "";
			if (totalCells == 0) {log = source.Token+" attempted to knock "+target+" back, but there was something in the way.";}
			else {
				log = source.Token+" knocked "+target+" back "+totalCells+" cells";
				if (totalDamage > 0) {
					EffectQueue.Add(new Damage(source, (Unit)target, totalDamage));
					log += ", dealing "+totalDamage+" damage.";
				}
				else {log +=".";}
			}
			GameLog.Out(log);
		}
	}	
}