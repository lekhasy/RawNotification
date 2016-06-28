using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawNotification.SharedLibs;
using Windows.Storage;

namespace RawNotification.DotNetCoreDataProviders
{
    public class ListEntitySerializer<T>
    {
        StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
        StorageFile file = null;

        private ListEntitySerializer()
        {

        }

        public static async Task<ListEntitySerializer<T>> CreateListAsync(string fileName)
        {
            ListEntitySerializer<T> rtObject = new ListEntitySerializer<T>();
            await rtObject.InitializeFileAsync(fileName);
            return rtObject;
        }

        private async Task InitializeFileAsync(string FileName)
        {
            file = await folder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
        }

        JSONObjectSerializer<List<T>> serializer = new JSONObjectSerializer<List<T>>();

        private List<T> _List = null;

        private async Task<List<T>> List()
        {
            if (_List == null)
            {
                try
                {
                    using (var stream = (await file.OpenSequentialReadAsync()).AsStreamForRead())
                    {
                        _List = serializer.ReadObjectFromStream(stream);
                    }
                } catch
                {
                    return new List<T>();
                }
            }
            return _List;
        }

        internal async Task<T> First(Func<T, bool> p)
        {
            return (await List()).FirstOrDefault(p);
        }

        internal async Task<T> FirstOrDefault(Func<T, bool> p)
        {
            return ((await List()).FirstOrDefault(p));
        }

        public async Task SaveAsync()
        {
            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                serializer.WriteObjectToStream(await List(), stream);
            }
        }

        public async Task<int> Count()
        {
            return ((await List()).Count);
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public async Task<int> IndexOf(T item)
        {
            return (await List()).IndexOf(item);
        }

        public async Task Insert(int index, T item)
        {
            (await List()).Insert(index, item);
            await SaveAsync();
        }

        public async Task RemoveAt(int index)
        {
            (await List()).RemoveAt(index);
            await SaveAsync();
        }

        public async Task Add(T item)
        {
            (await List()).Add(item);
            await SaveAsync();
        }

        public async Task Clear()
        {
            (await List()).Clear();
            await SaveAsync();
        }

        public async Task<bool> Contains(T item)
        {
            return (await List()).Contains(item);
        }

        public async Task CopyTo(T[] array, int arrayIndex)
        {
            (await List()).CopyTo(array, arrayIndex);
        }

        public async Task<bool> Remove(T item)
        {
            bool result = (await List()).Remove(item);
            await SaveAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetIEnumrableAsync()
        {
            return new List<T>(await List());
        }
    }
}
