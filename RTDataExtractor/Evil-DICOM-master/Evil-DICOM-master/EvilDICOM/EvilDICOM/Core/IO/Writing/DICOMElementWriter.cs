﻿#region

using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMElementWriter
    {
        public static void WriteLittleEndian(DICOMBinaryWriter dw, DICOMIOSettings settings, IDICOMElement toWrite)
        {
            var vr = VRDictionary.GetVRFromType(toWrite);
            if (vr == VR.Sequence)
            {
                SequenceWriter.WriteLittleEndian(dw, settings, toWrite);
            }
            else
            {
                DICOMTagWriter.WriteLittleEndian(dw, toWrite.Tag);
                VRWriter.WriteVR(dw, settings, vr);
                DataWriter.WriteLittleEndian(dw, vr, settings, toWrite);
            }
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, DICOMIOSettings settings, IDICOMElement toWrite)
        {
            DICOMTagWriter.WriteBigEndian(dw, toWrite.Tag);
            var vr = VRDictionary.GetVRFromType(toWrite);
            VRWriter.WriteVR(dw, settings, vr);
            DataWriter.WriteBigEndian(dw, vr, settings, toWrite);
        }

        public static void Write(DICOMBinaryWriter dw, DICOMIOSettings settings, IDICOMElement el)
        {
            if (settings.TransferSyntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                WriteBigEndian(dw, settings, el);
            else
                WriteLittleEndian(dw, settings, el);
        }
    }
}