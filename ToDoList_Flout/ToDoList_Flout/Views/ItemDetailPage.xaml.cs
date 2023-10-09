using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ToDoList_Flout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        private CategoriesViewModel categoryViewModel;

        bool PanelIsShow = false;

        public Item Item { get; set; }


        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            Item = viewModel.Item;

            categoryViewModel = new CategoriesViewModel();

            LoadCategories();

            this.SizeChanged += (object sender, EventArgs e) =>
            {
                this.HideSlidingPanel();
            };
        }

        private async void HideSlidingPanel()
        {
            TaskTitle.TranslationX = this.Width;
            TaskDescription.TranslationX = this.Width;
            TaskDate.TranslationX = this.Width;
            TaskImportance.TranslationX = this.Width;
            TaskCategory.TranslationX = this.Width;

            await WaitAndExecute(50, () => {
                АnimatedРage();
                PanelIsShow = !PanelIsShow;
            }); 
         }

        protected async Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await Task.Delay(milisec); actionToExecute();
        }

        private async void АnimatedРage()
        {
            await TaskTitle.TranslateTo(this.Width - PanelPage.Width, 0, 150, Easing.SinInOut);
            await TaskDescription.TranslateTo(this.Width - PanelPage.Width, 0, 130, Easing.SinInOut);
            await TaskDate.TranslateTo(this.Width - PanelPage.Width, 0, 110, Easing.SinInOut);
            await TaskImportance.TranslateTo(this.Width - PanelPage.Width, 0, 100, Easing.SinInOut);
            await TaskCategory.TranslateTo(this.Width - PanelPage.Width, 0, 90, Easing.SinInOut);
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}