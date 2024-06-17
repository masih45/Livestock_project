using SQLite;

namespace task4a.Pages;

public partial class SheepTable : ContentPage
{
    public ObservableCollection<Sheep> CowsCollection { get; set; }
    public SheepTable()
	{
		InitializeComponent();
        CowsCollection = new ObservableCollection<Sheep>();
        PopulateSheepCollection();
        BindingContext = this;
    }
    // Method to populate the CowsCollection with data from your database
    private void PopulateSheepCollection()
    {
        var dbPath = App.DatabasePath;
        using (var conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<Sheep>();

            var sheep = conn.Table<Sheep>().ToList();
            foreach (var sheeps in sheep)
            {
                CowsCollection.Add(sheeps);
            }
        }
    }
}