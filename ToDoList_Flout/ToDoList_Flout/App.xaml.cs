using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoList_Flout.Views;
using ToDoList_Flout.Services;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToDoList_Flout
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<FireBaseDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
