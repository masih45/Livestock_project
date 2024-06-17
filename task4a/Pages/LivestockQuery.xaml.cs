namespace task4a.Pages;

public partial class LivestockQuery : ContentPage
{
    private ObservableCollection<Store> _stores;
    public LivestockQuery()
	{
		InitializeComponent();
        LoadStores();
    }
    private void LoadStores()
    {
        // Example: Loading stores from a database or another source
        _stores = new ObservableCollection<Store>
            {
                new Cow { Colour = "black", Weight = 300, Milk = 20, Cost = 100 },
                new Cow { Colour = "red", Weight = 280, Milk = 18, Cost = 95 },
                new Sheep { Colour = "white", Weight = 150, Wool = 8, Cost = 50 }
                // Add more as needed...
            };
    }

    private void OnQueryLivestockClicked(object sender, EventArgs e)
    {
        string livestockTypeInput = LivestockTypeEntry.Text?.ToLower();
        string livestockColorInput = LivestockColorEntry.Text?.ToLower();
        string result = QueryFarmLivestock(livestockTypeInput, livestockColorInput);
        LabelQueryResult.Text = result;
    }

    public string QueryFarmLivestock(string livestockTypeInput, string livestockColorInput)
    {
        string livestockType = "";
        if (livestockTypeInput == "cow" || livestockTypeInput == "sheep")
        {
            livestockType = livestockTypeInput;
        }
        else
        {
            return "Invalid input.";
        }

        string livestockColor = "";
        if (livestockColorInput == "all" || livestockColorInput == "black" || livestockColorInput == "red" || livestockColorInput == "white")
        {
            livestockColor = livestockColorInput;
        }
        else
        {
            return "Invalid input.";
        }

        IEnumerable<Store> livestock = _stores;

        if (livestockType == "cow")
        {
            livestock = _stores.OfType<Cow>();
        }
        else if (livestockType == "sheep")
        {
            livestock = _stores.OfType<Sheep>();
        }

        if (livestockColor != "all")
        {
            livestock = livestock.Where(animal => animal.Colour.ToLower() == livestockColor);
        }

        double totalLivestock = _stores.Count();
        double totalSelectedLivestock = livestock.Count();
        double percentageSelected = (totalSelectedLivestock / totalLivestock) * 100;

        double totalWeight = livestock.Sum(animal => animal.Weight);
        double totalTax = livestock.Sum(animal => animal.Weight * 0.2);
        double totalProfit = livestock.Sum(animal => livestockType == "cow" ? ((Cow)animal).Milk * 9.4 - animal.Cost - animal.Weight * 0.2 : ((Sheep)animal).Wool * 6.2 - animal.Cost - animal.Weight * 0.2);
        double totalProduce = livestock.Sum(animal => livestockType == "cow" ? ((Cow)animal).Milk : ((Sheep)animal).Wool);

        return $"Number of livestock ({livestockType} in {livestockColor} colour): {totalSelectedLivestock}\n" +
               $"Percentage of selected livestock from all: {percentageSelected:F2}%\n" +
               $"Daily tax of selected livestock: ${totalTax:F2}\n" +
               $"Profit: ${totalProfit:F2}\n" +
               $"Average weight: {(totalWeight / totalSelectedLivestock):F2} kg\n" +
               $"Produce amount: {totalProduce:F2} kg";
    }
}