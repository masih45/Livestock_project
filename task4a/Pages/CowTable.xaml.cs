using Microsoft.Maui.Controls;
using SQLite;
using System.Collections.ObjectModel;
using task4a;

namespace task4a.Pages;

public partial class CowTable : ContentPage
{
    // Collection property to hold the list of Cow objects
    public ObservableCollection<Cow> CowsCollection { get; set; }

    public CowTable()
    {
        InitializeComponent();

        CowsCollection = new ObservableCollection<Cow>();
        PopulateCowsCollection();
        BindingContext = this;
    }
    // Method to populate the CowsCollection with data from your database
    private void PopulateCowsCollection()
    {
        var dbPath = App.DatabasePath;
        using (var conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<Cow>();

            var cows = conn.Table<Cow>().ToList();
            foreach (var cow in cows)
            {
                CowsCollection.Add(cow);
            }
        }
    }
}