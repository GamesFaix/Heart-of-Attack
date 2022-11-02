using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Map;
using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;

namespace HOA.Tokens {
	public enum STAT {HP, MHP, DEF, IN, AP, FP, STUN, COR}

	public abstract class Unit : Token{
	
		public Health health;
		protected Wallet wallet;
		protected Watch watch;
		protected Arsenal arsenal;
		
		protected void NewHealth (int h=0, int d=0) {health = new Health(this, h, d);}
		protected void NewWallet (int f=2) {wallet = new Wallet(this, f);}
		protected void NewWatch (int i=0) {watch = new Watch(this, i);}
		protected void NewArsenal () {arsenal = new Arsenal(this);}
		
		protected void BuildStandard () {
			sprite = new HOASprite(this);
			SetOnDeath(TTYPE.CORP, false);
			NewWallet();
			NewArsenal();
			arsenal.Add(new AFocus(this));
		}
		protected void AddCorpseless () {
			SetOnDeath(TTYPE.NONE);
		}
		protected void BuildGround () {
			NewBody(PLANE.GND);
			BuildStandard();
		}
		protected void BuildAir () {
			NewBody(PLANE.AIR);
			BuildStandard();
		}
		protected void BuildEth () {
			NewBody(PLANE.ETH);
			BuildStandard();
			AddCorpseless();
		}		
		protected void BuildTrample () {
			NewBody(PLANE.GND, SPECIAL.TRAM);
			BuildStandard();
		}
		protected void BuildTall () {
			NewBody(new PLANE[] {PLANE.GND, PLANE.AIR});
		}
		protected void AddKing () {
			AddSpecial(SPECIAL.KING);
			NewWallet(3);
		}
		protected void AddRem () {
			AddSpecial(SPECIAL.DEST);
			AddSpecial(SPECIAL.REM);
		}
		
		public int SetStat (Source s, STAT stat, int n, bool log=true) {
			switch (stat) {
				case STAT.HP:
					return health.SetHP(s, n, log);
				case STAT.MHP:
					return health.SetMaxHP(s, n, log);
				case STAT.DEF:
					return health.SetDEF(s, n, log);
				case STAT.IN:
					return watch.SetIN(s, n, log);
				case STAT.AP:
					return wallet.SetAP(s, n, log);
				case STAT.FP:
					return wallet.SetFP(s, n, log);
				case STAT.STUN:
					return watch.SetSTUN(s, n, log);
				case STAT.COR:
					return watch.SetCOR(s, n, log);
				default:
					return -1;
			}
		}

		public int AddStat (Source s, STAT stat, int n, bool log=true) {
			switch (stat) {
				case STAT.HP:
					return health.AddHP(s, n, log);
				case STAT.MHP:
					return health.AddMaxHP(s, n, log);
				case STAT.DEF:
					return health.AddDEF(s, n, log);
				case STAT.IN:
					return watch.AddIN(s, n, log);
				case STAT.AP:
					return wallet.AddAP(s, n, log);
				case STAT.FP:
					return wallet.AddFP(s, n, log);
				case STAT.STUN:
					return watch.AddSTUN(s, n, log);
				case STAT.COR:
					return watch.AddCOR(s, n, log);
				default:
					return -1;
			}
		}
		
		//health
		public int Damage(Source s, int n, bool log=true) {return health.Damage(s, n, log);}
		public void FillHealth() {health.Fill();}
		//hp
		public int HP() {return health.HP();}
		public int MaxHP() {return health.MaxHP();}
		public int MultHP(Source s, float f, bool log=true) {return health.MultHP(s, f, log);}
		public int MultMaxHP(Source s, float f, bool log=true) {return health.MultMaxHP(s, f, log);}
		public string HPString() {return health.HPString();}
		//def
		public int DEF() {return health.DEF();}
		public string DEFString() {return health.DEFString();}
		
		//wallet
		public bool CanAfford (Price p) {return wallet.CanAfford(p);}
		public void Charge (Price p) {wallet.Charge(p);}
		public void Refund (Price p) {wallet.Refund(p);}
		//ap
		public int AP() {return wallet.AP();}
		public int MaxAP() {return wallet.MaxAP();}
		public int SetMaxAP(Source s, int n, bool log=true) {return wallet.SetMaxAP(s, n,log);}
		public void FillAP(bool log=true){wallet.FillAP(log);}
		public string APString(){return wallet.APString();}
		//fp
		public int FP() {return wallet.FP();}
		public string FPString(){return wallet.FPString();}
				
		//in
		public int IN() {return watch.IN();}
		//skip
		public bool IsSkipped() {return watch.IsSkipped();}
		public void Skip (bool log=true) {watch.Skip(log);}
		public void ClearSkip (bool log=true) {watch.ClearSkip(log);}
		//stun
		public int Stunned() {return watch.Stunned();}
		public bool IsStunned() {return watch.IsStunned();}
		//corrosion
		public bool IsCOR() {return watch.IsCOR();}
		public int COR() {return watch.COR();}
		public int DecayCOR(bool log=true) {return watch.DecayCOR(log);}
		
		//arsenal
		public Arsenal Arsenal () {return arsenal;}
		public void Move () {arsenal.Move();}
		public void Focus () {arsenal.Focus();}
		public void ResetArsenal () {arsenal.Reset();}
		
	}
}