using to.Models;
using Alina.Models;

namespace Alina.Storage
{
    public class StorageService
    {
        private readonly IStorage<lab1Data> _storage;

        public StorageService(IStorage<lab1Data> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }

        public int GetNumberOfItems()
        {
            return _storage.All.Count;
        }
    }
} 