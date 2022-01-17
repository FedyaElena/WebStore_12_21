using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace WebStore.Domain.Entities;

public class Cart 
{
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    public int ItemCount => Items.Sum(item => item.Quantity);
}

public class CartItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}


