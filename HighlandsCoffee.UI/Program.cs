using System;
using System.Windows.Forms;

namespace HighlandsCoffee.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmDangNhap());
        }
    }
}