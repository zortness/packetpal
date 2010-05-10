using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class ARPEditorForm : Form
    {
        ARPEditor myParent;
        int myHTYPE;
        int myPTYPE;
        int myHLEN;
        int myPLEN;
        int myOPER;
        string mySHA;
        string mySPA;
        string myTHA;
        string myTPA;

        /*
         * Constructor
         */
        public ARPEditorForm(ARPEditor parent, int hardwareType, int protocolType, int hardwareLength, int protocolLength,
            int operation, string senderHwAddress, string senderProtocolAddress, string targetHwAddress,
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

            txtHTYPE.Text = myHTYPE.ToString();
            txtPTYPE.Text = myPTYPE.ToString();
            txtHLEN.Text = myHLEN.ToString();
            txtPLEN.Text = myPLEN.ToString();
            txtOPER.Text = myOPER.ToString();
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
        public int getHardwareType()
        {
            return myHTYPE;
        }

        /*
        * PTYPE
        */
        public int getProtocolType()
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
        public int getOperation()
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
            try
            {
                if (myParent.verifyHardwareType(int.Parse(((TextBox)sender).Text)))
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
        * ptype
        */
        private void verifyPTYPE(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyProtocolType(int.Parse(((TextBox)sender).Text)))
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
            try
            {
                if (myParent.verifyOperation(int.Parse(((TextBox)sender).Text)))
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
            myHTYPE = int.Parse(txtHTYPE.Text);
            myPTYPE = int.Parse(txtPTYPE.Text);
            myHLEN = int.Parse(txtHLEN.Text);
            myPLEN = int.Parse(txtPLEN.Text);
            myOPER = int.Parse(txtOPER.Text);
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