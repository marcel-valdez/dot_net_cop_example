namespace Game.Core.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics.Contracts;
    using Game.Core;

    internal class Container<T> : IEnumerable<T>
    {
        private List<T> elements;

        public Container(T[] elements)
        {
            Contract.Requires(elements != null);
            this.elements = new List<T>(elements);
        }

        public Container()
        {
            this.elements = new List<T>();
        }


        /// <summary>
        /// Gets a value indicating whether this instance has elements.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has elements; otherwise, <c>false</c>.
        /// </value>    
        [Pure]
        public bool HasElements
        {
            get
            {
                return this.elements.Count > 0;
            }
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Add(T element)
        {
            this.elements.Add(element);
        }

        /// <summary>
        /// Pop out an element from this instance.
        /// Throws an Exception if there are no more elements left
        /// </summary>
        /// <returns>The First element in the list</returns>
        public T Pop()
        {
            Contract.Requires(this.HasElements);
            T msg = this.elements[0];
            this.elements.RemoveAt(0);
            return msg;
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// true if the element was removed, false if it wasn't
        /// </returns>
        public bool Remove(T element)
        {
            return this.elements.Remove(element);
        }

        /// <summary>
        /// Clears all elements in this instance.
        /// </summary>
        public void Clear()
        {
            this.elements.Clear();
        }

        /// <summary>
        /// Gets a copy of this container.
        /// </summary>
        /// <returns>The copy of this container</returns>
        public Container<T> GetCopy()
        {
            return new Container<T>(this.elements.ToArray());
        }

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }
        #endregion
    }
}
