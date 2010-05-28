namespace Kopf.PacketPal.PacketEditors
{
    partial class TCPEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxRecompute = new System.Windows.Forms.CheckBox();
            this.txtUrgent = new System.Windows.Forms.TextBox();
            this.txtChecksum = new System.Windows.Forms.TextBox();
            this.txtWindow = new System.Windows.Forms.TextBox();
            this.txtLen = new System.Windows.Forms.TextBox();
            this.txtAck = new System.Windows.Forms.TextBox();
            this.txtSequence = new System.Windows.Forms.TextBox();
            this.txtDst = new System.Windows.Forms.TextBox();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxFIN = new System.Windows.Forms.CheckBox();
            this.checkBoxSYN = new System.Windows.Forms.CheckBox();
            this.checkBoxRST = new System.Windows.Forms.CheckBox();
            this.checkBoxPSH = new System.Windows.Forms.CheckBox();
            this.checkBoxACK = new System.Windows.Forms.CheckBox();
            this.checkBoxURG = new System.Windows.Forms.CheckBox();
            this.checkBoxECN = new System.Windows.Forms.CheckBox();
            this.checkBoxCWR = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtReserved = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sequence Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "ACK Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Header Length:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Window Size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(253, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Checksum:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(253, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Urgent Pointer:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReserved);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.checkBoxRecompute);
            this.groupBox1.Controls.Add(this.txtUrgent);
            this.groupBox1.Controls.Add(this.txtChecksum);
            this.groupBox1.Controls.Add(this.txtWindow);
            this.groupBox1.Controls.Add(this.txtLen);
            this.groupBox1.Controls.Add(this.txtAck);
            this.groupBox1.Controls.Add(this.txtSequence);
            this.groupBox1.Controls.Add(this.txtDst);
            this.groupBox1.Controls.Add(this.txtSrc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 148);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Header Fields";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(253, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Reserved:";
            // 
            // checkBoxRecompute
            // 
            this.checkBoxRecompute.AutoSize = true;
            this.checkBoxRecompute.Location = new System.Drawing.Point(256, 123);
            this.checkBoxRecompute.Name = "checkBoxRecompute";
            this.checkBoxRecompute.Size = new System.Drawing.Size(177, 17);
            this.checkBoxRecompute.TabIndex = 16;
            this.checkBoxRecompute.Text = "Recompute Checksum on Save";
            this.checkBoxRecompute.UseVisualStyleBackColor = true;
            // 
            // txtUrgent
            // 
            this.txtUrgent.Location = new System.Drawing.Point(376, 97);
            this.txtUrgent.Name = "txtUrgent";
            this.txtUrgent.Size = new System.Drawing.Size(100, 20);
            this.txtUrgent.TabIndex = 15;
            this.txtUrgent.Leave += new System.EventHandler(this.verifyUrgent);
            // 
            // txtChecksum
            // 
            this.txtChecksum.Location = new System.Drawing.Point(376, 71);
            this.txtChecksum.Name = "txtChecksum";
            this.txtChecksum.Size = new System.Drawing.Size(100, 20);
            this.txtChecksum.TabIndex = 14;
            this.txtChecksum.Leave += new System.EventHandler(this.verifyChecksum);
            // 
            // txtWindow
            // 
            this.txtWindow.Location = new System.Drawing.Point(376, 45);
            this.txtWindow.Name = "txtWindow";
            this.txtWindow.Size = new System.Drawing.Size(100, 20);
            this.txtWindow.TabIndex = 13;
            this.txtWindow.Leave += new System.EventHandler(this.verifyWindow);
            // 
            // txtLen
            // 
            this.txtLen.Location = new System.Drawing.Point(129, 121);
            this.txtLen.Name = "txtLen";
            this.txtLen.Size = new System.Drawing.Size(100, 20);
            this.txtLen.TabIndex = 12;
            this.txtLen.Leave += new System.EventHandler(this.verifyLength);
            // 
            // txtAck
            // 
            this.txtAck.Location = new System.Drawing.Point(129, 97);
            this.txtAck.Name = "txtAck";
            this.txtAck.Size = new System.Drawing.Size(100, 20);
            this.txtAck.TabIndex = 11;
            this.txtAck.Leave += new System.EventHandler(this.verifyAck);
            // 
            // txtSequence
            // 
            this.txtSequence.Location = new System.Drawing.Point(129, 71);
            this.txtSequence.Name = "txtSequence";
            this.txtSequence.Size = new System.Drawing.Size(100, 20);
            this.txtSequence.TabIndex = 10;
            this.txtSequence.Leave += new System.EventHandler(this.verifySeq);
            // 
            // txtDst
            // 
            this.txtDst.Location = new System.Drawing.Point(129, 45);
            this.txtDst.Name = "txtDst";
            this.txtDst.Size = new System.Drawing.Size(100, 20);
            this.txtDst.TabIndex = 9;
            this.txtDst.Leave += new System.EventHandler(this.verifyDest);
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(129, 19);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(100, 20);
            this.txtSrc.TabIndex = 8;
            this.txtSrc.Leave += new System.EventHandler(this.verifySource);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxFIN);
            this.groupBox2.Controls.Add(this.checkBoxSYN);
            this.groupBox2.Controls.Add(this.checkBoxRST);
            this.groupBox2.Controls.Add(this.checkBoxPSH);
            this.groupBox2.Controls.Add(this.checkBoxACK);
            this.groupBox2.Controls.Add(this.checkBoxURG);
            this.groupBox2.Location = new System.Drawing.Point(12, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 47);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Flags";
            // 
            // checkBoxFIN
            // 
            this.checkBoxFIN.AutoSize = true;
            this.checkBoxFIN.Location = new System.Drawing.Point(412, 19);
            this.checkBoxFIN.Name = "checkBoxFIN";
            this.checkBoxFIN.Size = new System.Drawing.Size(43, 17);
            this.checkBoxFIN.TabIndex = 7;
            this.checkBoxFIN.Text = "FIN";
            this.checkBoxFIN.UseVisualStyleBackColor = true;
            // 
            // checkBoxSYN
            // 
            this.checkBoxSYN.AutoSize = true;
            this.checkBoxSYN.Location = new System.Drawing.Point(358, 19);
            this.checkBoxSYN.Name = "checkBoxSYN";
            this.checkBoxSYN.Size = new System.Drawing.Size(48, 17);
            this.checkBoxSYN.TabIndex = 6;
            this.checkBoxSYN.Text = "SYN";
            this.checkBoxSYN.UseVisualStyleBackColor = true;
            // 
            // checkBoxRST
            // 
            this.checkBoxRST.AutoSize = true;
            this.checkBoxRST.Location = new System.Drawing.Point(304, 19);
            this.checkBoxRST.Name = "checkBoxRST";
            this.checkBoxRST.Size = new System.Drawing.Size(48, 17);
            this.checkBoxRST.TabIndex = 5;
            this.checkBoxRST.Text = "RST";
            this.checkBoxRST.UseVisualStyleBackColor = true;
            // 
            // checkBoxPSH
            // 
            this.checkBoxPSH.AutoSize = true;
            this.checkBoxPSH.Location = new System.Drawing.Point(250, 19);
            this.checkBoxPSH.Name = "checkBoxPSH";
            this.checkBoxPSH.Size = new System.Drawing.Size(48, 17);
            this.checkBoxPSH.TabIndex = 4;
            this.checkBoxPSH.Text = "PSH";
            this.checkBoxPSH.UseVisualStyleBackColor = true;
            // 
            // checkBoxACK
            // 
            this.checkBoxACK.AutoSize = true;
            this.checkBoxACK.Location = new System.Drawing.Point(197, 19);
            this.checkBoxACK.Name = "checkBoxACK";
            this.checkBoxACK.Size = new System.Drawing.Size(47, 17);
            this.checkBoxACK.TabIndex = 3;
            this.checkBoxACK.Text = "ACK";
            this.checkBoxACK.UseVisualStyleBackColor = true;
            // 
            // checkBoxURG
            // 
            this.checkBoxURG.AutoSize = true;
            this.checkBoxURG.Location = new System.Drawing.Point(141, 19);
            this.checkBoxURG.Name = "checkBoxURG";
            this.checkBoxURG.Size = new System.Drawing.Size(50, 17);
            this.checkBoxURG.TabIndex = 2;
            this.checkBoxURG.Text = "URG";
            this.checkBoxURG.UseVisualStyleBackColor = true;
            // 
            // checkBoxECN
            // 
            this.checkBoxECN.AutoSize = true;
            this.checkBoxECN.Location = new System.Drawing.Point(410, 208);
            this.checkBoxECN.Name = "checkBoxECN";
            this.checkBoxECN.Size = new System.Drawing.Size(48, 17);
            this.checkBoxECN.TabIndex = 1;
            this.checkBoxECN.Text = "ECN";
            this.checkBoxECN.UseVisualStyleBackColor = true;
            this.checkBoxECN.Visible = false;
            // 
            // checkBoxCWR
            // 
            this.checkBoxCWR.AutoSize = true;
            this.checkBoxCWR.Location = new System.Drawing.Point(352, 208);
            this.checkBoxCWR.Name = "checkBoxCWR";
            this.checkBoxCWR.Size = new System.Drawing.Size(52, 17);
            this.checkBoxCWR.TabIndex = 0;
            this.checkBoxCWR.Text = "CWR";
            this.checkBoxCWR.UseVisualStyleBackColor = true;
            this.checkBoxCWR.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtData);
            this.groupBox3.Location = new System.Drawing.Point(12, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(486, 166);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payload";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(6, 19);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(474, 141);
            this.txtData.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(341, 391);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(422, 391);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtReserved
            // 
            this.txtReserved.Location = new System.Drawing.Point(376, 19);
            this.txtReserved.Name = "txtReserved";
            this.txtReserved.Size = new System.Drawing.Size(100, 20);
            this.txtReserved.TabIndex = 18;
            this.txtReserved.Leave += new System.EventHandler(this.verifyReserved);
            // 
            // TCPEditorForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(509, 425);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxECN);
            this.Controls.Add(this.checkBoxCWR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TCPEditorForm";
            this.ShowIcon = false;
            this.Text = "TCP Stream Packet";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtChecksum;
        private System.Windows.Forms.TextBox txtWindow;
        private System.Windows.Forms.TextBox txtLen;
        private System.Windows.Forms.TextBox txtAck;
        private System.Windows.Forms.TextBox txtSequence;
        private System.Windows.Forms.TextBox txtDst;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtUrgent;
        private System.Windows.Forms.CheckBox checkBoxFIN;
        private System.Windows.Forms.CheckBox checkBoxSYN;
        private System.Windows.Forms.CheckBox checkBoxRST;
        private System.Windows.Forms.CheckBox checkBoxPSH;
        private System.Windows.Forms.CheckBox checkBoxACK;
        private System.Windows.Forms.CheckBox checkBoxURG;
        private System.Windows.Forms.CheckBox checkBoxECN;
        private System.Windows.Forms.CheckBox checkBoxCWR;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.CheckBox checkBoxRecompute;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtReserved;
    }
}