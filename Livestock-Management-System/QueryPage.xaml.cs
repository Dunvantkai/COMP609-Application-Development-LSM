namespace Livestock_Management_System;

public partial class QueryPage : ContentPage
{
	private readonly Database _database;
	public QueryPage()
	{
		InitializeComponent();
		_database = new Database();
    }

    private void OnQueryButtonClicked(object sender, EventArgs e)
    {
        string livestockType = string.IsNullOrWhiteSpace(TypeEntry.Text) ? "All" : TypeEntry.Text;
        string animalColour = string.IsNullOrWhiteSpace(ColourEntry.Text) ? "All" : ColourEntry.Text;

        var animalType = Util.VerifyAnimalType(livestockType);
        var colour = Util.VerifyColour(animalColour);
        int totalAnimalCount = _database.totalAnimals();
        List<Animal> animals = _database.QueryAnimals(animalType, colour);
        int animalCount = animals.Count;
        double animalCountPercent = totalAnimalCount > 0 ? (animalCount / (double)totalAnimalCount) * 100 : 0;
        double dailyTax = _database.CalculateTotalTax(animals);
        double dailyProfit = _database.CalculateTotalProfit(animals);
        double averageWeight = _database.CalculateAverageWeight(animals);
        double totalProduce = _database.CalculateTotalProduce(animals);

        string details = $"Number of Animals ({animalType} in {colour} colour): {animalCount}\n" +
                         $"Daily Tax of selected Animals : ${dailyTax:F1}\n" +
                         $"{(dailyProfit > 0 ? "Profit" : "Loss")}: ${dailyProfit:F1}\n" +
                         $"Percentage of selected Animals : {animalCountPercent:F0}%\n" +
                         $"Average Weight: {averageWeight:F1}kg\n" +
                         $"Total Produce: {totalProduce:F1}kg";

        ResultsListView.ItemsSource = new List<string> { details };
    }

}