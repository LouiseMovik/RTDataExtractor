using EvilDICOM.Network.SCUOps;
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
using static System.ActivationContext;

namespace RTDataExtractor
{
    public partial class LocalEntitiesForm : Form
    {
        private LocalEntitiesManager localManager;
        private Local currLocal;

        public LocalEntitiesForm()
        {
            InitializeComponent();

            localManager = new LocalEntitiesManager();
            currLocal = new Local();

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
                string[] savedLocals = File.ReadAllLines(workPath + @"\LocalEntities.txt");

                foreach (var strLocal in savedLocals)
                {
                    string[] splittedLocal = strLocal.Split('\t');
                    localManager.AddLocalEntity(new Local(splittedLocal[0], int.Parse(splittedLocal[1])));
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
            lstLocalEntities.Items.Clear();
            lstLocalEntities.Items.AddRange(localManager.EntitiesListToString());
        }

        /// <summary>
        /// Method for saving the list of entities.
        /// </summary>
        private void SaveEntitiesList()
        {
            string exeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string workPath = Path.GetDirectoryName(exeFilePath);
            StreamWriter saveLocals = new StreamWriter(workPath + @"\LocalEntities.txt");
            foreach (var item in lstLocalEntities.Items)
            {
                string[] splittedItem = item.ToString().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                saveLocals.WriteLine(splittedItem[0] + "\t" + splittedItem[1]);
            }
            saveLocals.Close();
        }

        /// <summary>
        /// Event-handler method for when the Add button is clicked.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LocalForm localForm = new LocalForm(currLocal);
            DialogResult dlgResult = localForm.ShowDialog();
            currLocal = localForm.LocalData;

            if (dlgResult == DialogResult.OK)
            {
                bool wasAdded = localManager.AddLocalEntity(currLocal);
                if (!wasAdded) MessageBox.Show("The entity was not added.");
                currLocal = new Local();
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
                currLocal = localManager.GetEntityAt(lstLocalEntities.SelectedIndex);
                LocalForm localForm = new LocalForm(currLocal);
                localForm.UpdateGUI();
                DialogResult dlgResult = localForm.ShowDialog();
                currLocal = localForm.LocalData;

                if (dlgResult == DialogResult.OK)
                {
                    bool wasChanged = localManager.ChangeEntityAt(lstLocalEntities.SelectedIndex, currLocal);
                    if (!wasChanged) MessageBox.Show("The entity was not changed.");
                    currLocal = new Local();
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
                    bool wasDeleted = localManager.DeleteEntityAt(lstLocalEntities.SelectedIndex);
                    if (!wasDeleted) MessageBox.Show("The entity was not deleted.");
                    lstLocalEntities.SelectedIndex = -1;
                    currLocal = new Local();
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
                currLocal = localManager.GetEntityAt(lstLocalEntities.SelectedIndex);
                Settings.Default.LocalAETitle = currLocal.AETitle;
                Settings.Default.LocalPort = currLocal.Port;
                Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Mark the entity to select before pressing Select.");
            }
        }

        /// <summary>
        /// Method for checking if the index is used for an entity in lstLocalEntities
        /// </summary>
        private bool CheckIndex()
        {
            bool isOK = false;
            if (lstLocalEntities.SelectedIndex >= 0 && lstLocalEntities.SelectedIndex < localManager.CurrentNumberOfEntities())
            {
                isOK = true;
            }
            return isOK;
        }
    }
}
