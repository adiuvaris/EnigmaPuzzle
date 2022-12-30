namespace EnigmaPuzzle
{
    partial class SetupDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDlg));
            this.cbxLevel = new System.Windows.Forms.ComboBox();
            this.nudRotationDelay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudRotationSteps = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudNumTurns = new System.Windows.Forms.NumericUpDown();
            this.chkShowTurns = new System.Windows.Forms.CheckBox();
            this.chkSwing = new System.Windows.Forms.CheckBox();
            this.colDlg = new System.Windows.Forms.ColorDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSwingSteps = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefaults = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkFullScreen = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTurns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwingSteps)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxLevel
            // 
            this.cbxLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxLevel.BackColor = System.Drawing.Color.Black;
            this.cbxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLevel.ForeColor = System.Drawing.Color.White;
            this.cbxLevel.FormattingEnabled = true;
            this.cbxLevel.Items.AddRange(new object[] {
            resources.GetString("cbxLevel.Items"),
            resources.GetString("cbxLevel.Items1"),
            resources.GetString("cbxLevel.Items2"),
            resources.GetString("cbxLevel.Items3"),
            resources.GetString("cbxLevel.Items4"),
            resources.GetString("cbxLevel.Items5"),
            resources.GetString("cbxLevel.Items6"),
            resources.GetString("cbxLevel.Items7"),
            resources.GetString("cbxLevel.Items8"),
            resources.GetString("cbxLevel.Items9"),
            resources.GetString("cbxLevel.Items10")});
            resources.ApplyResources(this.cbxLevel, "cbxLevel");
            this.cbxLevel.Name = "cbxLevel";
            // 
            // nudRotationDelay
            // 
            this.nudRotationDelay.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.nudRotationDelay, "nudRotationDelay");
            this.nudRotationDelay.Name = "nudRotationDelay";
            this.nudRotationDelay.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // nudRotationSteps
            // 
            this.nudRotationSteps.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.nudRotationSteps, "nudRotationSteps");
            this.nudRotationSteps.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudRotationSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRotationSteps.Name = "nudRotationSteps";
            this.nudRotationSteps.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // nudNumTurns
            // 
            this.nudNumTurns.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.nudNumTurns, "nudNumTurns");
            this.nudNumTurns.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNumTurns.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumTurns.Name = "nudNumTurns";
            this.nudNumTurns.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // chkShowTurns
            // 
            resources.ApplyResources(this.chkShowTurns, "chkShowTurns");
            this.chkShowTurns.Checked = true;
            this.chkShowTurns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTurns.ForeColor = System.Drawing.Color.White;
            this.chkShowTurns.Name = "chkShowTurns";
            this.chkShowTurns.UseVisualStyleBackColor = true;
            // 
            // chkSwing
            // 
            resources.ApplyResources(this.chkSwing, "chkSwing");
            this.chkSwing.Checked = true;
            this.chkSwing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSwing.ForeColor = System.Drawing.Color.White;
            this.chkSwing.Name = "chkSwing";
            this.chkSwing.UseVisualStyleBackColor = true;
            // 
            // colDlg
            // 
            this.colDlg.AnyColor = true;
            this.colDlg.FullOpen = true;
            this.colDlg.ShowHelp = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Name = "label5";
            // 
            // nudSwingSteps
            // 
            this.nudSwingSteps.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.nudSwingSteps, "nudSwingSteps");
            this.nudSwingSteps.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSwingSteps.Name = "nudSwingSteps";
            this.nudSwingSteps.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Black;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDefaults
            // 
            this.btnDefaults.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnDefaults, "btnDefaults");
            this.btnDefaults.ForeColor = System.Drawing.Color.White;
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.UseVisualStyleBackColor = false;
            this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.chkFullScreen);
            this.panel1.Controls.Add(this.btnDefaults);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.nudSwingSteps);
            this.panel1.Controls.Add(this.chkSwing);
            this.panel1.Controls.Add(this.chkShowTurns);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nudNumTurns);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nudRotationSteps);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.nudRotationDelay);
            this.panel1.Controls.Add(this.cbxLevel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // chkFullScreen
            // 
            resources.ApplyResources(this.chkFullScreen, "chkFullScreen");
            this.chkFullScreen.Checked = true;
            this.chkFullScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFullScreen.ForeColor = System.Drawing.Color.White;
            this.chkFullScreen.Name = "chkFullScreen";
            this.chkFullScreen.UseVisualStyleBackColor = true;
            // 
            // SetupDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SetupDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTurns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwingSteps)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudRotationDelay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRotationSteps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudNumTurns;
        private System.Windows.Forms.CheckBox chkShowTurns;
        private System.Windows.Forms.CheckBox chkSwing;
        private System.Windows.Forms.ColorDialog colDlg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSwingSteps;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefaults;
        public System.Windows.Forms.ComboBox cbxLevel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkFullScreen;
    }
}