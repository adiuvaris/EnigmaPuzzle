using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnigmaPuzzle
{
    /// <summary>
    /// Simple class to show a message
    /// </summary>
    public partial class MessageDlg : Form
    {
        /// <summary>
        /// Message string
        /// </summary>
        string m_msg;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="title">Title sting</param>
        /// <param name="b">buttons to show</param>
        public MessageDlg(string msg, string title = "", MessageBoxButtons b = MessageBoxButtons.OK)
        {
            InitializeComponent();
            Text = title;

            m_msg = msg;

            if (b == MessageBoxButtons.OK)
            {
                btnNo.Visible = false;
            }
            else
            {
                okButton.Text = Properties.Resources.Yes;
            }

            if (title.Length == 0)
            {
                this.Text = Properties.Resources.Message;
            }
        }

        /// <summary>
        /// Handle the load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageDlg_Load(object sender, EventArgs e)
        {
            tbMsg.Text = m_msg;
        }

        /// <summary>
        /// Handle the click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMsg_Enter(object sender, EventArgs e)
        {
            okButton.Focus();
        }

    }
}
