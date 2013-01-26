namespace Game.Core.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Game.Core;

    internal class HandlerMap
    {
        private Dictionary<object, ChannelHandlersContainer> map;

        public HandlerMap()
        {
            this.map = new Dictionary<object, ChannelHandlersContainer>();
        }

        public IEnumerable<Tuple<Action<object, T>, Predicate<T>>> GetHandlers<T>(object channel)
        {
            lock (this.map)
            {
                return this.map
                    .Where(pair => pair.Key.Equals(channel))
                    .Select(pair => pair.Value)
                    .SelectMany(container => container)
                    .Where(tuple => tuple.Item1.IsAssignableFrom(typeof(T)))
                    .Select(
                        tuple => Tuple.Create(
                            tuple.Item2 as Action<object, T>,
                            tuple.Item3 as Predicate<T>));
            }
        }

        public void AddHandler<T>(object channel, Action<object, T> handler, Predicate<T> filter)
        {
            lock (this.map)
            {
                if (!this.map.ContainsKey(channel))
                {
                    this.map.Add(channel, new ChannelHandlersContainer());
                }

                this.map[channel].Add(Tuple.Create(typeof(T), handler as object, filter as object));
            }
        }

        public int Remove(object channel, object handler)
        {
            int removed = 0;
            lock (this.map)
            {
                foreach (var element in this.map[channel]
                                        .Where(tuple => tuple.Item2.Equals(handler))
                                        .ToArray())
                {
                    this.map[channel].Remove(element);
                    removed++;
                }
            }

            return removed;
        }
    }
}
