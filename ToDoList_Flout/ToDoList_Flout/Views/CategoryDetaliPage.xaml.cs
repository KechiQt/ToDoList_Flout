using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;

namespace ToDoList_Flout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryDetaliPage : ContentPage
	{
        private CategoryDetaliViewModel viewModel;

        public Category Category { get; set; }


        public CategoryDetaliPage(CategoryDetaliViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public CategoryDetaliPage()
        {
            InitializeComponent();

            Category = new Category
            {
            };

            viewModel = new CategoryDetaliViewModel(Category);
            BindingContext = viewModel;
        }

        public void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelSuccessRate.Text = e.NewValue.ToString("f0");
        }


    }
}