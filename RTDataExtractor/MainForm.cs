using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using RTDataExtractor.Properties;
using System.Drawing.Drawing2D;
using System.Diagnostics.Eventing.Reader;
using VMS.TPS.Common.Model.API;

namespace RTDataExtractor
{
    public partial class MainForm : Form
    {
        private Prioritizer prioritizer;
        private DataTable dtPatients;
        private string pathOutput;


        public MainForm()
        {
            // Optional for user: Reset previous property settings. 
            Settings.Default.Reset();

            InitializeComponent();

            // Optional for user: Enter static paths
            //txtPath.Text = @"";
            //pathOutput = @"";
            //txtOutputPath.Text = pathOutput;

            prioritizer = new Prioritizer(pathOutput);
            toolTipInput.SetToolTip(btnSelectFolder, "Path to a textfile with patient information structured as:\nPatient ID \t Pseudo ID \t Course ID \t Plan ID");
            toolTipInput.SetToolTip(txtPath, "Path to a textfile with patient information structured as:\nPatient ID \t Pseudo ID \t Course ID \t Plan ID");
            UpdateDICOMSettings();

            txtPath.Select();
        }

        /// <summary>
        /// Start extraction.
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ControlInput())
            {
                btnStart.Enabled = false;
                if (dtPatients.Rows.Count > 0)
                {
                    prioritizer.StartExtraction(dtPatients, pathOutput, this);
                }
                else
                {
                    
                }
                btnStart.Enabled = true;
            }
        }

        /// <summary>
        /// Method for updating the DICOM settings in the GUI.
        /// </summary>
        private void UpdateDICOMSettings()
        {
            lblDaemonAEResult.Text = prioritizer.Extractor.Daemon.AeTitle;
            lblDaemonIPResult.Text = prioritizer.Extractor.Daemon.IpAddress;
            lblDaemonPortResult.Text = prioritizer.Extractor.Daemon.Port.ToString();

            lblLocalAEResult.Text = prioritizer.Extractor.Local.AeTitle;
            lblLocalIPResult.Text = prioritizer.Extractor.Local.IpAddress;
            lblLocalPortResult.Text = prioritizer.Extractor.Local.Port.ToString();
        }

        /// <summary>
        /// Updates the progress bar related to all requests.
        /// </summary>
        public void UpdateRequestsBar(int value)
        {
            prgBar.Value = value;
        }

        /// <summary>
        /// Updates the progress bar related to the patient request.
        /// </summary>
        public void UpdatePatientRequestsBar(int value)
        {
            prgBarPatient.Value = value;
        }

        /// <summary>
        /// Sets maximum for the progress bar related to the patient request.
        /// </summary>
        public void SetPatientProgressMaximum(int value)
        {
            prgBarPatient.Maximum = value;  
        }

        /// <summary>
        /// Event handler method for when txtPath has been edited. 
        /// </summary>
        private void txtPath_Leave(object sender, EventArgs e)
        {
            try
            {
                string pathToData = txtPath.Text;
                dtPatients = LoadPatientDataIntoDataTable(pathToData);
                gridList.DataSource = dtPatients;

                if (dtPatients.Rows.Count > 0)
                {
                    prgBar.Maximum = dtPatients.AsEnumerable().Select(row => row.Field<string>("Patient ID")).Distinct().Count();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Event handler method for when txtOutputPath has been edited. 
        /// </summary>
        private void txtOutputPath_Leave(object sender, EventArgs e)
        {
            pathOutput = txtOutputPath.Text;
            if (!Directory.Exists(pathOutput))
            {
                Directory.CreateDirectory(pathOutput);
            }
        }

        /// <summary>
        /// Event handler method for when entered is pressed in txtPath. 
        /// </summary>
        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPath_Leave(this, new EventArgs());
            }
        }

        /// <summary>
        /// Event handler method for when entered is pressed in txtOutputPath. 
        /// </summary>
        private void txtOutputPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOutputPath_Leave(this, new EventArgs());
            }
        }

        /// <summary>
        /// Event handler method for when the MainForm is clicked. 
        /// </summary>
        private void MainForm_Click(object sender, EventArgs e)
        {
            txtPath_Leave(this, new EventArgs());
        }

        /// <summary>
        /// Event handler method for when the SelectFolder button is clicked. 
        /// </summary>
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Event handler method for when the SelectOutputFolder button is clicked. 
        /// </summary>
        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtOutputPath.Text = fbd.SelectedPath;
                }
            }
        }

       /// <summary>
       /// Control of all input before extraction is started.       
       /// </summary>
       /// <returns></returns>
        private bool ControlInput()
        {
            bool allOK = true;
            bool tableEmpty = dtPatients == null;
            allOK &= !tableEmpty;
            if (!tableEmpty) allOK &= (dtPatients?.Rows?.Count ?? 0) > 0;
            if (!allOK) MessageBox.Show("No identified export request", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            allOK &= Directory.Exists(pathOutput);
            if (!Directory.Exists(pathOutput)) MessageBox.Show("The output folder does not exist", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            bool contactOK = prioritizer.Extractor.Echo();
            allOK &= contactOK;
            if (!contactOK) MessageBox.Show("Unsuccessful DICOM connection", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return allOK;
        }

        
        /// <summary>
        /// Reads a text file containing patient data and populates a DataTable with the contents.
        /// </summary>
        /// <param name="filePath">The path to the text file containing patient data.</param>
        /// <returns>A DataTable populated with patient information.</returns>
        public DataTable LoadPatientDataIntoDataTable(string filePath)
        {
            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Define columns for the DataTable
            dataTable.Columns.Add("Patient ID", typeof(string));
            dataTable.Columns.Add("Pseudo ID", typeof(string));
            dataTable.Columns.Add("Course ID", typeof(string));
            dataTable.Columns.Add("Plan ID", typeof(string));

            try
            {
                // Read each line from the file
                foreach (string line in File.ReadLines(filePath))
                {
                    // Split the line by comma to get individual fields
                    string[] fields = line.Split('\t');

                    // Check if the line has the expected number of fields
                    if (fields.Length == 4)
                    {
                        // Create a new DataRow and add it to the DataTable
                        DataRow row = dataTable.NewRow();
                        row["Patient ID"] = fields[0];
                        row["Pseudo ID"] = fields[1];
                        row["Course ID"] = fields[2];
                        row["Plan ID"] = fields[3];
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid line format: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred while reading the file: { ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            // Return the populated DataTable
            return dataTable;
        }

        /// <summary>
        /// Event handler method for when the Edit Local button is clicked. 
        /// </summary>
        private void editLocalButton_Click(object sender, EventArgs e)
        {
            LocalEntitiesForm LocalEntitiesForm = new LocalEntitiesForm();
            LocalEntitiesForm.ShowDialog();
            prioritizer = new Prioritizer(pathOutput);
            UpdateDICOMSettings();
        }

        /// <summary>
        /// Event handler method for when the Edit Daemon button is clicked. 
        /// </summary>
        private void editDaemonButton_Click(object sender, EventArgs e)
        {
            DaemonEntitiesForm daemonEntitiesForm = new DaemonEntitiesForm();
            daemonEntitiesForm.ShowDialog();
            prioritizer = new Prioritizer(pathOutput);
            UpdateDICOMSettings();
        }

        /// <summary>
        /// Event handler method for when the Test DICOM Connection button is clicked. 
        /// </summary>
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            bool contactOK = prioritizer.Extractor.Echo();

            if (contactOK)
            {
                MessageBox.Show("Successful", "Testing DICOM Connection (Echo)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed", "Testing DICOM Connection (Echo)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
