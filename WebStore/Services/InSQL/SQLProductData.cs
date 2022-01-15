//using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Data;
using WebStore.Domain;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Services.InSQL;

public class SQLProductData : IProductData
 {
    private readonly WebStoreDB _db;

    public SQLProductData(WebStoreDB db) => _db = db;

    public IEnumerable<Section> GetSections() => _db.Sections;

    public IEnumerable<Brand> GetBrands() => _db.Brands;

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
       IQueryable<Product> query = _db.Products;

        if (Filter?.SectionId is { } section_id)
            query = query.Where(p => p.SectionId == section_id);

        if (Filter?.BrandId is { } brand_id)
            query = query.Where(p => p.BrandId == brand_id);

        return query;
    }

    public Product? GetProductById(int Id) => _db.Products
        .Include(p => p.Section)
        .Include(p => p.Brand)
        .FirstOrDefault(p => p.Id == Id);
}

