using System;
using System.Collections.Generic;
using System.Linq;

namespace EShoppingLab4.CodeOnly
{
    public sealed class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string StockStatus { get { return StockQuantity > 0 ? "Còn hàng" : "Hết hàng"; } }
    }

    public sealed class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get { return Product.ProductCode; } }
        public string ProductName { get { return Product.ProductName; } }
        public decimal UnitPrice { get { return Product.Price; } }
        public decimal LineTotal { get { return Product.Price * Quantity; } }
    }

    public sealed class RegistrationData
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityDocument { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public sealed class UserSession
    {
        public long? CustomerId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public bool IsLoggedIn { get { return CustomerId.HasValue; } }
        public void Clear() { CustomerId = null; Username = null; FullName = null; }
    }

    public sealed class OrderDraft
    {
        public long CustomerId { get; set; }
        public byte DeliveryMethodId { get; set; }
        public string AreaCode { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhone { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal PaymentFee { get; set; }
        public string CardBrand { get; set; }
        public string MaskedPan { get; set; }
        public List<CartLine> Items { get; set; }
        public decimal GrandTotal { get { return Subtotal + ShippingFee + PaymentFee; } }
    }

    public sealed class OrderResult
    {
        public bool Success { get; set; }
        public string OrderNumber { get; set; }
        public string Message { get; set; }
    }

    public sealed class AppServices
    {
        public AppServices(bool useSql, IProductRepository products, IAccountRepository accounts,
            IOrderRepository orders, CartService cart, ShippingService shipping, UserSession session)
        {
            UseSql = useSql; Products = products; Accounts = accounts; Orders = orders;
            Cart = cart; Shipping = shipping; Session = session;
        }
        public bool UseSql { get; private set; }
        public IProductRepository Products { get; private set; }
        public IAccountRepository Accounts { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public CartService Cart { get; private set; }
        public ShippingService Shipping { get; private set; }
        public UserSession Session { get; private set; }
        public string ModeText { get { return UseSql ? "ĐÃ KẾT NỐI SQL" : "CHƯA KẾT NỐI SQL (DỮ LIỆU MẪU)"; } }
    }
}
