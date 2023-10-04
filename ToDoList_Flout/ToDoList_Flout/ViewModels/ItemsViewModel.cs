using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using ToDoList_Flout.Models;
using ToDoList_Flout.Views;

namespace ToDoList_Flout.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command MoveToTopCommand { protected set; get; }
        public Command MoveToBottomCommand { protected set; get; }
        public Command RemoveCommand { protected set; get; }


        public ItemsViewModel()
        {
            Title = "Task";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MoveToTopCommand = new Command(MoveToTop);
            MoveToBottomCommand = new Command(MoveToBottom);
            RemoveCommand = new Command(Remove);


            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStoreItems.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStoreItems.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void MoveToTop(object categoryObj)
        {
            Item Item = categoryObj as Item;
            if (Item == null) return;

            int oldIndex = Items.IndexOf(Item);

            if (oldIndex > 0)
                Items.Move(oldIndex, oldIndex - 1);
        }

        private void MoveToBottom(object categoryObj)
        {
            Item Item = categoryObj as Item;
            if (Item == null) return;

            int oldIndex = Items.IndexOf(Item);

            if (oldIndex < Items.Count - 1)
                Items.Move(oldIndex, oldIndex + 1);
        }

        private void Remove(object categoryObj)
        {
            Item Item = categoryObj as Item;
            if (Item == null) return;

            DataStoreCategories.DeleteItemAsync(Items.IndexOf(Item));
            Items.Remove(Item);
        }

        public int GetCurrentID()
        {
            int maxID = 0;

            if (Items.Count > 0)
                maxID = Items.Max(x => x.Id);



            return maxID;
        }

    }
}