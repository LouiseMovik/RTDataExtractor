﻿#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Logging;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Replaces standard indentification profile with empty data
    /// </summary>
    public class ProfileAnonymizer : IAnonymizer
    {
        ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<ProfileAnonymizer>();
        public void Anonymize(DICOMObject d)
        {
            _logger.LogInformation("Clearing DICOM profile...");
            foreach (var el in GenerateProfile())
                d.Replace(el);
        }

        /// <summary>
        /// Returns a collection of identifiable elements
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<IDICOMElement> GenerateProfile()
        {
            var profile = new List<IDICOMElement>();

            //PATIENT SEX
            profile.Add(GenerateEmptyDICOMString(TagHelper.Patient​Sex));

            //OTHER PATIENT IDS
            profile.Add(GenerateEmptyDICOMString(TagHelper.Other​Patient​IDs));

            //OTHER PATIENT NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Other​Patient​Names));

            //ETHNIC GROUP
            profile.Add(GenerateEmptyDICOMString(TagHelper.Ethnic​Group));

            //PATIENT COMMENTS
            profile.Add(GenerateEmptyDICOMString(TagHelper.Patient​Comments));

            //REFERRING PHYSICIAN NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Referring​Physician​Name));

            //ACCESSION NUMBER
            profile.Add(GenerateEmptyDICOMString(TagHelper.Accession​Number));

            //PHYSICIANS RECORD
            profile.Add(GenerateEmptyDICOMString(TagHelper.Physicians​Of​Record));

            //PHYSICIANS READING STUDY
            profile.Add(GenerateEmptyDICOMString(TagHelper.Name​Of​Physicians​Reading​Study));

            //ADMITTING DIAGNOSIS DESCRIPTION
            profile.Add(GenerateEmptyDICOMString(TagHelper.Admitting​Diagnoses​Description));

            //PATIENTS SIZE
            profile.Add(GenerateZeroDecimalString(TagHelper.Patient​Size));

            //PATIENTS WEIGHT
            profile.Add(GenerateZeroDecimalString(TagHelper.Patient​Weight));

            //OCCUPATION
            profile.Add(GenerateEmptyDICOMString(TagHelper.Occupation));

            //ADDITIONAL PATIENT HISTORY
            profile.Add(GenerateEmptyDICOMString(TagHelper.Additional​Patient​History));

            //PERFORMING PHYSICIAN NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Performing​Physician​Name));

            //PROTOCOL NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Protocol​Name));

            //SERIES DESCRIPTION
            profile.Add(GenerateEmptyDICOMString(TagHelper.Series​Description));

            //OPERATORS NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Operators​Name));

            //INSTITUITION NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Institution​Name));

            //INSTITUTION ADDRESS
            profile.Add(GenerateEmptyDICOMString(TagHelper.Institution​Address));

            //STATION NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Station​Name));

            //INSTITUTION DEPARTMENT NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.Institutional​Department​Name));

            //DEVICE SERIAL NUMBER
            profile.Add(GenerateEmptyDICOMString(TagHelper.Device​Serial​Number));

            //DERIVATION DESCRIPTION
            profile.Add(GenerateEmptyDICOMString(TagHelper.Derivation​Description));

            //IMAGE COMMENTS
            profile.Add(GenerateEmptyDICOMString(TagHelper.Image​Comments));

            return profile;
        }

        private IDICOMElement GenerateZeroDecimalString(Tag tag)
        {
            var ds = new DecimalString(tag, new[] {0.0});
            return ds;
        }

        private IDICOMElement GenerateEmptyDICOMString(Tag tag)
        {
            var s = new LongString();
            s.Tag = tag;
            s.Data = string.Empty;
            return s;
        }
    }
}