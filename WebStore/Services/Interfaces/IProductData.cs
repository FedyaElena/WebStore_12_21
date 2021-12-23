using WebStore.Models;
using WebStore.Domain.Entities;
using WebStore.Domain;

namespace WebStore.Services.Interfaces;

    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter? Filter = null);

}

