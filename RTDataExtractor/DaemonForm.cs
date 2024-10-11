using System;
using System.Windows.Forms;

namespace RTDataExtractor
{
    public partial class DaemonForm : Form
    {
        private Daemon m_daemon;
        private bool m_closeForm = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DaemonForm() : this(new Daemon())
        {

        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        public DaemonForm(Daemon daemon)
        {
            InitializeComponent();
            InitializeGUI();

            this.m_daemon = daemon;
        }

        /// <summary>
        /// Read and write property connected to m_daemon.
        /// </summary>
        public Daemon DaemonData
        {
            get
            {
                return m_daemon;
            }
            set
            {
                if (value != null) { m_daemon = value; }
            }
        }

        /// <summary>
        /// Method for initializing the GUI
        /// </summary>
        private void InitializeGUI()
        {
            ClearFields();
        }

        /// <summary>
        /// Method for updating the fields in the DaemonForm GUI
        /// </summary>
        public void UpdateGUI()
        {
            txtDaemonAETitle.Text = m_daemon.AETitle;
            txtDaemonIPAddress.Text = m_daemon.IPAddress;
            txtDaemonPort.Text = m_daemon.Port.ToString();
        }

        /// <summary>
        /// Method for clearing all fields in the form
        /// </summary>
        private void ClearFields()
        {
            txtDaemonAETitle.Clear();
            txtDaemonIPAddress.Clear();
            txtDaemonPort.Clear();
        }

        /// <summary>
        /// Event-handler method for the OK button
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            m_closeForm = true;
            ReadInput();
        }

        /// <summary>
        /// Alternative method for OK button
        /// </summary>
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnOK.PerformClick();
            }
        }

        /// <summary>
        /// Method for reading all input. 
        /// </summary>
        private void ReadInput()
        {
            if (m_daemon.CheckAETitle(txtDaemonAETitle.Text) && m_daemon.CheckIPAddress(txtDaemonIPAddress.Text) && m_daemon.CheckPort(txtDaemonPort.Text))
            {
                m_daemon = new Daemon(txtDaemonAETitle.Text, txtDaemonIPAddress.Text, int.Parse(txtDaemonPort.Text));
            }
            else
            {
                MessageBox.Show(m_daemon.ErrorMessage);
                m_closeForm = false;
                return;
            }
        }

        /// <summary>
        /// Event-handler method for form closing
        /// </summary>
        private void DaemonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_closeForm)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        /// <summary>
        /// Event-handler method for the Cancel button
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_closeForm = true;
            DialogResult closeForm = MessageBox.Show("Are you sure you want to close the form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeForm == DialogResult.No)
            {
                m_closeForm = false;
            }
        }


    }
}

