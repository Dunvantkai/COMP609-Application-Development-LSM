namespace Livestock_Management_System;

public partial class DatabasePage : ContentPage
{
    private readonly Database _database;
    public DatabasePage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        DataListView.ItemsSource = vm.Animals;
        _database = new Database();

    }
    private void OnInsertAnimalButtonClicked(object sender, EventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;
        var animalType = Util.VerifyAnimalType(TypeEntry.Text);
        var cost = Util.VerifyCost(CostEntry.Text);
        var weight = Util.VerifyWeight(WeightEntry.Text);
        var colour = Util.VerifyColour(ColourEntry.Text);
        var product = Util.VerifyProduct(ProductEntry.Text);
        if (animalType == Util.BAD_STRING)
        {
            DisplayAlert("Invalid Input", "Please select a valid livestock type.", "OK");
            return;
        }
        else if (cost == Util.BAD_STRING)
        {
            DisplayAlert("Invalid Input", "Please select a valid cost.", "OK");
            return;
        }
        else if (weight == Util.BAD_STRING)
        {
            DisplayAlert("Invalid Input", "Please select a valid weight.", "OK");
            return;
        }
        else if (colour == Util.BAD_STRING)
        {
            DisplayAlert("Invalid Input", "Please select a valid colour.", "OK");
            return;
        }
        else if (product == Util.BAD_STRING)
        {
            DisplayAlert("Invalid Input", "Please select a valid product.", "OK");
            return;
        }
        else
        {
            Animal newAnimal = null;
            if (animalType.Equals("Cow", StringComparison.OrdinalIgnoreCase))
            {
                newAnimal = new Cow
                {
                    Cost = double.Parse(cost),
                    Weight = double.Parse(weight),
                    Colour = colour,
                    Milk = double.Parse(product)
                };
            }
            else if (animalType.Equals("Sheep", StringComparison.OrdinalIgnoreCase))
            {

                newAnimal = new Sheep
                {
                    Cost = double.Parse(cost),
                    Weight = double.Parse(weight),
                    Colour = colour,
                    Wool = double.Parse(product)
                };
            }

            int result = _database.InsertItem(newAnimal);
            viewModel.Animals.Add(newAnimal);


            TypeEntry.Text = string.Empty;
            CostEntry.Text = string.Empty;
            WeightEntry.Text = string.Empty;
            ColourEntry.Text = string.Empty;
            ProductEntry.Text = string.Empty;
        }


    }
    private void OnDeleteAnimalButtonClicked(object sender, EventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;
        if (int.TryParse(DeleteIdEntry.Text, out var id))
        {
            // Check if the animal exists in the database
            var animalDelete = viewModel.Animals.FirstOrDefault(animal => animal.Id == id);
            if (animalDelete != null)
            {
                // If it exists, delete it
                _database.DeleteItem(animalDelete);
                viewModel.Animals.Remove(animalDelete);
            }
            else
            {
                DisplayAlert("Error", "Animal not found!", "OK");
            }
        }
        else
        {
            DisplayAlert("Invalid Input", "Please select a valid Id.", "OK");

        }
        DeleteIdEntry.Text = string.Empty;

    }
    private void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;
        var cost = Util.VerifyCost(UpdateCostEntry.Text);
        var weight = Util.VerifyWeight(UpdateWeightEntry.Text);
        var colour = Util.VerifyColour(UpdateColourEntry.Text);
        var product = Util.VerifyProduct(UpdateProductEntry.Text);
        if (int.TryParse(UpdateIdEntry.Text, out var id))
        {
            // Check if the animal exists in the database
            var animalUpdate = viewModel.Animals.FirstOrDefault(animal => animal.Id == id);
            if (animalUpdate != null)
            {
                if (cost == Util.BAD_STRING)
                {
                    DisplayAlert("Invalid Input", "Please select a valid cost.", "OK");
                    return;
                }
                else if (weight == Util.BAD_STRING)
                {
                    DisplayAlert("Invalid Input", "Please select a valid weight.", "OK");
                    return;
                }
                else if (colour == Util.BAD_STRING)
                {
                    DisplayAlert("Invalid Input", "Please select a valid colour.", "OK");
                    return;
                }
                else if (product == Util.BAD_STRING)
                {
                    DisplayAlert("Invalid Input", "Please select a valid product.", "OK");
                    return;
                }
                else
                {

                    if (animalUpdate is Cow cow)
                    {

                        animalUpdate.Cost = double.Parse(cost);
                        animalUpdate.Weight = double.Parse(weight);
                        animalUpdate.Colour = colour;
                        cow.Milk = double.Parse(product);
                    }
                    else if (animalUpdate is Sheep sheep)
                    {


                        animalUpdate.Cost = double.Parse(cost);
                        animalUpdate.Weight = double.Parse(weight);
                        animalUpdate.Colour = colour;
                        sheep.Wool = double.Parse(product);
                    }

                    int result = _database.UpdateItem(animalUpdate);
                    viewModel.Animals.Remove(animalUpdate);
                    viewModel.Animals.Add(animalUpdate);


                    UpdateIdEntry.Text = string.Empty;
                    UpdateCostEntry.Text = string.Empty;
                    UpdateWeightEntry.Text = string.Empty;
                    UpdateColourEntry.Text = string.Empty;
                    UpdateProductEntry.Text = string.Empty;
                }
            }

        }
        else
        {
            DisplayAlert("Invalid Input", "Please select a valid ID.", "OK");

        }
    }
}