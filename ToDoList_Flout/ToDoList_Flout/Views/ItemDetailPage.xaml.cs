using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;

namespace ToDoList_Flout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public Item Item { get; set; }


        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            Item = new Item
            {
            };

            viewModel = new ItemDetailViewModel(Item);
            BindingContext = viewModel;
        }
        
        public void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //if (label != null)
            //    label.Text = "Вы выбрали " + e.NewDate.ToString("dd/MM/yyyy"); ;
        }

        public void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }

        void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (Item != null)
                if (selectedIndex != -1)
                    Item.Category = picker.Items[selectedIndex];
        }

        async void GoToBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}