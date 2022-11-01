using UnityEngine;
using System.Collections;

public struct Ratio {
	int n;
	int d;

	public Ratio (int num, int dem){
		n = num;
		d = dem;
		Clamp();
	}

	public void Fill(){
		n = d;
	}

	void Clamp(){
		if (n>d){n=d;}
	}

	public void Set(int x, bool num=true){
		if (num) {n=x;}
		else {d=x;}
		Clamp();
	}

	public void Add(int x, bool num=true){
		if (num) {n+=x;}
		else {n+=x;}
		Clamp();
	}
	public void Mult(float x, bool num=true){
		if (num) {n = (int)Mathf.Ceil(n*x);}
		else {d = (int)Mathf.Ceil(d*x);}
		Clamp();
	}
}
