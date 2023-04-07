namespace Entities.Models;

public class Book
{
    public int Id { get; set; }
    public string Iban { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public DateTime BookTaken { get; set; }
    public DateTime BookWillBeReturned { get; set; }
}