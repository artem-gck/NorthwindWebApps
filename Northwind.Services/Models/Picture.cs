namespace Northwind.Services.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public byte[] Content { get; set; }
    }
}
