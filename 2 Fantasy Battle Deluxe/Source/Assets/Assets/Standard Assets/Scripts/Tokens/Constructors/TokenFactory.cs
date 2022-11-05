using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FBI.Map;

namespace FBI.Tokens {
	
	public delegate void TokenConstruct(Token token);

	public static class TokenFactory {
		
		static GameObject tokenPF;
		
		public static void Start(){
			tokenPF = Resources.Load("Prefabs/TokenPF") as GameObject;
			LoadConstructors();
		}
	
		public static Token Create(int owner, TokenValue t, GamePoint location){
			GameObject tokenGO = GameObject.Instantiate (tokenPF, location.ToWorldPoint(), Quaternion.identity) as GameObject;		
			Token token = tokenGO.GetComponent("Token") as Token;
			token.owner = owner;
			
			token.tokenValue = t;
			//invoke constructor for TokenValue t
			TokenConstruct constructor = constructors[(int)t];
			constructor(token);
			
			return token;
		}
		
		
		static TokenConstruct[] constructors;
		
		static void LoadConstructors(){
			constructors = new TokenConstruct[50];
			
			constructors[(int)TokenValue.NINJOID] 	   = TBuildGearp.Ninjoid;
			constructors[(int)TokenValue.SENTINEL]	   = TBuildGearp.Sentinel;
			constructors[(int)TokenValue.MOTH]		   = TBuildGearp.Moth;
			constructors[(int)TokenValue.STAGBOT]	   = TBuildGearp.Stagbot;
			/*
			constructors[(int)TokenValue.DEMOLITIA]    = TConstructNewRe.Demolitia;
			constructors[(int)TokenValue.CONDOR]	   = TConstructNewRe.Condor;
			constructors[(int)TokenValue.MINE] 		   = TConstructNewRe.Mine;
			constructors[(int)TokenValue.PANOPTICLOPS] = TConstructNewRe.Panopticlops;
			constructors[(int)TokenValue.ROBOTANK] 	   = TConstructNewRe.Robotank;
			
			constructors[(int)TokenValue.MOURNKING]	   = TConstructTorr.Mournking;
			constructors[(int)TokenValue.PHOENIX]      = TConstructTorr.Phoenix;
			constructors[(int)TokenValue.PHOENIXASH]   = TConstructTorr.PhoenixAsh;
			constructors[(int)TokenValue.RAMBUCHET]    = TConstructTorr.Rambuchet;
			constructors[(int)TokenValue.DRAGON] 	   = TConstructTorr.Dragon;
			
			constructors[(int)TokenValue.GRIZZLY]	   = TConstructGrove.Grizzly;
			constructors[(int)TokenValue.OWL] 		   = TConstructGrove.Owl;
			constructors[(int)TokenValue.GOLEM] 	   = TConstructGrove.Golem;
			constructors[(int)TokenValue.YETI] 		   = TConstructGrove.Yeti;
			
			constructors[(int)TokenValue.GUNSLINGER]   = TConstructChrono.Gunslinger;
			constructors[(int)TokenValue.PIECEMAKER]   = TConstructChrono.Piecemaker;
			constructors[(int)TokenValue.PORTAL] 	   = TConstructChrono.Portal;
			constructors[(int)TokenValue.CHIEF] 	   = TConstructChrono.Chief;
			constructors[(int)TokenValue.GRANDDAD] 	   = TConstructChrono.Granddad;
			
			constructors[(int)TokenValue.LARVA] 	   = TConstructPsycho.Larva;
			constructors[(int)TokenValue.BEE] 		   = TConstructPsycho.Bee;
			constructors[(int)TokenValue.SHROOMAN] 	   = TConstructPsycho.Shrooman;
			constructors[(int)TokenValue.PLANT] 	   = TConstructPsycho.Plant;
			constructors[(int)TokenValue.SPIDER] 	   = TConstructPsycho.Spider;
			
			constructors[(int)TokenValue.GLASSMAN] 	   = TConstructPsi.GlassMan;
			constructors[(int)TokenValue.HAZE] 		   = TConstructPsi.Haze;
			constructors[(int)TokenValue.PRIEST] 	   = TConstructPsi.Priest;
			constructors[(int)TokenValue.SULTAN] 	   = TConstructPsi.Sultan;
			
			constructors[(int)TokenValue.FIEND] 	   = TConstructVoid.Fiend;
			constructors[(int)TokenValue.CHANCELLOR]   = TConstructVoid.Chancellor;
			constructors[(int)TokenValue.HELLGATE]     = TConstructVoid.HellGate;
			constructors[(int)TokenValue.MAGMAN] 	   = TConstructVoid.Magman;
			constructors[(int)TokenValue.MONOLITH]     = TConstructVoid.Monolith;
			*/
			
		}


	}
}
