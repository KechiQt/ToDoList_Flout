using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList_Flout.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesPage : ContentPage
	{
        private CategoriesViewModel viewModel;

		public CategoriesPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new CategoriesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var category = args.SelectedItem as Category;
            if (category == null)
                return;

            await Navigation.PushAsync(new CategoryDetaliPage(new CategoryDetaliViewModel(category)));

            // Manually deselect item.
            CategoriesListVeiw.SelectedItem = null;
        }


        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCategoryPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

    }
} 