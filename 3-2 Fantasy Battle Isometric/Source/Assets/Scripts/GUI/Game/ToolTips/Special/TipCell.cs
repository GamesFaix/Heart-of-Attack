﻿using UnityEngine;

namespace HOA {
	
	public class TipCell : Tip{
		
		public TipCell () {
			Name = "Cell";
			Icon = Icons.Special(EType.CELL);
			ETip = ETip.CELL;
		}
		
		public override void Content (Panel p) {
			p.NudgeX();
			GUI.Label(p.TallBox(5), 
			          "",
			          p.s);
		}	
		public override void SeeAlso (Panel p) {
			Tip tip = new TipPlane();
			tip.Link(p.LinePanel);
		}
	}
	
}
