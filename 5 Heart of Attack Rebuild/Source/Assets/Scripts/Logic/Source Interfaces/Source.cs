using System;
using System.Collections.Generic;

namespace HOA 
{
	
    public class Source
    {
        /// <summary> Inner object of source. </summary>
        public object obj { get; private set; }
        public static ForcedSource force { get; private set; }
        
        public Source(object obj)
        {
            this.obj = obj;
            if (force == null)
                force = new ForcedSource();
        }

        /// <summary>Recusive list of object, then object's object, then o's o's o...</summary>
        public List<object> history
        {
            get
            {
                List<object> list = new List<object>(){obj};
                if (obj is ISourced)
                    list.Add((obj as ISourced).source.history);
                return list;
            }
        }

        /// <summary>Source history from old to new.</summary>
        public override string ToString()
        {
            string str = "";
            foreach (object o in history)
                str.Insert(0, o + " => ");
            return str;
        }

        /// <summary> Return last T in source history, or null.</summary>
        public T Last<T>()
            where T : class
        {
            foreach (object o in history)
                if (o.GetType() == typeof(T))
                    return (o as T);
            return null;
        }

        /// <summary>Returns true if T found in history, outputs last T.  
        /// Otherwise returns false, outputs default(T).</summary>
        public bool Last<T>(out T item)
            where T : class
        {
            item = default(T);
            foreach (object o in history)
                if (o.GetType() == typeof(T))
                {
                    item = (o as T);
                    return true;
                }
            return false;
        }

        /// <summary> Is this object's runtime type in this type array?</summary>
        public static bool IsValid(Type[] types, object obj)
        {
            if (obj == null)
                return false;
            foreach (Type t in types)
                if (t == obj.GetType())
                    return true;
            return false;
        }

        public class ForcedSource
        {
            public ForcedSource() 
            {
                if (Source.force != null)
                    throw new Exception("ForcedSource must remain unique!");            
            }
            public override string ToString() { return "Forced source!"; }
        }

    }
}