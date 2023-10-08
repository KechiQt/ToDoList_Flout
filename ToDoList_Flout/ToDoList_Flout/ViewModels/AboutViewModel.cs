using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace ToDoList_Flout.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/KechiQt/ToDoList_Flout")));
        }

        public ICommand OpenWebCommand { get; }
    }
}