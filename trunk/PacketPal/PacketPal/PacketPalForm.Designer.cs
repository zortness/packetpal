namespace Kopf.PacketPal
{
    partial class PacketPalForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutPacketPalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tutorialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxCapture = new System.Windows.Forms.ListBox();
            this.contextMenuStripCapture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToSendQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlPackets = new System.Windows.Forms.TabControl();
            this.tabPageCapture = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnStartCapture = new System.Windows.Forms.Button();
            this.btnStopCapture = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelCapturedTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCapturedSession = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonExtCap = new System.Windows.Forms.Button();
            this.txtCapDeviceInfo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.checkBoxScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxCaptureInterface = new System.Windows.Forms.ListBox();
            this.tabPageSend = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.labelSendTotal = new System.Windows.Forms.Label();
            this.labelSendSession = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnStopSend = new System.Windows.Forms.Button();
            this.btnStartSend = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panelSending = new System.Windows.Forms.Panel();
            this.progressBarSend = new System.Windows.Forms.ProgressBar();
            this.labelSendingTotal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelSendingComp = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBoxSend = new System.Windows.Forms.ListBox();
            this.contextMenuStripSend = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonExtSend = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSendDeviceInfo = new System.Windows.Forms.TextBox();
            this.checkBoxSendSelected = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxSendInterface = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.faqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.contextMenuStripCapture.SuspendLayout();
            this.tabControlPackets.SuspendLayout();
            this.tabPageCapture.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageSend.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelSending.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.newToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCaptureToolStripMenuItem,
            this.loadSendToolStripMenuItem,
            this.saveCaptureToolStripMenuItem,
            this.saveSendToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadCaptureToolStripMenuItem
            // 
            this.loadCaptureToolStripMenuItem.Name = "loadCaptureToolStripMenuItem";
            this.loadCaptureToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadCaptureToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.loadCaptureToolStripMenuItem.Text = "Load Captured Packets";
            this.loadCaptureToolStripMenuItem.Click += new System.EventHandler(this.loadFileToCapture_Click);
            // 
            // loadSendToolStripMenuItem
            // 
            this.loadSendToolStripMenuItem.Name = "loadSendToolStripMenuItem";
            this.loadSendToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.O)));
            this.loadSendToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.loadSendToolStripMenuItem.Text = "Load Send Queue";
            this.loadSendToolStripMenuItem.Click += new System.EventHandler(this.loadFileToSend_Click);
            // 
            // saveCaptureToolStripMenuItem
            // 
            this.saveCaptureToolStripMenuItem.Name = "saveCaptureToolStripMenuItem";
            this.saveCaptureToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveCaptureToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveCaptureToolStripMenuItem.Text = "Save Captured Packets";
            this.saveCaptureToolStripMenuItem.Click += new System.EventHandler(this.saveCaptureToFile_Click);
            // 
            // saveSendToolStripMenuItem
            // 
            this.saveSendToolStripMenuItem.Name = "saveSendToolStripMenuItem";
            this.saveSendToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.S)));
            this.saveSendToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveSendToolStripMenuItem.Text = "Save Send Queue";
            this.saveSendToolStripMenuItem.Click += new System.EventHandler(this.saveSendToFile_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editMenuOpening);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.DropDownOpening += new System.EventHandler(this.newMenuOpening);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.reloadToolStripMenuItem.Text = "Reload Plugins";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutPacketPalToolStripMenuItem,
            this.tutorialsToolStripMenuItem,
            this.faqToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutPacketPalToolStripMenuItem
            // 
            this.aboutPacketPalToolStripMenuItem.Name = "aboutPacketPalToolStripMenuItem";
            this.aboutPacketPalToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.aboutPacketPalToolStripMenuItem.Text = "About Packet Pal";
            this.aboutPacketPalToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tutorialsToolStripMenuItem
            // 
            this.tutorialsToolStripMenuItem.Name = "tutorialsToolStripMenuItem";
            this.tutorialsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.tutorialsToolStripMenuItem.Text = "Tutorials";
            this.tutorialsToolStripMenuItem.Click += new System.EventHandler(this.tutorialsToolStripMenuItem_Click);
            // 
            // listBoxCapture
            // 
            this.listBoxCapture.ContextMenuStrip = this.contextMenuStripCapture;
            this.listBoxCapture.FormattingEnabled = true;
            this.listBoxCapture.Location = new System.Drawing.Point(6, 19);
            this.listBoxCapture.Name = "listBoxCapture";
            this.listBoxCapture.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCapture.Size = new System.Drawing.Size(756, 225);
            this.listBoxCapture.TabIndex = 1;
            // 
            // contextMenuStripCapture
            // 
            this.contextMenuStripCapture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editAsToolStripMenuItem,
            this.addToSendQueueToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripCapture.Name = "contextMenuStripCapture";
            this.contextMenuStripCapture.Size = new System.Drawing.Size(169, 70);
            this.contextMenuStripCapture.Opening += new System.ComponentModel.CancelEventHandler(this.capturedContextOpening);
            // 
            // editAsToolStripMenuItem
            // 
            this.editAsToolStripMenuItem.Name = "editAsToolStripMenuItem";
            this.editAsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.editAsToolStripMenuItem.Text = "Edit As";
            // 
            // addToSendQueueToolStripMenuItem
            // 
            this.addToSendQueueToolStripMenuItem.Name = "addToSendQueueToolStripMenuItem";
            this.addToSendQueueToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addToSendQueueToolStripMenuItem.Text = "Add to Send Queue";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // tabControlPackets
            // 
            this.tabControlPackets.Controls.Add(this.tabPageCapture);
            this.tabControlPackets.Controls.Add(this.tabPageSend);
            this.tabControlPackets.Location = new System.Drawing.Point(0, 27);
            this.tabControlPackets.Name = "tabControlPackets";
            this.tabControlPackets.SelectedIndex = 0;
            this.tabControlPackets.Size = new System.Drawing.Size(792, 418);
            this.tabControlPackets.TabIndex = 2;
            // 
            // tabPageCapture
            // 
            this.tabPageCapture.Controls.Add(this.groupBox6);
            this.tabPageCapture.Controls.Add(this.groupBox5);
            this.tabPageCapture.Controls.Add(this.groupBox2);
            this.tabPageCapture.Controls.Add(this.groupBox1);
            this.tabPageCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageCapture.Name = "tabPageCapture";
            this.tabPageCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCapture.Size = new System.Drawing.Size(784, 392);
            this.tabPageCapture.TabIndex = 0;
            this.tabPageCapture.Text = "Capture Packets";
            this.tabPageCapture.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnStartCapture);
            this.groupBox6.Controls.Add(this.btnStopCapture);
            this.groupBox6.Location = new System.Drawing.Point(533, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(243, 56);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Control";
            // 
            // btnStartCapture
            // 
            this.btnStartCapture.Location = new System.Drawing.Point(6, 19);
            this.btnStartCapture.Name = "btnStartCapture";
            this.btnStartCapture.Size = new System.Drawing.Size(112, 23);
            this.btnStartCapture.TabIndex = 4;
            this.btnStartCapture.Text = "Start";
            this.btnStartCapture.UseVisualStyleBackColor = true;
            this.btnStartCapture.Click += new System.EventHandler(this.btnStartCapture_Click);
            // 
            // btnStopCapture
            // 
            this.btnStopCapture.Location = new System.Drawing.Point(125, 19);
            this.btnStopCapture.Name = "btnStopCapture";
            this.btnStopCapture.Size = new System.Drawing.Size(112, 23);
            this.btnStopCapture.TabIndex = 5;
            this.btnStopCapture.Text = "Stop";
            this.btnStopCapture.UseVisualStyleBackColor = true;
            this.btnStopCapture.Click += new System.EventHandler(this.btnStopCapture_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labelCapturedTotal);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.labelCapturedSession);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(533, 68);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(243, 58);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Statistics";
            // 
            // labelCapturedTotal
            // 
            this.labelCapturedTotal.AutoSize = true;
            this.labelCapturedTotal.Location = new System.Drawing.Point(170, 33);
            this.labelCapturedTotal.Name = "labelCapturedTotal";
            this.labelCapturedTotal.Size = new System.Drawing.Size(43, 13);
            this.labelCapturedTotal.TabIndex = 3;
            this.labelCapturedTotal.Text = "000000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Total Packets Captured:";
            // 
            // labelCapturedSession
            // 
            this.labelCapturedSession.AutoSize = true;
            this.labelCapturedSession.Location = new System.Drawing.Point(170, 16);
            this.labelCapturedSession.Name = "labelCapturedSession";
            this.labelCapturedSession.Size = new System.Drawing.Size(43, 13);
            this.labelCapturedSession.TabIndex = 1;
            this.labelCapturedSession.Text = "000000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Packets Captured This Session:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxCapture);
            this.groupBox2.Location = new System.Drawing.Point(8, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 252);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Captured Packets";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonExtCap);
            this.groupBox1.Controls.Add(this.txtCapDeviceInfo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFilter);
            this.groupBox1.Controls.Add(this.checkBoxScroll);
            this.groupBox1.Controls.Add(this.checkBoxUpdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBoxCaptureInterface);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 120);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capture Configuration";
            // 
            // buttonExtCap
            // 
            this.buttonExtCap.Location = new System.Drawing.Point(143, 58);
            this.buttonExtCap.Name = "buttonExtCap";
            this.buttonExtCap.Size = new System.Drawing.Size(133, 23);
            this.buttonExtCap.TabIndex = 12;
            this.buttonExtCap.Text = "Extended Information";
            this.buttonExtCap.UseVisualStyleBackColor = true;
            this.buttonExtCap.Click += new System.EventHandler(this.buttonExtCap_Click);
            // 
            // txtCapDeviceInfo
            // 
            this.txtCapDeviceInfo.Enabled = false;
            this.txtCapDeviceInfo.Location = new System.Drawing.Point(143, 32);
            this.txtCapDeviceInfo.Name = "txtCapDeviceInfo";
            this.txtCapDeviceInfo.Size = new System.Drawing.Size(370, 20);
            this.txtCapDeviceInfo.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Device Information:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "PCap Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(207, 94);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(126, 20);
            this.txtFilter.TabIndex = 8;
            this.txtFilter.Leave += new System.EventHandler(this.validateFilter);
            // 
            // checkBoxScroll
            // 
            this.checkBoxScroll.AutoSize = true;
            this.checkBoxScroll.Location = new System.Drawing.Point(345, 97);
            this.checkBoxScroll.Name = "checkBoxScroll";
            this.checkBoxScroll.Size = new System.Drawing.Size(164, 17);
            this.checkBoxScroll.TabIndex = 7;
            this.checkBoxScroll.Text = "Scroll to last packet captured";
            this.checkBoxScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpdate
            // 
            this.checkBoxUpdate.AutoSize = true;
            this.checkBoxUpdate.Location = new System.Drawing.Point(345, 74);
            this.checkBoxUpdate.Name = "checkBoxUpdate";
            this.checkBoxUpdate.Size = new System.Drawing.Size(168, 17);
            this.checkBoxUpdate.TabIndex = 6;
            this.checkBoxUpdate.Text = "Update capture list in real time";
            this.checkBoxUpdate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Network Interface:";
            // 
            // listBoxCaptureInterface
            // 
            this.listBoxCaptureInterface.FormattingEnabled = true;
            this.listBoxCaptureInterface.Location = new System.Drawing.Point(6, 32);
            this.listBoxCaptureInterface.Name = "listBoxCaptureInterface";
            this.listBoxCaptureInterface.Size = new System.Drawing.Size(128, 82);
            this.listBoxCaptureInterface.TabIndex = 2;
            this.listBoxCaptureInterface.SelectedIndexChanged += new System.EventHandler(this.captureDeviceChange);
            // 
            // tabPageSend
            // 
            this.tabPageSend.Controls.Add(this.groupBox8);
            this.tabPageSend.Controls.Add(this.groupBox7);
            this.tabPageSend.Controls.Add(this.groupBox4);
            this.tabPageSend.Controls.Add(this.groupBox3);
            this.tabPageSend.Location = new System.Drawing.Point(4, 22);
            this.tabPageSend.Name = "tabPageSend";
            this.tabPageSend.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSend.Size = new System.Drawing.Size(784, 392);
            this.tabPageSend.TabIndex = 1;
            this.tabPageSend.Text = "Send Packets";
            this.tabPageSend.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.labelSendTotal);
            this.groupBox8.Controls.Add(this.labelSendSession);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Location = new System.Drawing.Point(533, 68);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(243, 58);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Statistics";
            // 
            // labelSendTotal
            // 
            this.labelSendTotal.AutoSize = true;
            this.labelSendTotal.Location = new System.Drawing.Point(149, 35);
            this.labelSendTotal.Name = "labelSendTotal";
            this.labelSendTotal.Size = new System.Drawing.Size(43, 13);
            this.labelSendTotal.TabIndex = 3;
            this.labelSendTotal.Text = "000000";
            // 
            // labelSendSession
            // 
            this.labelSendSession.AutoSize = true;
            this.labelSendSession.Location = new System.Drawing.Point(149, 16);
            this.labelSendSession.Name = "labelSendSession";
            this.labelSendSession.Size = new System.Drawing.Size(43, 13);
            this.labelSendSession.TabIndex = 2;
            this.labelSendSession.Text = "000000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Total Packets Sent:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Packets Sent This Session:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnStopSend);
            this.groupBox7.Controls.Add(this.btnStartSend);
            this.groupBox7.Location = new System.Drawing.Point(533, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(243, 56);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Control";
            // 
            // btnStopSend
            // 
            this.btnStopSend.Location = new System.Drawing.Point(125, 19);
            this.btnStopSend.Name = "btnStopSend";
            this.btnStopSend.Size = new System.Drawing.Size(112, 23);
            this.btnStopSend.TabIndex = 1;
            this.btnStopSend.Text = "Stop";
            this.btnStopSend.UseVisualStyleBackColor = true;
            // 
            // btnStartSend
            // 
            this.btnStartSend.Location = new System.Drawing.Point(6, 19);
            this.btnStartSend.Name = "btnStartSend";
            this.btnStartSend.Size = new System.Drawing.Size(112, 23);
            this.btnStartSend.TabIndex = 0;
            this.btnStartSend.Text = "Start";
            this.btnStartSend.UseVisualStyleBackColor = true;
            this.btnStartSend.Click += new System.EventHandler(this.btnStartSend_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panelSending);
            this.groupBox4.Controls.Add(this.listBoxSend);
            this.groupBox4.Location = new System.Drawing.Point(8, 132);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(768, 252);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Send Queue";
            // 
            // panelSending
            // 
            this.panelSending.BackColor = System.Drawing.SystemColors.Control;
            this.panelSending.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSending.Controls.Add(this.progressBarSend);
            this.panelSending.Controls.Add(this.labelSendingTotal);
            this.panelSending.Controls.Add(this.label9);
            this.panelSending.Controls.Add(this.labelSendingComp);
            this.panelSending.Controls.Add(this.label8);
            this.panelSending.Location = new System.Drawing.Point(266, 85);
            this.panelSending.Name = "panelSending";
            this.panelSending.Size = new System.Drawing.Size(248, 75);
            this.panelSending.TabIndex = 1;
            // 
            // progressBarSend
            // 
            this.progressBarSend.Location = new System.Drawing.Point(17, 35);
            this.progressBarSend.Name = "progressBarSend";
            this.progressBarSend.Size = new System.Drawing.Size(213, 23);
            this.progressBarSend.TabIndex = 4;
            // 
            // labelSendingTotal
            // 
            this.labelSendingTotal.AutoSize = true;
            this.labelSendingTotal.Location = new System.Drawing.Point(187, 10);
            this.labelSendingTotal.Name = "labelSendingTotal";
            this.labelSendingTotal.Size = new System.Drawing.Size(43, 13);
            this.labelSendingTotal.TabIndex = 3;
            this.labelSendingTotal.Text = "000000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(165, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "of";
            // 
            // labelSendingComp
            // 
            this.labelSendingComp.AutoSize = true;
            this.labelSendingComp.Location = new System.Drawing.Point(103, 10);
            this.labelSendingComp.Name = "labelSendingComp";
            this.labelSendingComp.Size = new System.Drawing.Size(43, 13);
            this.labelSendingComp.TabIndex = 1;
            this.labelSendingComp.Text = "000000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Sending packet:";
            // 
            // listBoxSend
            // 
            this.listBoxSend.ContextMenuStrip = this.contextMenuStripSend;
            this.listBoxSend.FormattingEnabled = true;
            this.listBoxSend.Location = new System.Drawing.Point(6, 19);
            this.listBoxSend.Name = "listBoxSend";
            this.listBoxSend.Size = new System.Drawing.Size(756, 225);
            this.listBoxSend.TabIndex = 0;
            // 
            // contextMenuStripSend
            // 
            this.contextMenuStripSend.Name = "contextMenuStripSend";
            this.contextMenuStripSend.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStripSend.Opening += new System.ComponentModel.CancelEventHandler(this.sendContextOpening);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonExtSend);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtSendDeviceInfo);
            this.groupBox3.Controls.Add(this.checkBoxSendSelected);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.listBoxSendInterface);
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(519, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send Configuration";
            // 
            // buttonExtSend
            // 
            this.buttonExtSend.Location = new System.Drawing.Point(143, 58);
            this.buttonExtSend.Name = "buttonExtSend";
            this.buttonExtSend.Size = new System.Drawing.Size(133, 23);
            this.buttonExtSend.TabIndex = 13;
            this.buttonExtSend.Text = "Extended Information";
            this.buttonExtSend.UseVisualStyleBackColor = true;
            this.buttonExtSend.Click += new System.EventHandler(this.buttonExtSend_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Device Information:";
            // 
            // txtSendDeviceInfo
            // 
            this.txtSendDeviceInfo.Enabled = false;
            this.txtSendDeviceInfo.Location = new System.Drawing.Point(143, 32);
            this.txtSendDeviceInfo.Name = "txtSendDeviceInfo";
            this.txtSendDeviceInfo.Size = new System.Drawing.Size(370, 20);
            this.txtSendDeviceInfo.TabIndex = 4;
            // 
            // checkBoxSendSelected
            // 
            this.checkBoxSendSelected.AutoSize = true;
            this.checkBoxSendSelected.Location = new System.Drawing.Point(143, 97);
            this.checkBoxSendSelected.Name = "checkBoxSendSelected";
            this.checkBoxSendSelected.Size = new System.Drawing.Size(157, 17);
            this.checkBoxSendSelected.TabIndex = 3;
            this.checkBoxSendSelected.Text = "Send only selected packets";
            this.checkBoxSendSelected.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Network Interface:";
            // 
            // listBoxSendInterface
            // 
            this.listBoxSendInterface.FormattingEnabled = true;
            this.listBoxSendInterface.Location = new System.Drawing.Point(6, 32);
            this.listBoxSendInterface.Name = "listBoxSendInterface";
            this.listBoxSendInterface.Size = new System.Drawing.Size(128, 82);
            this.listBoxSendInterface.TabIndex = 1;
            this.listBoxSendInterface.SelectedIndexChanged += new System.EventHandler(this.sendDeviceChange);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Pcap File|*.pcap|Cap File|*.cap|All Files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "pcap";
            this.saveFileDialog1.Filter = "Pcap File|*.pcap|Cap File|*.cap|All Files|*.*";
            // 
            // faqToolStripMenuItem
            // 
            this.faqToolStripMenuItem.Name = "faqToolStripMenuItem";
            this.faqToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.faqToolStripMenuItem.Text = "F.A.Q.";
            this.faqToolStripMenuItem.Click += new System.EventHandler(this.faqToolStripMenuItem_Click);
            // 
            // PacketPalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 445);
            this.Controls.Add(this.tabControlPackets);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "PacketPalForm";
            this.Text = "PacketPal";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenuStripCapture.ResumeLayout(false);
            this.tabControlPackets.ResumeLayout(false);
            this.tabPageCapture.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageSend.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panelSending.ResumeLayout(false);
            this.panelSending.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxCapture;
        private System.Windows.Forms.TabControl tabControlPackets;
        private System.Windows.Forms.TabPage tabPageCapture;
        private System.Windows.Forms.TabPage tabPageSend;
        private System.Windows.Forms.ListBox listBoxSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxCaptureInterface;
        private System.Windows.Forms.ListBox listBoxSendInterface;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStopCapture;
        private System.Windows.Forms.Button btnStartCapture;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxScroll;
        private System.Windows.Forms.CheckBox checkBoxUpdate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labelCapturedTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCapturedSession;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxSendSelected;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCapDeviceInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSendDeviceInfo;
        private System.Windows.Forms.Panel panelSending;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelSendingTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelSendingComp;
        private System.Windows.Forms.ProgressBar progressBarSend;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label labelSendTotal;
        private System.Windows.Forms.Label labelSendSession;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnStopSend;
        private System.Windows.Forms.Button btnStartSend;
        private System.Windows.Forms.ToolStripMenuItem loadCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutPacketPalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tutorialsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCapture;
        private System.Windows.Forms.ToolStripMenuItem editAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToSendQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSend;
        private System.Windows.Forms.Button buttonExtCap;
        private System.Windows.Forms.Button buttonExtSend;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faqToolStripMenuItem;
    }
}

