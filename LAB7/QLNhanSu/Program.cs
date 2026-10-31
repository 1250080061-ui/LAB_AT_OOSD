
using System;
using System.Windows.Forms;
using QLNhanSu.UI;

namespace QLNhanSu
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            // Khoi dong giao dien chinh
            Application.Run(new FrmMain());
        }
    }
}
