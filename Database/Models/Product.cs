namespace DB.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
