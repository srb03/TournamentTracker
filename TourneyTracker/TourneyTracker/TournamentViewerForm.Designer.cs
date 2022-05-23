namespace TourneyTracker
{
    partial class TournamentViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TournamentViewerForm));
            this.TitleHeaderLabel = new System.Windows.Forms.Label();
            this.RoundsComboBox = new System.Windows.Forms.ComboBox();
            this.RoundLabel = new System.Windows.Forms.Label();
            this.MatchupsListBox = new System.Windows.Forms.ListBox();
            this.UnplayedMatchesCheckBox = new System.Windows.Forms.CheckBox();
            this.TeamOneLabel = new System.Windows.Forms.Label();
            this.TeamOneTextBox = new System.Windows.Forms.TextBox();
            this.VersusLabel = new System.Windows.Forms.Label();
            this.TeamTwoTextBox = new System.Windows.Forms.TextBox();
            this.TeamTwoLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleHeaderLabel
            // 
            this.TitleHeaderLabel.AutoSize = true;
            this.TitleHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleHeaderLabel.Location = new System.Drawing.Point(12, 24);
            this.TitleHeaderLabel.Name = "TitleHeaderLabel";
            this.TitleHeaderLabel.Size = new System.Drawing.Size(505, 45);
            this.TitleHeaderLabel.TabIndex = 0;
            this.TitleHeaderLabel.Text = "Tournament: <tournamentName>";
            // 
            // RoundsComboBox
            // 
            this.RoundsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoundsComboBox.FormattingEnabled = true;
            this.RoundsComboBox.Location = new System.Drawing.Point(127, 87);
            this.RoundsComboBox.Name = "RoundsComboBox";
            this.RoundsComboBox.Size = new System.Drawing.Size(153, 33);
            this.RoundsComboBox.TabIndex = 1;
            this.RoundsComboBox.SelectedIndexChanged += new System.EventHandler(this.RoundsComboBox_SelectedIndexChanged);
            // 
            // RoundLabel
            // 
            this.RoundLabel.AutoSize = true;
            this.RoundLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoundLabel.Location = new System.Drawing.Point(47, 90);
            this.RoundLabel.Name = "RoundLabel";
            this.RoundLabel.Size = new System.Drawing.Size(74, 30);
            this.RoundLabel.TabIndex = 2;
            this.RoundLabel.Text = "Round";
            // 
            // MatchupsListBox
            // 
            this.MatchupsListBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchupsListBox.FormattingEnabled = true;
            this.MatchupsListBox.ItemHeight = 30;
            this.MatchupsListBox.Location = new System.Drawing.Point(20, 184);
            this.MatchupsListBox.Name = "MatchupsListBox";
            this.MatchupsListBox.Size = new System.Drawing.Size(336, 214);
            this.MatchupsListBox.TabIndex = 4;
            this.MatchupsListBox.SelectedIndexChanged += new System.EventHandler(this.MatchupsListBox_SelectedIndexChanged);
            // 
            // UnplayedMatchesCheckBox
            // 
            this.UnplayedMatchesCheckBox.AutoSize = true;
            this.UnplayedMatchesCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnplayedMatchesCheckBox.Location = new System.Drawing.Point(127, 139);
            this.UnplayedMatchesCheckBox.Name = "UnplayedMatchesCheckBox";
            this.UnplayedMatchesCheckBox.Size = new System.Drawing.Size(129, 25);
            this.UnplayedMatchesCheckBox.TabIndex = 3;
            this.UnplayedMatchesCheckBox.Text = "Unplayed only";
            this.UnplayedMatchesCheckBox.UseVisualStyleBackColor = true;
            // 
            // TeamOneLabel
            // 
            this.TeamOneLabel.AutoSize = true;
            this.TeamOneLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamOneLabel.Location = new System.Drawing.Point(414, 184);
            this.TeamOneLabel.Name = "TeamOneLabel";
            this.TeamOneLabel.Size = new System.Drawing.Size(133, 30);
            this.TeamOneLabel.TabIndex = 5;
            this.TeamOneLabel.Text = "<Team one>";
            // 
            // TeamOneTextBox
            // 
            this.TeamOneTextBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamOneTextBox.Location = new System.Drawing.Point(419, 228);
            this.TeamOneTextBox.Name = "TeamOneTextBox";
            this.TeamOneTextBox.Size = new System.Drawing.Size(128, 35);
            this.TeamOneTextBox.TabIndex = 6;
            this.TeamOneTextBox.Text = "-";
            // 
            // VersusLabel
            // 
            this.VersusLabel.AutoSize = true;
            this.VersusLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersusLabel.Location = new System.Drawing.Point(471, 279);
            this.VersusLabel.Name = "VersusLabel";
            this.VersusLabel.Size = new System.Drawing.Size(37, 30);
            this.VersusLabel.TabIndex = 7;
            this.VersusLabel.Text = "VS";
            // 
            // TeamTwoTextBox
            // 
            this.TeamTwoTextBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamTwoTextBox.Location = new System.Drawing.Point(419, 370);
            this.TeamTwoTextBox.Name = "TeamTwoTextBox";
            this.TeamTwoTextBox.Size = new System.Drawing.Size(128, 35);
            this.TeamTwoTextBox.TabIndex = 9;
            this.TeamTwoTextBox.Text = "-";
            // 
            // TeamTwoLabel
            // 
            this.TeamTwoLabel.AutoSize = true;
            this.TeamTwoLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamTwoLabel.Location = new System.Drawing.Point(414, 326);
            this.TeamTwoLabel.Name = "TeamTwoLabel";
            this.TeamTwoLabel.Size = new System.Drawing.Size(132, 30);
            this.TeamTwoLabel.TabIndex = 8;
            this.TeamTwoLabel.Text = "<Team two>";
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(582, 279);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(95, 64);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 423);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TeamTwoTextBox);
            this.Controls.Add(this.TeamTwoLabel);
            this.Controls.Add(this.VersusLabel);
            this.Controls.Add(this.TeamOneTextBox);
            this.Controls.Add(this.TeamOneLabel);
            this.Controls.Add(this.UnplayedMatchesCheckBox);
            this.Controls.Add(this.MatchupsListBox);
            this.Controls.Add(this.RoundLabel);
            this.Controls.Add(this.RoundsComboBox);
            this.Controls.Add(this.TitleHeaderLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TournamentViewerForm";
            this.Text = "Tournament Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleHeaderLabel;
        private System.Windows.Forms.ComboBox RoundsComboBox;
        private System.Windows.Forms.Label RoundLabel;
        private System.Windows.Forms.ListBox MatchupsListBox;
        private System.Windows.Forms.CheckBox UnplayedMatchesCheckBox;
        private System.Windows.Forms.Label TeamOneLabel;
        private System.Windows.Forms.TextBox TeamOneTextBox;
        private System.Windows.Forms.Label VersusLabel;
        private System.Windows.Forms.TextBox TeamTwoTextBox;
        private System.Windows.Forms.Label TeamTwoLabel;
        private System.Windows.Forms.Button SaveButton;
    }
}