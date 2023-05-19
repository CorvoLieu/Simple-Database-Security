using System;
using System.Windows.Forms;

namespace Lab4_Nhom
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            //Application.Run(new MainMenu(new Account("NV07", "NV07")));
            //Application.Run(new DSNV(new Account("NV07", "NV07")));
        }
    }
}
