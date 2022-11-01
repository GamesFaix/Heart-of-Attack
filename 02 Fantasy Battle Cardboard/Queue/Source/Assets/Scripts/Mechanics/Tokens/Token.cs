using UnityEngine;
using System.Collections;

using Tokens;

public interface Token {
	//label
	void NewLabel(TTYPE code);
	void SetOwner(int n, bool log=true);

	TTYPE Code();
	string Name();
	char Instance();
	string FullName();
	string ToString();
	int Owner();

	//body
	void NewBody (PLANE p, SPECIAL s=SPECIAL.NONE);
	void NewBody (PLANE[] p, SPECIAL s=SPECIAL.NONE);
	void NewBody (PLANE p, SPECIAL[] s);
	void NewBody (PLANE[] p, SPECIAL[] s);

	void SetPlane (PLANE p, bool log=true);
	void SetPlane (PLANE[] ps, bool log=true);
	bool IsPlane (PLANE p);
	string PlaneString ();

	void SetSpecial (SPECIAL s, bool log=true);
	void SetSpecial (SPECIAL[] ss, bool log=true);
	bool IsSpecial (SPECIAL s);
	string SpecialString ();

	void SetOnDeath (TTYPE code, bool log=true);
	TTYPE OnDeath ();




}
