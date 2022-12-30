namespace EnigmaPuzzle
{
    partial class EnigmaPuzzleDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnigmaPuzzleDlg));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnLevelUp = new System.Windows.Forms.Button();
            this.btnLevelDown = new System.Windows.Forms.Button();
            this.saveDlg = new System.Windows.Forms.SaveFileDialog();
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMoves = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.timGame = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnClose.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnSave.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.EnabledChanged += new System.EventHandler(this.btnSave_EnabledChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnPrint.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnNew.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSetup
            // 
            resources.ApplyResources(this.btnSetup, "btnSetup");
            this.btnSetup.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnSetup.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.UseVisualStyleBackColor = false;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnLevelUp
            // 
            resources.ApplyResources(this.btnLevelUp, "btnLevelUp");
            this.btnLevelUp.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnLevelUp.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnLevelUp.Name = "btnLevelUp";
            this.btnLevelUp.UseVisualStyleBackColor = false;
            this.btnLevelUp.Click += new System.EventHandler(this.btnLevelUp_Click);
            // 
            // btnLevelDown
            // 
            resources.ApplyResources(this.btnLevelDown, "btnLevelDown");
            this.btnLevelDown.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnLevelDown.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnLevelDown.Name = "btnLevelDown";
            this.btnLevelDown.UseVisualStyleBackColor = false;
            this.btnLevelDown.Click += new System.EventHandler(this.btnLevelDown_Click);
            // 
            // saveDlg
            // 
            resources.ApplyResources(this.saveDlg, "saveDlg");
            // 
            // openDlg
            // 
            resources.ApplyResources(this.openDlg, "openDlg");
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnHelp.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnAbout
            // 
            resources.ApplyResources(this.btnAbout, "btnAbout");
            this.btnAbout.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnAbout.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // tbLevel
            // 
            resources.ApplyResources(this.tbLevel, "tbLevel");
            this.tbLevel.BackColor = System.Drawing.Color.Black;
            this.tbLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLevel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbLevel.ForeColor = System.Drawing.Color.White;
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.ReadOnly = true;
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnReset.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // picOriginal
            // 
            resources.ApplyResources(this.picOriginal, "picOriginal");
            this.picOriginal.BackColor = System.Drawing.Color.Black;
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // lblMoves
            // 
            resources.ApplyResources(this.lblMoves, "lblMoves");
            this.lblMoves.BackColor = System.Drawing.Color.Black;
            this.lblMoves.ForeColor = System.Drawing.Color.White;
            this.lblMoves.Name = "lblMoves";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.BackColor = System.Drawing.Color.Black;
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Name = "lblTime";
            // 
            // timGame
            // 
            this.timGame.Interval = 1000;
            this.timGame.Tick += new System.EventHandler(this.timGame_Tick);
            // 
            // EnigmaPuzzleDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblMoves);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picOriginal);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tbLevel);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnLevelDown);
            this.Controls.Add(this.btnLevelUp);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.btnClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnigmaPuzzleDlg";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EnigmaGameDlg_Load);
            this.ResizeBegin += new System.EventHandler(this.EnigmaPuzzleDlg_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.EnigmaPuzzleDlg_ResizeEnd);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnigmaPuzzleDlg_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EnigmaPuzzleDlg_MouseUp);
            this.Resize += new System.EventHandler(this.EnigmaPuzzleDlg_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnLevelUp;
        private System.Windows.Forms.Button btnLevelDown;
        private System.Windows.Forms.SaveFileDialog saveDlg;
        private System.Windows.Forms.OpenFileDialog openDlg;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.PictureBox picOriginal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timGame;





    }
}

