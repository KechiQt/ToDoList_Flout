using System;

namespace ToDoList_Flout.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SuccessRate { get; set; }
    }
}