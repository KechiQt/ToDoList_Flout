using System;
using System.Threading.Tasks;
using ToDoList_Flout.Models;
using Xamarin.Forms;

namespace ToDoList_Flout.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public Command DeleteCommand { get; set; }

        public Command SaveCommand { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;

            DeleteCommand = new Command(async () => await Delete());
            SaveCommand = new Command(async () => await Save());
        }

        async Task Delete()
        {
            await DataStoreItems.DeleteItemAsync(Item.Id);
        }

        async Task Save()
        {
            await DataStoreItems.UpdateItemAsync(Item);
        }

    }
}
