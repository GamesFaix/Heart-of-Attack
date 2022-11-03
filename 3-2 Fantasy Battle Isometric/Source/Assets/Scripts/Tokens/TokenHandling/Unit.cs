using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Unit : Token{
	
		public Health health;
		protected Wallet wallet;
		protected Watch watch;
		protected Arsenal arsenal;
		
		protected void NewHealth (int h=0, int d=0) {health = new Health(this, h, d);}
		protected void NewWallet (int f=2) {wallet = new Wallet(this, f);}
		protected void NewWatch (int i=0) {watch = new Watch(this, i);}

		public Unit () {
			body = new Body(this);
			type = new Special(EType.UNIT);
			onDeath = EToken.CORP;
			NewWallet();
		}

		protected virtual void BuildArsenal () {
			arsenal = new Arsenal(this);
			arsenal.Add(new AFocus(this));
		}


		public int SetStat (Source s, EStat stat, int n, bool log=true) {
			switch (stat) {
				case EStat.HP:
					return health.HP = n;
				case EStat.MHP:
					return health.MaxHP = n;
				case EStat.DEF:
					return health.DEF = n;
				case EStat.IN:
					return watch.IN = n;
				case EStat.AP:
					return wallet.AP = n;
				case EStat.FP:
					return wallet.FP = n;
				case EStat.STUN:
					return watch.STUN = n;
				case EStat.COR:
					return watch.COR = n;
				default:
					return -1;
			}
		}

		public int AddStat (Source s, EStat stat, int n, bool log=true) {
			switch (stat) {
				case EStat.HP:
					return health.AddHP(s, n, log);
				case EStat.MHP:
					return health.AddMaxHP(s, n, log);
				case EStat.DEF:
					return health.AddDEF(s, n, log);
				case EStat.IN:
					return watch.AddIN(s, n, log);
				case EStat.AP:
					return wallet.AddAP(s, n, log);
				case EStat.FP:
					return wallet.AddFP(s, n, log);
				case EStat.STUN:
					return watch.AddSTUN(s, n, log);
				case EStat.COR:
					return watch.AddCOR(s, n, log);
				default:
					return -1;
			}
		}
		
		//health
		public int Damage(Source s, int n, bool log=true) {return health.Damage(s, n, log);}
		public void FillHealth() {health.Fill();}
		//hp
		public int HP {get {return health.HP;} }
		public int MaxHP {get {return health.MaxHP;} }
		public int MultHP(Source s, float f, bool log=true) {return health.MultHP(s, f, log);}
		public int MultMaxHP(Source s, float f, bool log=true) {return health.MultMaxHP(s, f, log);}
		public string HPString {get {return health.HPString;} }
		//def
		public int DEF {get {return health.DEF;} }
		public string DEFString {get {return health.DEFString;} }
		
		//wallet
		public bool CanAfford (Price p) {return wallet.CanAfford(p);}
		public void Charge (Price p) {wallet.Charge(p);}
		public void Refund (Price p) {wallet.Refund(p);}
		//ap
		public int AP {
			get {return wallet.AP;}
			set {wallet.AP = value;}
		}
		public int MaxAP {
			get {return wallet.MaxAP;}
			set {wallet.MaxAP = value;}
		}
		public void FillAP(bool log=true){wallet.FillAP(log);}
		public string APString {get {return wallet.APString;} }
		//fp
		public int FP {
			get {return wallet.FP;} 
			set {wallet.FP = value;}
		}
		public string FPString {get {return wallet.FPString;} }
				
		//in
		public int IN { 
			get {return watch.IN;}
			set {watch.IN = value;}
		}
		//skip
		public bool IsSkipped() {return watch.IsSkipped();}
		public void Skip (bool log=true) {watch.Skip(log);}
		public void ClearSkip (bool log=true) {watch.ClearSkip(log);}
		//stun
		public int STUN {
			get {return watch.STUN;}
			set {watch.STUN = value;}
		}
		public bool IsStunned() {return watch.IsStunned();}

		//corrosion
		public bool IsCOR() {return watch.IsCOR();}
		public int COR {
			get {return watch.COR;}
			set {watch.COR = value;}
		}
		public int DecayCOR(bool log=true) {return watch.DecayCOR(log);}
		
		//arsenal
		public Arsenal Arsenal () {return arsenal;}
		public void ResetArsenal () {arsenal.Reset();}
		
	}
}