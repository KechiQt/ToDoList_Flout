using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoList_Flout.Models;
using ToDoList_Flout.ViewModels;
using System.Threading.Tasks;

namespace ToDoList_Flout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryDetaliPage : ContentPage
	{
        private CategoryDetaliViewModel viewModel;

        public Category Category { get; set; }

        bool PanelIsShow = false;

        public CategoryDetaliPage(CategoryDetaliViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            this.SizeChanged += (object sender, EventArgs e) =>
            {
                this.HideSlidingPanel();
            };
        }

        public CategoryDetaliPage()
        {
            InitializeComponent();

            Category = new Category
            {
            };

            viewModel = new CategoryDetaliViewModel(Category);
            BindingContext = viewModel;

            this.SizeChanged += (object sender, EventArgs e) =>
            {
                this.HideSlidingPanel();
            };
        }

        public void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelSuccessRate.Text = e.NewValue.ToString("f0");
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

    }
}