Livestock Management System (LMS) App
Overview
The Livestock Management System (LMS) app is a C# and .NET MAUI application designed to manage livestock data efficiently. It connects to a SQLite database (FarmData.db) containing information about cows and sheep, enabling users to perform various operations, including data retrieval, statistical reporting, profit forecasting, querying, and database manipulation.

Features
1. Data Modelling and Display
Database Connection: Establish a connection to the SQLite database.
Class Modelling: Create classes to represent the Cow and Sheep tables.![Screenshot 2024-06-27 184308](https://github.com/masih45/Livestock_project/assets/164842757/aa7a1bb1-8ae8-4abc-8faa-b8eaa8a3febd)
![Screenshot 2024-06-27 184241](https://github.com/masih45/Livestock_project/assets/164842757/947d7222-87e1-4078-863f-58b9c523e17a)

Data Loading: Load table rows as objects into data structures.
Data Display: Show all livestock records in a columnar format.
2. Livestock Statistics and Profit Forecasting
Government Tax Calculation: Calculate and display the total government tax paid by the farm per month (30 days).
Total Profit/Loss Calculation: Show the total profit or loss of all animals per day.
Average Weight Calculation: Display the average weight of all farm animals.
Daily Profit Calculation:
For all cows.
For all sheep.
Investment Profit Forecasting: Based on current livestock profitability, estimate the daily profit if the farmer invests in more livestock (specified by the user).
3. Livestock Querying
Allow users to query livestock based on type and color, with combinations such as:

All cows
Black sheep
White sheep
Black cows
Etc.
For the selected livestock, the app calculates and displays:

Total number of selected livestock.
Percentage of the selected livestock.
Profit or loss per day.
Average weight.
Government tax paid per day.
Produce amount (milk for cows, wool for sheep).
4. Database Operations
Delete Record: Allow users to delete a row from the database in case an animal dies.
Insert Record: Allow users to add a new record for new arrivals of livestock, with the ID auto-generated.
Update Record: Allow users to modify existing livestock records (e.g., weight changes), with updates reflected in the database and UI.
