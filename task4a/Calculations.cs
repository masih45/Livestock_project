using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.Maui.Controls;

namespace task4a
{
    class Calculations: ContentPage
    {
        public ObservableCollection<Store> _stores { get; set; }
        public readonly Database _database;
        private Entry _livestockTypeEntry;
        private Entry _livestockColorEntry;
        private Label _outputLabel;

        public Calculations()
        {
            _stores = new ObservableCollection<Store>();
            _database = new Database();
            ReadDB(); // Fill the collection

            // Setting up the UI
            _livestockTypeEntry = new Entry { Placeholder = "Enter livestock type (Cow/Sheep)" };
            _livestockColorEntry = new Entry { Placeholder = "Enter livestock colour (All/black/red/white)" };
            _outputLabel = new Label();

            var queryButton = new Button
            {
                Text = "Query Farm Livestock"
            };
            queryButton.Clicked += OnQueryButtonClicked;

            Content = new StackLayout
            {
                Padding = new Thickness(20),
                Children = { _livestockTypeEntry, _livestockColorEntry, queryButton, _outputLabel }
            };
        }

        private void OnQueryButtonClicked(object sender, EventArgs e)
        {
            string livestockTypeInput = _livestockTypeEntry.Text.ToLower();
            string livestockColorInput = _livestockColorEntry.Text.ToLower();
            QueryFarmLivestock(livestockTypeInput, livestockColorInput);
        }

        public void CalculateGovernmentTaxFor30Days()
        {
            double totalTax = 0;

            foreach (var store in _stores)
            {
                double tax = store.Weight * 0.2;
                totalTax += tax;
            }
            DisplayOutput($"30-day government tax: ${totalTax * 30}");
        }

        public void CalculateFarmDailyProfit()
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

            DisplayOutput($"Total farm daily profit: ${totalProfit}");
        }

        public void CalculateAverageWeightOfLivestocks()
        {
            double totalWeight = 0;
            int totalLivestockCount = 0;

            foreach (var item in _stores)
            {
                totalWeight += item.Weight;
                totalLivestockCount++;
            }

            if (totalLivestockCount == 0)
            {
                DisplayOutput("No livestock available to calculate average weight.");
                return;
            }

            double averageWeight = totalWeight / totalLivestockCount;
            DisplayOutput($"Average weight of all livestock: {averageWeight}");
        }

        public void CalculateAverageDailyProfitPerCow()
        {
            double totalProfit = 0;
            int cowCount = 0;

            foreach (var item in _stores)
            {
                if (item is Cow cow)
                {
                    double income = cow.Milk * 9.4;
                    double tax = cow.Weight * 0.2;
                    double profit = income - cow.Cost - tax;
                    totalProfit += profit;
                    cowCount++;
                }
            }

            if (cowCount == 0)
            {
                DisplayOutput("No cows available to calculate average daily profit.");
                return;
            }

            double averageProfit = totalProfit / cowCount;
            DisplayOutput($"Average daily profit per cow: ${averageProfit}");
        }

        public void CalculateAverageDailyProfitPerSheep()
        {
            double totalProfit = 0;
            int sheepCount = 0;

            foreach (var item in _stores)
            {
                if (item is Sheep sheep)
                {
                    double income = sheep.Wool * 6.2;
                    double tax = sheep.Weight * 0.2;
                    double profit = income - sheep.Cost - tax;
                    totalProfit += profit;
                    sheepCount++;
                }
            }

            if (sheepCount == 0)
            {
                DisplayOutput("No sheep available to calculate average daily profit.");
                return;
            }

            double averageProfit = totalProfit / sheepCount;
            DisplayOutput($"Average daily profit per sheep: ${averageProfit}");
        }

        public void GetCurrentDailyProfitOfAllSheep()
        {
            double totalProfit = 0;

            foreach (var item in _stores)
            {
                if (item is Sheep sheep)
                {
                    double income = sheep.Wool * 6.2;
                    double tax = sheep.Weight * 0.2;
                    double profit = income - sheep.Cost - tax;
                    totalProfit += profit;
                }
            }

            DisplayOutput($"Current daily profit of all sheep: ${totalProfit}");
        }

        public void GetCurrentDailyProfitOfAllCows()
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
            }

            DisplayOutput($"Current daily profit of all cows: ${totalProfit}");
        }

        public string CalculateEstimatedDailyProfit(string livestockType, int quantity)
        {
            double averageProfitPerAnimal = 0;

            if (livestockType.ToLower() == "cow")
            {
                CalculateAverageDailyProfitPerCow();
                averageProfitPerAnimal = _stores.OfType<Cow>().Average(cow => cow.Milk * 9.4 - cow.Cost - cow.Weight * 0.2);
            }
            else if (livestockType.ToLower() == "sheep")
            {
                CalculateAverageDailyProfitPerSheep();
                averageProfitPerAnimal = _stores.OfType<Sheep>().Average(sheep => sheep.Wool * 6.2 - sheep.Cost - sheep.Weight * 0.2);
            }
            else
            {
                return "Invalid livestock type.";
            }

            double estimatedDailyProfit = averageProfitPerAnimal * quantity;
            return $"Buying {quantity} {livestockType}s would bring in estimated daily profit of {estimatedDailyProfit.ToString("0.00")}";
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
                   $"Percentage of selected livestock from all: {percentageSelected}%\n" +
                   $"Daily tax of selected livestock: ${totalTax}\n" +
                   $"Profit: ${totalProfit}\n" +
                   $"Average weight: {totalWeight / totalSelectedLivestock} kg\n" +
                   $"Produce amount: {totalProduce} kg";
        }

        public void DeleteRecord(int id)
        {
            var recordToDelete = _stores.FirstOrDefault(s => s.Id == id);
            if (recordToDelete == null)
            {
                DisplayOutput($"Non-existent livestock id: {id}");
                return;
            }

            int result = _database.DeleteItem(recordToDelete);
            if (result > 0)
            {
                DisplayOutput($"Record deleted: {recordToDelete}");
                _stores.Remove(recordToDelete);
            }
            else
            {
                DisplayOutput("Failed to delete record from database.");
            }
        }

        public void InsertRecord(string tableName, double cost, double weight, string colour, double produce)
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

        public void UpdateRecord(int livestockId, double? cost, double? weight, string colour, double? produce)
        {
            Store livestock = _stores.FirstOrDefault(item => item.Id == livestockId);
            if (livestock == null)
            {
                DisplayOutput($"Non-existent livestock id: {livestockId}");
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

        public void Print()
        {
            var output = "==Livestock List==\n" + string.Join("\n", _stores.Select(item => item.ToString()));
            DisplayOutput(output);
        }

        private void ReadDB()
        {
            if (_database != null)
            {
                _database.ReadItems().ForEach(x => _stores.Add(x));
            }
            else
            {
                // Handle the case where _database is null
                // You might want to log an error, display a message, or handle it gracefully
                Console.WriteLine("_database is null. Cannot read items.");
            }
        }

        private void DisplayOutput(string message)
        {
            _outputLabel.Text = message;
        }
    }
}