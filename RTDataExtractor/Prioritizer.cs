using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Modules;
using EvilDICOM.Network.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace RTDataExtractor
{
    class Prioritizer
    {
        private Extractor extractor;
        private string pathOutput;
        private string[] modalities = new string[] { "CT", "MR", "PT", "US", "RTSTRUCT", "RTPLAN", "RTDOSE" };

        /// <summary>
        /// Default constructor that will create an extractor using the default entities.  
        /// </summary>
        public Prioritizer(string pathOutput) : this(new Extractor(), pathOutput)
        {

        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        public Prioritizer(Extractor extractor, string pathOutput)
        {
            this.extractor = extractor;
            this.pathOutput = pathOutput;
        }

        /// <summary>
        /// Read-only property.
        /// </summary>
        public Extractor Extractor
        {
            get { return extractor; }
        }

        /// <summary>
        /// Initializes the extractor.
        /// </summary>
        private void InitializeExtractor()
        {
            extractor.CreateHelpers();
            extractor.StartDICOMServer(pathOutput);
            //extractor.PrepareReceiver(pathOutput);
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartExtraction(DataTable dtPatients, string pathOutput, MainForm mainForm)
        {
            InitializeExtractor();

            // Get unique patient IDs 
            var uniquePatientIDs = dtPatients.AsEnumerable()
                                        .Select(row => row.Field<string>("Patient ID"))
                                        .Distinct();

            // Initiate/restart counter
            int counter = 0;

            foreach (var patient in uniquePatientIDs)
            {
                // A list that stores UIDs to extract for this patient
                List<string> UIDs = new List<string>();

                // All the rows for the current patient
                var patientRows = dtPatients.AsEnumerable()
                                       .Where(row => row.Field<string>("Patient ID") == patient);

                // Save pseudo ID to use for anonymization
                string pseudoID = null;

                // Look at all patient rows to identify UIDs to extract
                foreach (DataRow planRow in patientRows)
                {
                    // Information from the data row
                    string ID = planRow["Patient ID"].ToString();
                    pseudoID = planRow["Pseudo ID"].ToString();
                    string courseID = planRow["Course ID"].ToString();
                    string planID = planRow["Plan ID"].ToString();

                    // Add identified UIDs to the list
                    UIDs.AddRange(extractor.IdentifyLinkedObjects(ID, courseID, planID));
                }

                // Keep only distinct UIDs
                UIDs = UIDs.Distinct().ToList();

                // Extract all requested UIDs for this patient. The files will be writted directly in the output folder.
                extractor.Extract(UIDs, mainForm);

                // Anonymize all written files and structure them.
                extractor.Anonymize(pathOutput, pseudoID, mainForm);

                // Update counter
                counter++;
                mainForm.UpdateRequestsBar(counter);
            }
        }

        /// <summary>
        /// Counts the number of DICOM files having the modality CT, MR, PT, US, RTSTRUCT, RTPLAN or RTDOSE.
        /// </summary>
        public int CountValidDicomFiles(string[] dcmFiles)
        {
            int counter = 0;
            foreach (string modality in modalities)
            {
                for (int fileIndex = 0; fileIndex < dcmFiles.Length; fileIndex++)
                {
                    DICOMObject dicomObject = DICOMObject.Read(dcmFiles[fileIndex]);
                    if (modality.Equals((string)dicomObject.FindFirst(TagHelper.Modality).DData))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}
