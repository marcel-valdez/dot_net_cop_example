using System;

namespace Game.Logic.DynamicModel
{
    internal class Weak<T>
    {
        WeakReference wRef;
        public Weak(T value)
        {
            wRef = new WeakReference(value);
        }

        public T Value
        {
            get
            {
                return (T)wRef.Target;
            }
        }

        public bool IsAlive
        {
            get
            {
                return wRef.IsAlive;
            }
        }

        public override int GetHashCode()
        {
            if (wRef.IsAlive)
            {
                return wRef.Target.GetHashCode();
            }

            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is T && this.wRef.IsAlive)
            {
                return obj.Equals(this.wRef.Target);
            }

            if (obj is Weak<T> && this.wRef.IsAlive && ((Weak<T>)obj).IsAlive)
            {
                return (obj as Weak<T>).Equals(this.Value);
            }

            return base.Equals(obj);
        }
    }
}
