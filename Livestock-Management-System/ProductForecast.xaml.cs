namespace Livestock_Management_System;

public partial class ProductForecast : ContentPage
{
    private readonly Database _database;
	public ProductForecast()
	{
		InitializeComponent();
        _database = new Database();
    }
    private void OnForcastButtonClicked(object sender, EventArgs e)
    {
        var currentAnimals = _database.ReadAnimals();
        var (averageCowProfit, averageSheepProfit) = _database.CalculateAverageProfit(currentAnimals);
        string typeInput = TypeEntry.Text?.Trim();
        string quantityInput = QuantityEntry.Text?.Trim();
        if (!int.TryParse(quantityInput, out int quantity) || quantity <= 0)
        {
            DisplayAlert("Input Error", "Please enter a valid positive number for the quantity.", "OK");
            return;
        }

        double totalProfit = 0;

        if (string.Equals(typeInput, "Cow", StringComparison.OrdinalIgnoreCase))
        {
            totalProfit = Math.Round(averageCowProfit, 1) * quantity;
        }
        else if (string.Equals(typeInput, "Sheep", StringComparison.OrdinalIgnoreCase))
        {
            totalProfit = Math.Round(averageSheepProfit, 2) * quantity;
        }
        else
        {
            DisplayAlert("Input Error", "Please enter a valid livestock type (Cow/Sheep).", "OK");
            return;
        }

        ResultsListView.ItemsSource = new[]
        {

        $"Buying {quantity} {typeInput}(s) would bring in an estimated daily profit: ${totalProfit:F1}"
    };
    }
}