using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpPcap;

namespace Kopf.PacketPal
{
    public partial class ExtendedInfoForm : Form
    {
        public ExtendedInfoForm(PcapDevice dev)
        {
            
            InitializeComponent();

            string myInfo = "";
            // get general info for the device
            myInfo += dev.Description + "\r\n";
            myInfo += "Name:\t\t" + dev.Name + "\r\n";
            myInfo += "Hardware Address:\t\t" + dev.Interface.MacAddress.ToString() + "\r\n";
            myInfo += "Gateway Address:\t\t" + dev.Interface.GatewayAddress.ToString() + "\r\n";

            if (dev is LivePcapDevice)
            {
                LivePcapDevice netDev = (LivePcapDevice)dev;
				if (netDev.Loopback)
				{
					myInfo += "Loopback:\tTRUE\r\n";
				}
				
				IEnumerator<SharpPcap.PcapAddress> addresses = netDev.Addresses.GetEnumerator();
				while (addresses.MoveNext())
				{
					SharpPcap.PcapAddress address = addresses.Current;
					myInfo += "Address:\t\t" + address.Addr.ToString() + "\r\n";
				}
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