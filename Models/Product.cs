namespace SportsStore.Models
{
    public class Product
    {
        public long ProductID { get; set; } // Modifică de la int la long (Int64)
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}

