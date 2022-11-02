/* Generic Dictionary that can do regular and reverse look-ups. */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ReDictionary<TKey, TValue>{
	Dictionary<TKey, TValue> dict;
	Dictionary<TValue, TKey> reDict;
	
	public ReDictionary(){
		dict = new Dictionary<TKey, TValue>();
		reDict = new Dictionary<TValue, TKey>();		
	}
	
	public int Count(){return dict.Count;}
	
	public TValue GetValue(TKey key){
		if (dict.ContainsKey(key)){
			return dict[key];
		}
		else{
			Debug.Log("ReDictionary.GetValue: Invalid key!");
			return default(TValue);
		}
	}
		
	public TKey GetKey(TValue val){
		if (reDict.ContainsKey(val)){
			return reDict[val];
		}
		else{
			Debug.Log("ReDictionary.GetKey: Invalid value!");
			return default(TKey);
		}
	}
	
	public void Add(TKey key, TValue val){
			dict.Add(key, val);
			reDict.Add(val, key);
	}
	/*
	bool NullEntry(TKey key, TValue val){
		if (key.Equals(default(TKey))) {return true;}
		else if (default(TValue).Equals(
		val)) 
		{return true;}
		else {return false;}
	}*/
	
	public void RemoveKey(TKey key){
		dict.Remove(key);	
	}
	public void RemoveVal(TValue val){
		reDict.Remove(val);
	}
	
	public bool Contains(TKey key){
		if (dict.ContainsKey(key)) {return true;}
		else {return false;}
	}
	public bool Contains(TValue val){
		if (dict.ContainsValue(val)) {return true;}
		else {return false;}
	}
	
	public Dictionary<TKey, TValue>.KeyCollection Keys(){return dict.Keys;}
	
	public Dictionary<TKey, TValue>.ValueCollection Values(){return dict.Values;}
	
		
	
}
