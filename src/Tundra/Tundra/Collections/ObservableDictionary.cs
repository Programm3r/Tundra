using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Tundra.Collections
{
    /// <summary>
    /// Observable Dictionary Class
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// The count string{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        private const string CountString = "Count";
        /// <summary>
        /// The indexer name
        /// </summary>
        private const string IndexerName = "Item[]";
        /// <summary>
        /// The keys name
        /// </summary>
        private const string KeysName = "Keys";
        /// <summary>
        /// The values name
        /// </summary>
        private const string ValuesName = "Values";

        /// <summary>
        /// The _ dictionary
        /// </summary>
        private IDictionary<TKey, TValue> _Dictionary;

        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        /// <value>
        /// The dictionary.
        /// </value>
        protected IDictionary<TKey, TValue> Dictionary
        {
            get
            {
                return _Dictionary;
            }
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        public ObservableDictionary()
        {
            _Dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _Dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _Dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ObservableDictionary(int capacity)
        {
            _Dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        #endregion

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            Insert(key, value, true);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
        /// </returns>
        public bool ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException("key");

            TValue value;
            Dictionary.TryGetValue(key, out value);
            var removed = Dictionary.Remove(key);
            if (removed)
                //OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value));
                OnCollectionChanged();

            return removed;
        }


        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }


        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return Dictionary.Values;
            }
        }


        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                return Dictionary[key];
            }
            set
            {
                Insert(key, value, false);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Insert(item.Key, item.Value, true);
        }


        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            if (Dictionary.Count > 0)
            {
                Dictionary.Clear();
                OnCollectionChanged();
            }
        }


        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Dictionary.Contains(item);
        }


        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Dictionary.CopyTo(array, arrayIndex);
        }


        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count
        {
            get
            {
                return Dictionary.Count;
            }
        }


        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return Dictionary.IsReadOnly;
            }
        }


        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Dictionary).GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">items</exception>
        /// <exception cref="System.ArgumentException">An item with the same key has already been added.</exception>
        public void AddRange(IDictionary<TKey, TValue> items)
        {
            if (items == null) throw new ArgumentNullException("items");


            if (items.Count > 0)
            {
                if (Dictionary.Count > 0)
                {
                    if (items.Keys.Any(k => Dictionary.ContainsKey(k)))
                        throw new ArgumentException("An item with the same key has already been added.");
                    
                    foreach (var item in items) Dictionary.Add(item);
                }
                else
                    _Dictionary = new Dictionary<TKey, TValue>(items);


                OnCollectionChanged(NotifyCollectionChangedAction.Add, items.ToArray());
            }
        }


        /// <summary>
        /// Inserts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        /// <exception cref="System.ArgumentException">An item with the same key has already been added.</exception>
        private void Insert(TKey key, TValue value, bool add)
        {
            if (key == null) throw new ArgumentNullException("key");


            TValue item;
            if (Dictionary.TryGetValue(key, out item))
            {
                if (add) throw new ArgumentException("An item with the same key has already been added.");
                if (Equals(item, value)) return;
                Dictionary[key] = value;


                OnCollectionChanged(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, item));
            }
            else
            {
                Dictionary[key] = value;

                OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
            }
        }


        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        private void OnPropertyChanged()
        {
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnPropertyChanged(KeysName);
            OnPropertyChanged(ValuesName);
        }


        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Called when the collection has changed.
        /// </summary>
        private void OnCollectionChanged()
        {
            OnPropertyChanged();
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        /// <summary>
        /// Called when the collection has changed.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="changedItem">The changed item.</param>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
        {
            OnPropertyChanged();
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem));
        }


        /// <summary>
        /// Called when the collection has changed.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="newItem">The new item.</param>
        /// <param name="oldItem">The old item.</param>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
        {
            OnPropertyChanged();
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
        }


        /// <summary>
        /// Called when the collection has changed.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="newItems">The new items.</param>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
        {
            OnPropertyChanged();
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItems));
        }
    }
}