using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;
using System.Diagnostics;

namespace ToDoList_Flout.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCategoryPage : ContentPage
	{

        public Category Category { get; set; }


        public NewCategoryPage ()
		{
			InitializeComponent ();

            Category = new Category
            {
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Category);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelSuccessRate.Text = e.NewValue.ToString("f0");
        }

    }


}