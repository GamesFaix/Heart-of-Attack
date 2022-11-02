using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public static class TemplateFactory {
	
		static Dictionary<EToken, Token> templates = new Dictionary<EToken, Token>();
		public static Cell c = new Cell (-1,-1);
		public static Player p = new Player ("TEMPLATE", false);
		static Source s = new Source(p);	
		
		public static void MakeTemplates () {
			
			foreach (EToken code in (EToken[])Enum.GetValues(typeof(EToken))) {
				if (code != EToken.NONE) {
					Token template = Make(code);
		//			Debug.Log(template);
					template.BuildTemplate();
					templates.Add(code, template);
				}
			}		
		}
		
		public static Token Template (EToken code) {
			return templates[code];
		}
		
		public static Token Template (Token t) {
			return templates[t.Code];
		}
	
		
		static Token Make (EToken code){
			switch(code){
				case EToken.KATA: return new Katandroid(s,true);
				case EToken.CARA: return new CarapaceInvader(s,true);
				case EToken.MAWT: return new Mawth(s,true);
				case EToken.KABU: return new Kabutomachine(s,true);
				case EToken.HSIL: return new SiliconHOA(s,true);
		
				case EToken.DEMO: return new Demolitia(s,true);
				case EToken.MEIN: return new MeinSchutz(s,true);
				case EToken.MINE: return new Mine(s,true);
				case EToken.PANO: return new Panopticannon(s,true);
				case EToken.DECI: return new Decimatrix(s,true);
				case EToken.HSTE: return new SteelHOA(s,true);
	
				case EToken.ROOK: return new Rook(s,true);
				case EToken.SMAS: return new Smashbuckler(s,true);
				case EToken.CONF: return new Conflagragon(s,true);
				case EToken.ASHE: return new Ashes(s,true);	
				case EToken.BATT: return new BatteringRambuchet(s,true);
				case EToken.GARG: return new Gargoliath(s,true);	
				case EToken.HSTO: return new StoneHOA(s,true);
	
				case EToken.GRIZ: return new GrizzlyElder(s,true);
				case EToken.TALO: return new TalonedScout(s,true);
				case EToken.META: return new Metaterrainean(s,true);
				case EToken.ULTR: return new Ultratherium(s,true);
				case EToken.HFIR: return new FirHOA(s,true);
	
				case EToken.REVO: return new RevolvingTom(s,true);
				case EToken.PIEC: return new Piecemaker(s,true);
				case EToken.APER: return new Aperture(s,true);
				case EToken.REPR: return new Reprospector(s,true);
				case EToken.OLDT: return new OldThreeHands(s,true);
				case EToken.HBRA: return new BrassHOA(s,true);
	
				case EToken.LICH: return new Lichenthrope(s,true);
				case EToken.BEES: return new Beesassin(s,true);
				case EToken.MYCO: return new Mycolonist(s,true);
				case EToken.MART: return new MartianManTrap(s,true);
				case EToken.BLAC: return new BlackWinnow(s,true);
				case EToken.WEBB: return new Web(s,true);
				case EToken.HSLK: return new SilkHOA(s,true);
	
				case EToken.PRIS: return new PrismGuard(s,true);
				case EToken.AREN: return new ArenaNonSensus(s,true);
				case EToken.PRIE: return new PriestOfNaja(s,true);
				case EToken.DREA: return new DreamReaver(s,true);
				case EToken.HGLA: return new GlassHOA(s,true);
	
				case EToken.RECY: return new Recyclops(s,true);
				case EToken.NECR: return new Necrochancellor(s,true);
				case EToken.MOUT: return new MouthOfTheUnderworld(s,true);
				case EToken.MONO: return new Monolith(s,true);
				case EToken.HBLO: return new BloodHOA(s,true);
	
				case EToken.MNTN: return new Mountain(s,true);
				case EToken.HILL: return new Hill(s,true);
				case EToken.ROCK: return new Rock(s,true);
				case EToken.TREE: return new HOA.Tree(s,true);
				case EToken.CORP: return new Corpse(s,true);
				case EToken.WATR: return new Water(s,true);
				case EToken.LAVA: return new Lava(s,true);
	
				default: return default(Token);
			}
		}
	}
}
