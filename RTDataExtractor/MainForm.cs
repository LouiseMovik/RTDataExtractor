using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using RTDataExtractor.Properties;
using System.Drawing.Drawing2D;
using System.Diagnostics.Eventing.Reader;
using VMS.TPS.Common.Model.API;
using System.Runtime.InteropServices;

namespace RTDataExtractor
{
    public partial class MainForm : Form
    {
        private Prioritizer prioritizer;
        private DataTable dtPatients;
        private string inputTextfile;
        private string pathOutput;

        public MainForm()
        {
            // OPTIONAL: Reset previous property settings.
            bool resetSettings = false;

            // OPTIONAL: Enter static paths
            inputTextfile = @"";
            pathOutput = @"";

            // Handle user input
            if (resetSettings)
            {
                Settings.Default.Reset();
                prioritizer = new Prioritizer(pathOutput);
                prioritizer.Extractor.ResetReceiver();
            }
            else
            {
                prioritizer = new Prioritizer(pathOutput);
            }

            // Prepare the GUI
            InitializeComponent();
            txtPath.Text = inputTextfile;
            txtOutputPath.Text = pathOutput;
            toolTipInput.SetToolTip(btnSelectFolder, "Path to a textfile with patient information structured as:\nPatient ID \t Pseudo ID \t Plan ID");
            toolTipInput.SetToolTip(txtPath, "Path to a textfile with patient information structured as:\nPatient ID \t Pseudo ID \t Plan ID");
            toolTipQC.SetToolTip(btnQC, "Optional: A quality control can be performed following extraction. \nThe export request is compared to the generated files and a summary is generated in the output folder.");
            UpdateDICOMSettings();
            this.FormClosing += MainForm_FormClosing;
            txtPath.Select();
        }

        /// <summary>
        /// Start extraction.
        /// </summary>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (ControlInput())
            {
                btnStart.Enabled = false;
                txbOutput.BringToFront();
                if (dtPatients.Rows.Count > 0)
                {
                    await Task.Run(() => prioritizer.StartExtraction(dtPatients, pathOutput, this));
                }
                else
                {
                    
                }
                WriteMessage("Extraction completed");
                btnStart.Enabled = true;
            }
        }

        /// <summary>
        /// Writes the message used as argument in the output textbox. 
        /// </summary>
        public void WriteMessage(string message)
        {
            Invoke((Action)(() =>
            {
                txbOutput.AppendText(message);
                txbOutput.AppendText(Environment.NewLine);
            }));
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
            Invoke((Action)(() =>
            {
                prgBar.Value = value;
            }));
        }

        /// <summary>
        /// Updates the progress bar related to the patient request.
        /// </summary>
        public void UpdatePatientRequestsBar(int value)
        {
            Invoke((Action)(() =>
            {
                prgBarPatient.Value = value;
            }));
        }

        /// <summary>
        /// Sets maximum for the progress bar related to the patient request.
        /// </summary>
        public void SetPatientProgressMaximum(int value)
        {
            Invoke((Action)(() =>
            {
                prgBarPatient.Maximum = value;
            }));
        }

        /// <summary>
        /// Event handler method for when txtPath has been edited. 
        /// </summary>
        private void txtPath_Leave(object sender, EventArgs e)
        {
            try
            {
                // View data in DataTable in DataGridView
                string pathToData = txtPath.Text;
                dtPatients = LoadPatientDataIntoDataTable(pathToData);
                gridList.DataSource = dtPatients;

                // Set the DataGridView properties
                gridList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Disable global autosize mode

                // Set the width of the first and second columns
                gridList.Columns[0].Width = 120; // Set the desired width for the first column
                gridList.Columns[1].Width = 120; // Set the desired width for the second column

                // Set the third column to fill the remaining space
                gridList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Prepare progress bar
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
            prioritizer = new Prioritizer(pathOutput);
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
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to show only .txt files
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Select a text file";

            // Show the dialog and compare the result with DialogResult.OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                txtPath.Text = filePath;
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
            dataTable.Columns.Add("Plan ID", typeof(string));

            try
            {
                // Read each line from the file
                foreach (string line in File.ReadLines(filePath))
                {
                    // Split the line by comma to get individual fields
                    string[] fields = line.Split('\t');

                    // Check if the line has the expected number of fields
                    if (fields.Length == 3)
                    {
                        // Create a new DataRow and add it to the DataTable
                        DataRow row = dataTable.NewRow();
                        row["Patient ID"] = fields[0];
                        row["Pseudo ID"] = fields[1];
                        row["Plan ID"] = fields[2];
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid line format: {line}");
                    }
                }
            }
            catch
            {
                //MessageBox.Show($"An error occurred while reading the file: { ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            // Return the populated DataTable
            return dataTable;
        }

        /// <summary>
        /// Event handler method for when the Exit button is clicked. 
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Display a confirmation dialog
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancel the close action
            }
            // If "Yes" is selected, the form will close
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

        private void button1_Click(object sender, EventArgs e)
        {
            btnQC.Enabled = false;

            // Perform QC
            QC qc = new QC(txtPath.Text, pathOutput);
            btnQC.Enabled = true;
        }
    }
}
