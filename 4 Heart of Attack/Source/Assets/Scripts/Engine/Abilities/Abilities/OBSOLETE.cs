/*
	public class APiecAper : Ability {
		public override string Desc {get {return "Create Aperture in Target cell.";} }
		public override Token Template {get {return TemplateFactory.Template(Species.Aperture);} }
		
		public APieceAper (Unit par) {
			Name = "Open Aperture";
			Weight = 4;
			Parent = par;
			Price = new Price(1,1);
			Aims.Add(HOA.Aim.CreateArc(3));
		}
		
		protected override void ExecuteMain (TargetGroup Targets) {
			Cell c = (Cell)Targets[0];
			
			EffectQueue.Add(new ECreate(new Source(Parent), Species.Aperture, c));
			
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			Price.Draw(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			
			Aims[0].Draw(p.LinePanel);
			Template.DisplayThumbNameTemplate(p.LinePanel);
			float descH = (p.H-(p.LineH*2))/p.H;
			GUI.Label(p.TallWideBox(descH), Desc);	
		}
	}
	*/

	/*
	public class AMonoFlame : Ability {
		int damage;
		
		public AMonoFlame (Unit u) {
			weight = 4;
			Price = new Price(1,2);
			Parent = u;
			
			Aims.Add(new Aim (ETraj.LINE, new List<TargetClasses> {TargetClasses.Unit, TargetClasses.Dest}, 2));
			damage = 20;
			
			name = "Eternal Flame";
			desc = "Do "+damage+" damage to Target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (TargetGroup Targets) {
			Charge();
			Token tar = (Token)Targets[0];

			TargetGroup affected = new TargetGroup(Parent);
			TargetGroup thisRad = new TargetGroup(tar);
			TargetGroup nextRad = new TargetGroup();
			
			int dmg = damage;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.Display.Effect(AVEffects.FIRE);
						AEffects.DamageDest(new Source(Parent), next, dmg);
						
						foreach (Token t in next.Neighbors(true)) {
							nextRad.Add(t);
						}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new TargetGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
				Targeter.Reset();
			}

		}
	}
	*/
