namespace task4a.Pages;

public partial class UpdateRecord : ContentPage
{
    private Database _database;
    private ObservableCollection<Store> _stores;

    public UpdateRecord()
    {
        InitializeComponent();
        _database = new Database();
        _stores = new ObservableCollection<Store>();
    }

    private void OnUpdateRecordClicked(object sender, EventArgs e)
    {
        if (int.TryParse(IdEntry.Text, out int livestockId))
        {
            double? cost = double.TryParse(CostEntry.Text, out double costResult) ? costResult : (double?)null;
            double? weight = double.TryParse(WeightEntry.Text, out double weightResult) ? weightResult : (double?)null;
            string colour = ColourEntry.Text;
            double? produce = double.TryParse(ProduceEntry.Text, out double produceResult) ? produceResult : (double?)null;

            UpdateRecordById(livestockId, cost, weight, colour, produce);
        }
        else
        {
            DisplayOutput("Invalid ID format.");
        }
    }

    public void UpdateRecordById(int livestockId, double? cost, double? weight, string colour, double? produce)
    {
        var livestock = _database.GetItemById(livestockId);
        if (livestock == null)
        {
            DisplayOutput($"Non-existent livestock ID: {livestockId}");
            return;
        }

        if (cost.HasValue)
        {
            livestock.Cost = cost.Value;
        }

        if (weight.HasValue)
        {
            livestock.Weight = weight.Value;
        }

        if (!string.IsNullOrWhiteSpace(colour))
        {
            livestock.Colour = colour;
        }

        if (livestock is Cow cow && produce.HasValue)
        {
            cow.Milk = produce.Value;
        }
        else if (livestock is Sheep sheep && produce.HasValue)
        {
            sheep.Wool = produce.Value;
        }

        int result = _database.UpdateItem(livestock);
        if (result > 0)
        {
            DisplayOutput($"Record updated: {livestock.GetType().Name} {livestock.Id} {livestock.Cost} {livestock.Weight} {livestock.Colour}");
        }
        else
        {
            DisplayOutput("Failed to update record in database.");
        }
    }

    private void DisplayOutput(string message)
    {
        LabelOutput.Text = message;
    }
}