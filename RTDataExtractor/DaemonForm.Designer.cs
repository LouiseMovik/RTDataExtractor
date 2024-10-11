namespace RTDataExtractor
{
    partial class DaemonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DaemonForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpName = new System.Windows.Forms.GroupBox();
            this.txtDaemonPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtDaemonIPAddress = new System.Windows.Forms.TextBox();
            this.txtDaemonAETitle = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblAETitle = new System.Windows.Forms.Label();
            this.grpName.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(246, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(220, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(11, 121);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(220, 30);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpName
            // 
            this.grpName.Controls.Add(this.txtDaemonPort);
            this.grpName.Controls.Add(this.lblPort);
            this.grpName.Controls.Add(this.txtDaemonIPAddress);
            this.grpName.Controls.Add(this.txtDaemonAETitle);
            this.grpName.Controls.Add(this.lblIPAddress);
            this.grpName.Controls.Add(this.lblAETitle);
            this.grpName.Location = new System.Drawing.Point(12, 11);
            this.grpName.Name = "grpName";
            this.grpName.Size = new System.Drawing.Size(453, 104);
            this.grpName.TabIndex = 11;
            this.grpName.TabStop = false;
            // 
            // txtDaemonPort
            // 
            this.txtDaemonPort.Location = new System.Drawing.Point(139, 71);
            this.txtDaemonPort.Name = "txtDaemonPort";
            this.txtDaemonPort.Size = new System.Drawing.Size(302, 20);
            this.txtDaemonPort.TabIndex = 5;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(8, 74);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port:";
            // 
            // txtDaemonIPAddress
            // 
            this.txtDaemonIPAddress.Location = new System.Drawing.Point(139, 45);
            this.txtDaemonIPAddress.Name = "txtDaemonIPAddress";
            this.txtDaemonIPAddress.Size = new System.Drawing.Size(302, 20);
            this.txtDaemonIPAddress.TabIndex = 3;
            // 
            // txtDaemonAETitle
            // 
            this.txtDaemonAETitle.Location = new System.Drawing.Point(139, 19);
            this.txtDaemonAETitle.Name = "txtDaemonAETitle";
            this.txtDaemonAETitle.Size = new System.Drawing.Size(302, 20);
            this.txtDaemonAETitle.TabIndex = 2;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(8, 48);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(61, 13);
            this.lblIPAddress.TabIndex = 1;
            this.lblIPAddress.Text = "IP Address:";
            // 
            // lblAETitle
            // 
            this.lblAETitle.AutoSize = true;
            this.lblAETitle.Location = new System.Drawing.Point(8, 22);
            this.lblAETitle.Name = "lblAETitle";
            this.lblAETitle.Size = new System.Drawing.Size(47, 13);
            this.lblAETitle.TabIndex = 0;
            this.lblAETitle.Text = "AE Title:";
            // 
            // DaemonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 163);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DaemonForm";
            this.Text = "DaemonForm";
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpName;
        private System.Windows.Forms.TextBox txtDaemonPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtDaemonIPAddress;
        private System.Windows.Forms.TextBox txtDaemonAETitle;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblAETitle;
    }
}