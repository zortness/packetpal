using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kopf.PacketPal.PacketEditors
{
    public partial class TCPEditorForm : Form
    {
        private TCPEditor myParent;

        private int mySrc;
        private int myDst;
        private long mySeqNum;
        private long myAckNum;
        private int myLen;
        private bool myCWR;
        private bool myECN;
        private bool myURG;
        private bool myACK;
        private bool myPSH;
        private bool myRST;
        private bool mySYN;
        private bool myFIN;
        private int myWindow;
        private int myChecksum;
        private int myUrgentPointer;
        private string myData;


        public bool reCompile = false;
        public bool reCompute = false;

        public TCPEditorForm(TCPEditor parent, int srcPort, int dstPort, long sequenceNumber, long ackNumber, 
            int headerLength, bool cwrFlag, bool ecnFlag, bool urgFlag, bool ackFlag, bool pshFlag, 
            bool rstFlag, bool synFlag, bool finFlag, int windowSize, int checksum, int urgentPointer, string data)
        {
            InitializeComponent();

            myParent = parent;

            mySrc = srcPort;
            myDst = dstPort;
            mySeqNum = sequenceNumber;
            myAckNum = ackNumber;
            myLen = headerLength;
            myCWR = cwrFlag;
            myECN = ecnFlag;
            myURG = urgFlag;
            myACK = ackFlag;
            myPSH = pshFlag;
            myRST = rstFlag;
            mySYN = synFlag;
            myFIN = finFlag;
            myWindow = windowSize;
            myChecksum = checksum;
            myUrgentPointer = urgentPointer;
            myData = data;

            txtSrc.Text = mySrc.ToString();
            txtDst.Text = myDst.ToString();
            txtSequence.Text = mySeqNum.ToString();
            txtAck.Text = myAckNum.ToString();
            txtLen.Text = myLen.ToString();

            checkBoxCWR.Checked = myCWR;
            checkBoxECN.Checked = myECN;
            checkBoxURG.Checked = myURG;
            checkBoxACK.Checked = myACK;
            checkBoxPSH.Checked = myPSH;
            checkBoxRST.Checked = myRST;
            checkBoxSYN.Checked = mySYN;
            checkBoxFIN.Checked = myFIN;

            txtWindow.Text = myWindow.ToString();
            txtChecksum.Text = myChecksum.ToString();
            txtUrgent.Text = myUrgentPointer.ToString();

            txtData.Text = myData;
            
        }

        /**
         * field retrieval
         */
        public int getSourcePort()
        {
            return mySrc;
        }

        public int getDestinationPort()
        {
            return myDst;
        }

        public long getSequenceNumber()
        {
            return mySeqNum;
        }

        public long getAcknowledgementNumber()
        {
            return myAckNum;
        }

        public int getHeaderLength()
        {
            return myLen;
        }

        public bool getCwrFlag()
        {
            return myCWR;
        }

        public bool getEcnFlag()
        {
            return myECN;
        }

        public bool getUrgFlag()
        {
            return myURG;
        }

        public bool getAckFlag()
        {
            return myACK;
        }

        public bool getPshFlag()
        {
            return myPSH;
        }

        public bool getRstFlag()
        {
            return myRST;
        }

        public bool getSynFlag()
        {
            return mySYN;
        }

        public bool getFinFlag()
        {
            return myFIN;
        }

        public int getWindowSize()
        {
            return myWindow;
        }

        public int getChecksum()
        {
            return myChecksum;
        }

        public int getUrgentPointer()
        {
            return myUrgentPointer;
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
                if (myParent.verifySourcePort(int.Parse(((TextBox)sender).Text))) //((TextBox)sender).Text
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
        private void verifyDest(object sender, EventArgs e)
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
        * sequence number
        */
        private void verifySeq(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifySequenceNumber(long.Parse(((TextBox)sender).Text)))
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
        * ack number
        */
        private void verifyAck(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyAcknowledgementNumber(long.Parse(((TextBox)sender).Text)))
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
        * window size
        */
        private void verifyWindow(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyWindowSize(int.Parse(((TextBox)sender).Text)))
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
        * urgent pointer
        */
        private void verifyUrgent(object sender, EventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            try
            {
                if (myParent.verifyUrgentPointer(int.Parse(((TextBox)sender).Text)))
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


        /**
         * save changes
         */

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (myData != txtData.Text)
            {
                reCompile = true;
                myData = txtData.Text;
            }
            if (checkBoxRecompute.Checked == true)
            {
                reCompute = true;
            }

            mySrc = int.Parse(txtSrc.Text);
            myDst = int.Parse(txtDst.Text);

            mySeqNum = long.Parse(txtSequence.Text);
            myAckNum = long.Parse(txtAck.Text);

            myLen = int.Parse(txtLen.Text);

            myCWR = checkBoxCWR.Checked;
            myECN = checkBoxECN.Checked;
            myURG = checkBoxURG.Checked;
            myACK = checkBoxACK.Checked;
            myPSH = checkBoxPSH.Checked;
            myRST = checkBoxRST.Checked;
            mySYN = checkBoxSYN.Checked;
            myFIN = checkBoxFIN.Checked;

            myWindow = int.Parse(txtWindow.Text);
            myChecksum = int.Parse(txtChecksum.Text);
            myUrgentPointer = int.Parse(txtUrgent.Text);

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