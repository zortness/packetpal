using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class EthernetEditorForm : Form
    {
        private EthernetEditor myParent;
        private string myDest; // 111.111.111.111
        private string mySource;
        private string myProtocol; // hex string
        private string myPayload;  // hex string

        /*
         * Constructor.
         */
        public EthernetEditorForm(EthernetEditor parent, string destination, string source, string protocol, string payload)
        {
            InitializeComponent();

            myParent = parent;
            myDest = destination;
            mySource = source;
            myProtocol = protocol;
            myPayload = payload;

            txtPayloadHex.Enabled = false;
            btnUnlock.Enabled = true;
            btnRandom.Enabled = false;

            txtDst.Text = myDest;
            txtSrc.Text = mySource;
            txtFrame.Text = myProtocol;
            txtPayloadHex.Text = myPayload;
        }

        /*
         * Retrieve destination.
         */
        public string getDest()
        {
            return myDest;
        }

        /*
         * Retrieve source.
         */
        public string getSource()
        {
            return mySource;
        }

        /*
         * Retrieve protocol.
         */
        public string getProtocol()
        {
            return myProtocol;
        }

        /*
         * Retrieve payload.
         */
        public string getPayload()
        {
            return myPayload;
        }

        /*
         * Verify the MAC address.
         */
        private void verifyMac(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            TextBox t = (TextBox)sender;
            // match
            if (myParent.verifyMac(t.Text))
            {
                btnSave.Enabled = true;
                t.BackColor = Color.White;
                t.ForeColor = Color.Black;
            }
            // no match
            else
            {
                btnSave.Enabled = false;
                t.Focus();
                t.BackColor = Color.Red;
                t.ForeColor = Color.White;
            }
        }

        /*
         * Verify the Protocol.
         */
        private void verifyProtocol(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            TextBox t = (TextBox)sender;
            // match
            if (myParent.verifyProtocol(t.Text))
            {
                btnSave.Enabled = true;
                t.BackColor = Color.White;
                t.ForeColor = Color.Black;
            }
            // no match
            else
            {
                btnSave.Enabled = false;
                t.Focus();
                t.BackColor = Color.Red;
                t.ForeColor = Color.White;
            }
        }

        /*
         * Verify Payload.
         */
        private void verifyPayloadHex(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            TextBox t = (TextBox)sender;
            // match
            if (myParent.verifyPayload(t.Text))
            {
                btnSave.Enabled = true;
                t.BackColor = Color.White;
                t.ForeColor = Color.Black;
            }
            // no match
            else
            {
                btnSave.Enabled = false;
                t.Focus();
                t.BackColor = Color.Red;
                t.ForeColor = Color.White;
            }
        }

        private void payloadChanged(object sender, EventArgs e)
        {
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            btnUnlock.Enabled = false;
            btnRandom.Enabled = true;
            txtPayloadHex.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            myDest = txtDst.Text;
            mySource = txtSrc.Text;
            myProtocol = txtFrame.Text;
            myPayload = txtPayloadHex.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            // generate 1500 random bytes
            txtPayloadHex.Text = myParent.randomPayload();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}