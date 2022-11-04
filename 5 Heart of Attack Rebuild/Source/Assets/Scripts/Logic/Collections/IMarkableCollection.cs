using System.Collections.Generic;

namespace HOA.Collections
{
    /// <summary>
    /// Collection with sublist for selecting and manipulating a subset of indecies.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMarkable<T>
    {
        void Mark(T item);
        void Mark(int index);
        void Mark(IEnumerable<T> collection);
        void Unmark(T item);
        void Unmark(int index);
        void Unmark(IEnumerable<T> collection);
        void UnmarkAll();
        void RemoveMarked();
    }
}
