using UnityEngine; 

namespace HOA.Sounds 
{ 

    public static class GUI 
    {
        public static AudioClip Click { get; private set; }
        public static AudioClip Inspect { get; private set; }
        public static AudioClip Target { get; private set; }

        public static void Load()
        {
            Click = LoadSound("Click");
            Inspect = LoadSound("Inspect");
            Target = LoadSound("Target");
        }
        
        static AudioClip LoadSound(string fileName)
        {
            return Resources.Load("Audio/GUI/" + fileName) as AudioClip;
        }
    
    }
}
