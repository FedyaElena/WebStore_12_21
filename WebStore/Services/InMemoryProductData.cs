using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Data;

namespace WebStore.Services;
public class InMemoryProductData : IProductData
{
    public IEnumerable<Section> GetSections() => TestData.Sections;

    public IEnumerable<Brand> GetBrands() => TestData.Brands;
}

