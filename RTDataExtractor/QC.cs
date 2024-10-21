using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using EvilDICOM.Network;
using System.Dynamic;
using System.Windows.Forms;
using System.ComponentModel;
using OfficeOpenXml;

namespace RTDataExtractor
{
    internal class QC
    {
        private DataTable DICOMFiles;
        string[] listExtractor;
        string[] listOfPseudoIDs;

        /// <summary>
        /// Overloaded constructor. 
        /// </summary>
        public QC(string inputPath, string outputPath)
        {
            // Read ínput
            listExtractor = File.ReadAllLines(inputPath);
            listOfPseudoIDs = listExtractor.Select(r => r.Split('\t').Skip(1).First()).Distinct().ToArray();

            // Create DataTable to store the results in
            CreateDataTable();
           
            // Perform patient-wise controls
            bool allPassed = PerformPatientWiseControls(outputPath);
            
            // Save the output
            SaveResultTable(outputPath + @"\QC_Results.xlsx");

            // Inform the user of the results
            if (allPassed)
            {
                MessageBox.Show("Successful", "All patients passed QC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed", "At least one patient failed QC, see QC_Results in the output folder for details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool PerformPatientWiseControls(string outputPath)
        {
            bool allOK = true;
            foreach (string pseudoID in listOfPseudoIDs)
            {
                int numberOfLines = listExtractor.Where(r => r.Split('\t').Skip(1).First().Equals(pseudoID)).ToArray().Length;
                int numberOfRPFiles = Directory.GetFiles(outputPath + @"\" + pseudoID, "*RP*.dcm").Length;
                int numberOfCTFiles = Directory.GetFiles(outputPath + @"\" + pseudoID, "*CT*.dcm").Length;
                int numberOfRDFiles = Directory.GetFiles(outputPath + @"\" + pseudoID, "*RD*.dcm").Length;
                int numberOfRSFiles = Directory.GetFiles(outputPath + @"\" + pseudoID, "*RS*.dcm").Length;
                
                bool rightNumberOfFiles = numberOfLines == numberOfRPFiles && numberOfLines == numberOfRDFiles && numberOfCTFiles >= 1 && numberOfRSFiles >= 1 ? true : false;
                allOK &= rightNumberOfFiles;

                DataRow row = DICOMFiles.NewRow();
                row["Pseudo ID"] = pseudoID;
                row["# Rows In Indata"] = numberOfLines;
                row["Correct (Same # RP and RD as # Rows)"] = rightNumberOfFiles;
                row["# RP Files"] = numberOfRPFiles;
                row["# RD Files"] = numberOfRDFiles;
                row["# RS Files"] = numberOfRSFiles;
                row["# CT Files"] = numberOfCTFiles;
                DICOMFiles.Rows.Add(row);
            }
            return allOK;
        }

        private void CreateDataTable()
        {
            DICOMFiles = new DataTable();
            DICOMFiles.TableName = "DICOMFiles";

            DataColumn column = new DataColumn("Pseudo ID");
            column.DataType = Type.GetType("System.String");
            DICOMFiles.Columns.Add(column);

            column = new DataColumn("# Rows In Indata");
            column.DataType = Type.GetType("System.Int32");
            DICOMFiles.Columns.Add(column);

            column = new DataColumn("Correct (Same # RP and RD as # Rows)");
            column.DataType = Type.GetType("System.Boolean");
            DICOMFiles.Columns.Add(column);

            column = new DataColumn("# RP Files");
            column.DataType = Type.GetType("System.Int32");
            DICOMFiles.Columns.Add(column);

            column = new DataColumn("# RD Files");
            column.DataType = Type.GetType("System.Int32");
            DICOMFiles.Columns.Add(column);

            column = new DataColumn("# RS Files");
            column.DataType = Type.GetType("System.Int32");
            DICOMFiles.Columns.Add(column);

            column = new DataColumn("# CT Files");
            column.DataType = Type.GetType("System.Int32");
            DICOMFiles.Columns.Add(column);
        }


        /// <summary>
        /// Writes the resulting DataTable to the path used as argument.
        /// </summary>
        private void SaveResultTable(string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Create an Excel package
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(DICOMFiles, true);

                // Save to a file
                FileInfo excelFile = new FileInfo(path);
                pck.SaveAs(excelFile);
            }
        }
    }
}
