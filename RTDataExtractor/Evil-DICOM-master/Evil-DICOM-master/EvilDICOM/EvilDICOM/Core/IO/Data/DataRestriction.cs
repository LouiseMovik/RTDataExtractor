﻿#region

using System.Net;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Logging;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Core.IO.Data
{
    public class DataRestriction
    {
        static ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<DataRestriction>();
        public static string EnforceLengthRestriction(uint lengthLimit, string data)
        {
            if (data.Length > lengthLimit)
            {
                _logger.LogInformation(
                    "Not DICOM compliant. Attempted data input of {0} characters. Data size is limited to {1} characters. Read anyway.",
                    data.Length, lengthLimit);
                return data;
            }
            return data;
        }

        public static byte[] EnforceEvenLength(byte[] data, VR vr)
        {
            switch (vr)
            {
                case VR.UniqueIdentifier:
                case VR.OtherByteString:
                case VR.Unknown:
                    return DataPadder.PadNull(data);
                case VR.AgeString:
                case VR.ApplicationEntity:
                case VR.CodeString:
                case VR.Date:
                case VR.DateTime:
                case VR.DecimalString:
                case VR.IntegerString:
                case VR.LongString:
                case VR.LongText:
                case VR.PersonName:
                case VR.ShortString:
                case VR.ShortText:
                case VR.Time:
                case VR.UnlimitedText:
                case VR.UnlimitedCharacter:
                case VR.UniversalResourceId:
                    return DataPadder.PadSpace(data);
                default:
                    return data;
            }
        }

        public static string EnforceUrlEncoding(string originalValue)
        {
            var encoded = WebUtility.UrlEncode(originalValue.TrimEnd(' '));
            if (encoded != originalValue.TrimEnd(' '))
                _logger.LogInformation(
                    "Not URI compliant data string Original = {0}, URI Encoded = {1}",
                    originalValue, encoded);
            return encoded;
        }

        public static bool EnforceRealNonZero(double value, string propertyName)
        {
            if (value == 0 || double.IsNaN(value))
            {
                var msg = string.Format("{0} must be real and non-zero. Current value is {1}", propertyName, value);
                _logger.LogInformation(msg);
                return false;
            }
            return true;
        }

        public static bool EnforceRealNonZero(int value, string propertyName)
        {
            if (value == 0)
            {
                var msg = string.Format("{0} must be real and non-zero. Current value is {1}", propertyName, value);
                _logger.LogInformation(msg);
                return false;
            }
            return true;
        }
    }
}