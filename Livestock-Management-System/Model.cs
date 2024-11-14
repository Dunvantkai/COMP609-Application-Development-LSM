using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livestock_Management_System;
 public class Animal
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Type { get; set; }
    public double Cost { get; set; }
    public double Weight { get; set; }
    public string Colour { get; set; }
    public string AnimalTypeName => GetType().Name;
}
    [SQLite.Table("Cow")]
    public class Cow : Animal
    {
        public double Milk { get; set; }
        public override string ToString()
        {
            return base.ToString() + $"{this.Milk}";
        }
    }
    [SQLite.Table("Sheep")]
    public class Sheep : Animal
    {
        public double Wool { get; set; }
        public override string ToString()
        {
            return base.ToString() + $"{this.Wool}";
        }
    }

