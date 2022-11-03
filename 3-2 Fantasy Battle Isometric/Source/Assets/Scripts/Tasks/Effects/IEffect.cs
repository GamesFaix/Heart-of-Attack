using UnityEngine; 

namespace HOA { 

	//Allows Effects and EffectGroups to be processed analogously by EffectQueue.

	public interface IEffect {
		void Process();
	}
}
