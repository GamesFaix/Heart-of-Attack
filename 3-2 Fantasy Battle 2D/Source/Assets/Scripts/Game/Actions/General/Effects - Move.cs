using UnityEngine;
using System.Collections.Generic;

namespace HOA {


	public class EMove : Effect {
		public override string ToString () {return "Effect - Move";}
		Token target; Cell cell;
		
		public EMove (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Cell;
			target.SpriteMove(cell);
			target.Enter(cell);
			Cell newCell = target.Cell;
			if (target.IsPlane(EPlane.GND)) {Mixer.Play(SoundLoader.Effect(EEffect.WALK));}
			else if (target.IsPlane(EPlane.AIR)) {Mixer.Play(SoundLoader.Effect(EEffect.FLY));}
			
			GameLog.Out (target+" moved from "+oldCell+" to "+newCell+".");
		}
	}	

	public class EBurrow : Effect {
		public override string ToString () {return "Effect - Burrow";}
		Token target; Cell cell;
		
		public EBurrow (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Cell;
			
			target.SpriteEffect(EEffect.BURROW);
			Mixer.Play(SoundLoader.Effect(EEffect.BURROW));
			EffectQueue.Add(new EBurrow2(source, target, cell));
			
			
			GameLog.Out (target+" burrowed from "+oldCell+" to "+cell+".");
		}
	}	

	public class EBurrow2 : Effect {
		public override string ToString () {return "Effect - Burrow2";}
		Token target; Cell cell;
		
		public EBurrow2 (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			target.Enter(cell);
			target.SpriteEffect(EEffect.BURROW);
			Mixer.Play(SoundLoader.Effect(EEffect.BURROW));
		}
	}	

	public class ETeleport : Effect {
		public override string ToString () {return "Effect - Teleport";}
		Token target; Cell cell;
		
		public ETeleport (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Cell;
			
			target.SpriteEffect(EEffect.TELEPORT);
			Mixer.Play(SoundLoader.Effect(EEffect.TELEPORT));
			EffectQueue.Add(new ETeleport2(source, target, cell));
			
			
			GameLog.Out (source.Token+" teleported "+target+" from "+oldCell+" to "+cell+".");
		}
	}	

	public class ETeleport2 : Effect {
		public override string ToString () {return "Effect - Teleport2";}
		Token target; Cell cell;
		
		public ETeleport2 (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			target.Enter(cell);
			target.SpriteEffect(EEffect.TELEPORT);
			Mixer.Play(SoundLoader.Effect(EEffect.TELEPORT));
		}
	}	

	public class ESwap : Effect {
		public override string ToString () {return "Effect - Swap";}
		Token target; Token other;
		
		public ESwap (Source s, Token t, Token t2) {
			source = s; target = t; other = t2;
		}
		public override void Process() {
			target.SpriteMove(other.Cell);
			other.SpriteMove(target.Cell);
			target.Swap(other);
			GameLog.Out (target+" swapped places with "+other+".");
		}
	}	

	public class EKnockback : Effect {
		public override string ToString () {return "Effect - Knockback";}
		Token target; int range; int damage;
		
		public EKnockback (Source s, Token t, int r, int d=0) {
			source = s; target = t; range = r; damage = d;
		}
		public override void Process() {
			Cell actorCell = source.Token.Cell;
			Cell start = target.Cell;

			int[] dir = Direction.FromCells(actorCell, start);

			CellGroup line = new CellGroup();

			Debug.Log("knockback range "+range);
			for (int i=0; i<range; i++) {
				int x = start.X+dir[0];
				int y = start.Y+dir[1];
				Cell next;
				Debug.Log(x+","+y);
				if (Board.HasCell(x,y, out next)) {
					Debug.Log(next);
					line.Add(next);
					start = next;
				}
			}

			int totalDamage = 0;
			int totalCells = 0;
			foreach (Cell c in line) {
				if (target.CanEnter(c)) {
					EffectQueue.Add(new EMove(source, target, c));
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
					EffectQueue.Add(new EDamage(source, (Unit)target, totalDamage));
					log += ", dealing "+totalDamage+" damage.";
				}
				else {log +=".";}
			}
			GameLog.Out(log);
		}
	}	
}