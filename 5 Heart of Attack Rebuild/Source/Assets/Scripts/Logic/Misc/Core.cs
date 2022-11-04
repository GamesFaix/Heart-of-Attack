#define DEBUG

using System;
using System.Collections.Generic;
using UnityEngine;
using HOA.Abilities;
using HOA.Tokens;
using HOA.GUI;

namespace HOA { 
    /// <summary>
    /// Main entry point.
    /// Must be anchored to GameObject in scene.
    /// </summary>
    public class Core : MonoBehaviour{

        public static Session session { get; private set; }

        private void Start()
        {
#if DEBUG 
            Debug.Log("Program started.");
#endif
            gameObject.AddComponent("Updater");
            Abilities.EffectQueue.Load();
            LoadAudioSystem();
            LoadGUISystem();            
            Reference.Main.Load();

            session = new Session();
#if DEBUG
            session.AutoPopulate();
            session.CreateBoard(new size2(10, 10));
            AbilityRequester.AbilityRequestPublish(Source.Force,
                Ability.ManualCreate(Species.Demolitia);
            AbilityRequester.AbilityRequestPublish(Source.Force,
                Ability.ManualCreate(Species.Tree);
            //Debug.Log(Session.Active.tokens.ToStringLong());
#endif
        }

        void LoadAudioSystem()
        {
            HOA.Audio.Load();
            GameObject mixerPrefab = UnityEngine.Resources.Load("Prefabs/Audio Mixer") as GameObject;
            mixerPrefab.InstantiateNowhereUnder(gameObject, "Audio Effect Mixer");
        }
        void LoadGUISystem()
        {
            HOA.Textures.Load();
            GameObject guiPrefab = UnityEngine.Resources.Load("Prefabs/GUI Anchor") as GameObject;
            guiPrefab.InstantiateNowhereUnder(gameObject, "GUI Anchor");   
        }
    
    }
}
