﻿namespace tvn.cosine.ai.common.collections
{
    public interface IQueue<T> : IEnumerable<T>, IStringable
    {
        T Get(int index);
        int IndexOf(T item);
        void Insert(int index, T item);
        void RemoveAt(int index);

        void AddAll(IQueue<T> items);
        bool IsReadonly();
        bool Add(T item);
        bool IsEmpty();
        int Size();
        T Pop();
        T Peek();
        void Clear();
        bool Contains(T item);
        bool ContainsAll(IQueue<T> other);
        bool Remove(T item);
        void RemoveAll(IQueue<T> items);
        void Sort(IComparer<T> comparer);
        T[] ToArray();
        void Reverse();
        IQueue<T> subList(int startPos, int endPos);
        void Set(int position, T item);
    }
}
