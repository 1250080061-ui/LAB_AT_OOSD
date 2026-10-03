using System;
using System.Configuration;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool useSql;

            bool.TryParse(
                ConfigurationManager.AppSettings["UseSql"],
                out useSql
            );

            IProductRepository products;

            if (useSql)
                products = new SqlProductRepository();
            else
                products = new MockProductRepository();

            IAccountRepository accounts;

            if (useSql)
                accounts = new SqlAccountRepository();
            else
                accounts = new MockAccountRepository();

            IOrderRepository orders;

            if (useSql)
                orders = new SqlOrderRepository();
            else
                orders = new MockOrderRepository();

            AppServices services = new AppServices(
                useSql,
                products,
                accounts,
                orders,
                new CartService(),
                new ShippingService(),
                new UserSession()
            );

            Application.Run(
                new FrmMain(services)
            );
        }
    }
}