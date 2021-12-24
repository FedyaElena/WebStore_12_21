namespace WebStore.ViewModels;


public class CatalogViewModel 
{
    public int? Id { get; set; }

    public int? SectionId { get; set; }

    public int? BrandId { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; } = null!;
}
