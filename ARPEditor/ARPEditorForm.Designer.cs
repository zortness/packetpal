namespace Kopf.PacketPal.PacketEditors
{
    partial class ARPEditorForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtHTYPE = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTHA = new System.Windows.Forms.TextBox();
            this.txtSHA = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHLEN = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTPA = new System.Windows.Forms.TextBox();
            this.txtSPA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPLEN = new System.Windows.Forms.TextBox();
            this.txtPTYPE = new System.Windows.Forms.TextBox();
            this.txtOPER = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(436, 170);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtHTYPE
            // 
            this.txtHTYPE.Location = new System.Drawing.Point(143, 19);
            this.txtHTYPE.Name = "txtHTYPE";
            this.txtHTYPE.Size = new System.Drawing.Size(100, 20);
            this.txtHTYPE.TabIndex = 2;
            this.txtHTYPE.Leave += new System.EventHandler(this.verifyHTYPE);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hardware Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Protocol Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hardware Length:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Protocol Length:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Operation:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTHA);
            this.groupBox1.Controls.Add(this.txtSHA);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtHLEN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtHTYPE);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 128);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Link Layer";
            // 
            // txtTHA
            // 
            this.txtTHA.Location = new System.Drawing.Point(143, 97);
            this.txtTHA.Name = "txtTHA";
            this.txtTHA.Size = new System.Drawing.Size(100, 20);
            this.txtTHA.TabIndex = 10;
            this.txtTHA.Leave += new System.EventHandler(this.verifyTHA);
            // 
            // txtSHA
            // 
            this.txtSHA.Location = new System.Drawing.Point(143, 71);
            this.txtSHA.Name = "txtSHA";
            this.txtSHA.Size = new System.Drawing.Size(100, 20);
            this.txtSHA.TabIndex = 9;
            this.txtSHA.Leave += new System.EventHandler(this.verifySHA);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Target Hardware Address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Sender Hardware Address:";
            // 
            // txtHLEN
            // 
            this.txtHLEN.Location = new System.Drawing.Point(143, 45);
            this.txtHLEN.Name = "txtHLEN";
            this.txtHLEN.Size = new System.Drawing.Size(100, 20);
            this.txtHLEN.TabIndex = 6;
            this.txtHLEN.Leave += new System.EventHandler(this.verifyHLEN);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTPA);
            this.groupBox2.Controls.Add(this.txtSPA);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtPLEN);
            this.groupBox2.Controls.Add(this.txtPTYPE);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(272, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 128);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Network Layer";
            // 
            // txtTPA
            // 
            this.txtTPA.Location = new System.Drawing.Point(139, 97);
            this.txtTPA.Name = "txtTPA";
            this.txtTPA.Size = new System.Drawing.Size(100, 20);
            this.txtTPA.TabIndex = 12;
            this.txtTPA.Leave += new System.EventHandler(this.verifyTPA);
            // 
            // txtSPA
            // 
            this.txtSPA.Location = new System.Drawing.Point(139, 71);
            this.txtSPA.Name = "txtSPA";
            this.txtSPA.Size = new System.Drawing.Size(100, 20);
            this.txtSPA.TabIndex = 11;
            this.txtSPA.Leave += new System.EventHandler(this.verifySPA);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Target Protocol Address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Sender Protocol Address:";
            // 
            // txtPLEN
            // 
            this.txtPLEN.Location = new System.Drawing.Point(139, 45);
            this.txtPLEN.Name = "txtPLEN";
            this.txtPLEN.Size = new System.Drawing.Size(100, 20);
            this.txtPLEN.TabIndex = 8;
            this.txtPLEN.Leave += new System.EventHandler(this.verifyPLEN);
            // 
            // txtPTYPE
            // 
            this.txtPTYPE.Location = new System.Drawing.Point(139, 19);
            this.txtPTYPE.Name = "txtPTYPE";
            this.txtPTYPE.Size = new System.Drawing.Size(100, 20);
            this.txtPTYPE.TabIndex = 7;
            this.txtPTYPE.Leave += new System.EventHandler(this.verifyPTYPE);
            // 
            // txtOPER
            // 
            this.txtOPER.Location = new System.Drawing.Point(68, 19);
            this.txtOPER.Name = "txtOPER";
            this.txtOPER.Size = new System.Drawing.Size(100, 20);
            this.txtOPER.TabIndex = 10;
            this.txtOPER.Leave += new System.EventHandler(this.verifyOPER);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtOPER);
            this.groupBox3.Location = new System.Drawing.Point(12, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 47);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Address Request Protocol";
            // 
            // ARPEditorForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(532, 202);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ARPEditorForm";
            this.ShowIcon = false;
            this.Text = "ARP Message";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtHTYPE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHLEN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPLEN;
        private System.Windows.Forms.TextBox txtPTYPE;
        private System.Windows.Forms.TextBox txtTHA;
        private System.Windows.Forms.TextBox txtSHA;
        private System.Windows.Forms.TextBox txtTPA;
        private System.Windows.Forms.TextBox txtSPA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOPER;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}