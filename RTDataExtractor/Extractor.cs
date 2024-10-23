using EvilDICOM.Network;
using EvilDICOM.Network.SCUOps;
using EvilDICOM.Core;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using System.Windows.Forms;
using RTDataExtractor.Properties;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using EvilDICOM.Core.Helpers;
using System.IO;
using System.Linq;
using System.Data;
using EvilDICOM.Network.DIMSE.IOD;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using VMS.TPS.Common.Model.API;
using System.Numerics;
using EvilDICOM.Anonymization.Settings;
using EvilDICOM.Core.Element;
using System.Xml;

namespace RTDataExtractor
{
    class Extractor
    {
        private Entity daemon;
        private Entity local;
        private DICOMSCP receiver;
        private CFinder finder;
        private CMover mover;
        private Process recieverProcess;

        /// <summary>
        /// Default constructor that will initialize the default entities.  
        /// </summary>
        public Extractor() : this(new Entity(Settings.Default.DaemonAETitle, Settings.Default.DaemonIPAddress, Settings.Default.DaemonPort), Entity.CreateLocal(Settings.Default.LocalAETitle, Settings.Default.LocalPort))
        {
            
        }

        /// <summary>
        /// Overloaded constructor. 
        /// </summary>
        public Extractor(Entity daemon, Entity local)
        {
            InitializeEntities(daemon, local);
        }

        /// <summary>
        /// Read and write property.
        /// </summary>
        public Entity Daemon
        {
            get { return daemon; }
            set { daemon = value; }
        }

        /// <summary>
        /// Read and write property.
        /// </summary>
        public Entity Local
        {
            get { return local; }
            set { local = value; }
        }

        /// <summary>
        /// Initializes the daemon and local entities. 
        /// </summary>
        private void InitializeEntities(Entity daemon, Entity local)
        {
            // Store the details of the daemon (Ae Title, IP, port)
            this.daemon = daemon;

            // Store the details of the client (Ae Title, port) -> IP address is determined by CreateLocal() method
            this.local = local;
        }

        /// <summary>
        /// Creates a DICOM SCU based on the local entity to get C Storer from the daemon. 
        /// </summary>
        public void CreateHelpers()
        {
            // Set up a client (DICOM SCU = Service Class User)
            DICOMSCU client = new DICOMSCU(local);

            // Build a finder class to help with C-FIND operations
            finder = client.GetCFinder(daemon);

            // Create a mover
            mover = client.GetCMover(daemon);
        }

        /// <summary>
        /// Using evil-DICOM, the receiver is told to catch files as they come in and store them at predefined location.
        /// </summary>
        public void PrepareReceiver(string pathOutput)
        {
            // Set up a receiver to catch the files as they come in
            receiver = new DICOMSCP(local);

            // Let the daemon know we can take anything it sends
            receiver.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;

            // Set the action when a DICOM file comes in
            receiver.DIMSEService.CStoreService.CStorePayloadAction = (dcm, asc) =>
            {
                Debug.WriteLine("Test");
                var path = Path.Combine(pathOutput, dcm.GetSelector().
                SOPInstanceUID.Data + ".dcm");
                dcm.Write(path);
                return true; // Lets daemom know if you successfully wrote to drive
            };
            receiver.ListenForIncomingAssociations(true);
        }

        /// <summary>
        /// Using DCMTK, a DICOM server (receiver) is started which will catch files as they come in and store them at predefined location.
        /// </summary>
        public void StartDICOMServer(string pathOutput)
        {
            KillProcesses();
            // Search for the StartDICOMServer.cmd file
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            basePath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            var directory = new DirectoryInfo(basePath);
            var cmdFile = directory.GetFiles("StartDICOMServer.cmd", searchOption: SearchOption.AllDirectories).First();
            string cmdFilePath = cmdFile.FullName;

            //// Read the current content of the .cmd file
            //string newCmdFileContent = File.ReadAllText(cmdFilePath);

            // Create content
            int port = local.Port;
            string ae_title = local.AeTitle;
            string newCmdFileContent = $"@echo off\r\n\r\ncls\r\n\r\nstorescp {port} -d -od {pathOutput} -aet {ae_title}";

            // Save the modified content back to the .cmd file
            File.WriteAllText(cmdFilePath, newCmdFileContent);

            // Execute the updated .cmd file
            recieverProcess = ExecuteCmdFile(cmdFilePath);
        }

        private Process ExecuteCmdFile(string cmdFilePath)
        {
            // Initialize a new process to run the .cmd file
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(cmdFilePath); 
            process.StartInfo.FileName = cmdFilePath;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = true; 

            // Start the process
            process.Start();
            return process;
        }

        public void KillProcesses()
        {
            // Kill the receiver process
            if (recieverProcess!= null) recieverProcess.Kill();

            // Try to kill all StoreSCP processes
            foreach (var process in Process.GetProcessesByName("storescp"))
            {
                try
                {
                    process.Kill();
                }
                catch 
                { }
            }
        }

        public void Extract(List<string> UIDs, MainForm mainForm)
        { 
            ushort msgId = 1;

            // Counter for patient requests
            mainForm.SetPatientProgressMaximum(UIDs.Count * 2);
            int patientCounter = 0;

            foreach (var UID in UIDs)
            {
                CFindImageIOD iod = new CFindImageIOD() { SOPInstanceUID = UID };
                
                // Make sure local is on the whitelist of the daemon
                var response = mover.SendCMove(iod, local.AeTitle, ref msgId);
                if (response.Status != 0)
                {
                    mainForm.WriteMessage($"Unable to extract with message: {response}");
                }
                patientCounter++;
                mainForm.UpdatePatientRequestsBar(patientCounter);
            }
        }

        /// <summary>
        /// Uses evil DICOM to identify the UIDs of the considered treatment plan and linked dose, ct and structure set. 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="courseID"></param>
        /// <param name="planID"></param>
        public List<string> IdentifyLinkedObjects(string ID, string courseID, string planID)
        {
            var studies = finder.FindStudies(ID);
            var series = finder.FindSeries(studies);
            var plans = finder.FindPlans(series);
            var consPlan = plans.First(r=>r.PlanLabel.Equals(planID));
            var consSOPInstanceUID = consPlan.SOPInstanceUID;

            var consStudyInstanceUID = IdentifyStudyInstanceUID(studies, consSOPInstanceUID);
            var consSeries = finder.FindSeries(studies.Where(r=>r.StudyInstanceUID.Equals(consStudyInstanceUID)));

            var plan = finder.FindPlans(consSeries).First(r=>r.PlanLabel.Equals(planID));
            var doses = finder.FindDoses(consSeries);
            var consDoses = doses.Where(r => r.ReferencedPlan.sopInstanceUID.Equals(plan.SOPInstanceUID) && r.DoseSummationType.Equals("PLAN"));
            var CTs = finder.FindImages(consSeries.Where(s => s.Modality == "CT"));
            var SS = finder.FindStructures(consSeries);

            List<string> outputList = new List<string>
            {
                plan.SOPInstanceUID
            };
            outputList.AddRange(consDoses.Select(r=>r.SOPInstanceUID).ToList());
            outputList.AddRange(CTs.Select(r => r.SOPInstanceUID).ToList());
            outputList.AddRange(SS.Select(r => r.SOPInstanceUID).ToList());
            outputList.Distinct();
            return outputList;
        }

        private string IdentifyStudyInstanceUID(IEnumerable<CFindStudyIOD> studies, string refSOPInstanceUID)
        {
            foreach (var study in studies)
            {
                string currStudyInstanceUID = study.StudyInstanceUID;
                var series = finder.FindSeries(study);
                foreach (var singleSeries in series)
                {
                    var plans = series.Where(s => s.Modality == "RTPLAN").SelectMany(ser => finder.FindImages(ser));
                    if (plans.Where(r => r.SOPInstanceUID.Equals(refSOPInstanceUID)).Count() > 0)
                    {
                        return currStudyInstanceUID;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
         /// Uses a combination of Evil DICOM and own methods to anonymize created files.
         /// </summary>
         /// <param name="ID"></param>
         /// <param name="courseID"></param>
         /// <param name="planID"></param>
        public void Anonymize(string pathOutput, string ID, string pseudoID, MainForm mainForm)
        {
            // Identify files to anonymize and set counter
            var toAnonymize = Directory.GetFiles(pathOutput);

            // Verify that files were exported for the considered patient
            var test = toAnonymize.Length;
            if (toAnonymize.Length == 0)
            {
                mainForm.WriteMessage($"No files were exported for patient: {ID}");
                return;
            }

            int patientCounter = toAnonymize.Length;

            var settings = AnonymizationSettings.Default;
            // Change mapping but keep connections
            settings.DoAnonymizeUIDs = true;
            settings.FirstName = pseudoID;
            settings.LastName = pseudoID;
            settings.Id = pseudoID;

            // Gets a current list of UIDs so it can create new ones 
            var queue = EvilDICOM.Anonymization.AnonymizationQueue.BuildQueue(settings, toAnonymize);

            // Look at plan files and save the references SOP instance UID to filter structure sets later
            List<string> referencedSOPs = new List<string>();
            var plans = toAnonymize.Where(r => Path.GetFileName(r).StartsWith("RP"));
            foreach (var plan in plans)
            {
                var dcm = DICOMObject.Read(plan);
                string referencedSOP = dcm.FindFirst(TagHelper.ReferencedSOPInstanceUID).DData.ToString();
                referencedSOPs.Add(referencedSOP);
                referencedSOPs = referencedSOPs.Distinct().ToList();
            }

            // A boolean variable is created that will look for remaining files in the output folder that does not belong to the considered patient
            bool otherPatientFiles = false;

            foreach (var file in toAnonymize)
            {
                DICOMObject dcm = DICOMObject.Read(file);
                if (dcm.FindFirst(TagHelper.PatientID).DData.ToString() == ID)
                { 
                    // Filters out structure sets that are not linked to the treatment plans. These will be deleted. 
                    if (dcm.FindFirst(TagHelper.Modality).DData.ToString() == "RTSTRUCT" && !referencedSOPs.Contains(dcm.FindFirst(TagHelper.SOPInstanceUID).DData.ToString()))
                    {
                        File.Delete(file);

                        // Update progress bar
                        patientCounter++;
                        mainForm.UpdatePatientRequestsBar(patientCounter);

                        continue;
                    }

                    // All other files are anonymized in two steps
                    queue.Anonymize(dcm);
                    dcm = EditDICOMFile(dcm);

                    // The anonymized files are saved in a new directory
                    string basePath = Directory.GetParent(file).FullName;
                    Directory.CreateDirectory(basePath + @"\" + pseudoID);
                    dcm.Write(Path.Combine(basePath, pseudoID, GetModalityCode(dcm.FindFirst(TagHelper.Modality).DData.ToString()) + "." + dcm.FindFirst(TagHelper.SOPInstanceUID).DData.ToString() + ".dcm"));
                
                    // The initial (not anonymized) file is deleted
                    File.Delete(file);

                    // Update progress bar
                    patientCounter++;
                    mainForm.UpdatePatientRequestsBar(patientCounter);
                }
                else
                {
                    otherPatientFiles = true;
                }
            }

            // Check that all not anonymized files have been deleted
            string[] remainingFiles = Directory.GetFiles(pathOutput, "*.*", SearchOption.TopDirectoryOnly);
            if (remainingFiles.Length > 0)
            {
                if (otherPatientFiles) 
                {
                    mainForm.WriteMessage($"There are remaining files in the output folder that does not belong to the considered patient: {ID}");
                }
                else
                {
                    mainForm.WriteMessage($"All files were not anonymized");
                }
            }
        }

        /// <summary>
        /// Help method to generate file names.
        /// </summary>
        /// <param name="modality"></param>
        /// <returns></returns>
        public string GetModalityCode(string modality)
        {
            switch (modality)
            {
                case "CT":
                    return "CT";
                case "RTPLAN":
                    return "RP";
                case "RTDOSE":
                    return "RD";
                case "RTSTRUCT":
                    return "RS";
                default:
                    return "UnknownModality";
            }
        }

        /// <summary>
        /// Edits the DICOM files depending on modality. 
        /// </summary>
        private DICOMObject EditDICOMFile(DICOMObject dicomObject)
        {
            if ((string)dicomObject.FindFirst(TagHelper.Modality).DData == "RTPLAN")
            {
                Unapprove(dicomObject);
                RemoveReferencedPlan(dicomObject);
                EmptySetupNote(dicomObject);
            }
            else if (dicomObject.FindFirst(TagHelper.Modality).DData.Equals("RTSTRUCT"))
            {
                Unapprove(dicomObject);
            }
            return dicomObject;
        }

        /// <summary>
        /// Unapproves the DICOM file.
        /// </summary>
        private void Unapprove(DICOMObject dicomObject)
        {
            var appStatus = new CodeString
            {
                DData = "UNAPPROVED",
                Tag = TagHelper.ApprovalStatus
            };
            dicomObject.Replace(appStatus);
        }

        /// <summary>
        /// Removes the DICOM tag ReferencedRTPlanSequence.
        /// </summary>
        private void RemoveReferencedPlan(DICOMObject dicomObjects)
        {
            dicomObjects.Remove(TagHelper.ReferencedRTPlanSequence);
        }

        /// <summary>
        /// Removes the DICOM tag Setup​Technique​Description.
        /// </summary>
        private void EmptySetupNote(DICOMObject dicomObjects)
        {
            dicomObjects.Remove(TagHelper.Setup​Technique​Description);
        }

        /// <summary>
        /// Performs the echo operation.
        /// </summary>
        public bool Echo()
        {
            // Set up a client
            DICOMSCU client = new DICOMSCU(local);

            if (client.Ping(daemon, 500))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Performs the SQL connection test.
        /// </summary>
        public bool TestSQLConnection(string connectionString)
        {
            try
            {
                // Using a local SqlConnection object for the test
                using (SqlConnection localSQLConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        localSQLConnection.Open();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
