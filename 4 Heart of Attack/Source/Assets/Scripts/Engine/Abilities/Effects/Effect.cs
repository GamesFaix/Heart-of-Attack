using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {
	public partial class Effect : IEffect {
        public string Name { get; protected set; }
        public new Func<string> ToString;
        public Source Source { get; protected set; }
        
        public Action Process = () => { };
        public void Process2() { Process(); }

        public Target[] Targets { get; protected set; }
        public Target Target { get { return Targets[0]; } }
        public int[] Modifiers { get; protected set; }
        public int Modifier { get { return Modifiers[0]; } }
        public Species Species { get; protected set; }
        public bool[] Flags { get; protected set; }
        public bool Flag 
        { 
            get 
            { 
                if (Flags.Length < 1) return false;
                return Flags[0]; 
            } 
        }
        public Stats Stat { get; protected set; }
        public Player Player { get; protected set; }

        private Effect(
          string name,
          Source source,
          Action process,
          Target[] Targets,
          int[] modifiers,
          bool[] flags,
          Species Species,
          Stats stat,
          Player player)
        {
            Name = name;
            ToString = () => { return Name + " Effect"; };
            Source = source;
            Process = process;
            this.Targets = Targets;
            Modifiers = modifiers;
            Flags = flags;
            this.Species = Species;
            Stat = stat;
            Player = player;
        }
        private Effect(
            string name,
            Source source,
            Action process,
            Target[] Targets,
            int[] modifiers,
            bool[] flags,
            Species Species) : this (name, source, process, Targets, modifiers, flags, Species, Stats.Health, null) {}

        private Effect(string name, Source source, Action process, Target[] Targets, int[] modifiers, Species Species)
            :this (name, source, process, Targets, modifiers, new bool[0], Species) {}

        private Effect(string name, Source source, Action process, Target[] Targets, int[] modifiers)
            :this (name, source, process, Targets, modifiers, Species.None) {}

        private Effect(string name, Source source, Target[] Targets, int[] modifiers)
            : this(name, source, () => { }, Targets, modifiers) { }

        private Effect(string name, Source source, Target Target, int[] modifiers)
            : this(name, source, new Target[1]{Target}, modifiers) { }

        private Effect(string name, Source source, Target Target, int modifier)
            : this(name, source, new Target[1] { Target }, new int[1] { modifier }) { }

        private Effect(string name, Source source, Target Target)
            : this(name, source, new Target[1] { Target }, new int[0]) { }

        private Effect(string name, Source source, Target[] Targets)
            : this(name, source, Targets, new int[0]) { }

        private Effect(string name, Source source, int[] modifiers)
            : this(name, source, new Target[0], new int[0]) { }

        private Effect(string name, Source source, Target Target, Species Species)
            : this(name, source, () => { }, new Target[1] { Target }, new int[0], Species) { }

        private Effect(string name, Source source, bool flag) : this(name, source, () => { }, new Target[0], new int[0], new bool[1] { flag }, Species.None) { }


        private Effect(Source source, Target[] Targets, int[] modifiers) 
            : this("Default Effect Name", source, Targets, modifiers) {}

        private Effect(Source source, Target[] Targets) 
            : this(source, Targets, new int[1] { 0 }) { }

        private Effect(Source source, Target Target = null, int modifier = 0) 
            : this (source, new Target[1]{Target}, new int[1]{modifier}) { }

        private Effect(Source source, Target Target, int[] modifiers) 
            : this(source, new Target[1]{Target}, modifiers) { }

        private Effect(Source source, int modifier) 
            : this(source, null, modifier) { }

        private Effect(string name, Source source)
            : this(name, source, new Target[0]) { }


        private Effect(string name, Source source, Target Target, int modifier, bool flag)
            : this(name, source, () => { }, 
            new Target[1] { Target }, 
            new int[1] { modifier }, 
            new bool[1] { flag },
            Species.None) { }

        private Effect(string name, Source source, Target Target, Player player) :
            this(name, source, () => { },
            new Target[1] { Target },
            new int[0], new bool[0],
            Species.None, Stats.Health, player) { }

        private Effect(string name, Source source, Target Target, Stats stat, int change)
            : this(name, source, () => { }, new Target[1] { Target }, new int[1] { change }, new bool[0], Species.None, stat, null) { } 

    }
}
