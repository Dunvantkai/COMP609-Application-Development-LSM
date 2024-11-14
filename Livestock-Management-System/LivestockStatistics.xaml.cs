namespace Livestock_Management_System;

public partial class LivestockStatistics : ContentPage
{
    private readonly Database _database;
    public LivestockStatistics()
	{
        InitializeComponent();
        _database = new Database();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var currentAnimals = _database.ReadAnimals();
        double monthlyTax = _database.CalculateTotalTax(currentAnimals) * 30;
        double dailyTotalProfit = _database.CalculateTotalProfit(currentAnimals);
        double averageWeight = _database.CalculateAverageWeight(currentAnimals);
        var (averageCowProfit, averageSheepProfit) = _database.CalculateAverageProfit(currentAnimals);
        string mostProfitableLivestock = averageCowProfit > averageSheepProfit ? "Cow" : "Sheep";
        string leastProfitableLivestock = averageCowProfit < averageSheepProfit ? "Cow" : "Sheep";

        StatisticsListView.ItemsSource = new[]
        {
            $"30 days govt tax: ${monthlyTax:F0}",
            $"Farm Daily Profit: ${dailyTotalProfit:F1}",
            $"Average Weight of All Livestock: {averageWeight:F1} kg\n",
            $"Based on current livestock data:",
            $"On average, a single cow makes daily profit: ${averageCowProfit:F1}",
            $"On average, a single sheep makes daily profit: ${averageSheepProfit:F1}",
            $"Therefore, {mostProfitableLivestock} are more profitable than {leastProfitableLivestock}"
        };
    }

}