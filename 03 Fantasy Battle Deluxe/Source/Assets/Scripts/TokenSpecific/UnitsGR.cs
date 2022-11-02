using HOA.Tokens.Components;
using HOA.Actions;
using HOA.Players;
using HOA.Map;

namespace HOA.Tokens {

	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			NewLabel(TTYPE.KATA, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(5);	
			
			arsenal.Add(new AMoveKata(Price.Cheap(), this, Aim.MovePath(4)));
			arsenal.Add(new AAttack(Price.Cheap(), this, Aim.Melee(), 8));
		}
		public override string Notes () {return "";}
	}
	public class CarapaceInvader : Unit {
		public CarapaceInvader(Source s, bool template=false){
			NewLabel(TTYPE.CARA, s, false, template);
			sprite = new HOASprite(this);
			body = new BodyCara(this);
			
			NewWallet();
			
			health = new HealthCara(this, 35, 2);
			NewWatch(4);
			
			NewArsenal();
			arsenal.Add(new AFocus(this)); 
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AShock(Price.Cheap(), this, Aim.Melee(), 10, 5));
			arsenal.Add(new ACaraDischarge(new Price(1,2), this, 10, 5));
		}
		public override string Notes () {return "+1 DEF per FP. DEF can never exceed 5.";}
	}
	public class Mawth : Unit {
		public Mawth(Source s, bool template=false){
			NewLabel(TTYPE.MAWT, s, false, template);
			BuildAir();
			
			NewHealth(55);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(4)));
		}
		public override string Notes () {return "";}
	}
	public class Kabutomachine : Unit {
		
		public Kabutomachine(Source s, bool template=false){
			NewLabel(TTYPE.KABU, s, true, template);
			BuildAir();
			AddKing();
			SetOnDeath(TTYPE.HSIL, false);
			
			NewHealth(75);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(5)));
			arsenal.Add(new AAttack(Price.Cheap(), this, Aim.Melee(), 12));
			arsenal.Add(new ACreate(Price.Cheap(), this, TTYPE.KATA));
			arsenal.Add(new ACreate(new Price(0,2), this, TTYPE.CARA));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.MAWT));
		}
		public override string Notes () {return "";}
	}
}
