using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ToDoList_Flout.Models;
using ToDoList_Flout.Views;
using ToDoList_Flout.Services;
using System.Collections.Generic;

namespace ToDoList_Flout.ViewModels
{
    public class CategoryDetaliViewModel : BaseViewModel
    {

        public Category Category { get; set; }
        public CategoryDetaliViewModel(Category category = null)
        {
            Title = category?.Title;
            Category = category;

        }

    }
}
