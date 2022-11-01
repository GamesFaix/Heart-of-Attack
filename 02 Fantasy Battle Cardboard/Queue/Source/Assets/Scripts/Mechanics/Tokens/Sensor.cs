using UnityEngine;
using System.Collections;

namespace Tokens {
	public class Sensor {
		Unit parent;

		public Sensor(Unit u){
			parent = u;
		}
	}
}