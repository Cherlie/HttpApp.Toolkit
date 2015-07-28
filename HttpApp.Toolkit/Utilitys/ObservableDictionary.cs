using System;
using System.Collections.Generic;
using Windows.Foundation.Collections;

namespace HttpApp.Toolkit.Utilitys
{
    /// <summary>
    /// 属性改变事件通知字典
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    public class ObservableDictionary<TKey, TValue> : IObservableMap<TKey,TValue>
    {
        private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<TKey>
        {
            public ObservableDictionaryChangedEventArgs(CollectionChange change, TKey key)
            {
                this.CollectionChange = change;
                this.Key = key;
            }
            public CollectionChange CollectionChange { get; private set; }
            public TKey Key { get; private set; }
        }

        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        public event MapChangedEventHandler<TKey, TValue> MapChanged;

        private void InvokeMapChanged(CollectionChange change, TKey key)
        {
            var eventHandler = MapChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new ObservableDictionaryChangedEventArgs(change, key));
            }
        }

        public void Add(TKey key, TValue value)
        {
            this.dictionary.Add(key, value);
            this.InvokeMapChanged(CollectionChange.ItemInserted, key);
        }

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return this.dictionary.Keys; }
        }

        public bool Remove(TKey key)
        {
            if (this.dictionary.Remove(key))
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value); 
        }

        public ICollection<TValue> Values
        {
            get { return this.dictionary.Values; } 
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.dictionary[key];
            }
            set
            {
                this.dictionary[key] = value;
                this.InvokeMapChanged(CollectionChange.ItemChanged, key);
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            var priorKeys = this.dictionary.Keys;
            this.dictionary.Clear();
            foreach (var key in priorKeys)
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
            } 
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.dictionary.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int arraySize = array.Length;
            foreach (var pair in this.dictionary)
            {
                if (arrayIndex >= arraySize) break;
                array[arrayIndex++] = pair;
            } 
        }

        public int Count
        {
            get { return this.dictionary.Count; } 
        }

        public bool IsReadOnly
        {
            get { return false; } 
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue currentValue;
            if (this.dictionary.TryGetValue(item.Key, out currentValue) && Object.Equals(item.Value, currentValue) && this.dictionary.Remove(item.Key))
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
                return true;
            }
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator(); 
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.dictionary.GetEnumerator(); 
        }
    }
}
