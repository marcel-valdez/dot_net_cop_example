namespace Game.Logic.DynamicModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    internal class WeakList<T> : IList<T>, IDisposable
    {
        List<Weak<T>> items = new List<Weak<T>>();
        private bool disposed;
        private void DestroyDead()
        {
            Weak<T>[] copy;

            lock (items)
            {
                copy = items.ToArray();
            }

            foreach (Weak<T> item in copy)
            {
                if (!item.IsAlive)
                {
                    lock (items)
                    {
                        items.Remove(item);
                    }
                }
            }

        }
        #region IList<T> Members
        public int IndexOf(T item)
        {
            this.DestroyDead();
            return this.items.IndexOf(new Weak<T>(item));
        }

        public void Insert(int index, T item)
        {
            this.items.Insert(index, new Weak<T>(item));
        }

        public void RemoveAt(int index)
        {
            this.DestroyDead();
            this.items.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                this.DestroyDead();
                return this.items[index].Value;
            }

            set
            {
                this.DestroyDead();
                this.items[index] = new Weak<T>(value);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            this.items.Add(new Weak<T>(item));
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public bool Contains(T item)
        {
            return this.items.Contains(new Weak<T>(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.DestroyDead();
            this.items.ToArray().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                this.DestroyDead();
                return this.items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<T>)this.items).IsReadOnly;
            }
        }

        public bool Remove(T item)
        {
            return this.items.Remove(new Weak<T>(item));
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            this.DestroyDead();
            return this.items
                .Where(w => w.IsAlive)
                .Select(w => w.Value)
                .ToArray().AsEnumerable()
                .GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            this.DestroyDead();
            return this.items
                .Where(w => w.IsAlive)
                .Select(w => w.Value)
                .ToArray()
                .GetEnumerator();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.disposed = true;
        }

        #endregion
    }
}