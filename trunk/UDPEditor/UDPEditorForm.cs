using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class UDPEditorForm : Form
    {
        private UDPEditor myParent;
        private int mySrc;
        private int myDest;
        private int myLength;
        private int myChecksum;
        private string myData;

        public bool reCompile = false;

        public bool reCompute = false;

        public UDPEditorForm(UDPEditor parent, int sourcePort, int destPort, int length, int checksum, string data)
        {
            InitializeComponent();

            myParent = parent;
            mySrc = sourcePort;
            myDest = destPort;
            myLength = length;
            myChecksum = checksum;
            myData = data;

            txtSrc.Text = mySrc.ToString();
            txtDest.Text = myDest.ToString();
            txtLength.Text = myLength.ToString();
            txtChecksum.Text = myChecksum.ToString();
            txtData.Text = myData;
        }

        public int getSourcePort()
        {
            return mySrc;
        }

        public int getDestinationPort()
        {
            return myDest;
        }

        public int getLength()
        {
            return myLength;
        }

        public int getChecksum()
        {
            return myChecksum;
        }

        public string getData()
        {
            return myData;
        }


        /**
         * field verification
         */

        /*
        * source
        */
        private void verifySource(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifySourcePort(int.Parse(((TextBox)sender).Text)))
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
        * destination
        */
        private void verifyDestination(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyDestinationPort(int.Parse(((TextBox)sender).Text)))
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
        * length
        */
        private void verifyLength(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyLength(int.Parse(((TextBox)sender).Text)))
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
        * checksum
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

        private void btnSave_Click(object sender, EventArgs e)
        {

            mySrc = int.Parse(txtSrc.Text);
            myDest = int.Parse(txtDest.Text);
            myLength = int.Parse(txtLength.Text);
            myChecksum = int.Parse(txtChecksum.Text);

            if (txtData.Text != myData)
            {
                reCompile = true;
            }
            myData = txtData.Text;

            if(checkBoxChecksum.Checked)
            {
                reCompute = true;
            }

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