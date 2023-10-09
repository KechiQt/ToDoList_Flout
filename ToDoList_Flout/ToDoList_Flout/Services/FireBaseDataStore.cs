using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_Flout.Models;

using Firebase.Database;
using Firebase.Database.Query;


namespace ToDoList_Flout.Services
{
    public class FireBaseDataStore : IDataStore<Item, int>
    {
        List<Item> items;

        FirebaseClient firebase = new FirebaseClient("https://todo-base-19054-default-rtdb.europe-west1.firebasedatabase.app/");

        int databaseId;


        public FireBaseDataStore()
        {
            items = new List<Item>();
            Object databaseIdObjet;
            Random _random = new Random();

            if (App.Current.Properties.TryGetValue("databaseId", out databaseIdObjet))
            {
                databaseId = (int)databaseIdObjet;
            }
            else
            {
                databaseId = _random.Next();

                App.Current.Properties.Add("databaseId", databaseId);
            }

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            await firebase
              .Child("Items_" + databaseId)
              .PostAsync(item);   


            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var itemToUpdate = (await firebase
                .Child("Items_" + databaseId)
                .OnceAsync<Item>()).Where(x => x.Object.Id == item.Id).FirstOrDefault();
            
            if (itemToUpdate != null)
            {
                await firebase
                  .Child("Items_" + databaseId)
                  .Child(itemToUpdate.Key)
                  .PutAsync(item);
            }



            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var itemToDelete = (await firebase
                .Child("Items_" + databaseId)
                .OnceAsync<Item>()).Where(x => x.Object.Id == id).FirstOrDefault();

            if (itemToDelete != null)
            {
                await firebase
                  .Child("Items_" + databaseId)
                  .Child(itemToDelete.Key)
                  .DeleteAsync();
            }


            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            items = (await firebase
              .Child("Items_" + databaseId)
              .OnceAsync<Item>()).Select(item => new Item
              {
                  Id = item.Object.Id,
                  Text = item.Object.Text,
                  Description = item.Object.Description,
                  Date = item.Object.Date,
                  Importance = item.Object.Importance,
                  Category = item.Object.Category

              }).ToList();

            return items;
        }
    }
}