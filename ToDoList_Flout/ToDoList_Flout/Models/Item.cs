using System;

namespace ToDoList_Flout.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public int Importance { get; set; }
        public string Category { get; set; } = "Defaut";
    }
}