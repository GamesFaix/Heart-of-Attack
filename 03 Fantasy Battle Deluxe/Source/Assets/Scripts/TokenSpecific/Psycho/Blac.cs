
namespace HOA{
	public class BlackWinnow : Unit {
		public BlackWinnow(Source s, bool template=false){
			NewLabel(TTYPE.BLAC, s, true, template);
			BuildGround();
			AddKing();
			OnDeath = TTYPE.HSLK;
			
			NewHealth(75);
			NewWatch(3); 
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ACorrode("Bite", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new ACreate(new Price(0,0), this, TTYPE.LICH));
			Aim webAim = new Aim (AIMTYPE.ARC, TARGET.CELL, CTAR.CREATE, 3);
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.WEBB, webAim));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}