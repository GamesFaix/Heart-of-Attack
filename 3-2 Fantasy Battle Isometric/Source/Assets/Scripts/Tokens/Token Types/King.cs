using UnityEngine; 

namespace HOA { 

	public class King : Unit {

		protected King () {
			Wallet = new Wallet(this, 3);
		}

		public override string Notes () {return "";}

	}
}
