using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_Flout.Models;

namespace ToDoList_Flout.Services
{
    public class DataStoreItems : IDataStore<Item, int>
    {
        List<Item> items;

        public DataStoreItems()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "First item", Description="This is an item description." },
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "Second item", Description="This is an item description." },
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "Third item", Description="This is an item description." },
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "Sixth item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
#pragma warning disable CS0019 // Оператор "==" невозможно применить к операнду типа "int" и "string".
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
#pragma warning restore CS0019 // Оператор "==" невозможно применить к операнду типа "int" и "string".
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
#pragma warning disable CS0019 // Оператор "==" невозможно применить к операнду типа "int" и "string".
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
#pragma warning restore CS0019 // Оператор "==" невозможно применить к операнду типа "int" и "string".
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}