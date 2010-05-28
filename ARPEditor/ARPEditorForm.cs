using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PacketDotNet;
using Kopf.PacketPal.Util;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class ARPEditorForm : Form
    {
        ARPEditor myParent;
        string myHTYPE;
        string myPTYPE;
        int myHLEN;
        int myPLEN;
        string myOPER;
        string mySHA;
        string mySPA;
        string myTHA;
        string myTPA;

        /*
         * Constructor
         */
        public ARPEditorForm(ARPEditor parent, string hardwareType, string protocolType, int hardwareLength, int protocolLength,
            string operation, string senderHwAddress, string senderProtocolAddress, string targetHwAddress,
            string targetProtocolAddress)
        {
            InitializeComponent();
            myParent = parent;
            myHTYPE = hardwareType;
            myPTYPE = protocolType;
            myHLEN = hardwareLength;
            myPLEN = protocolLength;
            myOPER = operation;
            mySHA = senderHwAddress;
            mySPA = senderProtocolAddress;
            myTHA = targetHwAddress;
            myTPA = targetProtocolAddress;

           
            txtHTYPE.Text = myHTYPE;
            txtPTYPE.Text = myPTYPE;
            txtHLEN.Text = myHLEN.ToString();
            txtPLEN.Text = myPLEN.ToString();
            txtOPER.Text = myOPER;
            txtSHA.Text = mySHA;
            txtSPA.Text = mySPA;
            txtTHA.Text = myTHA;
            txtTPA.Text = myTPA;
        }

        /**
         * field retrieval
         */

        /*
         * HTYPE
         */
        public string getHardwareType()
        {
            return myHTYPE;
        }

        /*
        * PTYPE
        */
        public string getProtocolType()
        {
            return myPTYPE;
        }

        /*
        * HLEN
        */
        public int getHardwareLength()
        {
            return myHLEN;
        }

        /*
        * PLEN
        */
        public int getProtocolLength()
        {
            return myPLEN;
        }

        /*
         * OPER
         */
        public string getOperation()
        {
            return myOPER;
        }

        /*
        * SHA
        */
        public string getSenderHardwareAddress()
        {
            return mySHA;
        }

        /*
        * SPA
        */
        public string getSenderProtocolAddress()
        {
            return mySPA;
        }

        /*
        * THA
        */
        public string getTargetHardwareAddress()
        {
            return myTHA;
        }

        /*
        * TPA
        */
        public string getTargetProtocolAddress()
        {
            return myTPA;
        }


        /**
         * field verification
         */
        /*
        * htype
        */
        private void verifyHTYPE(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyHardwareType(((TextBox)sender).Text))
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
        * ptype
        */
        private void verifyPTYPE(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyProtocolType(((TextBox)sender).Text))
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
        * hlen
        */
        private void verifyHLEN(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyHardwareLength(int.Parse(((TextBox)sender).Text)))
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
        * htype
        */
        private void verifyPLEN(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyProtocolLength(int.Parse(((TextBox)sender).Text)))
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
        * oper
        */
        private void verifyOPER(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyOperation(((TextBox)sender).Text))
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
        * sha
        */
        private void verifySHA(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifySenderHardwareAddress(((TextBox)sender).Text))
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
        * spa
        */
        private void verifySPA(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifySenderProtocolAddress(((TextBox)sender).Text))
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
        * tha
        */
        private void verifyTHA(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyTargetHardwareAddress(((TextBox)sender).Text))
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
        * tpa
        */
        private void verifyTPA(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            if (myParent.verifyTargetProtocolAddress(((TextBox)sender).Text))
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            myHTYPE = txtHTYPE.Text;
            myPTYPE = txtPTYPE.Text;
            myHLEN = int.Parse(txtHLEN.Text);
            myPLEN = int.Parse(txtPLEN.Text);
            myOPER = txtOPER.Text;
            mySHA = txtSHA.Text;
            mySPA = txtSPA.Text;
            myTHA = txtTHA.Text;
            myTPA = txtTPA.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}