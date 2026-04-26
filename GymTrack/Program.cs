using System;
using System.Windows.Forms;

namespace GymTrack
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DatabaseHelper.InitializeDatabase();
            }
            catch
            {
                // Continue even if DB setup fails
            }

            Application.Run(new Form1());
        }
    }
}