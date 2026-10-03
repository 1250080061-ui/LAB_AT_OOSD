using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EShoppingLab4.CodeOnly
{
    public interface IProductRepository { IList<Product> GetAll(); }
    public interface IAccountRepository
    {
        void Register(RegistrationData data);
        UserSession Login(string username, string password);
    }
    public interface IOrderRepository { OrderResult Save(OrderDraft order); }

    internal static class Db
    {
        public static SqlConnection CreateConnection()
        {
            var item = ConfigurationManager.ConnectionStrings["EShoppingDb"];
            if (item == null) throw new ConfigurationErrorsException("Thiếu connectionStrings/EShoppingDb trong App.config.");
            return new SqlConnection(item.ConnectionString);
        }
    }

    internal static class PasswordHelper
    {
        public static string Hash(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
                var sb = new StringBuilder(64);
                foreach (byte b in data) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }

    public sealed class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _items = new List<Product>
        {
            new Product { ProductId=1, ProductCode="SP001", ProductName="Laptop Dell Inspiron", Manufacturer="Dell", GroupName="Thiết bị máy tính", Description="Laptop học tập và văn phòng", Specifications="Core i5, RAM 16 GB, SSD 512 GB", Price=18500000, StockQuantity=10 },
            new Product { ProductId=2, ProductCode="SP002", ProductName="Chuột Logitech M331", Manufacturer="Logitech", GroupName="Thiết bị máy tính", Description="Chuột không dây", Specifications="USB 2.4 GHz", Price=450000, StockQuantity=20 },
            new Product { ProductId=3, ProductCode="SP003", ProductName="Bàn phím cơ", Manufacturer="Akko", GroupName="Thiết bị máy tính", Description="Bàn phím cơ có đèn nền", Specifications="USB Type-C", Price=1200000, StockQuantity=8 },
            new Product { ProductId=4, ProductCode="SP004", ProductName="Máy ảnh EOS R50", Manufacturer="Canon", GroupName="Máy ảnh", Description="Máy ảnh mirrorless", Specifications="APS-C 24.2 MP", Price=21990000, StockQuantity=5 },
            new Product { ProductId=5, ProductCode="SP005", ProductName="Nồi chiên không dầu", Manufacturer="Philips", GroupName="Thiết bị gia dụng", Description="Nồi chiên dung tích lớn", Specifications="6.2 lít", Price=3290000, StockQuantity=12 },
            new Product { ProductId=6, ProductCode="SP006", ProductName="Bộ xếp hình sáng tạo", Manufacturer="LEGO", GroupName="Đồ chơi", Description="Đồ chơi lắp ghép", Specifications="500 chi tiết", Price=1590000, StockQuantity=0 }
        };
        public IList<Product> GetAll() { return _items.Select(Clone).ToList(); }
        private static Product Clone(Product p) { return new Product { ProductId=p.ProductId, ProductCode=p.ProductCode, ProductName=p.ProductName, Manufacturer=p.Manufacturer, GroupName=p.GroupName, Description=p.Description, Specifications=p.Specifications, Price=p.Price, StockQuantity=p.StockQuantity }; }
    }

    public sealed class MockAccountRepository : IAccountRepository
    {
        private sealed class Account { public long Id; public string FullName; public string Username; public string PasswordHash; }
        private readonly List<Account> _accounts = new List<Account>
        {
            new Account { Id=1, FullName="Tài khoản demo", Username="demo", PasswordHash=PasswordHelper.Hash("123456") }
        };
        public void Register(RegistrationData d)
        {
            if (_accounts.Any(x => string.Equals(x.Username, d.Username, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");
            _accounts.Add(new Account { Id=_accounts.Count+1, FullName=d.FullName, Username=d.Username, PasswordHash=PasswordHelper.Hash(d.Password) });
        }
        public UserSession Login(string username, string password)
        {
            string hash = PasswordHelper.Hash(password);
            Account a = _accounts.FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase) && x.PasswordHash == hash);
            return a == null ? null : new UserSession { CustomerId=a.Id, Username=a.Username, FullName=a.FullName };
        }
    }

    public sealed class MockOrderRepository : IOrderRepository
    {
        private readonly List<OrderDraft> _orders = new List<OrderDraft>();
        public OrderResult Save(OrderDraft order)
        {
            _orders.Add(order);
            return new OrderResult { Success=true, OrderNumber="MOCK-" + DateTime.Now.ToString("yyyyMMddHHmmss"), Message="Đơn hàng đã được lưu trong bộ nhớ." };
        }
    }

    public sealed class SqlProductRepository : IProductRepository
    {
        public IList<Product> GetAll()
        {
            var result = new List<Product>();
            const string sql = @"SELECT p.ProductId,p.ProductCode,p.ProductName,p.Manufacturer,g.GroupName,
                                        p.[Description],p.Specifications,p.Price,p.StockQuantity
                                 FROM dbo.Products p INNER JOIN dbo.ProductGroups g ON p.GroupId=g.GroupId
                                 WHERE p.IsActive=1 ORDER BY p.ProductName";
            using (var cn=Db.CreateConnection()) using (var cmd=new SqlCommand(sql,cn))
            {
                cn.Open();
                using (var r=cmd.ExecuteReader()) while(r.Read()) result.Add(new Product
                {
                    ProductId=Convert.ToInt32(r["ProductId"]), ProductCode=r["ProductCode"].ToString(),
                    ProductName=r["ProductName"].ToString(), Manufacturer=r["Manufacturer"].ToString(),
                    GroupName=r["GroupName"].ToString(), Description=r["Description"].ToString(),
                    Specifications=r["Specifications"].ToString(), Price=Convert.ToDecimal(r["Price"]),
                    StockQuantity=Convert.ToInt32(r["StockQuantity"])
                });
            }
            return result;
        }
    }

    public sealed class SqlAccountRepository : IAccountRepository
    {
        public void Register(RegistrationData d)
        {
            using (var cn=Db.CreateConnection())
            {
                cn.Open(); using (var tx=cn.BeginTransaction())
                try
                {
                    using (var check=new SqlCommand("SELECT COUNT(*) FROM dbo.Accounts WHERE Username=@u",cn,tx))
                    {
                        check.Parameters.Add("@u",SqlDbType.NVarChar,50).Value=d.Username;
                        if(Convert.ToInt32(check.ExecuteScalar())>0) throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");
                    }
                    const string customerSql=@"INSERT dbo.Customers(FullName,BirthDate,IdentityDocument,[Address],Phone,Email)
                                               OUTPUT INSERTED.CustomerId VALUES(@n,@b,@i,@a,@p,@e)";
                    long id;
                    using(var cmd=new SqlCommand(customerSql,cn,tx))
                    {
                        cmd.Parameters.Add("@n",SqlDbType.NVarChar,100).Value=d.FullName;
                        cmd.Parameters.Add("@b",SqlDbType.Date).Value=d.BirthDate.Date;
                        cmd.Parameters.Add("@i",SqlDbType.NVarChar,30).Value=d.IdentityDocument;
                        cmd.Parameters.Add("@a",SqlDbType.NVarChar,250).Value=d.Address;
                        cmd.Parameters.Add("@p",SqlDbType.NVarChar,20).Value=d.Phone;
                        cmd.Parameters.Add("@e",SqlDbType.NVarChar,254).Value=string.IsNullOrWhiteSpace(d.Email)?(object)DBNull.Value:d.Email;
                        id=Convert.ToInt64(cmd.ExecuteScalar());
                    }
                    using(var cmd=new SqlCommand("INSERT dbo.Accounts(CustomerId,Username,PasswordHash) VALUES(@id,@u,@h)",cn,tx))
                    {
                        cmd.Parameters.Add("@id",SqlDbType.BigInt).Value=id;
                        cmd.Parameters.Add("@u",SqlDbType.NVarChar,50).Value=d.Username;
                        cmd.Parameters.Add("@h",SqlDbType.Char,64).Value=PasswordHelper.Hash(d.Password);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                catch { tx.Rollback(); throw; }
            }
        }

        public UserSession Login(string username, string password)
        {
            const string sql=@"SELECT a.CustomerId,a.Username,c.FullName FROM dbo.Accounts a
                               INNER JOIN dbo.Customers c ON a.CustomerId=c.CustomerId
                               WHERE a.Username=@u AND a.PasswordHash=@h AND a.IsActive=1";
            using(var cn=Db.CreateConnection()) using(var cmd=new SqlCommand(sql,cn))
            {
                cmd.Parameters.Add("@u",SqlDbType.NVarChar,50).Value=username;
                cmd.Parameters.Add("@h",SqlDbType.Char,64).Value=PasswordHelper.Hash(password);
                cn.Open(); using(var r=cmd.ExecuteReader())
                {
                    if(!r.Read()) return null;
                    return new UserSession { CustomerId=Convert.ToInt64(r["CustomerId"]), Username=r["Username"].ToString(), FullName=r["FullName"].ToString() };
                }
            }
        }
    }

    public sealed class SqlOrderRepository : IOrderRepository
    {
        public OrderResult Save(OrderDraft o)
        {
            string number="DH"+DateTime.Now.ToString("yyyyMMddHHmmssfff");
            using(var cn=Db.CreateConnection())
            {
                cn.Open(); using(var tx=cn.BeginTransaction())
                try
                {
                    const string orderSql=@"INSERT dbo.Orders(OrderNumber,CustomerId,DeliveryMethodId,AreaCode,
                        RecipientName,RecipientAddress,RecipientPhone,Subtotal,ShippingFee,PaymentFee,GrandTotal,[Status])
                        OUTPUT INSERTED.OrderId VALUES(@no,@cid,@dm,@area,@rn,@ra,@rp,@sub,@ship,@pf,@total,'CONFIRMED')";
                    long orderId;
                    using(var cmd=new SqlCommand(orderSql,cn,tx))
                    {
                        cmd.Parameters.AddWithValue("@no",number); cmd.Parameters.AddWithValue("@cid",o.CustomerId);
                        cmd.Parameters.AddWithValue("@dm",o.DeliveryMethodId); cmd.Parameters.AddWithValue("@area",o.AreaCode);
                        cmd.Parameters.AddWithValue("@rn",o.RecipientName); cmd.Parameters.AddWithValue("@ra",o.RecipientAddress);
                        cmd.Parameters.AddWithValue("@rp",o.RecipientPhone); cmd.Parameters.AddWithValue("@sub",o.Subtotal);
                        cmd.Parameters.AddWithValue("@ship",o.ShippingFee); cmd.Parameters.AddWithValue("@pf",o.PaymentFee);
                        cmd.Parameters.AddWithValue("@total",o.GrandTotal); orderId=Convert.ToInt64(cmd.ExecuteScalar());
                    }
                    foreach(var line in o.Items)
                    {
                        using(var stock=new SqlCommand("UPDATE dbo.Products SET StockQuantity=StockQuantity-@q WHERE ProductId=@pid AND StockQuantity>=@q",cn,tx))
                        {
                            stock.Parameters.AddWithValue("@q",line.Quantity); stock.Parameters.AddWithValue("@pid",line.Product.ProductId);
                            if(stock.ExecuteNonQuery()!=1) throw new InvalidOperationException("Sản phẩm "+line.Product.ProductName+" không đủ tồn kho.");
                        }
                        using(var item=new SqlCommand("INSERT dbo.OrderItems(OrderId,ProductId,ProductName,UnitPrice,Quantity) VALUES(@o,@p,@n,@u,@q)",cn,tx))
                        {
                            item.Parameters.AddWithValue("@o",orderId); item.Parameters.AddWithValue("@p",line.Product.ProductId);
                            item.Parameters.AddWithValue("@n",line.Product.ProductName); item.Parameters.AddWithValue("@u",line.Product.Price);
                            item.Parameters.AddWithValue("@q",line.Quantity); item.ExecuteNonQuery();
                        }
                    }
                    using(var pay=new SqlCommand(@"INSERT dbo.PaymentTransactions(OrderId,GatewayTransactionId,CardBrand,MaskedPan,Amount,Fee,[Status])
                                                   VALUES(@o,@g,@b,@m,@a,@f,'APPROVED')",cn,tx))
                    {
                        pay.Parameters.AddWithValue("@o",orderId); pay.Parameters.AddWithValue("@g","GW-"+Guid.NewGuid().ToString("N"));
                        pay.Parameters.AddWithValue("@b",o.CardBrand); pay.Parameters.AddWithValue("@m",o.MaskedPan);
                        pay.Parameters.AddWithValue("@a",o.GrandTotal); pay.Parameters.AddWithValue("@f",o.PaymentFee); pay.ExecuteNonQuery();
                    }
                    tx.Commit(); return new OrderResult { Success=true, OrderNumber=number, Message="Đã lưu Orders, OrderItems và PaymentTransactions." };
                }
                catch(Exception ex) { tx.Rollback(); return new OrderResult { Success=false, Message=ex.Message }; }
            }
        }
    }
}
