namespace task4a
{
    public partial class App : Application
    {
        public static string DatabasePath { get; private set; }

        public App()
        {
            InitializeComponent();

            // Construct the relative path to the database
            var dataDir = Path.Combine(AppContext.BaseDirectory, "../../../../../");
            DatabasePath = Path.Combine(dataDir, "FarmDataOriginal.db");

            MainPage = new AppShell();
        }
    }
}