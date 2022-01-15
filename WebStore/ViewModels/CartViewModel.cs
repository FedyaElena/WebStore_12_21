namespace WebStore.ViewModels;


public class CartViewModel 
{
    public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }
    public int ItemsCount => Items.Sum(x => x.Quantity);
    public decimal TotalPrice => Items.Sum(x => x.Product.Price * x.Quantity);
}
