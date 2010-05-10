using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class IPEditorForm : Form
    {
        private IPEditor myParent;
        private int myVersion; // 4 bits
        private int myHeaderLength; // in bytes, 4 bits
        private int myTOS; // 8 bits
        private int myLength; // in bytes, 16 bits
        private int myID; // 16 bits
        private int myFlags; // 3 bits
        private int myFragmentOffset; // 13 bits
        private int myTTL; // 8 bits
        private int myProtocol; // 8 bits
        private int myChecksum; // 16 bits
        private string mySource; // 32 bits
        private string myDest; // 32 bits
        private string myData; // 2^16 - 20 bytes max

        // recompile after data change
        public bool reCompile = false;

        // recompute checksum
        public bool reCompute = false;

        /*
         * Constructor
         */
        public IPEditorForm(IPEditor parent, int version, int headerLength, int tos, int length, int id, int flags,
            int fragmentOffset, int ttl, int protocol, int checksum, string source, string destination, string data)
        {
            InitializeComponent();

            // set local vars
            myParent = parent;
            myVersion = version;
            myHeaderLength = headerLength;
            myTOS = tos;
            myLength = length;
            myID = id;
            myFlags = flags;
            myFragmentOffset = fragmentOffset;
            myTTL = ttl;
            myProtocol = protocol;
            myChecksum = checksum;
            mySource = source;
            myDest = destination;
            myData = data;

            // set form displays
            txtVersion.Text = myVersion.ToString();
            txtHeaderLength.Text = myHeaderLength.ToString();
            txtTOS.Text = myTOS.ToString();
            txtLength.Text = myLength.ToString();
            txtID.Text = myID.ToString();
            // evil bit
            if (myFlags >= 4)
            {
                checkBoxEvil.Checked = true;
            }
            // don't fragment bit
            if (myFlags == 2 || myFlags == 3 || myFlags == 6 || myFlags == 7)
            {
                checkBoxDF.Checked = true;
            }
            // more fragments bit
            if (myFlags == 1 || myFlags == 3 || myFlags == 5 || myFlags == 7)
            {
                checkBoxMF.Checked = true;
            }
            txtFragOffset.Text = myFragmentOffset.ToString();
            txtTTL.Text = myTTL.ToString();
            txtProtocol.Text = myProtocol.ToString();
            txtChecksum.Text = myChecksum.ToString();
            txtSource.Text = mySource;
            txtDestination.Text = myDest;
            txtData.Text = myData;
            
        }

        /*
         * Get version.
         */
        public int getVersion()
        {
            return myVersion;
        }

        /*
         * Get header length.
         */
        public int getHeaderLength()
        {
            return myHeaderLength;
        }

        /*
         * Get type of service.
         */
        public int getTypeOfService()
        {
            return myTOS;
        }

        /*
         * Get total length.
         */
        public int getTotalLength()
        {
            return myLength;
        }

        /*
         * Get ID.
         */
        public int getID()
        {
            return myID;
        }

        /*
         * Get flags.
         */
        public int getFlags()
        {
            return myFlags;
        }

        /*
         * Get fragment offset.
         */
        public int getFragmentOffset()
        {
            return myFragmentOffset;
        }

        /*
         * Get TTL.
         */
        public int getTimeToLive()
        {
            return myTTL;
        }

        /*
         * Get IP Protocol.
         */
        public int getProtocol()
        {
            return myProtocol;
        }

        /*
         * Get Header Checksum.
         */
        public int getChecksum()
        {
            return myChecksum;
        }

        /*
         * Get source IP address.
         */
        public string getSourceAddress()
        {
            return mySource;
        }

        /*
         * Get destination IP address.
         */
        public string getDestinationAddress()
        {
            return myDest;
        }

        /*
         * Get Data.
         */
        public string getData()
        {
            return myData;
        }


        /**
         * Data verification functions
         */
        /*
         * version
         */
        private void verifyVersion(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyVersion(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * header length
        */
        private void verifyHeaderLength(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyHeaderLength(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * type of service
        */
        private void verifyTOS(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyTOS(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * total length
        */
        private void verifyTotalLength(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyTotalLength(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * id #
        */
        private void verifyID(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyID(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
         * Recompile flags
         */
        private void recompileFlags(object sender, EventArgs e)
        {
        }

        /*
        * frag offset
        */
        private void verifyFragmentOffset(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyFragmentOffset(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * time to live
        */
        private void verifyTTL(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyTimeToLive(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * protocol
        */
        private void verifyProtocol(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyProtocol(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * header checksum
        */
        private void verifyChecksum(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyChecksum(int.Parse(((TextBox)sender).Text)))
                {
                    btnSave.Enabled = true;
                    ((TextBox)sender).BackColor = Color.White;
                    ((TextBox)sender).ForeColor = Color.Black;
                }
                else
                {
                    btnSave.Enabled = false;
                    ((TextBox)sender).Focus();
                    ((TextBox)sender).BackColor = Color.Red;
                    ((TextBox)sender).ForeColor = Color.White;
                }
            }
            catch (Exception ee)
            {
                // we get here when the int.parse dies
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * source
        */
        private void verifySource(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifySourceAddress(((TextBox)sender).Text))
            {
                btnSave.Enabled = true;
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
            }
            else
            {
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * destination
        */
        private void verifyDest(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyDestinationAddress(((TextBox)sender).Text))
            {
                btnSave.Enabled = true;
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
            }
            else
            {
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        /*
        * data
        */
        private void verifyData(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyData(((TextBox)sender).Text))
            {
                btnSave.Enabled = true;
                ((TextBox)sender).BackColor = Color.White;
                ((TextBox)sender).ForeColor = Color.Black;
            }
            else
            {
                btnSave.Enabled = false;
                ((TextBox)sender).Focus();
                ((TextBox)sender).BackColor = Color.Red;
                ((TextBox)sender).ForeColor = Color.White;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            myVersion = int.Parse(txtVersion.Text);
            myHeaderLength = int.Parse(txtHeaderLength.Text);
            myTOS = int.Parse(txtTOS.Text);
            myLength = int.Parse(txtLength.Text);
            myID = int.Parse(txtID.Text);
            // flags
            myFlags = 0;
            if (checkBoxEvil.Checked)
            {
                myFlags = myFlags | 4; // 0x100b
            }
            if (checkBoxDF.Checked)
            {
                myFlags = myFlags | 2; // 0x10b
            }
            if (checkBoxMF.Checked)
            {
                myFlags = myFlags | 1; // 0x1b
            }
            myFragmentOffset = int.Parse(txtFragOffset.Text);
            myTTL = int.Parse(txtTTL.Text);
            myProtocol = int.Parse(txtProtocol.Text);
            if (txtChecksum.Text == "")
            {
                myChecksum = 0;
            }
            else
            {
                myChecksum = int.Parse(txtChecksum.Text);
            }
            mySource = txtSource.Text;
            myDest = txtDestination.Text;

            // check if data changed
            if (myData != txtData.Text)
            {
                myData = txtData.Text;
                reCompile = true;
            }

            // check if we need to recalculate the header checksum
            if (checkBoxChecksum.Checked)
            {
                reCompute = true;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}