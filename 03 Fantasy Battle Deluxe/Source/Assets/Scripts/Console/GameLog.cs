using System.Collections.Generic;

public enum LogIO {IN, OUT, IO, DEBUG};

public static class GameLog {

	static List<string> ioLog = new List<string>();
	static List<string> inLog = new List<string>();
	static List<string> outLog = new List<string>();
	static List<string> debug = new List<string>();

	static void Add(string text, LogIO io){
		if (io == LogIO.OUT) {outLog.Add(text); ioLog.Add(text); debug.Add(text);}
		if (io == LogIO.IN) {inLog.Add(text); ioLog.Add(text); debug.Add(text);}
		if (io == LogIO.IO) {Add("GameLog: Event added to IO log: "+text, LogIO.DEBUG);}
		if (io == LogIO.DEBUG){debug.Add(text);}
	}

	public static void In (string text) {Add(text, LogIO.IN);}
	public static void Out (string text) {Add(text, LogIO.OUT);}
	public static void Debug (string text) {Add(text, LogIO.DEBUG);}



	public static int Count(LogIO io) {
		if (io == LogIO.OUT) {return outLog.Count;}
		if (io == LogIO.IN) {return inLog.Count;}
		if (io == LogIO.IO) {return ioLog.Count;}
		if (io == LogIO.DEBUG){return debug.Count;}
		return -1;
	}
	public static int LastIndex(LogIO io) {return Count(io)-1;}

	public static string Index(int i, LogIO io){
		if (io == LogIO.OUT) {return outLog[i];}
		else if (io == LogIO.IN) {return inLog[i];}
		else if (io == LogIO.IO) {return ioLog[i];}
		else if (io == LogIO.DEBUG){return debug[i];}
		else {
			Add("GameLog: Index failure", LogIO.DEBUG);
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
