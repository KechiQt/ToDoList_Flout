using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList_Flout.Models
{
    public enum MenuItemType
    {
        Task,
        Categories,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
