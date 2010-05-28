using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpPcap;
using System.Collections;
using Kopf.PacketPal.Plugins;
using Kopf.PacketPal.PacketEditors;

namespace Kopf.PacketPal
{
    public partial class AboutForm : Form
    {
        PacketPalForm myParent;

        public AboutForm(PacketPalForm parent)
        {
            InitializeComponent();

            myParent = parent;

            // get packet pal interface version
            labelVersion.Text = parent.getVersion();

            // get packet pal lib version
            labelLibVersion.Text = Kopf.PacketPal.Version.getVersion();

            // get sharppcap version
            labelSharpVersion.Text = SharpPcap.Version.VersionString;

            // get plugin list with info
            string pluginList = "";
            ArrayList plugins = parent.getPlugins();
            IEnumerator ie = plugins.GetEnumerator();
            while (ie.MoveNext())
            {
                if (ie.Current is Plugin)
                {
                    pluginList = pluginList + ((Plugin)ie.Current).getName() + " version "
                        + ((Plugin)ie.Current).getVersion() + " by "
                        + ((Plugin)ie.Current).getAuthor() + " ("
                        + ((Plugin)ie.Current).getWebAddress() + ")\r\n\r\n";
                }
            }
            txtPlugins.Text = pluginList;

            // get packet editor list with info
            string editorList = "";
            ArrayList editors = parent.getPacketEditors();
            ie = editors.GetEnumerator();
            while (ie.MoveNext())
            {
                if (ie.Current is PacketEditor)
                {
                    editorList = editorList + ((PacketEditor)ie.Current).getName() + " version "
                        + ((PacketEditor)ie.Current).getVersion() + " by "
                        + ((PacketEditor)ie.Current).getAuthor() + " ("
                        + ((PacketEditor)ie.Current).getWebAddress() + ")\r\n\r\n";
                }
            }
            txtEditors.Text = editorList;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:Kurtis.Kopf@gmail.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.berghel.net");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codeproject.com/cs/internet/sharppcap.asp");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tamirgal.com/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://jpcap.sourceforge.net/");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tcpdump.org/");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.winpcap.org/");
        }
    }
}