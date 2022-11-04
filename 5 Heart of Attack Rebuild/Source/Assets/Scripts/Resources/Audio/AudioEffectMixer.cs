using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA
{
    /// <summary>
    /// Creates AudioSources and assigns sounds to them.
    /// </summary>
    public class AudioEffectMixer : MonoBehaviour
    {
        static GameObject channelPrefab;
        static byte channelCount;
        static List<AudioSource> sources;
        static Dictionary<AudioSource, float> startTimes;

        static float volume;

        void Awake()
        {
            channelPrefab = UnityEngine.Resources.Load("Prefabs/Audio Channel") as GameObject;
            channelCount = 8;
            sources = new List<AudioSource>();
            startTimes = new Dictionary<AudioSource, float>();

            for (byte i = 0; i < channelCount; i++)
            {
                GameObject channel =
                    channelPrefab.InstantiateNowhereUnder(gameObject, "Audio Channel " + i);
                sources.Add(channel.GetComponent("AudioSource") as AudioSource);
            }
        }

        /// <summary>
        /// Silence output.
        /// </summary>
        /// <param name="mute">Mute if true, unmute if false.</param>
        public static void Mute(bool mute)
        {
            foreach (AudioSource source in sources) 
                source.mute = mute;
        }

        /// <summary>
        /// Play an AudioClip
        /// </summary>
        /// <param name="clip"></param>
        public static void Play(AudioClip clip)
        {
            AudioSource channel = NextAvailableChannel();

            if (startTimes.ContainsKey(channel)) 
                startTimes.Remove(channel);
            startTimes.Add(channel, Time.time);

            channel.volume = volume;
            channel.clip = clip;
            channel.Play();
        }

        static AudioSource NextAvailableChannel()
        {

            for (int i = 0; i < sources.Count; i++)
                if (!sources[i].isPlaying) 
                    return sources[i];

            AudioSource oldest = null;
            float oldestTime = float.PositiveInfinity;
            for (int i = 0; i < sources.Count; i++)
                if (startTimes[sources[i]] < oldestTime)
                {
                    oldest = sources[i];
                    oldestTime = startTimes[sources[i]];
                }
            return oldest;
        }

        void Update()
        {
            Compress();   
        }

        void Compress()
        {
            int playing = 0;
            for (int i = 0; i < sources.Count; i++)
                if (sources[i].isPlaying)
                    playing++;
            if (playing > 0)
                volume = 1f / (float)playing;
            else
                volume = 1;
        }

    }
}
