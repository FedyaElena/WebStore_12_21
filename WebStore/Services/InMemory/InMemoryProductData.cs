﻿using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Data;
using WebStore.Domain;

namespace WebStore.Services.InMemory;
[Obsolete("Используйте класс WebStore.Services.InSQL.SQLProductData")]
public class InMemoryProductData : IProductData
{
    public IEnumerable<Section> GetSections() => TestData.Sections;

    public IEnumerable<Brand> GetBrands() => TestData.Brands;

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        IEnumerable<Product> query = TestData.Products;
       // if (Filter?.SectionId != null)
        //    query = query.Where(p => p.SectionId == Filter.SectionId);

        if (Filter?.SectionId is { } section_id)
            query = query.Where(p => p.SectionId == section_id);

        if (Filter?.BrandId is { } brand_id)
            query = query.Where(p => p.BrandId == brand_id);
        return query;
    }
}

