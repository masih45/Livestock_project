namespace task4a.Pages;

public partial class CalculateIncomeAndProfit : ContentPage
{
    private ObservableCollection<Store> _stores;
    private Database _database;

    public CalculateIncomeAndProfit()
    {
        InitializeComponent();
        _stores = new ObservableCollection<Store>();
        _database = new Database();

        // Load data into _stores from the database
        LoadData();

        CalculateGovernmentTaxFor30Days();
        CalculateFarmDailyProfit();
        CalculateAverageWeightOfLivestocks();
        DisplayCurrentDailyProfitOfAllSheep();
        DisplayCurrentDailyProfitOfAllCows();
        DisplayAverageDailyProfitPerCow();
        DisplayAverageDailyProfitPerSheep();
    }
    private void LoadData()
    {
        var cows = _database.GetCows(); // Assuming GetCows() returns a List<Cow> from the database
        var sheep = _database.GetSheep(); // Assuming GetSheep() returns a List<Sheep> from the database
        foreach (var cow in cows)
        {
            _stores.Add(cow);
        }
        foreach (var sheeps in sheep)
        {
            _stores.Add(sheeps);
        }
    }
    private void CalculateGovernmentTaxFor30Days()
    {
        double totalTax = 0;

        foreach (var store in _stores)
        {
            double tax = store.Weight * 0.2;
            totalTax += tax;
        }

        LabelGovernmentTax.Text = $"30 days government tax: ${totalTax * 30}";
    }

    private void CalculateFarmDailyProfit()
    {
        double totalProfit = 0;

        foreach (var item in _stores)
        {
            if (item is Cow cow)
            {
                double income = cow.Milk * 9.4;
                double tax = cow.Weight * 0.2;
                double profit = income - cow.Cost - tax;
                totalProfit += profit;
            }
            else if (item is Sheep sheep)
            {
                double income = sheep.Wool * 6.2;
                double tax = sheep.Weight * 0.2;
                double profit = income - sheep.Cost - tax;
                totalProfit += profit;
            }
        }

        LabelFarmDailyProfit.Text = $"Farm daily profit: ${totalProfit.ToString("0.00")}";
    }

    private void CalculateAverageWeightOfLivestocks()
    {
        double totalWeight = _stores.Sum(item => item.Weight);
        int totalLivestockCount = _stores.Count;

        if (totalLivestockCount == 0)
        {
            LabelAverageWeight.Text = "No livestock available to calculate average weight.";
            return;
        }

        double averageWeight = totalWeight / totalLivestockCount;
        LabelAverageWeight.Text = $"Average weight of all livestock: {averageWeight.ToString("0.00")} kg";
    }

    private void DisplayAverageDailyProfitPerCow()
    {
        double averageProfit = _stores.OfType<Cow>().Any() ?
            _stores.OfType<Cow>().Average(cow => cow.Milk * 9.4 - cow.Cost - cow.Weight * 0.2) :
            0;

        LabelAverageCowProfit.Text = $"On average, a single cow makes daily profit: ${averageProfit.ToString("0.00")}";
    }

    private void DisplayAverageDailyProfitPerSheep()
    {
        double averageProfit = _stores.OfType<Sheep>().Any() ?
            _stores.OfType<Sheep>().Average(sheep => sheep.Wool * 6.2 - sheep.Cost - sheep.Weight * 0.2) :
            0;

        LabelAverageSheepProfit.Text = $"On average, a single sheep makes daily profit: ${averageProfit.ToString("0.00")}";
    }

    private void DisplayCurrentDailyProfitOfAllSheep()
    {
        double totalProfit = _stores.OfType<Sheep>().Sum(sheep => sheep.Wool * 6.2 - sheep.Cost - sheep.Weight * 0.2);
        LabelCurrentDailySheepProfit.Text = $"Current daily profit of all sheep is: ${totalProfit.ToString("0.00")}";
    }

    private void DisplayCurrentDailyProfitOfAllCows()
    {
        double totalProfit = _stores.OfType<Cow>().Sum(cow => cow.Milk * 9.4 - cow.Cost - cow.Weight * 0.2);
        LabelCurrentDailyCowProfit.Text = $"Current daily profit of all cows is: ${totalProfit.ToString("0.00")}";
    }

    private void OnEstimateInvestmentsClicked(object sender, EventArgs e)
    {
        string livestockType = LivestockTypeEntry.Text?.ToLower();
        if (!int.TryParse(QuantityEntry.Text, out int quantity))
        {
            DisplayAlert("Error", "Invalid quantity. Please enter a valid integer.", "OK");
            return;
        }

        string result = CalculateEstimatedDailyProfit(livestockType, quantity);
        LabelInvestmentResult.Text = result;
    }

    public string CalculateEstimatedDailyProfit(string livestockType, int quantity)
    {
        double averageProfitPerAnimal = 0;

        if (livestockType.ToLower() == "cow")
        {
            averageProfitPerAnimal = _stores.OfType<Cow>().Any() ?
                _stores.OfType<Cow>().Average(cow => cow.Milk * 9.4 - cow.Cost - cow.Weight * 0.2) :
                0;
        }
        else if (livestockType.ToLower() == "sheep")
        {
            averageProfitPerAnimal = _stores.OfType<Sheep>().Any() ?
                _stores.OfType<Sheep>().Average(sheep => sheep.Wool * 6.2 - sheep.Cost - sheep.Weight * 0.2) :
                0;
        }
        else
        {
            return "Invalid livestock type.";
        }

        double estimatedDailyProfit = averageProfitPerAnimal * quantity;
        return $"Buying {quantity} {livestockType}s would bring in estimated daily profit of {estimatedDailyProfit.ToString("$0.00")}";
    }
}