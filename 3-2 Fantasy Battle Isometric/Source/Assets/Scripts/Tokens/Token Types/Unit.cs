using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Unit : Token{
	
		public Health Health {get; protected set;}
		public void SetHealth (Health health) {Health = health;}
		public Wallet Wallet {get; protected set;}
		public Watch Watch {get; protected set;}
		public Arsenal Arsenal {get; protected set;}
		
		protected void NewHealth (byte h=0, byte d=0) {Health = new Health(this, h, d);}
		protected void NewWallet (byte maxAP=2) {Wallet = new Wallet(this, maxAP);}
		protected void NewWatch (byte init=0) {Watch = new Watch(this, init);}

		public Unit () {
			Body = new Body(this);
			OnDeath = EToken.CORP;
			NewWallet();
		}

		protected virtual void BuildArsenal () {
			Arsenal = new Arsenal(this);
			Arsenal.Add(new Actions.Focus(this));
		}

		public int SetStat (Source s, EStat stat, int n, bool log=true) {
			checked {
				switch (stat) {
					case EStat.HP: return Health.HP.Set(n);
					case EStat.MHP: return Health.HP.SetMax((byte)n);
					case EStat.DEF: return Health.DEF.Set(n);
					case EStat.IN: return Watch.IN.Set(n);
					case EStat.AP: return Wallet.AP.Set(n);
					case EStat.FP: return Wallet.FP.Set(n);
					case EStat.STUN: return Watch.STUN.Set(n);
					default: return -1;
				}
			}
		}

		public int AddStat (Source s, EStat stat, int n, bool log=true) {
			checked {
				switch (stat) {
					case EStat.HP: return Health.HP.Add(s, n, log);
					case EStat.MHP: return Health.HP.AddMax(s, (byte)n, log);
					case EStat.DEF: return Health.DEF.Add(s, n, log);
					case EStat.IN: return Watch.IN.Add(s, n, log);
					case EStat.AP: return Wallet.AP.Add(s, n, log);
					case EStat.FP: return Wallet.FP.Add(s, n, log);
					case EStat.STUN: return Watch.STUN.Add(s, n, log);
					default: return -1;
				}
			}
		}
		
		//health
		public bool Damage(Source s, int n, bool log=true) {return Health.Damage(s, n, log);}
		public int HP {get {return Health.HP;} }
		public int MaxHP {get {return Health.HP.Max;} }
		public int DEF {get {return Health.DEF;} }

		//wallet
		public int AP {get {return Wallet.AP;} }
		public void FillAP (bool log=true) {Wallet.FillAP(log);}
		public int FP {get {return Wallet.FP;} }
		public int IN {get {return Watch.IN;} }

		//watch
		public int STUN {get {return Watch.STUN;} }
		public bool IsStunned() {return Watch.IsStunned();}
	}
}