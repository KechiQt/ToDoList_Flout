using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ToDoList_Flout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        ItemsViewModel _itemsViewModel;


        private CategoriesViewModel viewModel;
        //private IEnumerable<Category> categories;


        public NewItemPage(ItemsViewModel itemsViewModel)
        {
            InitializeComponent();

            _itemsViewModel = itemsViewModel;

            int CurrentMaxId = _itemsViewModel.GetCurrentID();
            Item = new Item
            {
                Id = CurrentMaxId + 1

            };

            viewModel = new CategoriesViewModel();

            BindingContext = this;
            LoadCategories();
        }

        async void LoadCategories()
        {
            try
            {
                var categories = await viewModel.DataStoreCategories.GetItemsAsync(true);

                List<string> CategoriesTitle = new List<string>();

                foreach (var category in categories)
                {
                    CategoriesTitle.Add(category.Title);
                }

                PickerCategory.ItemsSource = CategoriesTitle;

                if (CategoriesTitle.Count > 0)
                    PickerCategory.SelectedIndex = 0;

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


        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //if (label != null)
            //    label.Text = "Дата не установлена";
        }

        public void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelImportance.Text = Math.Round(e.NewValue).ToString();
        }

        void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
                Item.Category = picker.Items[selectedIndex];
        }
    }
}