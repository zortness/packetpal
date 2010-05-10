using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tamir.IPLib;

namespace Kopf.PacketPal
{
    public partial class ExtendedInfoForm : Form
    {
        public ExtendedInfoForm(PcapDevice dev)
        {
            
            InitializeComponent();

            string myInfo = "";
            // get general info for the device
            myInfo += dev.PcapDescription + "\r\n\r\n";
            myInfo += "Name:\t\t" + dev.PcapName + "\r\n";
            myInfo += "Loopback:\t" + dev.PcapLoopback + "\r\n";

            if (dev is NetworkDevice)
            {
                NetworkDevice netDev = (NetworkDevice)dev;
                myInfo += "\tIP Address:\t\t" + netDev.IpAddress + "\r\n";
                myInfo += "\tSubnet Mask:\t\t" + netDev.SubnetMask + "\r\n";
                myInfo += "\tMAC Address:\t\t" + netDev.MacAddress + "\r\n";
                myInfo += "\tDefault Gateway:\t\t" + netDev.DefaultGateway + "\r\n";
                myInfo += "\tPrimary WINS:\t\t" + netDev.WinsServerPrimary + "\r\n";
                myInfo += "\tSecondary WINS:\t\t" + netDev.WinsServerSecondary + "\r\n";
                myInfo += "\tDHCP Enabled:\t\t" + netDev.DhcpEnabled + "\r\n";
                myInfo += "\tDHCP Server:\t\t" + netDev.DhcpServer + "\r\n";
                myInfo += "\tDHCP Lease Obtained:\t" + netDev.DhcpLeaseObtained + "\r\n";
                myInfo += "\tDHCP Lease Expires:\t" + netDev.DhcpLeaseExpires + "\r\n";
            }

            textBoxInfo.Text = myInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}