namespace Game.Logic.DynamicModel
{
    using System.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Caching;
    using System.Web;

    internal class CachedList<T> : IList<T>
    {
        List<Cached<T>> items = new List<Cached<T>>();
        private bool disposed;
        TimeSpan ttl;
        public CachedList(TimeSpan? TTL = null)
        {
            ttl = TTL ?? TimeSpan.FromMinutes(20);
        }

        private void DestroyDead()
        {
            Cached<T>[] copy;

            lock (items)
            {
                copy = items.ToArray();
            }

            foreach (Cached<T> item in copy)
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
            return this.items.IndexOf(new Cached<T>(item, ttl));
        }

        public void Insert(int index, T item)
        {
            this.items.Insert(index, new Cached<T>(item, ttl));
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
                this.items[index] = new Cached<T>(value, ttl);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            this.items.Add(new Cached<T>(item, ttl));
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public bool Contains(T item)
        {
            return this.items.Contains(new Cached<T>(item, ttl));
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
            return this.items.Remove(new Cached<T>(item, ttl));
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