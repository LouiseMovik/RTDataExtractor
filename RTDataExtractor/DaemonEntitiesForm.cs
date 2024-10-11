using RTDataExtractor.Properties;
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

namespace RTDataExtractor
{
    public partial class DaemonEntitiesForm : Form
    {
        private DaemonEntitiesManager daemonManager;
        private Daemon currDaemon;

        public DaemonEntitiesForm()
        {
            InitializeComponent();

            daemonManager = new DaemonEntitiesManager();
            currDaemon = new Daemon();

            toolTipSelect.SetToolTip(btnSelect, "Mark the entity to select and press Select");
            toolTipChange.SetToolTip(btnChange, "Mark the entity to change and press Change");
            toolTipDelete.SetToolTip(btnDelete, "Mark the entity to delete and press Delete");

            InitializeEntitiesList();
            UpdateEntitiesList();
        }

        /// <summary>
        /// Method for initiating the list of entities.
        /// </summary>
        private void InitializeEntitiesList()
        {
            string exeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string workPath = Path.GetDirectoryName(exeFilePath);
            try
            {
                string[] savedDaemons = File.ReadAllLines(workPath + @"\DaemonEntities.txt");

                foreach (var strDaemon in savedDaemons)
                {
                    string[] splittedDaemon = strDaemon.Split('\t');
                    daemonManager.AddDaemonEntity(new Daemon(splittedDaemon[0], splittedDaemon[1], int.Parse(splittedDaemon[2])));
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Method for updating the list of entities.
        /// </summary>
        private void UpdateEntitiesList()
        {
            lstDaemonEntities.Items.Clear();
            lstDaemonEntities.Items.AddRange(daemonManager.EntitiesListToString());
        }

        /// <summary>
        /// Method for saving the list of entities.
        /// </summary>
        private void SaveEntitiesList()
        {
            string exeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string workPath = Path.GetDirectoryName(exeFilePath);
            StreamWriter saveDaemons = new StreamWriter(workPath + @"\DaemonEntities.txt");
            foreach (var item in lstDaemonEntities.Items)
            {
                string[] splittedItem = item.ToString().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                saveDaemons.WriteLine(splittedItem[0] + "\t" + splittedItem[1] + "\t" + splittedItem[2]);
            }
            saveDaemons.Close();
        }

        /// <summary>
        /// Event-handler method for when the Add button is clicked.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DaemonForm daemonForm = new DaemonForm(currDaemon);
            DialogResult dlgResult = daemonForm.ShowDialog();
            currDaemon = daemonForm.DaemonData;

            if (dlgResult == DialogResult.OK)
            {
                bool wasAdded = daemonManager.AddDaemonEntity(currDaemon);
                if (!wasAdded) MessageBox.Show("The entity was not added.");
                currDaemon = new Daemon();
                UpdateEntitiesList();
                SaveEntitiesList();
            }
        }

        /// <summary>
        /// Event-handler method for when the Change button is clicked.
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (CheckIndex())
            {
                currDaemon = daemonManager.GetEntityAt(lstDaemonEntities.SelectedIndex);
                DaemonForm daemonForm = new DaemonForm(currDaemon);
                daemonForm.UpdateGUI();
                DialogResult dlgResult = daemonForm.ShowDialog();
                currDaemon = daemonForm.DaemonData;

                if (dlgResult == DialogResult.OK)
                {
                    bool wasChanged = daemonManager.ChangeEntityAt(lstDaemonEntities.SelectedIndex, currDaemon);
                    if (!wasChanged) MessageBox.Show("The entity was not changed.");
                    currDaemon = new Daemon();
                    UpdateEntitiesList();
                    SaveEntitiesList();
                }
            }
            else
            {
                MessageBox.Show("Mark the entity to change before pressing Change");
            }
        }

        /// <summary>
        /// Event-handler method for when the Delete button is clicked
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckIndex())
            {
                DialogResult deleteQuestion = MessageBox.Show("Are you sure you want to delete the selected entity?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteQuestion == DialogResult.Yes)
                {
                    bool wasDeleted = daemonManager.DeleteEntityAt(lstDaemonEntities.SelectedIndex);
                    if (!wasDeleted) MessageBox.Show("The entity was not deleted.");
                    lstDaemonEntities.SelectedIndex = -1;
                    currDaemon = new Daemon();
                    UpdateEntitiesList();
                    SaveEntitiesList();
                }
            }
            else
            {
                MessageBox.Show("Mark the entity to delete before pressing Delete.");
            }
        }

        /// <summary>
        /// Event-handler method for when the Select button is clicked
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckIndex())
            {
                currDaemon = daemonManager.GetEntityAt(lstDaemonEntities.SelectedIndex);
                Settings.Default.DaemonAETitle = currDaemon.AETitle;
                Settings.Default.DaemonIPAddress = currDaemon.IPAddress;
                Settings.Default.DaemonPort = currDaemon.Port;
                Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Mark the entity to select before pressing Select.");
            }
        }

        /// <summary>
        /// Method for checking if the index is used for an entity in lstDaemonEntities
        /// </summary>
        private bool CheckIndex()
        {
            bool isOK = false;
            if (lstDaemonEntities.SelectedIndex >= 0 && lstDaemonEntities.SelectedIndex < daemonManager.CurrentNumberOfEntities())
            {
                isOK = true;
            }
            return isOK;
        }
    }
}
