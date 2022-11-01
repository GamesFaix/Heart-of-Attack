using UnityEngine;
using System;
using System.Collections.Generic;
using Tokens;

public class Unit : Token{

	Label label;
	Body body;

	Tokens.Resources resources;
	Health health;
	Tokens.Clock clock;
	
	public Unit(string code){
		label = new Label(this, code);
		resources = new Tokens.Resources(this);
		UnitConstructor.Make(this,label.Code());

	}
	//label
	public override string ToString() {return label.FullName();}
	public string FullName() {return label.FullName();}
	public string Code() {return label.Code();}
	public string Name() {return label.Name();}
	public char Instance() {return label.Instance();}
	public void SetOwner(int n, bool log=true){label.SetOwner(n, log);}
	public int Owner() {return label.Owner();}
	//body
	public void SetPlane(PLANE p, bool log=true) {body.SetPlane(p,log);}
	public void SetPlane(PLANE[] ps, bool log=true) {body.SetPlane(ps,log);}
	public string PlaneString() {return body.PlaneString();}
	public void SetSpecial(SPECIAL s, bool log=true) {body.SetSpecial(s,log);}
	public void SetSpecial(SPECIAL[] ss, bool log=true) {body.SetSpecial(ss,log);}
	public bool IsSpecial(SPECIAL s) {return body.IsSpecial(s);}
	public string SpecialString() {return body.SpecialString();}
	public void SetOnDeath(string code, bool log=true) {body.SetOnDeath(code,log);}
	public string OnDeath() {return body.OnDeath();}
	public void NewBody(PLANE p, SPECIAL s=SPECIAL.NONE){body = new Body(this,p,s);}
	public void NewBody(PLANE[] p, SPECIAL s=SPECIAL.NONE){body = new Body(this,p,s);}
	public void NewBody(PLANE p, SPECIAL[] s){body = new Body(this,p,s);}
	public void NewBody(PLANE[] p, SPECIAL[] s){body = new Body(this,p,s);}

	//resources
	public int AP() {return resources.AP();}
	public int FP() {return resources.FP();}
	public int MaxAP() {return resources.MaxAP();}
	public int SetMaxAP(int n, bool log=true) {return resources.SetMaxAP(n,log);}
	public int SetAP(int n, bool log=true){return resources.SetAP(n,log);}
	public int AddAP(int n){return resources.AddAP(n);}
	public int SetFP(int n, bool log=true){return resources.SetFP(n,log);}
	public int AddFP(int n){return resources.AddFP(n);}
	public void FillAP(bool log=true){resources.FillAP(log);}
	public string APString(){return resources.APString();}
	public string FPString(){return resources.FPString();}
	//health
	public void NewHealth(int n, int d=0) {health = new Health(this,n,d);}
	public int HP() {return health.HP();}
	public int MaxHP() {return health.MaxHP();}
	public int DEF() {return health.DEF();}
	public int SetHP(int n, bool log=true) {return health.SetHP(n, log);}
	public int SetMaxHP(int n, bool log=true) {return health.SetMaxHP(n, log);}
	public int SetDEF(int n, bool log=true) {return health.SetDEF(n, log);}
	public int AddHP(int n, bool log=true) {return health.AddHP(n, log);}
	public int AddMaxHP(int n, bool log=true) {return health.AddMaxHP(n,log);}
	public int AddDEF(int n, bool log=true) {return health.AddDEF(n, log);}
	public int Damage(int n, bool log=true) {return health.Damage(n, log);}
	public int MultHP(float f, bool log=true) {return health.MultHP(f, log);}
	public int MultMaxHP(float f, bool log=true) {return health.MultMaxHP(f, log);}
	public string HPString() {return health.HPString();}
	public string DEFString() {return health.DEFString();}
	public void FillHealth() {health.Fill();}
	//clock
	public void NewClock(int n) {clock = new Clock(this,n);}
	public int IN() {return clock.IN();}
	public int Stunned() {return clock.Stunned();}
	public bool IsSkipped() {return clock.IsSkipped();}
	public bool IsStunned() {return clock.IsStunned();}
	public bool IsCOR() {return clock.IsCOR();}
	public int COR() {return clock.COR();}
	public int SetIN(int n, bool log=true) {return clock.SetIN(n, log);}
	public int AddIN(int n, bool log=true) {return clock.AddIN(n, log);}
	public int SetStun(int n, bool log=true) {return clock.SetStun(n, log);}
	public int AddStun(int n, bool log=true) {return clock.AddStun(n,log);}
	public void Skip (bool log=true) {clock.Skip(log);}
	public void ClearSkip (bool log=true) {clock.ClearSkip(log);}
	public int SetCOR(int n, bool log=true) {return clock.SetCOR(n,log);}
	public int AddCOR(int n, bool log=true) {return clock.AddCOR(n,log);}
	public int DecayCOR(bool log=true) {return clock.DecayCOR(log);}





	

	
	
}
