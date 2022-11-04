using UnityEngine;

namespace HOA {
	
	public class TipPlane : Tip{
		
		public TipPlane () {
			Name = "Plane";
			Icon = null;
			ETip = ETip.PLANE;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.Box(0.9f), "A token can occupy one, or several, " +
				"\nof four planes." +
			    "\nTwo tokens occupying the same Plane " +
			    "\ncannot occupy the same Cell.", p.s);
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NextLine();

			p.NudgeX();
			GUI.Box(p.IconBox, Icons.Planes[Plane.Sunken], p.s);
			p.NudgeX();
			GUI.Label(p.Box(0.5f), 
				"Sunken" +
				"\nThe Sunken Plane is typically reserved " +
			    "\nfor trap type Obstacles, such as Water, " +
			    "\nLava, or Mines.",
			          p.s);
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NextLine();

			p.NudgeX();
			GUI.Box(p.IconBox, Icons.Planes[Plane.Ground], p.s);
			p.NudgeX();
			GUI.Label(p.Box(0.5f), 
			          "Ground" +
			          "\nThe Ground Plane is where most Units " +
			          "\nand Obstacles reside.", 
			          p.s);
			p.NextLine();
			p.NextLine();
			p.NextLine();

			p.NudgeX();
			GUI.Box(p.IconBox, Icons.Planes[Plane.Air], p.s);
			p.NudgeX();
			GUI.Label(p.Box(0.5f), 
			          "Air" +
			          "\nThe Air Plane is the home of flying " +
			          "\nmachines, winged beasts, and " +
			          "\noccasionally tall Obstacles, such as " +
			          "\nMountains, which occupy the Ground " +
			          "\nand Air Plane.", 
			          p.s);
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NextLine();
			p.NextLine();

			p.NudgeX();
			GUI.Box(p.IconBox, Icons.Planes[Plane.Ethereal], p.s);
			p.NudgeX();
			GUI.Label(p.Box(0.5f), 
			          "Ethereal" +
			          "\nThe Etheral Plane is rarely occupied, " +
			          "\nonly by ghostly or illusionary Units.", 
			          p.s);
			p.NextLine();
			p.NextLine();
			p.NextLine();

		}	
		public override void SeeAlso (Panel p) {
			p.NextLine();
			p.NextLine();
			p.NextLine();

			Tip tip = new TipToken();
			tip.Link(p.LinePanel);
			tip = new TipCell();
			tip.Link(p.LinePanel);
		}
	}
	
}
