using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ToDoList_Flout.Models;
using ToDoList_Flout.Views;
using ToDoList_Flout.Services;
using System.Collections.Generic;

namespace ToDoList_Flout.ViewModels
{
    class CategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command MoveToTopCommand { protected set; get; }
        public Command MoveToBottomCommand { protected set; get; }
        public Command RemoveCommand { protected set; get; }


        public CategoriesViewModel()
        {
            Title = "Categories";
            Items = new ObservableCollection<Category>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MoveToTopCommand = new Command(MoveToTop);
            MoveToBottomCommand = new Command(MoveToBottom);
            RemoveCommand = new Command(Remove);

            MessagingCenter.Subscribe<NewCategoryPage, Category>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Category;
                Items.Add(newItem);
                await DataStoreCategories.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Items.Clear();
                var items = await DataStoreCategories.GetItemsAsync(true);
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
            Category category = categoryObj as Category;
            if (category == null) return;

            int oldIndex = Items.IndexOf(category);

            if (oldIndex > 0)
                Items.Move(oldIndex, oldIndex - 1);
        }

        private void MoveToBottom(object categoryObj)
        {
            Category category = categoryObj as Category;
            if (category == null) return;

            int oldIndex = Items.IndexOf(category);

            if (oldIndex < Items.Count - 1)
                Items.Move(oldIndex, oldIndex + 1);
        }

        private void Remove(object categoryObj)
        {
            Category category = categoryObj as Category;
            if (category == null) return;

            Items.Remove(category);
            DataStoreCategories.DeleteItemAsync(category.Id);
        }

    }
}
