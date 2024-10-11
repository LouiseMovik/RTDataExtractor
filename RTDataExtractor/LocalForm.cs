using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTDataExtractor
{
    public partial class LocalForm : Form
    {
        private Local m_local;
        private bool m_closeForm = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocalForm() : this(new Local())
        {

        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        public LocalForm(Local local)
        {
            InitializeComponent();
            InitializeGUI();

            this.m_local = local;
        }

        /// <summary>
        /// Read and write property connected to m_local.
        /// </summary>
        public Local LocalData
        {
            get
            {
                return m_local;
            }
            set
            {
                if (value != null) { m_local = value; }
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
        /// Method for updating the fields in the LocalForm GUI
        /// </summary>
        public void UpdateGUI()
        {
            txtLocalAETitle.Text = m_local.AETitle;
            txtLocalPort.Text = m_local.Port.ToString();
        }

        /// <summary>
        /// Method for clearing all fields in the form
        /// </summary>
        private void ClearFields()
        {
            txtLocalAETitle.Clear();
            txtLocalPort.Clear();
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
            if (m_local.CheckAETitle(txtLocalAETitle.Text) && m_local.CheckPort(txtLocalPort.Text))
            {
                m_local = new Local(txtLocalAETitle.Text, int.Parse(txtLocalPort.Text));
            }
            else
            {
                MessageBox.Show(m_local.ErrorMessage);
                m_closeForm = false;
                return;
            }
        }

        /// <summary>
        /// Event-handler method for form closing
        /// </summary>
        private void LocalForm_FormClosing(object sender, FormClosingEventArgs e)
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
