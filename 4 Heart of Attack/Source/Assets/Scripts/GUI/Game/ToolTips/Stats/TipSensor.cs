using UnityEngine;

namespace HOA {
	
	public class TipSensor : Tip{
		
		public TipSensor () {
			Name = "Sensor";
			Icon = Icons.SENSOR();
			ETip = ETip.SENSOR;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Sensors are triggers that cause effects" +
			          "\nwhen Tokens enter, exit, or stay in their" +
			          "\nCell." +
			          "\n\n",
			          
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipToken();
			tip.Link(p.LinePanel);

			tip = new TipTimer();
			tip.Link(p.LinePanel);
		}
		
	}
	
}
