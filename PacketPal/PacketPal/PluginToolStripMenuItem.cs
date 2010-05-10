using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Kopf.PacketPal.Plugins;

namespace Kopf.PacketPal
{
    /**
     * Extend the ToolStripMenuItem class to hold additional information
     * for the Plugin menu.
     */
    class PluginToolStripMenuItem: ToolStripMenuItem
    {
        private Plugin myPlugin;

        public PluginToolStripMenuItem(Plugin plugin, string message): base(message)
        {
            myPlugin = plugin;
        }

        public Plugin getPlugin()
        {
            return myPlugin;
        }

    }
}
