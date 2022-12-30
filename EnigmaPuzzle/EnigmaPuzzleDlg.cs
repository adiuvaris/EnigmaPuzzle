using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.IO;


namespace EnigmaPuzzle
{
    /// <summary>
    /// Form class for the EnigmaPuzzle
    /// </summary>
    public partial class EnigmaPuzzleDlg : Form
    {
        /// <summary>
        /// Flag if dragging is active
        /// </summary>
        private bool m_bDrag = false;

        /// <summary>
        /// Staring coordinates for dragging
        /// </summary>
        private float m_dragx;
        private float m_dragy;

        /// <summary>
        /// The board
        /// </summary>
        private Board m_b = null;

        private Size m_s;
        private float m_aspect = 1.0f;


        /// <summary>
        /// Checks if a game is running and asks the user if it
        /// is ok to stop the game
        /// </summary>
        /// <returns>true if stopping is ok</returns>
        private bool StopRunningGame()
        {
            bool bRet = true;
            if (m_b.GameActive)
            {
                m_b.Pause = true;
                MessageDlg dlg = new MessageDlg(Properties.Resources.SureToCancelGame, Properties.Resources.Question, MessageBoxButtons.YesNo);
                bRet = dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK;
                m_b.Pause = false;
            }

            return bRet;
        }


        /// <summary>
        /// Show the moves and the elapsed time
        /// </summary>
        /// <param name="bStopGame">true if the timer should be disabled</param>
        public void ShowGameStatus(bool bStopGame = false)
        {
            if (m_b == null)
            {
                return;
            }

            // Do we have to stop the game
            if (bStopGame)
            {
                timGame.Enabled = false;
                m_b.GameActive = false;
                btnSave.Enabled = false;
            }

            // Show the date
            if (m_b.GameActive)
            {
                btnSave.Enabled = true;
                lblMoves.Text = m_b.Moves.ToString();
                DateTime d = DateTime.Now;
                TimeSpan di = (d - m_b.StartTime);
                lblTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", di.Hours, di.Minutes, di.Seconds);

                timGame.Enabled = true;
            }
            else
            {
                lblMoves.Text = "0";
                lblTime.Text = "00:00:00";
            }
        }

       

        /// <summary>
        /// Constructor
        /// </summary>
        public EnigmaPuzzleDlg()
        {
            InitializeComponent();

            // Read the settings 
            Settings.Read();
            m_b = new Board();

//            m_aspect = (1.0F * ClientSize.Width) / (1.0F * ClientSize.Height);
//            m_s = ClientSize;

            // Create the original picture for the current level
            // to show it as a reference
            picOriginal.Image = m_b.GetBoard();
            ShowGameStatus(true);

            // Check if a game is running and reconstruct the state
            m_b.GameMoves = Settings.GetInt(Settings.C_Key_GameActive);
            if (m_b.GameMoves > 0)
            {
                m_b.GameActive = false;
                m_b.SetBoard(Settings.GetValue(Settings.C_Key_Rotations), this);
                m_b.GameActive = true;
                long ticks = Settings.GetLong(Settings.C_Key_Time);
                m_b.StartTime = DateTime.Now.AddTicks(-ticks);
                timGame.Enabled = true;
                ShowGameStatus();
                Refresh();
            }
        }


        /// <summary>
        /// Paint all the board parts
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (e.ClipRectangle.Width == 0 || m_b == null)
            {
                return;
            }

            base.OnPaint(e);

            // Optimize to repaint only the necessary disk
            e.Graphics.DrawImageUnscaled(m_b.Background, 0, 0);
            if (m_b.RotDisk == Board.eDisc.eUpperDisc)
            {
                e.Graphics.DrawImageUnscaled(m_b.LowerDisk, 0, 0);
                e.Graphics.DrawImageUnscaled(m_b.UpperDisk, 0, 0);
            }
            else
            {
                e.Graphics.DrawImageUnscaled(m_b.UpperDisk, 0, 0);
                e.Graphics.DrawImageUnscaled(m_b.LowerDisk, 0, 0);
            }
        }


        /// <summary>
        /// Handle mouse down - init possible dragging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnigmaPuzzleDlg_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_b.MouseDown(e.Location, this))
            {
                return;
            }

            if (m_b.Level < 0)
            {
                return;
            }
            m_dragx = e.X;
            m_dragy = e.Y;
            m_bDrag = true;
        }


        /// <summary>
        /// Handle mouse up - check if we have to turn a disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnigmaPuzzleDlg_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_bDrag)
            {
                // Get the current mouse location
                float x2 = e.X;
                float y2 = e.Y;

                // Check if we have to turn the disk
                if (m_b.TurnDisk(m_dragx, m_dragy, x2, y2, this))
                {
                    // Disk has been turned - so refresh the board
                    Refresh();
                }

                // Stop dragging
                m_bDrag = false;
            }
        }


        /// <summary>
        /// Handle resizesing the form at the startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnigmaPuzzleDlg_Resize(object sender, EventArgs e)
        {
            if (m_b != null)
            {
                m_b.OnResize(ClientSize.Width, ClientSize.Height);
                Refresh();
            }
        }

        /// <summary>
        /// Save the current aspect and size of the client area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnigmaPuzzleDlg_ResizeBegin(object sender, EventArgs e)
        {
            m_s = ClientSize;
//            m_aspect = (1.0F * ClientSize.Width) / (1.0F * ClientSize.Height);
        }


        /// <summary>
        /// Adjust the aspect if necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnigmaPuzzleDlg_ResizeEnd(object sender, EventArgs e)
        {
            if (Math.Abs (ClientSize.Width - m_s.Width) > 1)
            {
                ClientSize = new Size(ClientSize.Width, (int)(ClientSize.Width / m_aspect));
            }

            if (Math.Abs (ClientSize.Height - m_s.Height) > 1)
            {
                ClientSize = new Size((int)(ClientSize.Height * m_aspect), ClientSize.Height);
            }
        }
        

        /// <summary>
        /// Close the app and save the current game if one is running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (m_b.GameActive)
            {
                Settings.SetLong(Settings.C_Key_Time, (DateTime.Now - m_b.StartTime).Ticks);
                Settings.SetInt(Settings.C_Key_GameActive, m_b.GameMoves);
                Settings.SetValue(Settings.C_Key_Rotations, m_b.GetMoves());
            }
            else
            {
                Settings.SetLong(Settings.C_Key_Time, 0);
                Settings.SetInt(Settings.C_Key_GameActive, 0);
                Settings.SetValue(Settings.C_Key_Rotations, "");
            }
            Settings.Write();
            Close();
        }


        /// <summary>
        /// Start a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // Check first if another game is active
            if (!StopRunningGame())
            {
                return;
            }
            ShowGameStatus(true);

            // Start a game an shuffle the disks
            m_b.NewGame(this);

            // Refresh screen
            Refresh();
            timGame.Enabled = true;
            ShowGameStatus();
        }


        /// <summary>
        /// Save a game to disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_b.GameActive)
            {
                m_b.Pause = true;

                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    string fName = saveDlg.FileName;
                    m_b.SaveBoard(fName, saveDlg.FilterIndex);
                }

                m_b.Pause = false;
            }
        }


        /// <summary>
        /// Load a saved game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Check first if a game is active
            if (!StopRunningGame())
            {
                return;
            }

            // Read a game from the disk
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                string fName = openDlg.FileName;
                m_b.LoadBoard(fName, this);
                Refresh();
            }
        }


        /// <summary>
        /// Print the board to standard printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageDlg dlg = new MessageDlg(Properties.Resources.SureToPrint, Properties.Resources.Question, MessageBoxButtons.YesNo);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += (s, ev) =>
                {
                    m_b.OnResize((int)(0.85 * ev.PageBounds.Width), (int)(0.85 * ev.PageBounds.Height));
                    m_b.CreateBackground(true);
                    ev.Graphics.DrawImage(m_b.Background, Point.Empty);
                    ev.Graphics.DrawImage(m_b.UpperDisk, Point.Empty);
                    ev.Graphics.DrawImage(m_b.LowerDisk, Point.Empty);
                    ev.HasMorePages = false;
                };
                doc.DefaultPageSettings.Landscape = false;
                doc.Print();

                m_b.OnResize(ClientSize.Width, ClientSize.Height);
                Refresh();
            }
        }


        /// <summary>
        /// Start the config dialog - a running game will be paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetup_Click(object sender, EventArgs e)
        {
            m_b.Pause = true;
            m_b.Setup(this);
            m_b.Pause = false;

            Refresh();
        }


        /// <summary>
        /// Level up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLevelUp_Click(object sender, EventArgs e)
        {
            // Check first if a game is active
            if (!StopRunningGame())
            {
                return;
            }

            // Set the new level and create the ref-picture
            m_b.LevelUp(this);
            picOriginal.Image = m_b.GetBoard();

            ShowGameStatus(true);
            ShowLevel();
            Refresh();
        }


        /// <summary>
        /// Level down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLevelDown_Click(object sender, EventArgs e)
        {
            // Check first if a game is active
            if (!StopRunningGame())
            {
                return;
            }

            // Set the new level and create the ref-picture
            m_b.LevelDown(this);
            picOriginal.Image = m_b.GetBoard();

            ShowGameStatus(true);
            ShowLevel();
            Refresh();
        }


        /// <summary>
        /// Show help document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            m_b.Pause = true;

            string language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string loc1 = Application.StartupPath + "\\" + language + Settings.C_Help_File;
            string loc2 = Application.StartupPath + "\\..\\" + language + Settings.C_Help_File;
            string loc3 = Application.StartupPath + "\\..\\..\\" + language + Settings.C_Help_File;

            if (File.Exists(loc1))
            {
                System.Diagnostics.Process.Start(loc1);
            }
            else if (File.Exists(loc2))
            {
                System.Diagnostics.Process.Start(loc2);
            }
            else if (File.Exists(loc3))
            {
                System.Diagnostics.Process.Start(loc3);
            }
            m_b.Pause = false;
        }


        /// <summary>
        /// Show about dialog - a running game will be paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            m_b.Pause = true;
            AboutDlg dlg = new AboutDlg();
            dlg.ShowDialog();
            m_b.Pause = false;
        }


        /// <summary>
        /// Handle load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnigmaGameDlg_Load(object sender, EventArgs e)
        {
            // Check the window size (fullscreen or not)
            if (Settings.GetBool(Settings.C_Key_FullScreen))
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;

                // Move window to the center of the screen
                int boundWidth = Screen.PrimaryScreen.Bounds.Width;
                int boundHeight = Screen.PrimaryScreen.Bounds.Height;
                int x = boundWidth - this.Width;
                int y = boundHeight - this.Height;
                Location = new Point(x / 2, y / 2);

                m_s = ClientSize;
                m_aspect = (1.0F * ClientSize.Width) / (1.0F * ClientSize.Height);

            }

            ShowLevel();
            EnigmaPuzzleDlg_Resize(null, null);
        }


        /// <summary>
        /// Show the Level 
        /// </summary>
        public void ShowLevel()
        {
            SetupDlg dlg = new SetupDlg();
            tbLevel.Text = dlg.cbxLevel.Items[m_b.Level].ToString();
        }


        /// <summary>
        /// Reset the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Check first if a game is active
            if (!StopRunningGame())
            {
                return;
            }

            // Reset the game 
            ShowGameStatus(true);
            m_b.Reset(this);
            Refresh();
        }


        /// <summary>
        /// Timer tick routine - just show the game status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timGame_Tick(object sender, EventArgs e)
        {
            if (m_b.Pause)
            {
                // Correct the start game by one second
                m_b.StartTime = m_b.StartTime.AddSeconds(1.0);
            }
            else
            {
                ShowGameStatus();
            }
        }


        /// <summary>
        /// Make Text on disabled Button visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_EnabledChanged(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                btnSave.BackColor = Color.Black;
            }
            else
            {
                btnSave.BackColor = Color.FromArgb(64, 64, 64);
            }
        }
    }
    
    

}
