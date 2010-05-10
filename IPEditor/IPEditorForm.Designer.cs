namespace Kopf.PacketPal.PacketEditors
{
    partial class IPEditorForm
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
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxEvil = new System.Windows.Forms.CheckBox();
            this.checkBoxMF = new System.Windows.Forms.CheckBox();
            this.checkBoxDF = new System.Windows.Forms.CheckBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtHeaderLength = new System.Windows.Forms.TextBox();
            this.txtTOS = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtFragOffset = new System.Windows.Forms.TextBox();
            this.txtTTL = new System.Windows.Forms.TextBox();
            this.txtProtocol = new System.Windows.Forms.TextBox();
            this.txtChecksum = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxChecksum = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Header Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Service Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total Length:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "ID Number:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Fragment Offset:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Time To Live:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Protocol:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Header Checksum:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Source Address:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Destination Address:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxEvil);
            this.groupBox1.Controls.Add(this.checkBoxMF);
            this.groupBox1.Controls.Add(this.checkBoxDF);
            this.groupBox1.Controls.Add(this.txtFragOffset);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(253, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 125);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fragmentation";
            // 
            // checkBoxEvil
            // 
            this.checkBoxEvil.AutoSize = true;
            this.checkBoxEvil.Location = new System.Drawing.Point(14, 20);
            this.checkBoxEvil.Name = "checkBoxEvil";
            this.checkBoxEvil.Size = new System.Drawing.Size(68, 17);
            this.checkBoxEvil.TabIndex = 2;
            this.checkBoxEvil.Text = "\"Evil\" Bit";
            this.checkBoxEvil.UseVisualStyleBackColor = true;
            // 
            // checkBoxMF
            // 
            this.checkBoxMF.AutoSize = true;
            this.checkBoxMF.Location = new System.Drawing.Point(14, 66);
            this.checkBoxMF.Name = "checkBoxMF";
            this.checkBoxMF.Size = new System.Drawing.Size(102, 17);
            this.checkBoxMF.TabIndex = 1;
            this.checkBoxMF.Text = "More Fragments";
            this.checkBoxMF.UseVisualStyleBackColor = true;
            // 
            // checkBoxDF
            // 
            this.checkBoxDF.AutoSize = true;
            this.checkBoxDF.Location = new System.Drawing.Point(14, 43);
            this.checkBoxDF.Name = "checkBoxDF";
            this.checkBoxDF.Size = new System.Drawing.Size(98, 17);
            this.checkBoxDF.TabIndex = 0;
            this.checkBoxDF.Text = "Don\'t Fragment";
            this.checkBoxDF.UseVisualStyleBackColor = true;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(122, 18);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(100, 20);
            this.txtVersion.TabIndex = 13;
            this.txtVersion.Leave += new System.EventHandler(this.verifyVersion);
            // 
            // txtHeaderLength
            // 
            this.txtHeaderLength.Enabled = false;
            this.txtHeaderLength.Location = new System.Drawing.Point(122, 44);
            this.txtHeaderLength.Name = "txtHeaderLength";
            this.txtHeaderLength.Size = new System.Drawing.Size(100, 20);
            this.txtHeaderLength.TabIndex = 14;
            this.txtHeaderLength.Leave += new System.EventHandler(this.verifyHeaderLength);
            // 
            // txtTOS
            // 
            this.txtTOS.Location = new System.Drawing.Point(122, 70);
            this.txtTOS.Name = "txtTOS";
            this.txtTOS.Size = new System.Drawing.Size(100, 20);
            this.txtTOS.TabIndex = 15;
            this.txtTOS.Leave += new System.EventHandler(this.verifyTOS);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(122, 96);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 20);
            this.txtLength.TabIndex = 16;
            this.txtLength.Leave += new System.EventHandler(this.verifyTotalLength);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(122, 122);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 17;
            this.txtID.Leave += new System.EventHandler(this.verifyID);
            // 
            // txtFragOffset
            // 
            this.txtFragOffset.Location = new System.Drawing.Point(122, 88);
            this.txtFragOffset.Name = "txtFragOffset";
            this.txtFragOffset.Size = new System.Drawing.Size(100, 20);
            this.txtFragOffset.TabIndex = 18;
            this.txtFragOffset.Leave += new System.EventHandler(this.verifyFragmentOffset);
            // 
            // txtTTL
            // 
            this.txtTTL.Location = new System.Drawing.Point(122, 18);
            this.txtTTL.Name = "txtTTL";
            this.txtTTL.Size = new System.Drawing.Size(100, 20);
            this.txtTTL.TabIndex = 19;
            this.txtTTL.Leave += new System.EventHandler(this.verifyTTL);
            // 
            // txtProtocol
            // 
            this.txtProtocol.Location = new System.Drawing.Point(122, 148);
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.Size = new System.Drawing.Size(100, 20);
            this.txtProtocol.TabIndex = 20;
            this.txtProtocol.Leave += new System.EventHandler(this.verifyProtocol);
            // 
            // txtChecksum
            // 
            this.txtChecksum.Location = new System.Drawing.Point(122, 174);
            this.txtChecksum.Name = "txtChecksum";
            this.txtChecksum.Size = new System.Drawing.Size(100, 20);
            this.txtChecksum.TabIndex = 21;
            this.txtChecksum.Leave += new System.EventHandler(this.verifyChecksum);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(122, 44);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(100, 20);
            this.txtSource.TabIndex = 22;
            this.txtSource.Leave += new System.EventHandler(this.verifySource);
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(122, 70);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(100, 20);
            this.txtDestination.TabIndex = 23;
            this.txtDestination.Leave += new System.EventHandler(this.verifyDest);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(9, 19);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData.Size = new System.Drawing.Size(454, 115);
            this.txtData.TabIndex = 24;
            this.txtData.Leave += new System.EventHandler(this.verifyData);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(400, 393);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(319, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxChecksum);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtVersion);
            this.groupBox2.Controls.Add(this.txtHeaderLength);
            this.groupBox2.Controls.Add(this.txtProtocol);
            this.groupBox2.Controls.Add(this.txtTOS);
            this.groupBox2.Controls.Add(this.txtChecksum);
            this.groupBox2.Controls.Add(this.txtLength);
            this.groupBox2.Controls.Add(this.txtID);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 229);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtDestination);
            this.groupBox3.Controls.Add(this.txtTTL);
            this.groupBox3.Controls.Add(this.txtSource);
            this.groupBox3.Location = new System.Drawing.Point(253, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 98);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Delivery";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtData);
            this.groupBox4.Location = new System.Drawing.Point(12, 247);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(474, 140);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Payload";
            // 
            // checkBoxChecksum
            // 
            this.checkBoxChecksum.AutoSize = true;
            this.checkBoxChecksum.Location = new System.Drawing.Point(9, 200);
            this.checkBoxChecksum.Name = "checkBoxChecksum";
            this.checkBoxChecksum.Size = new System.Drawing.Size(217, 17);
            this.checkBoxChecksum.TabIndex = 22;
            this.checkBoxChecksum.Text = "Recalculate Header Checksum on Save";
            this.checkBoxChecksum.UseVisualStyleBackColor = true;
            // 
            // IPEditorForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(498, 430);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IPEditorForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "IP Packet";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxMF;
        private System.Windows.Forms.CheckBox checkBoxDF;
        private System.Windows.Forms.CheckBox checkBoxEvil;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtHeaderLength;
        private System.Windows.Forms.TextBox txtTOS;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtFragOffset;
        private System.Windows.Forms.TextBox txtTTL;
        private System.Windows.Forms.TextBox txtProtocol;
        private System.Windows.Forms.TextBox txtChecksum;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxChecksum;
    }
}