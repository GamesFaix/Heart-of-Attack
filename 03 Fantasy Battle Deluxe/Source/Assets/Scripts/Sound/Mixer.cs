using UnityEngine;
using System.Collections.Generic;

public class Mixer : MonoBehaviour {

	public GameObject channelPrefab;

	static List<AudioSource> sources;
	static Dictionary<AudioSource, float> startTimes;

	static float volume;

	void Awake () {
		sources = new List<AudioSource>();
		startTimes = new Dictionary<AudioSource, float>();

		for (int i=0; i<8; i++) {

			GameObject channel = Instantiate (channelPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			channel.transform.parent = gameObject.transform;
			channel.name = "Sound Channel "+i;

			AudioSource source = channel.GetComponent("AudioSource") as AudioSource;

			sources.Add(source);
		}
	}


	public static void Play (AudioClip clip) {
		AudioSource channel = NextAvailableChannel();

		if (startTimes.ContainsKey(channel)) {startTimes.Remove(channel);}
		startTimes.Add(channel, Time.time);

		channel.volume = volume;
		channel.clip = clip;
		channel.Play();
	}

	static AudioSource NextAvailableChannel () {

		for (int i=0; i<sources.Count; i++) {
			if (!sources[i].isPlaying) {return sources[i];}
		}

		AudioSource oldest = default(AudioSource);
		float oldestTime = 1000000000000000;

		for (int i=0; i<sources.Count; i++) {
			if (startTimes[sources[i]] < oldestTime) {
				oldest = sources[i];
				oldestTime = startTimes[sources[i]];
			}
		}

		return oldest;
	}

	void Update () {

		int playing = 0;
		for (int i=0; i<sources.Count; i++) {
			if (sources[i].isPlaying) {playing++;}
		}
		if (playing > 0) {
			volume = 1f/(float)playing;
		}
		else {volume = 1;}

	}


}
