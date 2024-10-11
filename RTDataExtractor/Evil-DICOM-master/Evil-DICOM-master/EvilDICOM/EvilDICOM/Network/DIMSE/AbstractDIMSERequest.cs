﻿#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public abstract class AbstractDIMSERequest : AbstractDIMSE
    {
        protected UnsignedShort _messageId = new UnsignedShort {Tag = TagHelper.MessageID};

        public AbstractDIMSERequest()
        {
            DataSetType = 257; //No data
        }

        public AbstractDIMSERequest(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            MessageID = sel.MessageID.Data;
            DataSetType = sel.CommandDataSetType.Data;
        }

        public ushort MessageID
        {
            get { return _messageId.Data; }
            set { _messageId.Data = value; }
        }

        public override List<IDICOMElement> Elements
        {
            get { return new List<IDICOMElement>(); }
        }
    }
}