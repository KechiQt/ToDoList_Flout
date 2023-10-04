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
        }

        async Task Delete()
        {
            await DataStoreItems.UpdateItemAsync(Item);
        }
    }
}
