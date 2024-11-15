﻿using System;
namespace Livestock_Management_System;

public class MainViewModel
{
    public ObservableCollection<Animal> Animals { get; set; }
    readonly Database _database;

    public MainViewModel()
    {
        Animals = new();
        _database = new();
        _database.ReadAnimals().ForEach(x => Animals.Add(x));

    }
}
