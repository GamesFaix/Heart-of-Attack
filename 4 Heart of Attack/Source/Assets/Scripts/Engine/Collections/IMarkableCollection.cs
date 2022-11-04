using System.Collections.Generic;

namespace HOA { 

    public interface IMarkableCollection<T> 
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
