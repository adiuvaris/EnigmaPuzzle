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
    /// Class for the settings dialog
    /// </summary>
    public partial class SetupDlg : Form
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public SetupDlg()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load the current settings into the controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupDlg_Load(object sender, EventArgs e)
        {
            nudRotationDelay.Value = Settings.GetInt(Settings.C_Key_RotationDelay);
            nudRotationSteps.Value = Settings.GetInt(Settings.C_Key_RotationSteps);

            nudSwingSteps.Value = Settings.GetInt(Settings.C_Key_SwingSteps);
            chkSwing.Checked = Settings.GetBool(Settings.C_Key_Swing);

            nudNumTurns.Value = Settings.GetInt(Settings.C_Key_NumTurns);
            chkShowTurns.Checked = Settings.GetBool(Settings.C_Key_ShowTurns);
            chkFullScreen.Checked = Settings.GetBool(Settings.C_Key_FullScreen);
        }


        /// <summary>
        /// Save the current setting to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.SetInt(Settings.C_Key_RotationDelay, (int)nudRotationDelay.Value);
            Settings.SetInt(Settings.C_Key_RotationSteps, (int)nudRotationSteps.Value);

            Settings.SetInt(Settings.C_Key_SwingSteps, (int)nudSwingSteps.Value);
            Settings.SetBool(Settings.C_Key_Swing, chkSwing.Checked);

            Settings.SetInt(Settings.C_Key_NumTurns, (int)nudNumTurns.Value);
            Settings.SetBool(Settings.C_Key_ShowTurns, chkShowTurns.Checked);
            Settings.SetBool(Settings.C_Key_FullScreen, chkFullScreen.Checked);

            Settings.Write();
        }


        /// <summary>
        /// Reset to the default settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefaults_Click(object sender, EventArgs e)
        {
            Settings.FillDefault();
            SetupDlg_Load(null, null);
        }

    }

}
