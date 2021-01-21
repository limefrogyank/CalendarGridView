using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Interop;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;

namespace McPhersonApps
{
    
    public class CalendarItemList<ICalendarItem> : Microsoft.UI.Xaml.Interop.INotifyCollectionChanged, IList, IItemsRangeInfo
    {
        // Cache for the file data that is currently being used
        private ItemCacheManager<ICalendarItem> itemCache;

        // Dispatcher so we can marshal calls back to the UI thread
        private CoreDispatcher _dispatcher;

        // Total number of files available
        private int _count = 1000;

        private DateTime _pivotDay;

        
        public CalendarItemList(Func<List<DateTime>, CancellationToken, Task<ICalendarItem[]>> fetchDataCallback)
        {
            // Setup the dispatcher for the UI thread
            _dispatcher = Microsoft.UI.Xaml.Window.Current.Dispatcher;

            _fetchDataCallback = (range, c) => fetchDataCallback(ConvertIndexRangeToDateTimeRange(range), c);

            _pivotDay = DateTimeOffset.Now.Date;

            

            // The ItemCacheManager does most of the heavy lifting. We pass it a callback that it will use to actually fetch data, and the max size of a request
            itemCache = new ItemCacheManager<ICalendarItem>(_fetchDataCallback, 50);
            itemCache.CacheChanged += ItemCache_CacheChanged;
        }

        private Func<ItemIndexRange, CancellationToken, Task<ICalendarItem[]>> _fetchDataCallback;

        private List<DateTime> ConvertIndexRangeToDateTimeRange(ItemIndexRange range)
        {
            var dates = new List<DateTime>();
            if (SplitMonths)
            {

            }
            else
            {
                //today is the middle of the list for now.
                int todayIndex = _count / 2;
                int roundedUp = (int)Math.Ceiling((double)todayIndex / 7) * 7;  // rounding up means splitting the listview exactly in the middle scrolls to the right "middle" date
                                                                                //now adjust pivotDay to correct day of the week for calendar, but this only works for Sunday starts
                roundedUp += (int)_pivotDay.DayOfWeek;

                
                for (var i = range.FirstIndex; i <= range.LastIndex; i++)
                {
                    dates.Add(_pivotDay + TimeSpan.FromDays(i - roundedUp));
                }
            }
            return dates;
        }

        public int GetIndexForDate(DateTime dateTime)
        {
            if (SplitMonths)
            {
                throw new NotImplementedException();
            }
            else
            {
                //today is the middle of the list for now.
                int todayIndex = _count / 2;
                int roundedUp = (int)Math.Ceiling((double)todayIndex / 7) * 7;  // rounding up means splitting the listview exactly in the middle scrolls to the right "middle" date
                                                                                //now adjust pivotDay to correct day of the week for calendar, but this only works for Sunday starts
                roundedUp += (int)_pivotDay.DayOfWeek;
                var span = dateTime - _pivotDay;
                return roundedUp - (int)span.TotalDays;
            }
        }

        //// Callback from itemcache that it needs items to be retrieved
        //// Using this callback model abstracts the details of this specific datasource from the cache implementation
        //private async Task<T[]> fetchDataCallback(ItemIndexRange batch, CancellationToken ct)
        //{
        //    // Fetch file objects from filesystem
        //    IReadOnlyList<T> results = await _queryResult.GetFilesAsync((uint)batch.FirstIndex, Math.Max(batch.Length, 20)).AsTask(ct);
        //    List<T> items = new List<T>();
        //    if (results != null)
        //    {
        //        for (int i = 0; i < results.Count; i++)
        //        {
        //            // Check if request has been cancelled, if so abort getting additional data
        //            ct.ThrowIfCancellationRequested();
        //            // Create our FileItem object with the file data and thumbnail 
        //            FileItem newItem = await FileItem.fromStorageFile(results[i], ct);
        //            items.Add(newItem);
        //        }
        //    }
        //    return items.ToArray();
        //}

        // Event fired when items are inserted in the cache
        // Used to fire our collection changed event
        private void ItemCache_CacheChanged(object sender, CacheChangedEventArgs<ICalendarItem> args)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(
                    this, 
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace, 
                        new TestBindableVector<ICalendarItem>(new List<ICalendarItem>() { args.oldItem }),
                        new TestBindableVector<ICalendarItem>(new List<ICalendarItem>() { args.newItem }), 
                        args.itemIndex,
                        args.itemIndex));
            }
        }

        private void ResetCollection()
        {
            // Unhook the old change notification
            if (itemCache != null)
            {
                itemCache.CacheChanged -= ItemCache_CacheChanged;
            }

            // Create a new instance of the cache manager
            itemCache = new ItemCacheManager<ICalendarItem>(_fetchDataCallback, 50);
            itemCache.CacheChanged += ItemCache_CacheChanged;
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset,null,null,0,0));
            }
        }


        //async Task UpdateCount()
        //{
        //    _count = (int)await _queryResult.GetItemCountAsync();
        //    if (CollectionChanged != null)
        //    {
        //        CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        //    }
        //}

        public bool SplitMonths { get; set; } = false;


        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Count { get => _count; set => _count = value; }

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public object this[int index] { get => itemCache[index]; set => throw new NotImplementedException(); }

        public event Microsoft.UI.Xaml.Interop.NotifyCollectionChangedEventHandler CollectionChanged;

        //event Microsoft.UI.Xaml.Interop.NotifyCollectionChangedEventHandler Microsoft.UI.Xaml.Interop.INotifyCollectionChanged.CollectionChanged
        //{
        //    add
        //    {
        //        throw new NotImplementedException();
        //    }

        //    remove
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            return IndexOf(value) != -1;
        }

        public int IndexOf(object value)
        {
            return value != null ? itemCache.IndexOf((ICalendarItem)value) : -1;
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void RangesChanged(ItemIndexRange visibleRange, IReadOnlyList<ItemIndexRange> trackedItems)
        {
            //string s = string.Format("* RangesChanged fired: Visible {0}->{1}", visibleRange.FirstIndex, visibleRange.LastIndex);
            //foreach (ItemIndexRange r in trackedItems) { s += string.Format(" {0}->{1}", r.FirstIndex, r.LastIndex); }
            //Debug.WriteLine(s);

            // We know that the visible range is included in the broader range so don't need to hand it to the UpdateRanges call
            // Update the cache of items based on the new set of ranges. It will callback for additional data if required
            itemCache.UpdateRanges(trackedItems.ToArray());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        
    }

    public class TestBindableVector<T> : IList<T>, IBindableVector
    {
        IList<T> implementation;

        public TestBindableVector() { implementation = new List<T>(); }
        public TestBindableVector(IList<T> list) { implementation = new List<T>(list); }

        public T this[int index] { get => implementation[index]; set => implementation[index] = value; }

        public int Count => implementation.Count;

        public virtual bool IsReadOnly => implementation.IsReadOnly;

        public void Add(T item)
        {
            implementation.Add(item);
        }

        public void Clear()
        {
            implementation.Clear();
        }

        public bool Contains(T item)
        {
            return implementation.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            implementation.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return implementation.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return implementation.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            implementation.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return implementation.Remove(item);
        }

        public void RemoveAt(int index)
        {
            implementation.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return implementation.GetEnumerator();
        }

        public object GetAt(uint index)
        {
            return implementation[(int)index];
        }

        public IBindableVectorView GetView()
        {
            return new TestBindableVectorView<T>(implementation);
        }

        public bool IndexOf(object value, out uint index)
        {
            int indexOf = implementation.IndexOf((T)value);

            if (indexOf >= 0)
            {
                index = (uint)indexOf;
                return true;
            }
            else
            {
                index = 0;
                return false;
            }
        }

        public void SetAt(uint index, object value)
        {
            implementation[(int)index] = (T)value;
        }

        public void InsertAt(uint index, object value)
        {
            implementation.Insert((int)index, (T)value);
        }

        public void RemoveAt(uint index)
        {
            implementation.RemoveAt((int)index);
        }

        public void Append(object value)
        {
            implementation.Add((T)value);
        }

        public void RemoveAtEnd()
        {
            implementation.RemoveAt(implementation.Count - 1);
        }

        public uint Size => (uint)implementation.Count;

        public IBindableIterator First()
        {
            return new TestBindableIterator<T>(implementation);
        }
    }


    public class TestBindableVectorView<T> : TestBindableVector<T>, IBindableVectorView
    {
        public TestBindableVectorView(IList<T> list) : base(list) { }

        public override bool IsReadOnly => true;
    }

    public class TestBindableIterator<T> : IBindableIterator
    {
        private readonly IEnumerator<T> enumerator;

        public TestBindableIterator(IEnumerable<T> enumerable) { enumerator = enumerable.GetEnumerator(); }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        public object Current => enumerator.Current;

        public bool HasCurrent => enumerator.Current != null;
    }
}
