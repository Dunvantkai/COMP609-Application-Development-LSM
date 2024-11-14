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
}