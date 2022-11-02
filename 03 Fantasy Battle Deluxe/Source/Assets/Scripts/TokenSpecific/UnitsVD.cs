using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;

namespace HOA.Tokens{
	
	public class Recyclops : Unit {
		public Recyclops(Source s, bool template=false){
			NewLabel(TTYPE.RECY, s, false, template);
			BuildGround();
			AddRem();
			
			NewHealth(15);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new ARage(new Price(1,0), this, Aim.Melee(), 12));
			arsenal.Add(new ARecyExplode(new Price(1,1), this, 12));
			arsenal.Add(new ARecyCannibal(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class Necrochancellor : Unit {
		public Necrochancellor(Source s, bool template=false){
			NewLabel(TTYPE.NECR, s, false, template);
			BuildEth();
			
			NewHealth(30,5);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ANecrTeleport(new Price(0,1), this, 5));
			arsenal.Add(new ANecrTouch(Price.Cheap, this, Aim.Melee(), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class MouthOfTheUnderworld : Unit {
		public MouthOfTheUnderworld(Source s, bool template=false){
			NewLabel(TTYPE.MOUT, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(1)));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class Monolith : Unit {
		public Monolith(Source s, bool template=false){
			NewLabel(TTYPE.MONO, s, true, template);
			sprite = new HOASprite(this);
			BuildTall();
			AddKing();
			OnDeath = TTYPE.HBLO;
			
			NewHealth(100);
			NewWatch(2);
			
			NewArsenal();
			arsenal.Add(new AFocus(this));
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new ARage(Price.Cheap, this, Aim.Melee(), 20));
			
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 2);
			arsenal.Add(new AMonoFlame(new Price(2,2), this, fireAim, 20));
			
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.RECY));
			arsenal.Add(new AMonoReanimate(Price.Cheap, this, TTYPE.RECY));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.NECR));
			arsenal.Add(new ACreate(new Price(1,2), this, TTYPE.MOUT));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}