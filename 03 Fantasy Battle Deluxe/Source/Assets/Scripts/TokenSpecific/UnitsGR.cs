﻿using HOA.Tokens.Components;
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
			
			arsenal.Add(new AMoveKata(Price.Cheap, this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Sort();
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
			arsenal.Add(new AShock(Price.Cheap, this, Aim.Melee(), 10, 5));
			arsenal.Add(new ACaraDischarge(new Price(1,2), this, 10, 5));
			arsenal.Sort();
		}
		public override string Notes () {return "Defense +1 per Focus. Defense can never exceed 5.  \nAll non-Carapace neighboring teammates add Carapace's Defense.";}

		public override void Die (Source s, bool corpse=true, bool log=true) {
			BodyCara bc = (BodyCara)body;
			bc.DestroySensors();

			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			if (this == TurnQueue.Top) {TurnQueue.Advance();}
			TurnQueue.Remove((Unit)this);
			TokenFactory.Remove(this);
			Cell oldCell = Cell;
			Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (log) {GameLog.Out(s.Token+" killed "+this+".");}
		}

	}
	public class Mawth : Unit {
		public Mawth(Source s, bool template=false){
			NewLabel(TTYPE.MAWT, s, false, template);
			BuildAir();
			
			NewHealth(55);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(4)));
			arsenal.Add(new ALaser("Laser shot", Price.Cheap, this, Aim.Shoot(3), 16));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
	public class Kabutomachine : Unit {
		
		public Kabutomachine(Source s, bool template=false){
			NewLabel(TTYPE.KABU, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = TTYPE.HSIL;
			
			NewHealth(75);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(5)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 16));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.KATA));
			arsenal.Add(new ACreate(new Price(0,2), this, TTYPE.CARA));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.MAWT));
			arsenal.Add(new AKabuTeleport(new Price(1,1), this, 5));
			arsenal.Add(new AKabuLaser(new Price (1,2), this, Aim.Shoot(20), 16));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}