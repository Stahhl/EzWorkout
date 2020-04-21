using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzWorkout.Models;

namespace EzWorkout.Services
{
    public class MockDataStore : IDataStore<Workout>
    {
        readonly List<Workout> items;

        public MockDataStore()
        {
            items = new List<Workout>()
            {
                new Workout { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Workout { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Workout { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Workout { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Workout { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Workout { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Workout item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Workout item)
        {
            var oldItem = items.Where((Workout arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Workout arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Workout> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Workout>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}