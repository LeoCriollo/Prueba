using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Tools
{
    public struct SentMailInfo
    {
        #region Fields
        bool success;
        string message;
        #endregion

        #region Properties
        /// <summary>
        /// true if mail was sent successful, false otherwise
        /// </summary>
        public bool Success
        {
            get { return success; }
        }
        /// <summary>
        /// descriptive message of sent mail activity
        /// </summary>
        public string Message
        {
            get { return message; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// SentMailInfo constructor
        /// </summary>
        /// <param name="success">bool success</param>
        /// <param name="message">string message</param>
        public SentMailInfo(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
        #endregion
    }
}