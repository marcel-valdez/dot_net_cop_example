namespace Game.Logic.DynamicModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Caching;
    using System.Web;
    internal class Cached<T>
    {
        private Guid key;
        DateTime lastRetrieval;
        TimeSpan TTL;
        int valueHashCode;

        public Cached(T data, TimeSpan? ttl = null)
        {
            TTL = ttl ?? TimeSpan.FromMinutes(20);
            this.key = Guid.NewGuid();
            this.valueHashCode = data.GetHashCode();
            Insert(data, null, TTL);
            lastRetrieval = DateTime.Now;
        }

        public T Value
        {
            get
            {
                lastRetrieval = DateTime.Now;
                T value = (T)Cache.Remove(this.key.ToString());
                Insert(value, null, TTL);

                return value;
            }
        }

        private TimeSpan RemainingLifeSpan
        {
            get
            {
                return (DateTime.Now - lastRetrieval);
            }
        }
        public bool IsAlive
        {
            get
            {
                return RemainingLifeSpan < TTL;
            }
        }

        public T Touch()
        {
            T data = (T)Cache.Get(this.key.ToString());
            Cache.Remove(this.key.ToString());
            Insert(data, Cache.NoAbsoluteExpiration, RemainingLifeSpan);
            return data;
        }

        public T Forget()
        {
            object result = Cache.Remove(this.key.ToString());
            return result == null ? default(T) : (T)result;
        }

        private Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

        private void Insert(T data, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {

            Cache.Insert(key.ToString(),
                data,
                null,
                absoluteExpiration ?? Cache.NoAbsoluteExpiration,
                slidingExpiration ?? Cache.NoSlidingExpiration);
        }
        public override int GetHashCode()
        {
            if (this.IsAlive)
            {
                return valueHashCode;
            }

            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is T && this.IsAlive)
            {
                return obj.Equals(this.Touch());
            }

            if (obj is Cached<T> && this.IsAlive && ((Cached<T>)obj).IsAlive)
            {
        // SMELL: NOT THREAD SAFE.
        /** 
        * Aunque podría estar vivo el objeto a la hora de preguntar: 
        * this.IsAlive y obj.IsAlive, el GC podría llevarse ambos objetos 
        * en cualquier momento dentro del código.
        **/
                return ((Cached<T>)obj).Equals(this.Touch());
            }

            return base.Equals(obj);
        }
    }
}
