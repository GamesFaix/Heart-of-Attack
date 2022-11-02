using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	public static class InputBuffer {
		
		public static void DirectConsoleInput (string text) {
		//	Main.Submit(text);
		}

		static List<Request> requestQueue = new List<Request>();

		public static void Submit (Request r) {
			requestQueue.Add(r);
			//Debug.Log(r);
			
			r.Grant();
		}





	}

}