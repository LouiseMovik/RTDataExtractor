namespace RTDataExtractor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpList = new System.Windows.Forms.GroupBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.gridList = new System.Windows.Forms.DataGridView();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txbOutput = new System.Windows.Forms.TextBox();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.btnQC = new System.Windows.Forms.Button();
            this.prgBarPatient = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.lblUtvecklare = new System.Windows.Forms.Label();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.daemonGroupBox = new System.Windows.Forms.GroupBox();
            this.editDaemonButton = new System.Windows.Forms.Button();
            this.lblDaemonAE = new System.Windows.Forms.Label();
            this.lblDaemonAEResult = new System.Windows.Forms.Label();
            this.lblDaemonIP = new System.Windows.Forms.Label();
            this.lblDaemonIPResult = new System.Windows.Forms.Label();
            this.lblDaemonPort = new System.Windows.Forms.Label();
            this.lblDaemonPortResult = new System.Windows.Forms.Label();
            this.localGroupBox = new System.Windows.Forms.GroupBox();
            this.editLocalButton = new System.Windows.Forms.Button();
            this.lblLocalPortResult = new System.Windows.Forms.Label();
            this.lblLocalAE = new System.Windows.Forms.Label();
            this.lblLocalPort = new System.Windows.Forms.Label();
            this.lblLocalAEResult = new System.Windows.Forms.Label();
            this.lblLocalIPResult = new System.Windows.Forms.Label();
            this.lblLocalIP = new System.Windows.Forms.Label();
            this.toolTipInput = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipQC = new System.Windows.Forms.ToolTip(this.components);
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.grpExport.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.daemonGroupBox.SuspendLayout();
            this.localGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpList
            // 
            this.grpList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpList.Controls.Add(this.lblInput);
            this.grpList.Controls.Add(this.btnSelectFolder);
            this.grpList.Controls.Add(this.gridList);
            this.grpList.Controls.Add(this.txtPath);
            this.grpList.Controls.Add(this.txbOutput);
            this.grpList.Location = new System.Drawing.Point(11, 12);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(458, 381);
            this.grpList.TabIndex = 16;
            this.grpList.TabStop = false;
            this.grpList.Text = "Export requests";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.Location = new System.Drawing.Point(7, 23);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(58, 13);
            this.lblInput.TabIndex = 16;
            this.lblInput.Text = "Input path:";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(428, 19);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(24, 22);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            this.btnSelectFolder.Leave += new System.EventHandler(this.txtPath_Leave);
            // 
            // gridList
            // 
            this.gridList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridList.Location = new System.Drawing.Point(10, 47);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(441, 328);
            this.gridList.TabIndex = 0;
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(71, 20);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(352, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyDown);
            this.txtPath.Leave += new System.EventHandler(this.txtPath_Leave);
            // 
            // txbOutput
            // 
            this.txbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbOutput.BackColor = System.Drawing.SystemColors.Control;
            this.txbOutput.Location = new System.Drawing.Point(10, 47);
            this.txbOutput.Multiline = true;
            this.txbOutput.Name = "txbOutput";
            this.txbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbOutput.Size = new System.Drawing.Size(441, 328);
            this.txbOutput.TabIndex = 17;
            // 
            // grpExport
            // 
            this.grpExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExport.Controls.Add(this.btnQC);
            this.grpExport.Controls.Add(this.prgBarPatient);
            this.grpExport.Controls.Add(this.label2);
            this.grpExport.Controls.Add(this.label1);
            this.grpExport.Controls.Add(this.lblOutput);
            this.grpExport.Controls.Add(this.btnStart);
            this.grpExport.Controls.Add(this.prgBar);
            this.grpExport.Controls.Add(this.btnSelectOutputFolder);
            this.grpExport.Controls.Add(this.txtOutputPath);
            this.grpExport.Location = new System.Drawing.Point(11, 399);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(458, 106);
            this.grpExport.TabIndex = 15;
            this.grpExport.TabStop = false;
            this.grpExport.Text = "Extraction";
            // 
            // btnQC
            // 
            this.btnQC.Location = new System.Drawing.Point(8, 75);
            this.btnQC.Name = "btnQC";
            this.btnQC.Size = new System.Drawing.Size(81, 24);
            this.btnQC.TabIndex = 21;
            this.btnQC.Text = "QC";
            this.btnQC.UseVisualStyleBackColor = true;
            this.btnQC.Click += new System.EventHandler(this.button1_Click);
            // 
            // prgBarPatient
            // 
            this.prgBarPatient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBarPatient.Location = new System.Drawing.Point(201, 76);
            this.prgBarPatient.Name = "prgBarPatient";
            this.prgBarPatient.Size = new System.Drawing.Size(249, 21);
            this.prgBarPatient.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Patient requests:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Requests:";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(7, 24);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(66, 13);
            this.lblOutput.TabIndex = 17;
            this.lblOutput.Text = "Output path:";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(8, 48);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(81, 24);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(201, 49);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(249, 21);
            this.prgBar.TabIndex = 3;
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(426, 20);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(24, 22);
            this.btnSelectOutputFolder.TabIndex = 17;
            this.btnSelectOutputFolder.Text = "...";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            this.btnSelectOutputFolder.Leave += new System.EventHandler(this.txtOutputPath_Leave);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(79, 21);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(342, 20);
            this.txtOutputPath.TabIndex = 16;
            this.txtOutputPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutputPath_KeyDown);
            this.txtOutputPath.Leave += new System.EventHandler(this.txtOutputPath_Leave);
            // 
            // lblUtvecklare
            // 
            this.lblUtvecklare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUtvecklare.AutoSize = true;
            this.lblUtvecklare.Location = new System.Drawing.Point(334, 631);
            this.lblUtvecklare.Name = "lblUtvecklare";
            this.lblUtvecklare.Size = new System.Drawing.Size(139, 13);
            this.lblUtvecklare.TabIndex = 14;
            this.lblUtvecklare.Text = "Developed by Louise Mövik";
            this.lblUtvecklare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpSettings
            // 
            this.grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSettings.Controls.Add(this.btnTestConnection);
            this.grpSettings.Controls.Add(this.daemonGroupBox);
            this.grpSettings.Controls.Add(this.localGroupBox);
            this.grpSettings.Location = new System.Drawing.Point(11, 511);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(458, 113);
            this.grpSettings.TabIndex = 12;
            this.grpSettings.TabStop = false;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestConnection.Location = new System.Drawing.Point(8, -1);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(131, 24);
            this.btnTestConnection.TabIndex = 14;
            this.btnTestConnection.Text = "Test DICOM Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // daemonGroupBox
            // 
            this.daemonGroupBox.Controls.Add(this.editDaemonButton);
            this.daemonGroupBox.Controls.Add(this.lblDaemonAE);
            this.daemonGroupBox.Controls.Add(this.lblDaemonAEResult);
            this.daemonGroupBox.Controls.Add(this.lblDaemonIP);
            this.daemonGroupBox.Controls.Add(this.lblDaemonIPResult);
            this.daemonGroupBox.Controls.Add(this.lblDaemonPort);
            this.daemonGroupBox.Controls.Add(this.lblDaemonPortResult);
            this.daemonGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.daemonGroupBox.Location = new System.Drawing.Point(9, 28);
            this.daemonGroupBox.Name = "daemonGroupBox";
            this.daemonGroupBox.Size = new System.Drawing.Size(219, 78);
            this.daemonGroupBox.TabIndex = 16;
            this.daemonGroupBox.TabStop = false;
            this.daemonGroupBox.Text = "Daemon";
            // 
            // editDaemonButton
            // 
            this.editDaemonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDaemonButton.Location = new System.Drawing.Point(167, 49);
            this.editDaemonButton.Name = "editDaemonButton";
            this.editDaemonButton.Size = new System.Drawing.Size(46, 24);
            this.editDaemonButton.TabIndex = 15;
            this.editDaemonButton.Text = "Edit";
            this.editDaemonButton.UseVisualStyleBackColor = true;
            this.editDaemonButton.Click += new System.EventHandler(this.editDaemonButton_Click);
            // 
            // lblDaemonAE
            // 
            this.lblDaemonAE.AutoSize = true;
            this.lblDaemonAE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaemonAE.Location = new System.Drawing.Point(6, 17);
            this.lblDaemonAE.Name = "lblDaemonAE";
            this.lblDaemonAE.Size = new System.Drawing.Size(47, 13);
            this.lblDaemonAE.TabIndex = 1;
            this.lblDaemonAE.Text = "AE Title:";
            // 
            // lblDaemonAEResult
            // 
            this.lblDaemonAEResult.AutoSize = true;
            this.lblDaemonAEResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaemonAEResult.Location = new System.Drawing.Point(82, 17);
            this.lblDaemonAEResult.Name = "lblDaemonAEResult";
            this.lblDaemonAEResult.Size = new System.Drawing.Size(10, 13);
            this.lblDaemonAEResult.TabIndex = 2;
            this.lblDaemonAEResult.Text = "-";
            // 
            // lblDaemonIP
            // 
            this.lblDaemonIP.AutoSize = true;
            this.lblDaemonIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaemonIP.Location = new System.Drawing.Point(6, 32);
            this.lblDaemonIP.Name = "lblDaemonIP";
            this.lblDaemonIP.Size = new System.Drawing.Size(60, 13);
            this.lblDaemonIP.TabIndex = 3;
            this.lblDaemonIP.Text = "IP address:";
            // 
            // lblDaemonIPResult
            // 
            this.lblDaemonIPResult.AutoSize = true;
            this.lblDaemonIPResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaemonIPResult.Location = new System.Drawing.Point(82, 32);
            this.lblDaemonIPResult.Name = "lblDaemonIPResult";
            this.lblDaemonIPResult.Size = new System.Drawing.Size(10, 13);
            this.lblDaemonIPResult.TabIndex = 4;
            this.lblDaemonIPResult.Text = "-";
            // 
            // lblDaemonPort
            // 
            this.lblDaemonPort.AutoSize = true;
            this.lblDaemonPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaemonPort.Location = new System.Drawing.Point(6, 47);
            this.lblDaemonPort.Name = "lblDaemonPort";
            this.lblDaemonPort.Size = new System.Drawing.Size(29, 13);
            this.lblDaemonPort.TabIndex = 5;
            this.lblDaemonPort.Text = "Port:";
            // 
            // lblDaemonPortResult
            // 
            this.lblDaemonPortResult.AutoSize = true;
            this.lblDaemonPortResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaemonPortResult.Location = new System.Drawing.Point(82, 47);
            this.lblDaemonPortResult.Name = "lblDaemonPortResult";
            this.lblDaemonPortResult.Size = new System.Drawing.Size(10, 13);
            this.lblDaemonPortResult.TabIndex = 6;
            this.lblDaemonPortResult.Text = "-";
            // 
            // localGroupBox
            // 
            this.localGroupBox.Controls.Add(this.editLocalButton);
            this.localGroupBox.Controls.Add(this.lblLocalPortResult);
            this.localGroupBox.Controls.Add(this.lblLocalAE);
            this.localGroupBox.Controls.Add(this.lblLocalPort);
            this.localGroupBox.Controls.Add(this.lblLocalAEResult);
            this.localGroupBox.Controls.Add(this.lblLocalIPResult);
            this.localGroupBox.Controls.Add(this.lblLocalIP);
            this.localGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localGroupBox.Location = new System.Drawing.Point(232, 28);
            this.localGroupBox.Name = "localGroupBox";
            this.localGroupBox.Size = new System.Drawing.Size(219, 78);
            this.localGroupBox.TabIndex = 17;
            this.localGroupBox.TabStop = false;
            this.localGroupBox.Text = "Local";
            // 
            // editLocalButton
            // 
            this.editLocalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editLocalButton.Location = new System.Drawing.Point(167, 49);
            this.editLocalButton.Name = "editLocalButton";
            this.editLocalButton.Size = new System.Drawing.Size(46, 24);
            this.editLocalButton.TabIndex = 15;
            this.editLocalButton.Text = "Edit";
            this.editLocalButton.UseVisualStyleBackColor = true;
            this.editLocalButton.Click += new System.EventHandler(this.editLocalButton_Click);
            // 
            // lblLocalPortResult
            // 
            this.lblLocalPortResult.AutoSize = true;
            this.lblLocalPortResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalPortResult.Location = new System.Drawing.Point(82, 46);
            this.lblLocalPortResult.Name = "lblLocalPortResult";
            this.lblLocalPortResult.Size = new System.Drawing.Size(10, 13);
            this.lblLocalPortResult.TabIndex = 13;
            this.lblLocalPortResult.Text = "-";
            // 
            // lblLocalAE
            // 
            this.lblLocalAE.AutoSize = true;
            this.lblLocalAE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalAE.Location = new System.Drawing.Point(6, 16);
            this.lblLocalAE.Name = "lblLocalAE";
            this.lblLocalAE.Size = new System.Drawing.Size(47, 13);
            this.lblLocalAE.TabIndex = 8;
            this.lblLocalAE.Text = "AE Title:";
            // 
            // lblLocalPort
            // 
            this.lblLocalPort.AutoSize = true;
            this.lblLocalPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalPort.Location = new System.Drawing.Point(6, 46);
            this.lblLocalPort.Name = "lblLocalPort";
            this.lblLocalPort.Size = new System.Drawing.Size(29, 13);
            this.lblLocalPort.TabIndex = 12;
            this.lblLocalPort.Text = "Port:";
            // 
            // lblLocalAEResult
            // 
            this.lblLocalAEResult.AutoSize = true;
            this.lblLocalAEResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalAEResult.Location = new System.Drawing.Point(82, 16);
            this.lblLocalAEResult.Name = "lblLocalAEResult";
            this.lblLocalAEResult.Size = new System.Drawing.Size(10, 13);
            this.lblLocalAEResult.TabIndex = 9;
            this.lblLocalAEResult.Text = "-";
            // 
            // lblLocalIPResult
            // 
            this.lblLocalIPResult.AutoSize = true;
            this.lblLocalIPResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalIPResult.Location = new System.Drawing.Point(82, 31);
            this.lblLocalIPResult.Name = "lblLocalIPResult";
            this.lblLocalIPResult.Size = new System.Drawing.Size(10, 13);
            this.lblLocalIPResult.TabIndex = 11;
            this.lblLocalIPResult.Text = "-";
            // 
            // lblLocalIP
            // 
            this.lblLocalIP.AutoSize = true;
            this.lblLocalIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalIP.Location = new System.Drawing.Point(6, 31);
            this.lblLocalIP.Name = "lblLocalIP";
            this.lblLocalIP.Size = new System.Drawing.Size(60, 13);
            this.lblLocalIP.TabIndex = 10;
            this.lblLocalIP.Text = "IP address:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 654);
            this.Controls.Add(this.grpExport);
            this.Controls.Add(this.lblUtvecklare);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.grpList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "RT Data Extractor (Version 1.3)";
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.grpList.ResumeLayout(false);
            this.grpList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.grpExport.ResumeLayout(false);
            this.grpExport.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.daemonGroupBox.ResumeLayout(false);
            this.daemonGroupBox.PerformLayout();
            this.localGroupBox.ResumeLayout(false);
            this.localGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.GroupBox grpExport;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label lblUtvecklare;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.GroupBox daemonGroupBox;
        private System.Windows.Forms.Button editDaemonButton;
        private System.Windows.Forms.Label lblDaemonAE;
        private System.Windows.Forms.Label lblDaemonAEResult;
        private System.Windows.Forms.Label lblDaemonIP;
        private System.Windows.Forms.Label lblDaemonIPResult;
        private System.Windows.Forms.Label lblDaemonPort;
        private System.Windows.Forms.Label lblDaemonPortResult;
        private System.Windows.Forms.GroupBox localGroupBox;
        private System.Windows.Forms.Button editLocalButton;
        private System.Windows.Forms.Label lblLocalPortResult;
        private System.Windows.Forms.Label lblLocalAE;
        private System.Windows.Forms.Label lblLocalPort;
        private System.Windows.Forms.Label lblLocalAEResult;
        private System.Windows.Forms.Label lblLocalIPResult;
        private System.Windows.Forms.Label lblLocalIP;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.DataGridView gridList;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.ProgressBar prgBarPatient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.ToolTip toolTipInput;
        private System.Windows.Forms.TextBox txbOutput;
        private System.Windows.Forms.Button btnQC;
        private System.Windows.Forms.ToolTip toolTipQC;
    }
}

