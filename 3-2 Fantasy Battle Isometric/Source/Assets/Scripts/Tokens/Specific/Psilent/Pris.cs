namespace HOA{
	public class PrismGuard : Unit {
		public PrismGuard(Source s, bool template=false){
			ID = new ID(this, EToken.PRIS, s, false, template);
			Plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(15);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new AStrike(this, 8),
				new APrisRefract(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return 
			"Actions targetting "+ID.Name+" have a 50% chance of missing.";
		}
		
		public override void Select (Source s) {
			int flip = DiceCoin.Throw(s, EDice.COIN);
			if (flip == 1) {
				Display.Effect(EEffect.HEADS);
				GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to target "+ToString()+" and missed.");
				EffectQueue.Add(new ETails(new Source(this), this));
			}
		}
		
		
	}
}