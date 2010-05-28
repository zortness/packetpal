using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using Kopf.PacketPal.PacketEditors;
using Kopf.PacketPal.Plugins;
using Kopf.PacketPal.TCPIPLayers;
using Kopf.PacketPal.Util;
using SharpPcap;
using PacketDotNet;

namespace Kopf.PacketPal
{
    public partial class PacketPalForm : FormPluginInterface
    {
        /**
         * Constants
         */
        #region constants

        // capture timeout in milliseconds
        const int myTimeout = 1;

        // version
        public const string myVersion = "1.2";

        #endregion constants



        /**
         * Internal fields.
         */
        #region fields

        // list of devices
        ArrayList myNetworkDevices;
        // current capture device
        PcapDevice myCaptureDevice;
        // current send device
        PcapDevice mySendDevice;
        // packets captured this session
        int myPacketSessionCount = 0;
        // packets captured total
        int myPacketTotalCount = 0;
        // packets sent this session
        int myPacketSessionCountSent = 0;
        // packet sent total
        int myPacketTotalCountSent = 0;
        // capture packet list
        ArrayList myCapturedPackets;
        // send packet list
        ArrayList mySendPackets;
        // packet editor list
        ArrayList myPlugins;
        // plugin list
        ArrayList myPacketEditors;

        #endregion fields



        /**
         * Delegate methods for thread Callbacks.
         */
        #region delegate_callbacks

        // captured packet callback
        delegate void AddCapturedPacketCallback(Packet packet);
        // increment counters callback
        delegate void IncrementSessionCallback();

        // send packet callback (for loading files into send queue)
        delegate void AddSendPacketCallback(Packet packet);

        // sync captured packets
        delegate void SyncCapturedListCallback();
        // sync send queue packets
        delegate void SyncSendListCallback();

        // reset network devices
        delegate void ResetNetworkDeviceCallback();

        #endregion delegate_callbacks



        /**
         * Methods pertaning to loading information.
         */
        #region loading

        /*
         * Constructor
         */
        public PacketPalForm()
        {

            InitializeComponent();

            // setup capture form
            labelCapturedSession.Text = myPacketSessionCount.ToString();
            labelCapturedTotal.Text = myPacketTotalCount.ToString();
            btnStopCapture.Enabled = false;
            checkBoxScroll.Checked = true;
            checkBoxUpdate.Checked = true;

            // set up send form
            labelSendSession.Text = myPacketSessionCountSent.ToString();
            labelSendTotal.Text = myPacketTotalCountSent.ToString();
            labelSendingComp.Text = "0";
            labelSendingTotal.Text = "0";
            btnStopSend.Enabled = false;
            panelSending.Visible = false;

            myCapturedPackets = ArrayList.Synchronized(new ArrayList());
            mySendPackets = ArrayList.Synchronized(new ArrayList());
            myNetworkDevices = ArrayList.Synchronized(new ArrayList());

            // get network devices
            loadNetworkInterfaces();
            // get packets
            loadPackets();
            // get editors
            loadPacketEditors();
            // get plugins
            loadPlugins();
        }


        /*
         * Get a list of available network interfaces.
         * 
         * Note: This function is called after every capture or send
         *   session. There appears to be a bug in PCap that will not allow
         *   a device to capture more data after it has been closed
         *   once. The easy fix provided is to re-gather the NetworkDevice
         *   objects after every use. Luckily, this is usually very quick.
         */
        private void loadNetworkInterfaces()
        {
            int lastCapIndex = 0;
            int lastSendIndex = 0;
            // last selected interfaces
            if (listBoxCaptureInterface.SelectedIndex >= 0)
            {
                lastCapIndex = listBoxCaptureInterface.SelectedIndex;
            }
            if (listBoxSendInterface.SelectedIndex >= 0)
            {
                lastSendIndex = listBoxSendInterface.SelectedIndex;
            }

            myNetworkDevices.Clear();
            listBoxCaptureInterface.Items.Clear();
            listBoxSendInterface.Items.Clear();
            // all devices
			LivePcapDeviceList list = SharpPcap.LivePcapDeviceList.Instance;
			IEnumerator<LivePcapDevice> devices = list.GetEnumerator();
			while (devices.MoveNext())
            {
            		LivePcapDevice dev = devices.Current;
				// register our packet handling function with the OnPacketArrival event
				dev.OnPacketArrival +=
                    new SharpPcap.PacketArrivalEventHandler(device_PcapOnPacketArrival);
                // register our shutdown event
				dev.OnCaptureStopped +=
                    new SharpPcap.CaptureStoppedEventHandler(device_PcapOnPacketCaptureStopped);
                // add the device to our list
                myNetworkDevices.Add((PcapDevice)dev);

                // add a description of the device to our capture and send lists
                string ip = "";
                IEnumerator<PcapAddress> addresses = dev.Addresses.GetEnumerator();
                while (addresses.MoveNext())
                {
                		PcapAddress address = addresses.Current;
					ip += address.Addr.ToString() + ";";
				}
                listBoxCaptureInterface.Items.Add(ip);
                listBoxSendInterface.Items.Add(ip);
            }

            // no usable interaces found
            if (listBoxCaptureInterface.Items.Count < 1)
            {
                MessageBox.Show("No usable network devices were found. You will be unable to capture or send packets.");
                myCaptureDevice = null;
                mySendDevice = null;
                // disable capture
                btnStartCapture.Enabled = false;
                btnStopCapture.Enabled = false;
                txtFilter.Enabled = false;
                // disable send 
                btnStartSend.Enabled = false;
                btnStopSend.Enabled = false;
                // disable extended info
                buttonExtCap.Enabled = false;
                buttonExtSend.Enabled = false;
            }
            else
            {
                // select the first item in each of the lists
                if (lastCapIndex < listBoxCaptureInterface.Items.Count)
                {
                    listBoxCaptureInterface.SelectedIndex = lastCapIndex;
                }
                if (lastSendIndex < listBoxSendInterface.Items.Count)
                {
                    listBoxSendInterface.SelectedIndex = lastSendIndex;
                }
                // set the initial devices
                setCaptureDevice();
                setSendDevice();
            }
        }
        

        /*
         * Load the extended Packet objects.
         */
        private void loadPackets()
        {
            // Not implimented yet.
            // Method of registering new Packets with Pcap is unknown.
        }


        /*
         * Load available packet editors.
         */
        private void loadPacketEditors()
        {
            // use the packet editor loader
            try
            {
                myPacketEditors = new ArrayList();
                PacketEditorLoader.loadFromDir("editors", ref myPacketEditors);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to load packet editors!\n" + e.Message);
            }
        }


        /*
         * Load available plugins.
         */
        private void loadPlugins()
        {
            // use the plugin loader
            try
            {
                myPlugins = new ArrayList();
                PluginLoader.loadFromDir("plugins", ref myPlugins);

                pluginsToolStripMenuItem.DropDownItems.Clear();

                // add the plugins to the menu
                IEnumerator ie = myPlugins.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (ie.Current is Plugin)
                    {
                        ToolStripMenuItem temp = new PluginToolStripMenuItem((Plugin)ie.Current, ((Plugin)ie.Current).getName());
                        temp.Click += new EventHandler(activatePlugin_Click);
                        pluginsToolStripMenuItem.DropDownItems.Add(temp);
                    }
                }

                // add reload
                ToolStripMenuItem reload = new ToolStripMenuItem("Reload Plugins");
                reload.Click += new EventHandler(reloadPlugins_Click);
                pluginsToolStripMenuItem.DropDownItems.Add(reload);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to load plugins!\n" + e.Message);
            }
            // build the menu
        }

        #endregion loading



        /**
         * Helper methods.
         */
        #region helper_methods

        /*
         * Set the network device according to what's selected in the Capture list.
         */
        private void setCaptureDevice()
        {
            // the indexes between the device list and the displayed text should match up
            IEnumerator e = myNetworkDevices.GetEnumerator(listBoxCaptureInterface.SelectedIndex, 1);
            e.MoveNext();
            if (e.Current is PcapDevice)
            {
                myCaptureDevice = (PcapDevice)e.Current;
                txtCapDeviceInfo.Text = ((PcapDevice)e.Current).Description;
            }
        }


        /*
        * Set the network device according to what's selected in the Send list.
        */
        private void setSendDevice()
        {
            // the indexes between the device list and the displayed text should match up
            IEnumerator e = myNetworkDevices.GetEnumerator(listBoxSendInterface.SelectedIndex, 1);
            e.MoveNext();
            if (e.Current is PcapDevice)
            {
                mySendDevice = (PcapDevice)e.Current;
                txtSendDeviceInfo.Text = ((PcapDevice)e.Current).Description;
            }
        }



        /*
         * Sync captured packet list with display list. Thread safe.
         */
        private void syncCapturedList()
        {
            if (listBoxCapture.InvokeRequired)
            {
                SyncCapturedListCallback d = new SyncCapturedListCallback(syncCapturedList);
                Invoke(d, new object[0]);
            }
            else
            {
                listBoxCapture.Items.Clear();
                IEnumerator e = myCapturedPackets.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current is Packet)
                    {
                        listBoxCapture.Items.Add(((Packet)e.Current).ToString());
                    }
                }
            }
        }


        /*
         * Sync send packet list with display. Thread safe.
         */
        private void syncSendList()
        {
            if (listBoxSend.InvokeRequired)
            {
                SyncSendListCallback d = new SyncSendListCallback(syncSendList);
                Invoke(d, new object[0]);
            }
            else
            {
                listBoxSend.Items.Clear();
                IEnumerator e = mySendPackets.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current is Packet)
                    {
                        listBoxSend.Items.Add(((Packet)e.Current).ToString());
                    }
                }
            }
        }


        /*
         * Thread safe packet capture handler.
         */
        private void addCapturedPacket(Packet packet)
        {
            if (packet == null)
            {
                System.Console.WriteLine("Got aa null packet!");
                return;
            }
            if (checkBoxUpdate.Checked)
            {
                // not accessable yet
                if (listBoxCapture.InvokeRequired)
                {
                    // set up a callback so this function can be called later by the righ thread
                    AddCapturedPacketCallback d = new AddCapturedPacketCallback(addCapturedPacket);
                    // invoke the callback event
                    Invoke(d, new object[] { packet });
                }
                else
                {
                    // this is actually all we cared about
                    myCapturedPackets.Add(packet);
                    listBoxCapture.Items.Add(packet.ToString());

                    if (checkBoxScroll.Checked)
                    {
                        listBoxCapture.SelectedIndex = listBoxCapture.Items.Count - 1;
                    }
                }
            }
            else
            {
                myCapturedPackets.Add(packet);
            }
        }


        /*
         * Thread safe session packet count incrementer.
         */
        private void incrementSessionCount()
        {
            // not accessable yet
            if (labelCapturedSession.InvokeRequired)
            {
                // set up a callback so this function can be called later by the right thread
                IncrementSessionCallback d = new IncrementSessionCallback(incrementSessionCount);
                // invoke the callback event
                Invoke(d, new object[0]);
            }
            else
            {
                // all the stuff we actually want to do, increment some counters
                myPacketSessionCount++;
                myPacketTotalCount++;
                labelCapturedSession.Text = myPacketSessionCount.ToString();
                labelCapturedTotal.Text = myPacketTotalCount.ToString();
            }
        }


        /*
         * Grab the plugin arraylist (for about form)
         */
        public ArrayList getPlugins()
        {
            return myPlugins;
        }

        /*
         * Grab the packet editor arraylist (for about form)
         */
        public ArrayList getPacketEditors()
        {
            return myPacketEditors;
        }



        #endregion helper_methods



        /**
         * Pcap Event Handlers.
         */
        #region pcap_event_handlers

        /*
         * Packet capture handler, calls thread safe handlers.
         */
        private void device_PcapOnPacketArrival(object sender, CaptureEventArgs args)
        {
            Packet packet = Packet.ParsePacket(args.Packet);

            //System.Console.WriteLine("- " + packet.ToString());
            //Packet current = packet.PayloadPacket;
            //int count = 1;
            //while (current != null)
            //{
            //    System.Console.WriteLine("--" + count + " " + current.ToString());
            //    count++;
            //    current = current.PayloadPacket;
            //}

            // register packet
            addCapturedPacket(packet);
            // counter
            incrementSessionCount();
        }


        /*
         * Packet loaded from file to send queue.
         */
        private void device_PcapOnPacketArrivalSend(object sender, CaptureEventArgs args)
        {
            Packet packet = Packet.ParsePacket(args.Packet);
            mySendPackets.Add(packet);
        }


        /*
         * Handle PcapOnCaptureStopped event.
         */
        private void device_PcapOnPacketCaptureStopped(object sender, CaptureStoppedEventStatus status)
        {
            //MessageBox.Show("Sender:" + sender.ToString() + "\nFlag:" + flag.ToString());
        }

        #endregion pcap_event_handlers



        /**
         * Form Menu Event Handlers.
         */
        #region main_menu_event_handlers

        /*
         * "Exit" from the menu.
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // stop any network devices that are active
            if (myCaptureDevice is PcapDevice && myCaptureDevice.Opened)
            {
                if (myCaptureDevice.Started)
                {
                    myCaptureDevice.StopCapture();
                }
                myCaptureDevice.Close();
            }

            if (mySendDevice is PcapDevice && mySendDevice.Opened)
            {
                // not sure if this will stop sending a packet queue
                if (mySendDevice.Started)
                {
                    myCaptureDevice.StopCapture();
                }
                myCaptureDevice.Close();
            }

            this.Close();
        }

        /*
         * Show about form.
         */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm(this);
            form.ShowDialog();
        }


        /*
         * Show FAQ web page (local directory)
         */
        private void faqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // .NET *should* open the html file in the default browser
            Process.Start(".\\html\\faq.html");
        }

        /*
         * Show Tutorials web page (local directory)
         */
        private void tutorialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // .NET *should* open the html file in the default browser
            Process.Start(".\\html\\tutorials.html");
        }

        #endregion main_menu_event_handlers



        /**
         * Capture section Event Handlers
         */
        #region capture_event_handlers


        /*
         * Validate the filter string.
         */
        private void validateFilter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
                setCaptureDevice();
                myCaptureDevice.StopCaptureTimeout = new TimeSpan(0, 0, myTimeout);
				myCaptureDevice.Open();
                try
                {
					myCaptureDevice.SetFilter(txtFilter.Text);
                }
                catch (Exception ee)
                {
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                    ((TextBox)sender).Focus();
                    Console.WriteLine("Exception: " + ee.Message);
                }
                myCaptureDevice.Close();
                // reload devices... stupid bug
                loadNetworkInterfaces();
            }
        }


        /*
         * Start the capturing process.
         */
        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            // set the current capture device according to selection
            setCaptureDevice();

            // reset session packet counter
            myPacketSessionCount = 0;
            labelCapturedSession.Text = myPacketSessionCount.ToString();

            // open the device
			myCaptureDevice.StopCaptureTimeout = new TimeSpan(0, 0, myTimeout);
			myCaptureDevice.Open();

            // set the device filter
            try
            {
                myCaptureDevice.SetFilter(txtFilter.Text);
            }
            catch (Exception ee)
            {
                txtFilter.BackColor = Color.Red;
                txtFilter.ForeColor = Color.White;
                txtFilter.Focus();
                Console.WriteLine("Exception: " + ee.Message);
                return;
            }

            // set up the buttons on the form
            btnStartCapture.Enabled = false;
            btnStopCapture.Enabled = true;

            // start the capture
            myCaptureDevice.StartCapture();

        }


        /*
         * Stop the current capture session.
         */
        private void btnStopCapture_Click(object sender, EventArgs e)
        {
            btnStopCapture.Enabled = false;

            // stop capturing
            try
            {
                myCaptureDevice.StopCapture();
            }
            catch (PcapException exception)
            {
                btnStopCapture.Enabled = true;
                MessageBox.Show("Unable to stop PCAP device, please try again in a moment. " + exception.Message);
                return;
            }

            // close the device
            myCaptureDevice.Close();

            // reload the network interfaces... they don't seem to want to work more than once
            loadNetworkInterfaces();

            // resync the packet list
            syncCapturedList();

            // set up the buttons on the form
            btnStartCapture.Enabled = true;
        }


        /*
         * Handle capture device selection change event.
         */
        private void captureDeviceChange(object sender, EventArgs e)
        {
            // the indexes between the device list and the displayed text should match up
            IEnumerator en = myNetworkDevices.GetEnumerator(listBoxCaptureInterface.SelectedIndex, 1);
            en.MoveNext();
            if (en.Current is PcapDevice)
            {
                myCaptureDevice = (PcapDevice)en.Current;
                txtCapDeviceInfo.Text = ((PcapDevice)en.Current).Description;
            }
        }


        /*
         * Build "Captured" context menu.
         */
        private void capturedContextOpening(object sender, CancelEventArgs e)
        {
            // clear menu
            contextMenuStripCapture.Items.Clear();

            // 1 packet selected
            if (listBoxCapture.SelectedIndices.Count == 1)
            {
                // grab the packet
                Packet tempPacket = null;
                IEnumerator ie = myCapturedPackets.GetEnumerator(listBoxCapture.SelectedIndex, 1);
                while (ie.MoveNext())
                {
                    tempPacket = (Packet)ie.Current;
                }
                // check each of the PacketEditors that we have loaded to see if they can handle this packet
                ToolStripMenuItem tempMenu;
                while (tempPacket != null)
                {
                    ie = myPacketEditors.GetEnumerator();
                    PacketEditor tempEditor;
                    while (ie.MoveNext())
                    {
                        tempEditor = (PacketEditor)ie.Current;
                        if (tempEditor.canHandle(tempPacket))
                        {
                            // use our own special menu item wrapper
                            tempMenu = (ToolStripMenuItem)(new PacketEditorToolStripMenuItem(
                                tempEditor, listBoxCapture.SelectedIndex, "Edit As " + tempEditor.getEditAs()));
                            tempMenu.Click += new EventHandler(capturedEditAs_Click);
                            contextMenuStripCapture.Items.Add(tempMenu);
                        }
                    }
                    tempPacket = tempPacket.PayloadPacket;
                }

                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripCapture.Items.Add(separator);

                ToolStripMenuItem temp1 = new ToolStripMenuItem("Add to Send Queue");
                temp1.Click += new EventHandler(selectedCapturedToSend_Click);
                contextMenuStripCapture.Items.Add(temp1);

                ToolStripMenuItem temp3 = new ToolStripMenuItem("Delete Selected Packet");
                temp3.Click += new EventHandler(deleteSelectedCapture_Click);
                contextMenuStripCapture.Items.Add(temp3);
            }
            // multiple packets selected
            else if (listBoxCapture.SelectedIndices.Count > 1)
            {
                ToolStripMenuItem temp1 = new ToolStripMenuItem("Add Selected Packets to Send Queue");
                temp1.Click += new EventHandler(selectedCapturedToSend_Click);
                ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete Selected Packets");
                temp2.Click += new EventHandler(deleteSelectedCapture_Click);
                contextMenuStripCapture.Items.Add(temp1);
                contextMenuStripCapture.Items.Add(temp2);
            }

            // add / delete all
            if (listBoxCapture.Items.Count > 0)
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripCapture.Items.Add(separator);
                ToolStripMenuItem temp1 = new ToolStripMenuItem("Add all Packets to Send Queue");
                temp1.Click += new EventHandler(allCapturedToSend_Click);
                ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete all Packets");
                temp2.Click += new EventHandler(deleteAllCaptured_Click);
                contextMenuStripCapture.Items.Add(temp1);
                contextMenuStripCapture.Items.Add(temp2);
            }

            // create new packets
            if (myPacketEditors.Count > 0)
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripCapture.Items.Add(separator);
                IEnumerator ie = myPacketEditors.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (ie.Current is PacketEditor)
                    {
                        ToolStripMenuItem temp = new PacketEditorToolStripMenuItem((PacketEditor)ie.Current, 0,
                            "Create New " + ((PacketEditor)ie.Current).getEditAs());
                        temp.Click += new EventHandler(captureNew_Click);
                        contextMenuStripCapture.Items.Add(temp);
                    }
                }
            }
        }


        /*
         * "Add All Packets to Send Queue" (from captured queue) clicked
         */
        private void allCapturedToSend_Click(object sender, EventArgs e)
        {
            IEnumerator ie = myCapturedPackets.GetEnumerator();
            Packet tempPacket;
            //byte[] tempBytes;
            while (ie.MoveNext())
            {
                /**
                 * this method doesn't seem to work properly
                 *
                // we have to make a copy of the packet, otherwise we just get a reference to the same exact one
                tempBytes = ArrayHelper.copy(((Packet)ie.Current).Data);
                //MessageBox.Show("Packet Data Size: " + tempBytes.Length);
                tempPacket = PacketFactory.dataToPacket(LinkLayers_Fields.IEEE802, tempBytes);
                 * 
                 */
                /**
                 * This may not be the best workaround, since editing one list may edit the other, 
                 * but it works for now. Note that clearing the captured packet list appears to
                 * leave the send queue in tact, so it is probably just a dereferencing of the object.
                 */
                if (ie.Current is Packet)
                {
                    tempPacket = ((Packet)ie.Current);
                    mySendPackets.Add(tempPacket);
                }
            }

            // sync the send queue
            syncSendList();
        }


        /*
         * "Delete All Packets" (from captured queue) clicked
         */
        private void deleteAllCaptured_Click(object sender, EventArgs e)
        {
            // clear all
            myCapturedPackets.Clear();
            // sync the capture queue
            syncCapturedList();
        }


        /*
         * Add selected captured packets to send queue.
         */
        private void selectedCapturedToSend_Click(object sender, EventArgs e)
        {
            // get selected indices

            IEnumerator ie = listBoxCapture.SelectedIndices.GetEnumerator();
            int i;
            while (ie.MoveNext())
            {
                if (ie.Current is int)
                {
                    i = (int)ie.Current;
                    // grab the packet with this index from the captured array
                    IEnumerator ie2 = myCapturedPackets.GetEnumerator(i, 1);
                    while (ie2.MoveNext())
                    {
                        if (ie2.Current is Packet)
                        {
                            mySendPackets.Add((Packet)ie2.Current);
                        }
                    }
                }
            }
            // sync send list
            syncSendList();
        }


        /*
         * Delete selected packets from capture queue.
         */
        private void deleteSelectedCapture_Click(object sender, EventArgs e)
        {
            // gotta go backwards for this one to maintain index sync, using the enumerator only goes forward
            for (int x = (myCapturedPackets.Count - 1); x >= 0; x--)
            {
                // this one is selected, remove it
                if (listBoxCapture.SelectedIndices.Contains(x))
                {
                    myCapturedPackets.RemoveAt(x);
                }
            }
            // sync the captured list
            syncCapturedList();
        }


        /*
         * "Edit As" clicked from capture queue.
         */
        private void capturedEditAs_Click(object sender, EventArgs e)
        {
            if (sender is PacketEditorToolStripMenuItem)
            {
                // get the packet editor from the custome menu strip item wrapper
                PacketEditor myEditor = ((PacketEditorToolStripMenuItem)sender).getEditor();
                // get the selected packet index
                int myIndex = ((PacketEditorToolStripMenuItem)sender).getIndex();
                // get the selected packet
                IEnumerator ie = myCapturedPackets.GetEnumerator(myIndex, 1);
                Packet myPacket = null;
                while (ie.MoveNext())
                {
                    myPacket = (Packet)ie.Current;
                }
                // send the Packet to the PacketEditor
                if (myPacket != null)
                {
                    while (!myEditor.canHandle(myPacket))
                    {
                        // must be a child packet
                        myPacket = myPacket.PayloadPacket;
                        if (myPacket == null)
                        {
                            throw new Exception("Trying to edit with the wrong type of editor!");
                        }
                    }
                    myPacket = myEditor.guiEdit(myPacket);
                    syncCapturedList();
                }
            }
        }


        /*
        * "Create New" clicked from capture queue.
        */
        private void captureNew_Click(object sender, EventArgs e)
        {
            if (sender is PacketEditorToolStripMenuItem)
            {
                // get the packet editor from the custome menu strip item wrapper
                PacketEditor myEditor = ((PacketEditorToolStripMenuItem)sender).getEditor();
                Packet myPacket = myEditor.guiEdit();
                if (myPacket != null)
                {
                    myCapturedPackets.Add(myPacket);
                    syncCapturedList();
                }
            }
        }


        /*
        * Show extended capture device information.
        */
        private void buttonExtCap_Click(object sender, EventArgs e)
        {
            // send the current NetworkDevice to ExtendedInfoForm
            Form myForm = new ExtendedInfoForm(myCaptureDevice);
            DialogResult crap = myForm.ShowDialog();
        }


        #endregion capture_event_handlers



        /**
         * Send section event handlers.
         */
        #region send_event_handlers


        /*
        * Handle send device selection change event.
        */
        private void sendDeviceChange(object sender, EventArgs e)
        {
            // the indexes between the device list and the displayed text should match up
            IEnumerator en = myNetworkDevices.GetEnumerator(listBoxSendInterface.SelectedIndex, 1);
            en.MoveNext();
            if (en.Current is PcapDevice)
            {
                mySendDevice = (PcapDevice)en.Current;
                txtSendDeviceInfo.Text = ((PcapDevice)en.Current).Description;
            }
        }


        /*
         * Build "Send" context menu.
         */
        private void sendContextOpening(object sender, CancelEventArgs e)
        {
            // clear menu
            contextMenuStripSend.Items.Clear();

            // 1 packet selected
            if (listBoxSend.SelectedIndices.Count == 1)
            {
                // grab the packet
                Packet tempPacket = null;
                IEnumerator ie = mySendPackets.GetEnumerator(listBoxSend.SelectedIndex, 1);
                while (ie.MoveNext())
                {
                    tempPacket = (Packet)ie.Current;
                }
                // check each of the PacketEditors that we have loaded to see if they can handle this packet
                ToolStripMenuItem tempMenu;
                while (tempPacket != null)
                {
                    ie = myPacketEditors.GetEnumerator();
                    PacketEditor tempEditor;
                    while (ie.MoveNext())
                    {
                        tempEditor = (PacketEditor)ie.Current;
                        if (tempEditor.canHandle(tempPacket))
                        {
                            // use our own special menu item wrapper
                            tempMenu = (ToolStripMenuItem)(new PacketEditorToolStripMenuItem(
                                tempEditor, listBoxSend.SelectedIndex, "Edit As " + tempEditor.getEditAs()));
                            tempMenu.Click += new EventHandler(sendEditAs_Click);
                            contextMenuStripSend.Items.Add(tempMenu);
                        }
                    }
                    tempPacket = tempPacket.PayloadPacket;
                }

                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripSend.Items.Add(separator);

                ToolStripMenuItem temp3 = new ToolStripMenuItem("Delete Selected Packet");
                temp3.Click += new EventHandler(deleteSelectedSend_Click);
                contextMenuStripSend.Items.Add(temp3);

            }
            // multiple packets selected
            else if (listBoxSend.SelectedIndices.Count > 1)
            {
                ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete Selected Packets");
                temp2.Click += new EventHandler(deleteSelectedSend_Click);
                contextMenuStripSend.Items.Add(temp2);
            }

            // delete all
            if (listBoxSend.Items.Count > 0)
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripSend.Items.Add(separator);
                ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete all Packets");
                temp2.Click += new EventHandler(deleteAllSend_Click);
                contextMenuStripSend.Items.Add(temp2);
            }

            // create new packets
            if (myPacketEditors.Count > 0)
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripSend.Items.Add(separator);
                IEnumerator ie = myPacketEditors.GetEnumerator();
                while(ie.MoveNext())
                {
                    if(ie.Current is PacketEditor)
                    {
                        ToolStripMenuItem temp = new PacketEditorToolStripMenuItem((PacketEditor)ie.Current, 0, 
                            "Create New " + ((PacketEditor)ie.Current).getEditAs());
                        temp.Click += new EventHandler(sendNew_Click);
                        contextMenuStripSend.Items.Add(temp);
                    }
                }
            }

        }


        /*
         * "Delete All Packets" (from send queue) clicked
         */
        private void deleteAllSend_Click(object sender, EventArgs e)
        {
            // clear all
            mySendPackets.Clear();
            // sync the capture queue
            syncSendList();
        }


        /*
         * Delete selected packets from send queue.
         */
        private void deleteSelectedSend_Click(object sender, EventArgs e)
        {
            // gotta go backwards for this one to maintain index sync, using the enumerator only goes forward
            for (int x = (mySendPackets.Count - 1); x >= 0; x--)
            {
                // this one is selected, remove it
                if (listBoxSend.SelectedIndices.Contains(x))
                {
                    mySendPackets.RemoveAt(x);
                }
            }
            // sync the send list
            syncSendList();
        }


        /*
         * "Edit As" clicked from send queue.
         */
        private void sendEditAs_Click(object sender, EventArgs e)
        {
            if (sender is PacketEditorToolStripMenuItem)
            {
                // get the packet editor from the custome menu strip item wrapper
                PacketEditor myEditor = ((PacketEditorToolStripMenuItem)sender).getEditor();
                // get the selected packet index
                int myIndex = ((PacketEditorToolStripMenuItem)sender).getIndex();
                // get the selected packet
                IEnumerator ie = mySendPackets.GetEnumerator(myIndex, 1);
                Packet myPacket = null;
                while (ie.MoveNext())
                {
                    myPacket = (Packet)ie.Current;
                }
                // send the Packet to the PacketEditor
                if (myPacket != null)
                {
                    while (!myEditor.canHandle(myPacket))
                    {
                        // must be a child packet
                        myPacket = myPacket.PayloadPacket;
                        if (myPacket == null)
                        {
                            throw new Exception("Trying to edit with the wrong type of editor!");
                        }
                    }
                    myPacket = myEditor.guiEdit(myPacket);
                    syncSendList();
                }
            }
        }


        /*
         * "Create New" clicked from send queue.
         */
        private void sendNew_Click(object sender, EventArgs e)
        {
            if (sender is PacketEditorToolStripMenuItem)
            {
                // get the packet editor from the custome menu strip item wrapper
                PacketEditor myEditor = ((PacketEditorToolStripMenuItem)sender).getEditor();
                Packet myPacket = myEditor.guiEdit();
                if (myPacket != null)
                {
                    mySendPackets.Add(myPacket);
                    syncSendList();
                }
            }
        }


        /*
         * Show extended send device information.
         */
        private void buttonExtSend_Click(object sender, EventArgs e)
        {
            // send the current NetworkDevice to ExtendedInfoForm
            Form myForm = new ExtendedInfoForm(mySendDevice);
            DialogResult crap = myForm.ShowDialog();
        }


        /*
         * Send Packets!
         */
        private void btnStartSend_Click(object sender, EventArgs e)
        {
            // reset the network device
            resetNetworkDevice();

            // set the current send device according to selection
            setSendDevice();

            // reset session packet counter
            myPacketSessionCountSent = 0;
            labelSendSession.Text = myPacketSessionCountSent.ToString();

            int toSend = 0;
            labelSendingTotal.Text = toSend.ToString();
            int sent = 0;
            labelSendingComp.Text = sent.ToString();
            progressBarSend.Value = 0;

            // make sure our PcapDevice is capable of sending packets
            if (!(mySendDevice is LivePcapDevice))
            {
                MessageBox.Show("The selected device is not able to broadcast network packets.\r\n" +
                    "Please select another device before attempting to send packets.");
                return;
            }

            // make sure we have something to send
            if (mySendPackets.Count == 0 ||
                (checkBoxSendSelected.Checked && listBoxCapture.SelectedIndices.Count == 0))
            {
                MessageBox.Show("Nothing to send!");
                return;
            }

            // disable the start button
            btnStartSend.Enabled = false;

            // show the status information
            panelSending.Visible = true;

            // open the device
            mySendDevice.Open();

            List<Packet> packetsToSend = new List<Packet>();
            int sendSize = 0;

            // determine which packets to send
            if (checkBoxSendSelected.Checked)
            {
                toSend = listBoxSend.SelectedIndices.Count;
                labelSendingTotal.Text = toSend.ToString();
                // grab the selected packets
                IEnumerator ie1 = listBoxSend.SelectedIndices.GetEnumerator();
                while (ie1.MoveNext())
                {
                    if (ie1.Current is int)
                    {
                        IEnumerator ie2 = mySendPackets.GetEnumerator((int)ie1.Current, 1);
                        while (ie2.MoveNext())
                        {
                            if (ie2.Current is Packet)
                            {
                                packetsToSend.Add((Packet)ie2.Current);
                                sendSize += ((Packet)ie2.Current).Bytes.Length;
                            }
                        }
                    }
                }


            }
            else
            {
                toSend = mySendPackets.Count;
                labelSendingTotal.Text = toSend.ToString();
                // start sending
                IEnumerator ie = mySendPackets.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (ie.Current is Packet)
                    {
                        packetsToSend.Add((Packet)ie.Current);
                        sendSize += ((Packet)ie.Current).Bytes.Length;
                    }
                }
            }

            try
            {
                SendQueue myQueue = new SendQueue(sendSize);
                foreach(Packet packet in packetsToSend)
                {
                    myQueue.Add(packet.Bytes);
                }
                myQueue.Transmit((LivePcapDevice)mySendDevice, SendQueueTransmitModes.Normal);
                sent = toSend;
                labelSendingComp.Text = sent.ToString();
                progressBarSend.Value = sent / toSend;
                myPacketSessionCountSent += sent;
                myPacketTotalCountSent += sent;
                labelSendSession.Text = myPacketSessionCountSent.ToString();
                labelSendTotal.Text = myPacketTotalCountSent.ToString();
            }
            catch (PcapException ex)
            {
                MessageBox.Show("Failed to send packets!\r\nCheck to make sure the selected network device is enabled!\r\n"
                    + "Pcap Message: " + ex.Message);
            }

            // hide the status information
            panelSending.Visible = false;

            // close the device
            mySendDevice.Close();

            // reset the device
            resetNetworkDevice();

            // re-enable the start button
            btnStartSend.Enabled = true;
        }


        #endregion send_event_handlers



        /**
         * Edit Menu Event Handlers
         */
        #region edit_event_handlers
        /*
         * Build "Edit" dropdown menu.
         */
        private void editMenuOpening(object sender, EventArgs e)
        {
            if (!(sender is MenuItem))
            {
                System.Console.WriteLine("Got something that isn't a MenuItem for the menu handler");
                return;
            }
            MenuItem menu = (MenuItem)sender;
            
            
            // clear menu
            editToolStripMenuItem.DropDownItems.Clear();

            // are we looking at the captured or send tab?
            ListBox tempListBox;
            ArrayList tempPacketList;
            if (tabControlPackets.SelectedTab == tabPageCapture)
            {
                tempListBox = listBoxCapture;
                tempPacketList = myCapturedPackets;
            }
            else
            {
                tempListBox = listBoxSend;
                tempPacketList = mySendPackets;
            }


            // 1 packet selected
            if (tempListBox.SelectedIndices.Count == 1)
            {
                // grab the packet
                Packet tempPacket = null;
                IEnumerator ie = tempPacketList.GetEnumerator(tempListBox.SelectedIndex, 1);
                while (ie.MoveNext())
                {
                    tempPacket = (Packet)ie.Current;
                }
                // check each of the PacketEditors that we have loaded to see if they can handle this packet
                ToolStripMenuItem tempMenu;
                while (tempPacket != null)
                {
                    ie = myPacketEditors.GetEnumerator();
                    PacketEditor tempEditor;
                    while (ie.MoveNext())
                    {
                        tempEditor = (PacketEditor)ie.Current;
                        if (tempEditor.canHandle(tempPacket))
                        {
                            // use our own special menu item wrapper
                            tempMenu = (ToolStripMenuItem)(new PacketEditorToolStripMenuItem(
                                tempEditor, tempListBox.SelectedIndex, "Edit As " + tempEditor.getEditAs()));
                            if (tabControlPackets.SelectedTab == tabPageCapture)
                            {
                                tempMenu.Click += new EventHandler(capturedEditAs_Click);
                            }
                            else
                            {
                                tempMenu.Click += new EventHandler(sendEditAs_Click);
                            }
                            editToolStripMenuItem.DropDownItems.Add(tempMenu);
                        }
                    }
                    tempPacket = tempPacket.PayloadPacket;
                }

                ToolStripSeparator separator = new ToolStripSeparator();
                editToolStripMenuItem.DropDownItems.Add(separator);

                if (tabControlPackets.SelectedTab == tabPageCapture)
                {
                    ToolStripMenuItem temp1 = new ToolStripMenuItem("Add to Send Queue");
                    temp1.Click += new EventHandler(selectedCapturedToSend_Click);
                    editToolStripMenuItem.DropDownItems.Add(temp1);
                }

                ToolStripMenuItem temp3 = new ToolStripMenuItem("Delete Selected Packet");
                if (tabControlPackets.SelectedTab == tabPageCapture)
                {
                    temp3.Click += new EventHandler(deleteSelectedCapture_Click);
                }
                else
                {
                    temp3.Click += new EventHandler(deleteSelectedSend_Click);
                }
                editToolStripMenuItem.DropDownItems.Add(temp3);
            }
            // multiple packets selected
            else if (tempListBox.SelectedIndices.Count > 1)
            {
                if (tabControlPackets.SelectedTab == tabPageCapture)
                {
                    ToolStripMenuItem temp1 = new ToolStripMenuItem("Add Selected Packets to Send Queue");
                    temp1.Click += new EventHandler(selectedCapturedToSend_Click);
                    ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete Selected Packets");
                    temp2.Click += new EventHandler(deleteSelectedCapture_Click);
                    editToolStripMenuItem.DropDownItems.Add(temp1);
                    editToolStripMenuItem.DropDownItems.Add(temp2);
                }
                else
                {
                    ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete Selected Packets");
                    temp2.Click += new EventHandler(deleteSelectedSend_Click);
                    editToolStripMenuItem.DropDownItems.Add(temp2);
                }
            }

            // add / delete all
            if (tempListBox.Items.Count > 0)
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenuStripCapture.Items.Add(separator);
                if (tabControlPackets.SelectedTab == tabPageCapture)
                {
                    ToolStripMenuItem temp1 = new ToolStripMenuItem("Add all Packets to Send Queue");
                    temp1.Click += new EventHandler(allCapturedToSend_Click);
                    ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete all Packets");
                    temp2.Click += new EventHandler(deleteAllCaptured_Click);
                    editToolStripMenuItem.DropDownItems.Add(temp1);
                    editToolStripMenuItem.DropDownItems.Add(temp2);
                }
                else
                {
                    ToolStripMenuItem temp2 = new ToolStripMenuItem("Delete all Packets");
                    temp2.Click += new EventHandler(deleteAllSend_Click);
                    editToolStripMenuItem.DropDownItems.Add(temp2);
                }
            }
        }


        #endregion edit_event_handlers


        /**
         * New Menu Event Handlers
         */
        #region new_event_handlers

        /*
         * Build "New" tool strip menu.
         */
        private void newMenuOpening(object sender, EventArgs e)
        {
            // clear menu
            newToolStripMenuItem.DropDownItems.Clear();

            // create new packets
            if (myPacketEditors.Count > 0)
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                newToolStripMenuItem.DropDownItems.Add(separator);
                IEnumerator ie = myPacketEditors.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (ie.Current is PacketEditor)
                    {
                        ToolStripMenuItem temp = new PacketEditorToolStripMenuItem((PacketEditor)ie.Current, 0,
                            "Create New " + ((PacketEditor)ie.Current).getEditAs());
                        temp.Click += new EventHandler(menuNew_Click);
                        newToolStripMenuItem.DropDownItems.Add(temp);
                    }
                }
            }
        }

        /*
        * "Create New" clicked from "New" menu.
        */
        private void menuNew_Click(object sender, EventArgs e)
        {
            if (sender is PacketEditorToolStripMenuItem)
            {
                // get the packet editor from the custome menu strip item wrapper
                PacketEditor myEditor = ((PacketEditorToolStripMenuItem)sender).getEditor();
                Packet myPacket = myEditor.guiEdit();

                // figure out which tab we're on
                if (tabControlPackets.SelectedTab == tabPageCapture)
                {
                    myCapturedPackets.Add(myPacket);
                    syncCapturedList();
                }
                else
                {
                    mySendPackets.Add(myPacket);
                    syncSendList();
                }
            }
        }

        #endregion new_event_handlers


        /**
         * Plugin Menu Event Handlers
         */
        #region plugin_event_handlers

        /*
         * Activate plugin.
         */
        private void activatePlugin_Click(object sender, EventArgs e)
        {
            if (sender is PluginToolStripMenuItem)
            {
                Plugin temp = ((PluginToolStripMenuItem)sender).getPlugin();
                temp.activate(this);
            }
        }


        /*
         * Force reload of plugins.
         */
        private void reloadPlugins_Click(object sender, EventArgs e)
        {
            loadPlugins();
        }

        #endregion plugin_event_handlers



        /**
         * Methods for saving and loading files.
         */
        #region file_load_save

        /*
         * Load saved packets to captured queue.
         */
        private void loadFileToCapture_Click(object sender, EventArgs e)
        {
            // get file name to open
            openFileDialog1.Title = "Load Saved Packets into Capture Queue";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                // temp device for parsing the file
                PcapDevice device = new OfflinePcapDevice(openFileDialog1.FileName);
                // register packet arrival event
                device.OnPacketArrival +=
                    new PacketArrivalEventHandler(device_PcapOnPacketArrival);
                // open the device
                device.Open();
                // grab all of the packets in the file
                device.Capture();
                // close the device
                device.Close();
            }
        }


        /*
         * Load saved packets to send queue.
         */
        private void loadFileToSend_Click(object sender, EventArgs e)
        {
            // get file name to open
            openFileDialog1.Title = "Load Saved Packets into Capture Queue";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                // temp device for parsing the file
                PcapDevice device = new OfflinePcapDevice(openFileDialog1.FileName);
                // register packet arrival event
                device.OnPacketArrival +=
                    new PacketArrivalEventHandler(device_PcapOnPacketArrivalSend);
                // open the device
                device.Open();
                // grab all of the packets in the file
                device.Capture();
                // close the device
                device.Close();
                // sync the list
                syncSendList();
            }
        }


        /*
         * Save capture queue to file.
         */
        private void saveCaptureToFile_Click(object sender, EventArgs e)
        {
            // get file name to save to
            saveFileDialog1.Title = "Save Captured Packets To File";
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                // open device dump
                myCaptureDevice.Open();
                myCaptureDevice.DumpOpen(saveFileDialog1.FileName);
                // go through capture queue
                IEnumerator ie = myCapturedPackets.GetEnumerator();
                while(ie.MoveNext())
                {
                    if(ie.Current is Packet)
                    {
                        // dump the packet to a file
                        myCaptureDevice.Dump(((Packet)ie.Current).Bytes);
                    }
                }
                // close device
                myCaptureDevice.DumpClose();
                // reset device
                resetNetworkDevice();
            }
        }


        /*
         * Save send queue to file.
         */
        private void saveSendToFile_Click(object sender, EventArgs e)
        {
            // get file name to save to
            saveFileDialog1.Title = "Save Send Queue Packets To File";
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                // open device dump
                mySendDevice.Open();
                mySendDevice.DumpOpen(saveFileDialog1.FileName);
                // go through capture queue
                IEnumerator ie = mySendPackets.GetEnumerator();
                while(ie.MoveNext())
                {
                    if(ie.Current is Packet)
                    {
                        // dump the packet to a file
                        mySendDevice.Dump(((Packet)ie.Current).Bytes);
                    }
                }
                // close device
                mySendDevice.DumpClose();
                // reset device
                resetNetworkDevice();
            }
        }

        #endregion file_save_load




        /**
         * Public inherited methods from FormPluginInterface, used by plugins.
         * The underlying procedures for each of these needs to be thread safe.
         */
        #region public_methods


        /*
         * Get version string.
         */
        public override string getVersion()
        {
            return myVersion;
        }


        /*
         * Get captured packets as ArrayList.
         */
        public override ArrayList getCapturedPackets()
        {
            return myCapturedPackets;
        }


        /*
         * Get send queue packets as ArrayList.
         */
        public override ArrayList getSendQueuePackets()
        {
            return mySendPackets;
        }


        /*
         * Sync captured packets.
         */
        public override void syncCapturedPackets()
        {
            syncCapturedList();
        }

        
        /*
         * Sync send queue packets.
         */
        public override void syncSendQueuePackets()
        {
            syncSendList();
        }


        /*
         * Get network devices as ArrayList.
         */
        public override ArrayList getNetworkDevices()
        {
            return myNetworkDevices;
        }


        /*
         * Get current capture device.
         */
        public override PcapDevice getCaptureDevice()
        {
            return myCaptureDevice;
        }


        /*
         * Get current send device.
         */
        public override PcapDevice getSendDevice()
        {
            return mySendDevice;
        }


        /*
         * Reset the network devices.
         */
        public override void resetNetworkDevice()
        {
            if (listBoxCaptureInterface.InvokeRequired)
            {
                ResetNetworkDeviceCallback d = new ResetNetworkDeviceCallback(resetNetworkDevice);
                Invoke(d, new object[0]);
            }
            else
            {
                loadNetworkInterfaces();
            }
        }



        #endregion public_methods



        




    }
}