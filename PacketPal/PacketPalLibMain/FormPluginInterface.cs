using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpPcap;

namespace Kopf.PacketPal.Plugins
{
    public partial class FormPluginInterface: Form
    {
        // general information
        public virtual string getVersion() { return "0.0"; }

        // packet lists and control
        public virtual ArrayList getCapturedPackets() { return null; }
        public virtual ArrayList getSendQueuePackets() { return null; }
        public virtual void syncCapturedPackets() { }
        public virtual void syncSendQueuePackets() { }

        // network devices
        public virtual ArrayList getNetworkDevices() { return null; }
        public virtual PcapDevice getCaptureDevice() { return null; }
        public virtual PcapDevice getSendDevice() { return null; }
        public virtual void resetNetworkDevice() { }
        
    }
}
