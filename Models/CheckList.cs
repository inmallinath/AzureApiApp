namespace AzureApiApp.Models
{
    public class CheckList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public CategoryType Category { get; set; }
    }
}