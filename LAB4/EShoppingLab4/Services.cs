using System;
using System.Collections.Generic;
using System.Linq;

namespace EShoppingLab4.CodeOnly
{
    public sealed class CartService
    {
        private readonly List<CartLine> _lines = new List<CartLine>();
        public IList<CartLine> Lines { get { return _lines; } }
        public decimal Subtotal { get { return _lines.Sum(x => x.LineTotal); } }
        public int TotalQuantity { get { return _lines.Sum(x => x.Quantity); } }
        public void Add(Product product)
        {
            if(product==null) throw new InvalidOperationException("Chưa chọn sản phẩm.");
            if(product.StockQuantity<=0) throw new InvalidOperationException("Sản phẩm đã hết hàng.");
            CartLine line=_lines.FirstOrDefault(x=>x.Product.ProductId==product.ProductId);
            if(line==null) _lines.Add(new CartLine { Product=product, Quantity=1 });
            else if(line.Quantity<product.StockQuantity) line.Quantity++;
            else throw new InvalidOperationException("Số lượng vượt quá tồn kho.");
        }
        public void Change(Product product, int delta)
        {
            CartLine line=_lines.FirstOrDefault(x=>x.Product.ProductId==product.ProductId); if(line==null)return;
            int value=line.Quantity+delta; if(value<=0)_lines.Remove(line);
            else if(value<=product.StockQuantity)line.Quantity=value;
        }
        public void Remove(Product product) { _lines.RemoveAll(x=>x.Product.ProductId==product.ProductId); }
        public void Clear() { _lines.Clear(); }
        public List<CartLine> Snapshot() { return _lines.Select(x=>new CartLine { Product=x.Product, Quantity=x.Quantity }).ToList(); }
    }

    public sealed class ShippingService
    {
        public decimal Calculate(string area, byte method, decimal subtotal)
        {
            if(method==2 && subtotal>=1000000m) return 0m;
            if(method==3 && subtotal>=5000000m) return 0m;
            decimal factor=area=="INNER"?1m:area=="OUTER"?1.5m:2m;
            decimal fee=method==1?25000m:method==2?50000m:90000m;
            return fee*factor;
        }
    }
}
