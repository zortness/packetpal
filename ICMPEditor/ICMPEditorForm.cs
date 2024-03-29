using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class ICMPEditorForm : Form
    {
        private ICMPEditor myParent;
        private string myType;
        private string myCode;
        private string myChecksum;
        private string myData;

        public bool reCompile = false;

        public bool reCompute = false;

        public ICMPEditorForm(ICMPEditor parent, string messageType, string messageCode, string checksum, string data)
        {
            InitializeComponent();

            myParent = parent;
            myType = messageType;
            myCode = messageCode;
            myChecksum = checksum;
            myData = data;

            txtType.Text = myType.ToString();
            txtCode.Text = myCode.ToString();
            txtChecksum.Text = myChecksum.ToString();
            txtData.Text = myData;
        }

        public string getType()
        {
            return myType;
        }

        public string getCode()
        {
            return myCode;
        }

        public string getChecksum()
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
        * type
        */
        private void verifyType(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyMessageType(((TextBox)sender).Text))
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
        * code
        */
        private void verifyCode(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyMessageCode(((TextBox)sender).Text))
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
                if (myParent.verifyChecksum(((TextBox)sender).Text))
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
            myType = txtType.Text;
            myCode = txtCode.Text;
            myChecksum = txtChecksum.Text;

            if (txtData.Text != myData)
            {
                reCompile = true;
            }
            myData = txtData.Text;

            if (checkBoxUpdate.Checked)
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