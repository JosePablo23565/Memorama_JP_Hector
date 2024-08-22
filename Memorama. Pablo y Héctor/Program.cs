using System;
using System.Windows.Forms;

namespace Memorama.Pablo_y_Héctor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm()); // Get started with StarForm and not Form1.cs
        }
    }
}
