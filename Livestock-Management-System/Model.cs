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
    public double MilkCost = 9.4;
    public double WoolCost = 6.2;
    public double Taxes = 0.02;
    public override string ToString()
    {
        return $"{this.GetType().Name,-15}{this.Name,-5}{this.Type,-5}{this.Id,-5}{this.Cost,-5}{this.Weight,-5}{this.Colour,-5}";
    }
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

