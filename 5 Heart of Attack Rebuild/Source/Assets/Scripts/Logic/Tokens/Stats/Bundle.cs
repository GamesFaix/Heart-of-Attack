using System;
using System.Collections.Generic;
using System.Linq;

namespace HOA.To.St
{
    public class Bundle
    {
        Dictionary<string, Stat<sbyte>> dict;

        public Bundle(params Stat<sbyte>[] stats) 
        {
            dict = new Dictionary<string, Stat<sbyte>>();
            foreach (Stat<sbyte> s in stats)
                dict.Add(s.name, s);
        }

        public void Add(Stat<sbyte> stat) { dict.Add(stat.name, stat); }

        public Stat<sbyte> this[string name]
        {
            get { return dict[name]; }
            set { dict[name] = value; }
        }
	}
}