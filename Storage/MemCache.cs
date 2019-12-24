using System;
using System.Collections.Generic;
using System.Linq;
using Alina.Models;
using to.Models;

namespace Alina.Storage
{
    public class MemCache : IStorage<lab1Data>
    {
        private object _sync = new object();
        private List<lab1Data> _memCache = new List<lab1Data>();
        public lab1Data this[Guid id] 
        { 
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectLabDataException($"No LabData with id {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectLabDataException("Cannot request LabData with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<lab1Data> All => _memCache.Select(x => x).ToList();

        public void Add(lab1Data value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectLabDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
        public string StorageType => $"{nameof(MemCache)}";
    }
}