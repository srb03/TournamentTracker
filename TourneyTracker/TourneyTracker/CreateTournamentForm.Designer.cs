namespace TourneyTracker
{
    partial class CreateTournamentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTournamentForm));
            this.TitleHeaderLabel = new System.Windows.Forms.Label();
            this.TournamentNameLabel = new System.Windows.Forms.Label();
            this.TournamentNameTextBox = new System.Windows.Forms.TextBox();
            this.EntryFeeTextBox = new System.Windows.Forms.TextBox();
            this.EntryFeeLabel = new System.Windows.Forms.Label();
            this.SelectTeamLabel = new System.Windows.Forms.Label();
            this.SelectTeamComboBox = new System.Windows.Forms.ComboBox();
            this.NewTeamLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AddTeamButton = new System.Windows.Forms.Button();
            this.NewPrizeButton = new System.Windows.Forms.Button();
            this.TeamsSelectedLabel = new System.Windows.Forms.Label();
            this.TeamsSelectedMatchupsListBox = new System.Windows.Forms.ListBox();
            this.RemoveTeamsSelectedButton = new System.Windows.Forms.Button();
            this.RemovePrizesCreatedButton = new System.Windows.Forms.Button();
            this.PrizesCreatedListBox = new System.Windows.Forms.ListBox();
            this.PrizesCreatedLabel = new System.Windows.Forms.Label();
            this.CreateTournamentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleHeaderLabel
            // 
            this.TitleHeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleHeaderLabel.AutoSize = true;
            this.TitleHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleHeaderLabel.Location = new System.Drawing.Point(12, 22);
            this.TitleHeaderLabel.Name = "TitleHeaderLabel";
            this.TitleHeaderLabel.Size = new System.Drawing.Size(294, 45);
            this.TitleHeaderLabel.TabIndex = 1;
            this.TitleHeaderLabel.Text = "Create Tournament";
            // 
            // TournamentNameLabel
            // 
            this.TournamentNameLabel.AutoSize = true;
            this.TournamentNameLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TournamentNameLabel.Location = new System.Drawing.Point(15, 92);
            this.TournamentNameLabel.Name = "TournamentNameLabel";
            this.TournamentNameLabel.Size = new System.Drawing.Size(188, 30);
            this.TournamentNameLabel.TabIndex = 3;
            this.TournamentNameLabel.Text = "Tournament Name";
            // 
            // TournamentNameTextBox
            // 
            this.TournamentNameTextBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TournamentNameTextBox.Location = new System.Drawing.Point(20, 125);
            this.TournamentNameTextBox.Name = "TournamentNameTextBox";
            this.TournamentNameTextBox.Size = new System.Drawing.Size(307, 35);
            this.TournamentNameTextBox.TabIndex = 4;
            // 
            // EntryFeeTextBox
            // 
            this.EntryFeeTextBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntryFeeTextBox.Location = new System.Drawing.Point(119, 176);
            this.EntryFeeTextBox.Name = "EntryFeeTextBox";
            this.EntryFeeTextBox.Size = new System.Drawing.Size(108, 35);
            this.EntryFeeTextBox.TabIndex = 6;
            this.EntryFeeTextBox.Text = "$ 0.00";
            this.EntryFeeTextBox.Leave += new System.EventHandler(this.EntryFeeTextBox_Leave);
            // 
            // EntryFeeLabel
            // 
            this.EntryFeeLabel.AutoSize = true;
            this.EntryFeeLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntryFeeLabel.Location = new System.Drawing.Point(15, 179);
            this.EntryFeeLabel.Name = "EntryFeeLabel";
            this.EntryFeeLabel.Size = new System.Drawing.Size(98, 30);
            this.EntryFeeLabel.TabIndex = 5;
            this.EntryFeeLabel.Text = "Entry Fee";
            // 
            // SelectTeamLabel
            // 
            this.SelectTeamLabel.AutoSize = true;
            this.SelectTeamLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectTeamLabel.Location = new System.Drawing.Point(15, 234);
            this.SelectTeamLabel.Name = "SelectTeamLabel";
            this.SelectTeamLabel.Size = new System.Drawing.Size(125, 30);
            this.SelectTeamLabel.TabIndex = 7;
            this.SelectTeamLabel.Text = "Select Team";
            // 
            // SelectTeamComboBox
            // 
            this.SelectTeamComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectTeamComboBox.FormattingEnabled = true;
            this.SelectTeamComboBox.Location = new System.Drawing.Point(20, 267);
            this.SelectTeamComboBox.Name = "SelectTeamComboBox";
            this.SelectTeamComboBox.Size = new System.Drawing.Size(307, 33);
            this.SelectTeamComboBox.TabIndex = 8;
            // 
            // NewTeamLinkLabel
            // 
            this.NewTeamLinkLabel.AutoSize = true;
            this.NewTeamLinkLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewTeamLinkLabel.Location = new System.Drawing.Point(243, 243);
            this.NewTeamLinkLabel.Name = "NewTeamLinkLabel";
            this.NewTeamLinkLabel.Size = new System.Drawing.Size(84, 21);
            this.NewTeamLinkLabel.TabIndex = 9;
            this.NewTeamLinkLabel.TabStop = true;
            this.NewTeamLinkLabel.Text = "New Team";
            this.NewTeamLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.NewTeamLinkLabel_LinkClicked);
            // 
            // AddTeamButton
            // 
            this.AddTeamButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddTeamButton.Location = new System.Drawing.Point(75, 317);
            this.AddTeamButton.Name = "AddTeamButton";
            this.AddTeamButton.Size = new System.Drawing.Size(183, 45);
            this.AddTeamButton.TabIndex = 11;
            this.AddTeamButton.Text = "Add Team";
            this.AddTeamButton.UseVisualStyleBackColor = true;
            // 
            // NewPrizeButton
            // 
            this.NewPrizeButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPrizeButton.Location = new System.Drawing.Point(75, 378);
            this.NewPrizeButton.Name = "NewPrizeButton";
            this.NewPrizeButton.Size = new System.Drawing.Size(183, 45);
            this.NewPrizeButton.TabIndex = 12;
            this.NewPrizeButton.Text = "New Prize";
            this.NewPrizeButton.UseVisualStyleBackColor = true;
            this.NewPrizeButton.Click += new System.EventHandler(this.NewPrizeButton_Click);
            // 
            // TeamsSelectedLabel
            // 
            this.TeamsSelectedLabel.AutoSize = true;
            this.TeamsSelectedLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamsSelectedLabel.Location = new System.Drawing.Point(404, 92);
            this.TeamsSelectedLabel.Name = "TeamsSelectedLabel";
            this.TeamsSelectedLabel.Size = new System.Drawing.Size(73, 30);
            this.TeamsSelectedLabel.TabIndex = 13;
            this.TeamsSelectedLabel.Text = "Teams";
            // 
            // TeamsSelectedMatchupsListBox
            // 
            this.TeamsSelectedMatchupsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TeamsSelectedMatchupsListBox.FormattingEnabled = true;
            this.TeamsSelectedMatchupsListBox.Location = new System.Drawing.Point(409, 125);
            this.TeamsSelectedMatchupsListBox.Name = "TeamsSelectedMatchupsListBox";
            this.TeamsSelectedMatchupsListBox.Size = new System.Drawing.Size(275, 108);
            this.TeamsSelectedMatchupsListBox.TabIndex = 14;
            // 
            // RemoveTeamsSelectedButton
            // 
            this.RemoveTeamsSelectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveTeamsSelectedButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveTeamsSelectedButton.Location = new System.Drawing.Point(697, 137);
            this.RemoveTeamsSelectedButton.Name = "RemoveTeamsSelectedButton";
            this.RemoveTeamsSelectedButton.Size = new System.Drawing.Size(110, 74);
            this.RemoveTeamsSelectedButton.TabIndex = 15;
            this.RemoveTeamsSelectedButton.Text = "Remove Selected";
            this.RemoveTeamsSelectedButton.UseVisualStyleBackColor = true;
            // 
            // RemovePrizesCreatedButton
            // 
            this.RemovePrizesCreatedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemovePrizesCreatedButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemovePrizesCreatedButton.Location = new System.Drawing.Point(697, 310);
            this.RemovePrizesCreatedButton.Name = "RemovePrizesCreatedButton";
            this.RemovePrizesCreatedButton.Size = new System.Drawing.Size(110, 74);
            this.RemovePrizesCreatedButton.TabIndex = 18;
            this.RemovePrizesCreatedButton.Text = "Remove Selected";
            this.RemovePrizesCreatedButton.UseVisualStyleBackColor = true;
            // 
            // PrizesCreatedListBox
            // 
            this.PrizesCreatedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrizesCreatedListBox.FormattingEnabled = true;
            this.PrizesCreatedListBox.Location = new System.Drawing.Point(409, 298);
            this.PrizesCreatedListBox.Name = "PrizesCreatedListBox";
            this.PrizesCreatedListBox.Size = new System.Drawing.Size(275, 108);
            this.PrizesCreatedListBox.TabIndex = 17;
            // 
            // PrizesCreatedLabel
            // 
            this.PrizesCreatedLabel.AutoSize = true;
            this.PrizesCreatedLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrizesCreatedLabel.Location = new System.Drawing.Point(404, 265);
            this.PrizesCreatedLabel.Name = "PrizesCreatedLabel";
            this.PrizesCreatedLabel.Size = new System.Drawing.Size(67, 30);
            this.PrizesCreatedLabel.TabIndex = 16;
            this.PrizesCreatedLabel.Text = "Prizes";
            // 
            // CreateTournamentButton
            // 
            this.CreateTournamentButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateTournamentButton.Location = new System.Drawing.Point(288, 458);
            this.CreateTournamentButton.Name = "CreateTournamentButton";
            this.CreateTournamentButton.Size = new System.Drawing.Size(215, 61);
            this.CreateTournamentButton.TabIndex = 19;
            this.CreateTournamentButton.Text = "Create Tournament";
            this.CreateTournamentButton.UseVisualStyleBackColor = true;
            // 
            // CreateTournamentForm
            // 
            this.AcceptButton = this.CreateTournamentButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 531);
            this.Controls.Add(this.CreateTournamentButton);
            this.Controls.Add(this.RemovePrizesCreatedButton);
            this.Controls.Add(this.PrizesCreatedListBox);
            this.Controls.Add(this.PrizesCreatedLabel);
            this.Controls.Add(this.RemoveTeamsSelectedButton);
            this.Controls.Add(this.TeamsSelectedMatchupsListBox);
            this.Controls.Add(this.TeamsSelectedLabel);
            this.Controls.Add(this.NewPrizeButton);
            this.Controls.Add(this.AddTeamButton);
            this.Controls.Add(this.NewTeamLinkLabel);
            this.Controls.Add(this.SelectTeamComboBox);
            this.Controls.Add(this.SelectTeamLabel);
            this.Controls.Add(this.EntryFeeTextBox);
            this.Controls.Add(this.EntryFeeLabel);
            this.Controls.Add(this.TournamentNameTextBox);
            this.Controls.Add(this.TournamentNameLabel);
            this.Controls.Add(this.TitleHeaderLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CreateTournamentForm";
            this.Text = "New Tournament";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleHeaderLabel;
        private System.Windows.Forms.Label TournamentNameLabel;
        private System.Windows.Forms.TextBox TournamentNameTextBox;
        private System.Windows.Forms.TextBox EntryFeeTextBox;
        private System.Windows.Forms.Label EntryFeeLabel;
        private System.Windows.Forms.Label SelectTeamLabel;
        private System.Windows.Forms.ComboBox SelectTeamComboBox;
        private System.Windows.Forms.LinkLabel NewTeamLinkLabel;
        private System.Windows.Forms.Button AddTeamButton;
        private System.Windows.Forms.Button NewPrizeButton;
        private System.Windows.Forms.Label TeamsSelectedLabel;
        private System.Windows.Forms.ListBox TeamsSelectedMatchupsListBox;
        private System.Windows.Forms.Button RemoveTeamsSelectedButton;
        private System.Windows.Forms.Button RemovePrizesCreatedButton;
        private System.Windows.Forms.ListBox PrizesCreatedListBox;
        private System.Windows.Forms.Label PrizesCreatedLabel;
        private System.Windows.Forms.Button CreateTournamentButton;
    }
}