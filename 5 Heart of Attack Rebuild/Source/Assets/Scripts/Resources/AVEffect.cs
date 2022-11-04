using UnityEngine;

namespace HOA.Resources
{

    public partial class AVEffect
    {

        public Texture2D tex { get; private set; }
        public AudioClip sound { get; private set; }

        public AVEffect(Texture2D tex, AudioClip sound)
        {
            this.tex = tex;
            this.sound = sound;
        }

        public AVEffect(string texFileName, string soundFileName)
        {
            tex = UnityEngine.Resources.Load("Images/Effects/" + texFileName) as Texture2D;
            sound = UnityEngine.Resources.Load("Audio/Effects/" + soundFileName) as AudioClip;
        }

        public AVEffect(string fileName) : this(fileName, fileName) { }

        public void Play(IEntity target)
        {
            Debug.Log("Effect graphics not implemented.");
            if (tex != null)
            { }// target.Display.Effect(tex);
            if (sound != null)
                AudioEffectMixer.Play(sound);
        }

        public void PlayNonLocal() 
        { 
            if (sound != null) 
                AudioEffectMixer.Play(sound); 
        }

    }
}
