using System.Collections.Generic;

namespace HOA {
	public enum ELog {IN, OUT, IO, DEBUG};

	public static class GameLog {

		static List<string> ioLog = new List<string>();
		static List<string> inLog = new List<string>();
		static List<string> outLog = new List<string>();
		static List<string> debug = new List<string>();

		static void Add(string text, ELog io){
			if (io == ELog.OUT) {outLog.Add(text); ioLog.Add(text); debug.Add(text);}
			if (io == ELog.IN) {inLog.Add(text); ioLog.Add(text); debug.Add(text);}
			if (io == ELog.IO) {Add("GameLog: Event added to IO log: "+text, ELog.DEBUG);}
			if (io == ELog.DEBUG){debug.Add(text);}

			GUILog.ScrollToBottom();
		}

		public static void In (string text) {Add(text, ELog.IN);}
		public static void Out (string text) {Add(text, ELog.OUT);}
		public static void Debug (string text) {Add(text, ELog.DEBUG);}



		public static int Count(ELog io) {
			if (io == ELog.OUT) {return outLog.Count;}
			if (io == ELog.IN) {return inLog.Count;}
			if (io == ELog.IO) {return ioLog.Count;}
			if (io == ELog.DEBUG){return debug.Count;}
			return -1;
		}
		public static int LastIndex(ELog io) {return Count(io)-1;}

		public static string Index(int i, ELog io){
			if (io == ELog.OUT) {return outLog[i];}
			else if (io == ELog.IN) {return inLog[i];}
			else if (io == ELog.IO) {return ioLog[i];}
			else if (io == ELog.DEBUG){return debug[i];}
			else {
				Add("GameLog: Index failure", ELog.DEBUG);
				return "";
			}
		}

		public static void Reset(){
			ioLog = new List<string>();
			inLog = new List<string>();
			outLog = new List<string>();
			debug = new List<string>();
		}

	}
}
