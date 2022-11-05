/* Rounding to specific variable types. */

using UnityEngine;
using System.Collections;

namespace FBI.Math {
	public class Rounding {
	
		public static byte ToByte(double preRound){
			int rounded = Mathf.RoundToInt((float)preRound);
			if (rounded<256 && rounded>(-1)){
				return (byte)rounded;
			}
			else {
				Debug.Log("RoundToByte overflow.");
				return default(byte);
			}
		}
		public static byte ToByte(float preRound){
			int rounded = Mathf.RoundToInt(preRound);
			if (rounded<256 && rounded>(-1)){
				return (byte)rounded;
			}
			else {
				Debug.Log("RoundToByte overflow.");
				return default(byte);
			}
		}
		
		public static sbyte ToSByte(double preRound){
			int rounded = Mathf.RoundToInt((float)preRound);
			if (rounded<128 && rounded>(-129)){
				return (sbyte)rounded;
			}
			else {
				Debug.Log("RoundToSByte overflow.");
				return default(sbyte);
			}
		}
		public static sbyte ToSByte(float preRound){
			int rounded = Mathf.RoundToInt(preRound);
			if (rounded<128 && rounded>(-129)){
				return (sbyte)rounded;
			}
			else {
				Debug.Log("RoundToSByte overflow.");
				return default(sbyte);
			}
		}
	
		public static byte FloorToByte(double preRound){
			int rounded = Mathf.FloorToInt((float)preRound);
			if (rounded<256 && rounded>(-1)){
				return (byte)rounded;
			}
			else {
				Debug.Log("FloorToByte overflow.");
				return default(byte);
			}
		}
		public static byte FloorToByte(float preRound){
			int rounded = Mathf.FloorToInt(preRound);
			if (rounded<256 && rounded>(-1)){
				return (byte)rounded;
			}
			else {
				Debug.Log("FloorToByte overflow.");
				return default(byte);
			}
		}
		
		public static byte CeilToByte(double preRound){
			int rounded = Mathf.CeilToInt((float)preRound);
			if (rounded<256 && rounded>(-1)){
				return (byte)rounded;
			}
			else {
				Debug.Log("CeilToByte overflow.");
				return default(byte);
			}
		}
		public static byte CeilToByte(float preRound){
			int rounded = Mathf.CeilToInt(preRound);
			if (rounded<256 && rounded>(-1)){
				return (byte)rounded;
			}
			else {
				Debug.Log("CeilToByte overflow.");
				return default(byte);
			}
		}
		
		public static short ToShort(double preRound){
			int rounded = Mathf.RoundToInt((float)preRound);
			if (rounded<32768 && rounded>(-32769)){
				return (short)rounded;
			}
			else {
				Debug.Log("RoundToShort overflow.");
				return default(short);
			}
		}
		public static short ToShort(float preRound){
			int rounded = Mathf.RoundToInt(preRound);
			if (rounded<32768 && rounded>(-32769)){
				return (short)rounded;
			}
			else {
				Debug.Log("RoundToShort overflow.");
				return default(short);		
			}
		}
	
		public static short FloorToShort(double preRound){
			int rounded = Mathf.FloorToInt((float)preRound);
			if (rounded<32768 && rounded>(-32769)){
				return (short)rounded;
			}
			else {
				Debug.Log("FloorToShort overflow.");
				return default(short);
			}
		}
		public static short FloorToShort(float preRound){
			int rounded = Mathf.FloorToInt(preRound);
			if (rounded<32768 && rounded>(-32769)){
				return (short)rounded;
			}
			else {
				Debug.Log("FloorToShort overflow.");
				return default(short);
			}
		}
		
		public static short CeilToShort(double preRound){
			int rounded = Mathf.CeilToInt((float)preRound);
			if (rounded<32768 && rounded>(-32769)){
				return (short)rounded;
			}
			else {
				Debug.Log("CeilToShort overflow.");
				return default(short);
			}
		}
		public static short CeilToShort(float preRound){
			int rounded = Mathf.CeilToInt(preRound);
			if (rounded<32768 && rounded>(-32769)){
				return (short)rounded;
			}
			else {
				Debug.Log("CeilToShort overflow.");
				return default(short);
			}
		}

	}
}