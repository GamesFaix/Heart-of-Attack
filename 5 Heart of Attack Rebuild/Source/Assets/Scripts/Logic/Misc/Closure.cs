using System;
using System.Collections.Generic;

namespace HOA
{
    #region Actions

    public class ClosedAction<T>
    {
        public Action<T> action { get; protected set; }
        public T arg { get; protected set; }
        public ClosedAction(Action<T> action, T arg)
        {
            this.action = action;
            this.arg = arg;
        }
        public void Invoke() { action(arg); }
	}
    public class ClosedAction<T1, T2>
    {
        public Action<T1, T2> action { get; protected set; }
        public T1 arg1 { get; protected set; }
        public T2 arg2 { get; protected set; }
        
        public ClosedAction(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            this.action = action;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }
        public void Invoke() { action(arg1, arg2); }
    }
    public class ClosedAction<T1, T2, T3>
    {
        public Action<T1, T2, T3> action { get; protected set; }
        public T1 arg1 { get; protected set; }
        public T2 arg2 { get; protected set; }
        public T3 arg3 { get; protected set; }
        
        public ClosedAction(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            this.action = action;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }
        public void Invoke() { action(arg1, arg2, arg3); }
    }
    public class ClosedAction<T1, T2, T3, T4>
    {
        public Action<T1, T2, T3, T4> action { get; protected set; }
        public T1 arg1 { get; protected set; }
        public T2 arg2 { get; protected set; }
        public T3 arg3 { get; protected set; }
        public T4 arg4 { get; protected set; }
        
        public ClosedAction(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            this.action = action;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.arg4 = arg4;
        }
        public void Invoke() { action(arg1, arg2, arg3, arg4); }
    }

    #endregion

    #region Funcs

    public class ClosedFunc<TOut>
    {
        public Func<TOut> func {get; protected set;}
        public ClosedFunc(Func<TOut> func)
        {
            this.func = func;
        }
        public TOut Invoke() { return func(); }
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
        public TOut Invoke() { return func(arg); }
    }
    public class ClosedFunc<T1, T2, TOut>
    {
        public Func<T1, T2, TOut> func { get; protected set; }
        public T1 arg1 { get; protected set; }
        public T2 arg2 { get; protected set; }
        public ClosedFunc(Func<T1, T2, TOut> func, T1 arg1, T2 arg2)
        {
            this.func = func;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }
        public TOut Invoke() { return func(arg1, arg2); }
    }
    public class ClosedFunc<T1, T2, T3, TOut>
    {
        public Func<T1, T2, T3, TOut> func { get; protected set; }
        public T1 arg1 { get; protected set; }
        public T2 arg2 { get; protected set; }
        public T3 arg3 { get; protected set; }
        public ClosedFunc(Func<T1, T2, T3, TOut> func, T1 arg1, T2 arg2, T3 arg3)
        {
            this.func = func;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }
        public TOut Invoke() { return func(arg1, arg2, arg3); }
    }
    public class ClosedFunc<T1, T2, T3, T4, TOut>
    {
        public Func<T1, T2, T3, T4, TOut> func { get; protected set; }
        public T1 arg1 { get; protected set; }
        public T2 arg2 { get; protected set; }
        public T3 arg3 { get; protected set; }
        public T4 arg4 { get; protected set; }
        public ClosedFunc(Func<T1, T2, T3, T4, TOut> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            this.func = func;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.arg4 = arg4;
        }
        public TOut Invoke() { return func(arg1, arg2, arg3, arg4); }
    }
    #endregion

    #region Predicate

    public class ClosedPredicate<T>
    {
        public Predicate<T> predicate { get; protected set; }
        public T arg { get; protected set; }
        public ClosedPredicate(Predicate<T> predicate, T arg)
        {
            this.predicate = predicate;
            this.arg = arg;
        }
        public bool Invoke() { return predicate(arg); }
    }

    #endregion
}