using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_Flout.Models;

namespace ToDoList_Flout.Services
{
    public class DataStoreCategories : IDataStore<Category, int>
    {
        List<Category> items;

        public DataStoreCategories()
        {
            items = new List<Category>();
            var mockItems = new List<Category>
            {
                //new Category { Id= 0, Title = "Defaut", Description = "Defaut Description",  SuccessRate = 50},
                //new Category { Id= 1, Title = "Personal", Description = "Personal Description",  SuccessRate = 50},
                //new Category { Id= 2, Title = "Purchases", Description = "Purchases Description",  SuccessRate = 50},
                //new Category { Id= 3, Title = "Wish List", Description = "Wish List Description",  SuccessRate = 50},
                //new Category { Id= 4, Title = "Study", Description = "Study Description",  SuccessRate = 50}
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Category item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Category item)
        {
            var oldItem = items.Where((Category arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Category arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}