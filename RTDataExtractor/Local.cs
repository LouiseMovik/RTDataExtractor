using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTDataExtractor
{
    public class Local
    {
        private string m_aeTitle;
        private int m_port;
        private string m_strErrorMessage;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Local() : this(string.Empty, 0)
        {
        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        public Local(string aeTitle, int port)
        {
            this.m_aeTitle = aeTitle;
            this.m_port = port;
        }

        /// <summary>
        /// Read and write property connected to m_aeTitle.
        /// </summary>
        public string AETitle
        {
            get { return m_aeTitle; }
            set { if (Valid(m_aeTitle)) m_aeTitle = value; }
        }

        /// <summary>
        /// Read and write property connected to m_aeTitle.
        /// </summary>
        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        /// <summary>
        /// Read-only property connected to m_strErrorMessage.
        /// </summary>
        public string ErrorMessage
        {
            get { return m_strErrorMessage; }
        }

        /// <summary>
        /// Method for formatting the string to be displayed in the entities list.
        /// </summary>
        public string FormatString(int index)
        {
            return string.Format("{0, -33}  {1,-17}", m_aeTitle, m_port);
        }

        /// <summary>
        /// Method that returns true if an AE Title has been entered.
        /// </summary>
        public bool CheckAETitle(string aeTitle)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(aeTitle))
            {
                m_strErrorMessage = "AE Title is mandatory.";
                return false;
            }
            else if (aeTitle.Any(x => Char.IsWhiteSpace(x)))
            {
                m_strErrorMessage = "The AE Title can not cantain spaces.";
                return false;
            }
            return isValid;
        }

        /// <summary>
        /// Method that returns true if an port that can be parsed to an integer has been entered.
        /// </summary>
        public bool CheckPort(string port)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(port))
            {
                m_strErrorMessage = "Port is mandatory.";
                return false;
            }
            else if (!int.TryParse(port, out int parsedInt))
            {
                m_strErrorMessage = "Port must be entered as an integer.";
                return false;
            }
            return isValid;
        }

        /// <summary>
        /// Method for confirming that the entered value is valid, i.e. not null or empty. 
        /// </summary>
        private bool Valid(string inputString)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(inputString))
                return true;
            return isValid;
        }
    }
}
