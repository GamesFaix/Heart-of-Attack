namespace HOA {

	public class RCreate : RCellSelect{
		public EToken token;
		public RCreate (Source s, EToken t, Cell c) {source = s; token = t; cell = c;}

		public override void Grant () {
			Token newToken;
			TokenFactory.Add(token, source, cell, out newToken);
			newToken.SpriteEffect(EEffect.BIRTH);
			Reset();
		}
	}

	public class RKill : RInstanceSelect{
		public RKill (Source s, Token t) {source = s; instance = t;}

		public override void Grant () {
			instance.Die(source);
			Reset();
		}
	}

	public class RReplace : RInstanceSelect{
		public EToken token;
		public RReplace (Source s, Token t, EToken newT) {source = s; instance = t; token = newT;}

		public override void Grant () {
			Cell cell = instance.Cell;
			instance.Die(source, false, false);
			TokenFactory.Add(token, source, cell, false);
			Reset();
		}
	}
}