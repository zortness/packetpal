namespace Kopf.PacketPal.PacketEditors
{
    partial class EthernetEditorForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFrame = new System.Windows.Forms.TextBox();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.txtDst = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPayloadHex = new System.Windows.Forms.TextBox();
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFrame);
            this.groupBox1.Controls.Add(this.txtSrc);
            this.groupBox1.Controls.Add(this.txtDst);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frame Header Information";
            // 
            // txtFrame
            // 
            this.txtFrame.Location = new System.Drawing.Point(347, 17);
            this.txtFrame.MaxLength = 4;
            this.txtFrame.Name = "txtFrame";
            this.txtFrame.Size = new System.Drawing.Size(100, 20);
            this.txtFrame.TabIndex = 8;
            this.txtFrame.Leave += new System.EventHandler(this.verifyProtocol);
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(126, 43);
            this.txtSrc.MaxLength = 17;
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(100, 20);
            this.txtSrc.TabIndex = 7;
            this.txtSrc.Leave += new System.EventHandler(this.verifyMac);
            // 
            // txtDst
            // 
            this.txtDst.Location = new System.Drawing.Point(126, 17);
            this.txtDst.MaxLength = 17;
            this.txtDst.Name = "txtDst";
            this.txtDst.Size = new System.Drawing.Size(100, 20);
            this.txtDst.TabIndex = 6;
            this.txtDst.Leave += new System.EventHandler(this.verifyMac);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Frame Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Source Address:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Destination Address:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPayloadHex);
            this.groupBox2.Controls.Add(this.btnRandom);
            this.groupBox2.Controls.Add(this.btnUnlock);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 169);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frame Payload Information";
            // 
            // txtPayloadHex
            // 
            this.txtPayloadHex.Location = new System.Drawing.Point(6, 19);
            this.txtPayloadHex.MaxLength = 3000;
            this.txtPayloadHex.Multiline = true;
            this.txtPayloadHex.Name = "txtPayloadHex";
            this.txtPayloadHex.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPayloadHex.Size = new System.Drawing.Size(502, 115);
            this.txtPayloadHex.TabIndex = 4;
            this.txtPayloadHex.Leave += new System.EventHandler(this.verifyPayloadHex);
            this.txtPayloadHex.TextChanged += new System.EventHandler(this.payloadChanged);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(150, 140);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(181, 23);
            this.btnRandom.TabIndex = 3;
            this.btnRandom.Text = "Generate Random Payload";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(6, 140);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(135, 23);
            this.btnUnlock.TabIndex = 2;
            this.btnUnlock.Text = "Unlock Payload";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(370, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(451, 277);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EthernetEditorForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(541, 312);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EthernetEditorForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Ethernet Frame";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.TextBox txtFrame;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.TextBox txtDst;
        private System.Windows.Forms.TextBox txtPayloadHex;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}