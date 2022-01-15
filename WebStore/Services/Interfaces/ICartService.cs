//using WebStore.Models;
using WebStore.Domain.Entities;
using WebStore.Domain;
using WebStore.ViewModels;

namespace WebStore.Services.Interfaces;

    public interface ICartService
{
    void Add(int Id);
    void Decrement(int Id);
    void Remove (int Id);
    void Clear();
    
    CartViewModel GetViewModel();
}

