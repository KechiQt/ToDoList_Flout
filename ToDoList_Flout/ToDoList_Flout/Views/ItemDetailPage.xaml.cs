using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToDoList_Flout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        private CategoriesViewModel categoryViewModel;


        public Item Item { get; set; }


        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            Item = viewModel.Item;

            categoryViewModel = new CategoriesViewModel();

            LoadCategories();

        }

        public ItemDetailPage()
        {
            InitializeComponent();

            Item = new Item
            {
            };

            viewModel = new ItemDetailViewModel(Item);
            BindingContext = viewModel;

            categoryViewModel = new CategoriesViewModel();

            LoadCategories();
        }

        async void LoadCategories()
        {
            try
            {
                var categories = await categoryViewModel.DataStoreCategories.GetItemsAsync(true);

                List<string> CategoriesTitle = new List<string>();

                int SelectedIndex = 0;
                int i = 0;
                foreach (var category in categories)
                {
                    CategoriesTitle.Add(category.Title);

                    if (category.Title == Item.Category)
                        SelectedIndex = i;

                    i += 1;
                }

                PickerCategory.ItemsSource = CategoriesTitle;

                if (CategoriesTitle.Count > 0)
                    PickerCategory.SelectedIndex = SelectedIndex;

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


        public void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //if (label != null)
            //    label.Text = "Вы выбрали " + e.NewDate.ToString("dd/MM/yyyy"); ;
        }

        public void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelImportance.Text = Math.Round(e.NewValue).ToString();
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
            if (Item != null) {
                if (string.IsNullOrWhiteSpace(viewModel.Item.Text) != true)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Alert", "Fill in the task name!", "OK");
                }
            }
        }


    }
}