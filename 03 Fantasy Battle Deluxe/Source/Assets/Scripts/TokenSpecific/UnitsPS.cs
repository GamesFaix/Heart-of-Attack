using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;

namespace HOA.Tokens{

	public class PrismGuard : Unit {
		public PrismGuard(Source s, bool template=false){
			NewLabel(TTYPE.PRIS, s, false, template);
			BuildGround();
			
			NewHealth(15);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new APrisRefract(new Price(1,1), this, Aim.Shoot(3), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "Actions targetting "+Name+" have a 50% of missing.";}

		public override void Select (Source s) {
			int flip = DiceCoin.Throw(s, DICE.COIN);
			if (flip == 1) {
				SpriteEffect(EFFECT.HEADS);
				GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to target "+FullName+" and missed.");
				SpriteEffect(EFFECT.TAILS);
			}
		}


	}

	public class ArenaNonSensus : Unit {
		public ArenaNonSensus(Source s, bool template=false){
			NewLabel(TTYPE.AREN, s, false, template);
			BuildEth();
			
			NewHealth(55,3);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class PriestOfNaja : Unit {
		public PriestOfNaja(Source s, bool template=false){
			NewLabel(TTYPE.PRIE, s, false, template);
			BuildGround();
			
			NewHealth(50,2);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class DreamReaver : Unit {
		public DreamReaver(Source s, bool template=false){
			NewLabel(TTYPE.DREA, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = TTYPE.HGLA;
			
			NewHealth(75,2);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.PRIS));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.AREN));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.PRIE));
			arsenal.Add(new ADreaTeleport(new Price(1,1), this, 5));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}