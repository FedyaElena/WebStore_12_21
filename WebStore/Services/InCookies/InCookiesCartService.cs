//using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Services;
using WebStore.Domain.Entities;
using WebStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebStore.Domain;
using System.Data;
using System.Data.SqlClient;
using WebStore.DAL.Context;
using WebStore.ViewModels;
using Newtonsoft.Json;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Services.InCookies;

public class InCookiesCartService : ICartService
{
    private readonly IHttpContextAccessor _HttpContextAccessor;
    private readonly IProductData _ProductData;
    private readonly string _CartName;

    private Cart Cart
    {
        get
        {
            var context = _HttpContextAccessor.HttpContext;
            var cookies = context!.Response.Cookies;
            var cart_cookie = context.Request.Cookies[_CartName];
            if (cart_cookie is null)
            {
                var cart = new Cart();
                cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                return cart;
            }
            ReplaceCart(cookies, cart_cookie);
            return JsonConvert.DeserializeObject<Cart>(cart_cookie);
        }
        set => ReplaceCart(_HttpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
    }

    private void ReplaceCart (IResponseCookies cookies, string cart)
    {
        cookies.Delete(_CartName);
        cookies.Append(_CartName, cart);
    }
    public InCookiesCartService(IHttpContextAccessor HttpContextAccessor, IProductData ProductData)
    {
        _HttpContextAccessor = HttpContextAccessor;
        _ProductData = ProductData;
        var user = _HttpContextAccessor.HttpContext!.User;
        var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;
        _CartName = $"WebStore.GB.Cart{user_name}";
    }
    void ICartService.Add(int Id)
    {
        var cart = Cart;
        var item = Cart.Items.FirstOrDefault(x => x.ProductId == Id);
        if (item is null)
            cart.Items.Add(new CartItem { ProductId = Id, Quantity = 1 });
        else
            item.Quantity++;
        Cart = cart;
    }

    void ICartService.Clear()
    {
        //Cart = new();
        var cart = Cart;
        cart.Items.Clear();
        Cart = cart;
    }

    void ICartService.Decrement(int Id)
    {
        var cart = Cart;
        var item = Cart.Items.FirstOrDefault(x => x.ProductId == Id);
        if (item is null)
            return;
        if (item.Quantity > 0)
            item.Quantity--;
        if (item.Quantity == 0)
            cart.Items.Remove(item);
            Cart = cart;
    }

    CartViewModel ICartService.GetViewModel()
    {
        var cart = Cart;
        var products = _ProductData.GetProducts(new()
        {
            Ids = Cart.Items.Select(i => i.ProductId).ToArray()
        });

        var products_views = products.ToView().ToDictionary(p => p!.Id);
        return new()
        {
            Items = cart.Items
            .Where(item => products_views.ContainsKey(item.ProductId))
            .Select(item => (products_views[item.ProductId], item.Quantity))!
        };
    }

    void ICartService.Remove(int Id)
    {
        var cart = Cart;
        var item = Cart.Items.FirstOrDefault(x => x.ProductId == Id);
        if (item is null)
            return;
        cart.Items.Remove(item);
        Cart = cart;
    }
}

