using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livestock_Management_System
{
    public class MainViewModel
    {
        public ObservableCollection<Animal> Animals { get; set; }
        readonly Database _database;

        public MainViewModel()
        {
            Animals = new();
            _database = new();
            _database.ReadItems().ForEach(x => Animals.Add(x));

        }
    }
}
