namespace task4a.Pages;

public partial class DeleteRecord : ContentPage
{
    private Database _database;

    public DeleteRecord()
    {
        InitializeComponent();
        _database = new Database();
    }

    private void OnDeleteRecordClicked(object sender, EventArgs e)
    {
        if (int.TryParse(IdEntry.Text, out int id))
        {
            DeleteRecordById(id);
        }
        else
        {
            DisplayOutput("Invalid ID format.");
        }
    }

    private void DeleteRecordById(int id)
    {
        var recordToDelete = _database.GetItemById(id); // Ensure this method exists
        if (recordToDelete == null)
        {
            DisplayOutput($"Non-existent livestock ID: {id}");
            return;
        }

        int result = _database.DeleteItem(recordToDelete);
        if (result > 0)
        {
            DisplayOutput($"Record deleted: {recordToDelete}");
        }
        else
        {
            DisplayOutput("Failed to delete record from database.");
        }
    }

    private void DisplayOutput(string message)
    {
        LabelOutput.Text = message;
    }
}