using System;
using System.Collections.Generic;

namespace HOA
{

    public class ClosedAction<T>
    {
        public Action<T> action { get; protected set; }
        public T arg { get; protected set; }
        public ClosedAction(Action<T> action, T arg)
        {
            this.action = action;
            this.arg = arg;
        }
        protected ClosedAction(Action<T> action) 
            : this(action, default(T)) { }
        protected ClosedAction(T arg) 
            : this(null, arg) { }

        public void Invoke() { action(arg); }
	}
    
    
    
    public class ClosedFunc<T, TOut>
    {
        public Func<T, TOut> func { get; protected set; }
        public T arg { get; protected set; }
        public ClosedFunc(Func<T, TOut> func, T arg)
        {
            this.func = func;
            this.arg = arg;
        }
        protected ClosedFunc(Func<T, TOut> f)
            : this(f, default(T)) { }
        protected ClosedFunc(T arg)
            : this(null, arg) { }
        public TOut Invoke() { return func(arg); }
    }
   
}