namespace Canducci.EntityFramework.ConsoleAppTest.Models
{
    public class ViewModel
    {
        public ViewModel()
        {

        }
        public ViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class ViewSoma
    {
        public int ItemId { get; set; }
        public decimal Valor { get; set; }
    }
}
