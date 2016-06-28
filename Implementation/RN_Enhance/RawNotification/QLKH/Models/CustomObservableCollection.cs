#region using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

#endregion
namespace QLKH.Models
{
    public interface ICustomObservableCollection<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }
    [Serializable]
    public class CustomObservableCollection<T> : ICustomObservableCollection<T>
    {
        #region Fields

        private const string CountString = "Count";
        private const string IndexerName = "Item[]";

        private readonly SimpleMonitor _monitor;

        #endregion


        #region Properties

        protected ICollection<T> InnerCollection { get; private set; }


        #endregion

        #region Constructors

        public CustomObservableCollection(ICollection<T> innerCollection)
        {
            this._monitor = new SimpleMonitor();

            if (innerCollection == null)
            {
                throw new ArgumentNullException("innerCollection");
            }

            InnerCollection = innerCollection;
        }

        #endregion

        #region Implementation

        #region Implementation of INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                using (BlockReentrancy())
                {
                    CollectionChanged(this, e);
                }
            }
        }

        // overload bị lỗi mà anh ta dùng
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
        }

        // em dùng overload khác của anh ta và hết lỗi
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return InnerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            CheckReentrancy();
            InnerCollection.Add(item);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, InnerCollection.Count);
        }

        public void Clear()
        {
            CheckReentrancy();
            InnerCollection.Clear();
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionReset();
        }

        public bool Contains(T item)
        {
            return InnerCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            CheckReentrancy();
            int k = 0;
            int index = -1;
            foreach (var i in InnerCollection)
            {
                if (object.ReferenceEquals(item,i))
                {
                    index = k;
                    break;
                }
                k++;
            }
            if (index == -1)
            {
                return false;
            }

            bool result = InnerCollection.Remove(item);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item,index);
            return result;
        }

        public int Count
        {
            get { return InnerCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return InnerCollection.IsReadOnly; }
        }

        #endregion

        #region Simple Monitor

        protected IDisposable BlockReentrancy()
        {
            this._monitor.Enter();
            return this._monitor;
        }

        protected void CheckReentrancy()
        {
            if ((this._monitor.Busy && (CollectionChanged != null)) && (CollectionChanged.GetInvocationList().Length > 1))
            {
                throw new InvalidOperationException("Collection Reentrancy Not Allowed");
            }
        }

        [Serializable]
        private class SimpleMonitor : IDisposable
        {
            private int _busyCount;

            public bool Busy
            {
                get { return this._busyCount > 0; }
            }

            public void Enter()
            {
                this._busyCount++;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                this._busyCount--;
            }

            #endregion
        }

        #endregion

        #endregion
    }
}