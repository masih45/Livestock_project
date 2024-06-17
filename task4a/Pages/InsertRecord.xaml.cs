namespace task4a.Pages;

public partial class InsertRecord : ContentPage
{
    private Database _database;
    private ObservableCollection<Store> _stores;
    public InsertRecord()
    {
        InitializeComponent();
        _database = new Database();
        _stores = new ObservableCollection<Store>();
    }
    private void OnInsertRecordClicked(object sender, EventArgs e)
    {
        string tableName = TableNameEntry.Text?.ToLower();
        double cost = double.TryParse(CostEntry.Text, out double costResult) ? costResult : 0;
        double weight = double.TryParse(WeightEntry.Text, out double weightResult) ? weightResult : 0;
        string colour = ColourEntry.Text;
        double produce = double.TryParse(ProduceEntry.Text, out double produceResult) ? produceResult : 0;

        InsertRecord1(tableName, cost, weight, colour, produce);
    }
    public void InsertRecord1(string tableName, double cost, double weight, string colour, double produce)
    {
        Store newLivestock;
        if (tableName == "cow")
        {
            newLivestock = new Cow { Cost = cost, Weight = weight, Colour = colour, Milk = produce };
        }
        else if (tableName == "sheep")
        {
            newLivestock = new Sheep { Cost = cost, Weight = weight, Colour = colour, Wool = produce };
        }
        else
        {
            DisplayOutput("Invalid table name.");
            return;
        }

        // Validate colour input
        if (!IsValidColour(colour))
        {
            DisplayOutput("Invalid colour. Valid colours are: black, red, white.");
            return;
        }

        int result = _database.InsertItem(newLivestock);
        if (result > 0)
        {
            DisplayOutput($"Record added: {newLivestock}");
            _stores.Add(newLivestock);
        }
        else
        {
            DisplayOutput("Failed to insert record into database.");
        }
    }

    private bool IsValidColour(string colour)
    {
        return colour == "black" || colour == "red" || colour == "white";
    }

    private void DisplayOutput(string message)
    {
        // Example: Displaying output in a label or messagebox
        LabelOutput.Text = message;
    }
}