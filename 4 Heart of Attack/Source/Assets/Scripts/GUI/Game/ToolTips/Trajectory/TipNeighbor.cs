using UnityEngine;

using HOA.Textures;

namespace HOA {
	
	public class TipNeighbor : Tip{
		
		public TipNeighbor () {
			Name = "Neighbor";
			Icon = Icons.Trajectories[ETraj.NEIGHBOR];
			ETip = ETip.NEIGHBOR;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), 
			          "Actions Targeting Neighbor Cells' may " +
			          "\nTarget any Cell touching the performing " +
			          "\nUnit's Cell, or the performing Unit's Cell." +

			          "\n\nActions Targeting Neighbor Units' may " +
			          "\nTarget any Unit in a Cell touching the " +
			          "\nperforming Unit's Cell, or the performing " +
			          "\nUnit's Cell.",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipArc();
			tip.Link(p.LinePanel);

			tip = new TipLine();
			tip.Link(p.LinePanel);
			
			tip = new TipPath();
			tip.Link(p.LinePanel);
			
			tip = new TipFree();
			tip.Link(p.LinePanel);
			
			tip = new TipSelf();
			tip.Link(p.LinePanel);
		}
	}
	
}
