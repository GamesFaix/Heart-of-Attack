using UnityEngine;

namespace HOA {
	
	public class TipTimer : Tip{
		
		public TipTimer () {
			Name = "Timer";
			Icon = Icons.TIMER();
			ETip = ETip.TIMER;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Timers trigger effects in the future, " +
			          "\nsuch as at the end of a Unit's turn." +
			          "\n\n",
			          
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipUnit();
			tip.Link(p.LinePanel);

			tip = new TipSensor();
			tip.Link(p.LinePanel);

		}
		
	}
	
}
