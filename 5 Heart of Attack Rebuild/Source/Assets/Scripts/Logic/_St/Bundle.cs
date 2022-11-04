using System;
using System.Collections.Generic;
using System.Linq;

namespace HOA.St
{
    public class Bundle
    {
        Dictionary<string, Stat> dict;

        public Bundle(params Stat[] stats) 
        {
            dict = new Dictionary<string, Stat>();
            foreach (Stat s in stats)
                dict.Add(s.name, s);
        }

        public void Add(Stat stat) { dict.Add(stat.name, stat); }

        public bool Contains(string name) { return dict.ContainsKey(name); }

        public Stat this[string name]
        {
            get { return dict[name]; }
            set { dict[name] = value; }
        }
	}
}