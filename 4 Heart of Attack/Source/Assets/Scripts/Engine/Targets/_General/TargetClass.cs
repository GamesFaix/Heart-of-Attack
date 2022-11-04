using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace HOA {
    [Flags]
	public enum TargetClass : ushort 
    { 
        None = 0,
        Cell = 1, 
        Token = 2, 
        Unit = 4, 
        Ob = 8, 
        King = 16, 
        Heart = 32, 
        Tram = 64 , 
        Dest = 128, 
        Corpse = 256
    }

    public static class TargetClassExtensionMethods {

	
        
	}
    
}