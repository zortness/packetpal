using System;
using System.Collections.Generic;
using System.Text;
using Tamir.IPLib.Packets;

namespace Kopf.PacketPal.Plugins
{
    /**
     * Base class for all PacketPal Plugins.
     */
    public abstract class Plugin
    {
        // get the name of the plugin, for the menu
        public abstract string getName();

        // get the version string of the plugin
        public abstract string getVersion();

        // get the author of the plugin
        public abstract string getAuthor();

        // get the web site address for the plugin
        public abstract string getWebAddress();

        // get the tooltip for hovering over the menu
        public abstract string getTooltip();

        // get the help text for the help page
        public abstract string getHelp();

        // activate the plugin, pass the current working list of packets and which are selected
        public abstract void activate(FormPluginInterface parentForm);
    }
}
