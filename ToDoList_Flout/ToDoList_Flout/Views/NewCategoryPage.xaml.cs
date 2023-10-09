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

        bool PanelIsShow = false;

        public Category Category { get; set; }

        public NewCategoryPage ()
		{
			InitializeComponent ();

            Category = new Category
            {
            };

            BindingContext = this;

            this.SizeChanged += (object sender, EventArgs e) =>
            {
                this.HideSlidingPanel();
            };
        }

        private async void HideSlidingPanel()
        {
            TaskTitle.TranslationX = this.Width;
            TaskDescription.TranslationX = this.Width;
            TaskImportance.TranslationX = this.Width;

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
            await TaskImportance.TranslateTo(this.Width - PanelPage.Width, 0, 100, Easing.SinInOut);
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