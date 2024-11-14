
namespace Livestock_Management_System;
public class Database
{
    private readonly SQLiteConnection _connection;
    
    public Database()
    {
        try
        {
            var dataDir = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(dataDir, "FarmDataOriginal.db");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTables<Cow, Sheep>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization error: {ex.Message}");
            throw;

        }
    }

    public List<Animal> ReadAnimals()
    {
        var animals = new List<Animal>();
        var cows = _connection.Table<Cow>().ToList();
        animals.AddRange(cows.Cast<Animal>());
        var sheep = _connection.Table<Sheep>().ToList();
        animals.AddRange(sheep.Cast<Animal>());

        return animals;
    }
    public int totalAnimals()
    {
        int cowCount = _connection.Table<Cow>().Count();
        int sheepCount = _connection.Table<Sheep>().Count();
        return cowCount + sheepCount;
    }
    public double CalculateTotalTax(List<Animal> animals)
    {
        double totalTax = 0;
        foreach (var animal in animals)
        {
            double dailyTax = animal.Weight * animal.Taxes;
            totalTax += dailyTax;
        }
        return totalTax;
    }

    public double CalculateTotalProfit(List<Animal> animals)
    {
        double totalProfit = 0;
        foreach (var animal in animals)
        {
            double income = 0;
            double dailyTax = animal.Weight * animal.Taxes;

            if (animal is Cow cow)
            {
                income = cow.Milk * cow.MilkCost;
            }
            else if (animal is Sheep sheep)
            {
                income = sheep.Wool * sheep.WoolCost;
            }

            double profit = income - animal.Cost - dailyTax;
            totalProfit += profit;
        }
        return totalProfit;
    }

    public double CalculateAverageWeight(List<Animal> animals)
    {
        if (animals.Count == 0)
            return 0;

        double totalWeight = animals.Sum(a => a.Weight);
        return totalWeight / animals.Count;
    }

    public double CalculateTotalProduce(List<Animal> animals)
    {
        double totalMilk = 0;
        double totalWool = 0;

        foreach (var animal in animals)
        {
            if (animal is Cow cow)
            {
                totalMilk += cow.Milk;
            }
            else if (animal is Sheep sheep)
            {
                totalWool += sheep.Wool;
            }
        }
        return totalMilk + totalWool;
    }
    public List<Animal> QueryAnimals(string type, string colour)
    {
        List<Animal> allAnimals = ReadAnimals();

        var filteredAnimals = allAnimals.Where(x =>
            (type == "All" || x.GetType().Name == type) &&
            (colour == "All" || x.Colour == colour)
        ).ToList();

        return filteredAnimals;
    }
    public (double AverageCowProfit, double AverageSheepProfit) CalculateAverageProfit(List<Animal> animals)
    {
        double totalCowProfit = 0;
        double totalSheepProfit = 0;
        int cowCount = 0;
        int sheepCount = 0;

        foreach (var animal in animals)
        {
            double income = 0;
            double dailyTax = animal.Weight * animal.Taxes;

            if (animal is Cow cow)
            {
                income = cow.Milk * cow.MilkCost;
                double profit = income - cow.Cost - dailyTax;
                totalCowProfit += profit;
                cowCount++;
            }
            else if (animal is Sheep sheep)
            {
                income = sheep.Wool * sheep.WoolCost;
                double profit = income - sheep.Cost - dailyTax;
                totalSheepProfit += profit;
                sheepCount++;
            }
        }

        double averageCowProfit = cowCount > 0 ? totalCowProfit / cowCount : 0;
        double averageSheepProfit = sheepCount > 0 ? totalSheepProfit / sheepCount : 0;

        return (averageCowProfit, averageSheepProfit);
    }



    public int InsertItem(Animal item)
    {
        return _connection.Insert(item);
    }

    public int DeleteItem(Animal item)
    {
        return _connection.Delete(item);
    }

    public int UpdateItem(Animal item)
    {
        return _connection.Update(item);
    }
}


