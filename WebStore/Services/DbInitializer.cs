﻿using WebStore.Models;
using WebStore.DAL.Context;
using WebStore.Services.Interfaces;
using WebStore.Data;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Services;
public class DbInitializer : IDbInitializer
{
    private readonly WebStoreDB _db;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(WebStoreDB db, ILogger<DbInitializer> Logger)
    {
        _db = db;
        _logger = Logger;
    }

    public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
    {
        _logger.LogInformation("Удаление БД");
        var result = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);
        if (result)
            _logger.LogInformation("Удаление БД выполнено успешно");
        else
            _logger.LogInformation("Удаление БД не требуется");

        return result;
    }

    public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
    {
        _logger.LogInformation("Инициализация БД");
        if (RemoveBefore)
            await RemoveAsync(Cancel).ConfigureAwait(false);

        var pending_migrations = await _db.Database.GetPendingMigrationsAsync(Cancel);
        if (pending_migrations.Any())
        {
            _logger.LogInformation("Выполнение миграции БД");
            await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
            _logger.LogInformation("Выполнение миграции БД завершено успешно");
        }

       

        await InitializeProductsAsync(Cancel).ConfigureAwait(false);
        _logger.LogInformation("Инициализация БД выполнена успешно");
    }

    private async Task InitializeProductsAsync(CancellationToken Cancel)
    {
        if (_db.Sections.Any())
        {
            _logger.LogInformation("Инициализация данных БД не требуется");
            return;
        }
        _logger.LogInformation("Инициализация данных БД ...");

        _logger.LogInformation("Добавление секций в БД ...");
        await using (await _db.Database.BeginTransactionAsync(Cancel))
        {
            await _db.Sections.AddRangeAsync(TestData.Sections, Cancel);
            await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON", Cancel);
            await _db.SaveChangesAsync(Cancel);
            await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF", Cancel);

        }
        _logger.LogInformation("Добавление брендов в БД ...");
        await using (await _db.Database.BeginTransactionAsync(Cancel))
        {
            await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);
            await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON", Cancel);
            await _db.SaveChangesAsync(Cancel);
            await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF", Cancel);

        }
        _logger.LogInformation("Добавление продуктов в БД ...");
        await using (await _db.Database.BeginTransactionAsync(Cancel))
        {
            await _db.Products.AddRangeAsync(TestData.Products, Cancel);
            await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON", Cancel);
            await _db.SaveChangesAsync(Cancel);
            await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF", Cancel);

        }

        _logger.LogInformation("Инициализация данных БД выполнена успешно");
    }

}
