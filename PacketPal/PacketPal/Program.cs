using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Kopf.PacketPal.Plugins;
using Kopf.PacketPal.TCPIPLayers;
using Kopf.PacketPal.PacketEditors;

namespace Kopf.PacketPal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PacketPalForm());
        }
    }
}