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
        public EToken EToken { get; protected set; }
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
          Target[] targets,
          int[] modifiers,
          bool[] flags,
          EToken etoken,
          Stats stat,
          Player player)
        {
            Name = name;
            ToString = () => { return Name + " Effect"; };
            Source = source;
            Process = process;
            Targets = targets;
            Modifiers = modifiers;
            Flags = flags;
            EToken = etoken;
            Stat = stat;
            Player = player;
        }
        private Effect(
            string name,
            Source source,
            Action process,
            Target[] targets,
            int[] modifiers,
            bool[] flags,
            EToken etoken) : this (name, source, process, targets, modifiers, flags, etoken, Stats.Health, null) {}

        private Effect(string name, Source source, Action process, Target[] targets, int[] modifiers, EToken etoken)
            :this (name, source, process, targets, modifiers, new bool[0], etoken) {}

        private Effect(string name, Source source, Action process, Target[] targets, int[] modifiers)
            :this (name, source, process, targets, modifiers, EToken.NONE) {}

        private Effect(string name, Source source, Target[] targets, int[] modifiers)
            : this(name, source, () => { }, targets, modifiers) { }

        private Effect(string name, Source source, Target target, int[] modifiers)
            : this(name, source, new Target[1]{target}, modifiers) { }

        private Effect(string name, Source source, Target target, int modifier)
            : this(name, source, new Target[1] { target }, new int[1] { modifier }) { }

        private Effect(string name, Source source, Target target)
            : this(name, source, new Target[1] { target }, new int[0]) { }

        private Effect(string name, Source source, Target[] targets)
            : this(name, source, targets, new int[0]) { }

        private Effect(string name, Source source, int[] modifiers)
            : this(name, source, new Target[0], new int[0]) { }

        private Effect(string name, Source source, Target target, EToken etoken)
            : this(name, source, () => { }, new Target[1] { target }, new int[0], etoken) { }

        private Effect(string name, Source source, bool flag) : this(name, source, () => { }, new Target[0], new int[0], new bool[1] { flag }, EToken.NONE) { }


        private Effect(Source source, Target[] targets, int[] modifiers) 
            : this("Default Effect Name", source, targets, modifiers) {}

        private Effect(Source source, Target[] targets) 
            : this(source, targets, new int[1] { 0 }) { }

        private Effect(Source source, Target target = null, int modifier = 0) 
            : this (source, new Target[1]{target}, new int[1]{modifier}) { }

        private Effect(Source source, Target target, int[] modifiers) 
            : this(source, new Target[1]{target}, modifiers) { }

        private Effect(Source source, int modifier) 
            : this(source, null, modifier) { }

        private Effect(string name, Source source)
            : this(name, source, new Target[0]) { }


        private Effect(string name, Source source, Target target, int modifier, bool flag)
            : this(name, source, () => { }, 
            new Target[1] { target }, 
            new int[1] { modifier }, 
            new bool[1] { flag },
            EToken.NONE) { }

        private Effect(string name, Source source, Target target, Player player) :
            this(name, source, () => { },
            new Target[1] { target },
            new int[0], new bool[0],
            EToken.NONE, Stats.Health, player) { }

        private Effect(string name, Source source, Target target, Stats stat, int change)
            : this(name, source, () => { }, new Target[1] { target }, new int[1] { change }, new bool[0], EToken.NONE, stat, null) { } 

    }
}
