// ============================================
// 
// Robot PreIndex
// Program.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Windows.Forms;

#endregion

namespace INMET
{
    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Robot_INMET());
        }
    }
}