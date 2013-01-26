namespace Game.Logic.DynamicModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Caching;
    using System.Web;

    internal class ModelCache
    {
        Dictionary<object, CachedList<object>> dynamicObjects;
        public ModelCache()
        {
            dynamicObjects = new Dictionary<object, CachedList<object>>();
            dynamicObjects.Add(this, new CachedList<object>());
        }

        public void Add(object key, object value)
        {
            CachedList<object> list;
            if (!dynamicObjects.TryGetValue(key ?? this, out list))
            {
                list = new CachedList<object>();
                dynamicObjects.Add(key ?? this, list);
            }

            list.Add(value);
        }

        public IList<object> Retrieve(object key = null)
        {
            return dynamicObjects[key ?? this];
        }

        public T Retrieve<T>(Predicate<T> predicate, object key = null)
        {
            return this.dynamicObjects[key ?? this]
                .Where(item => (item is T) && predicate((T)item))
                .Cast<T>()
                .FirstOrDefault();
        }
    }
}