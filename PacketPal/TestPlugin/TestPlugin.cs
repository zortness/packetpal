using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tamir.IPLib.Packets;

namespace Kopf.PacketPal.Plugins
{
    public class TestPlugin : Plugin
    {
        public override string getName()
        {
            return "Test Plugin";
        }

        public override string getVersion()
        {
            return "1.0 alpha";
        }

        public override string getAuthor()
        {
            return "Kurtis Kopf";
        }

        public override string getWebAddress()
        {
            // should be a relevant way to find the author of the plugin, will not be a link
            return "Kurtis.Kopf@gmail.com";
        }

        public override string getTooltip()
        {
            return "Test Plugin Tooltip";
        }

        public override string getHelp()
        {
            return "Test Plugin help text.";
        }

        public override void activate(FormPluginInterface parentForm)
        {
            MessageBox.Show("Activated Test Plugin!");
        }
    }
}
