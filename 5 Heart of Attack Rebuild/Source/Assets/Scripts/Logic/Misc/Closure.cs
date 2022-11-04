using System;
using System.Collections.Generic;

namespace HOA
{
    public abstract class ClosureArgs
    {
    }

    public interface ICloseable<TArgs>
        where TArgs : ClosureArgs
    {

    }
    
    public class Closure<TArgs>
        where TArgs : ClosureArgs
    {
        public ICloseable<TArgs> functor { get; private set; }
        public TArgs args { get; private set; }

        public Closure(ICloseable<TArgs> functor, TArgs args)
        {
            this.functor = functor;
            this.args = args;
        }




	}
}