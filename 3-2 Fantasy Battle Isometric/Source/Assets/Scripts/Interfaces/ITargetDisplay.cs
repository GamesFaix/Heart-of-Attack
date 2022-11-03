using UnityEngine;

namespace HOA {

	public interface ITargetDisplay {

		ITarget Target();
		GameObject GO();

		void Effect(EEffect e);

	}

}