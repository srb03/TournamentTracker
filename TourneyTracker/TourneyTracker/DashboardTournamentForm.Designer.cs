namespace TourneyTracker
{
    partial class DashboardTournamentForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardTournamentForm));
            this.TitleHeaderLabel = new System.Windows.Forms.Label();
            this.AvailableTournamentsComboBox = new System.Windows.Forms.ComboBox();
            this.LoadTournamentButton = new System.Windows.Forms.Button();
            this.CreateTournamentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleHeaderLabel
            // 
            this.TitleHeaderLabel.AutoSize = true;
            this.TitleHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleHeaderLabel.Location = new System.Drawing.Point(132, 25);
            this.TitleHeaderLabel.Name = "TitleHeaderLabel";
            this.TitleHeaderLabel.Size = new System.Drawing.Size(305, 45);
            this.TitleHeaderLabel.TabIndex = 0;
            this.TitleHeaderLabel.Text = "Tournament Tracker";
            // 
            // AvailableTournamentsComboBox
            // 
            this.AvailableTournamentsComboBox.FormattingEnabled = true;
            this.AvailableTournamentsComboBox.Location = new System.Drawing.Point(140, 101);
            this.AvailableTournamentsComboBox.Name = "AvailableTournamentsComboBox";
            this.AvailableTournamentsComboBox.Size = new System.Drawing.Size(297, 38);
            this.AvailableTournamentsComboBox.TabIndex = 1;
            // 
            // LoadTournamentButton
            // 
            this.LoadTournamentButton.Location = new System.Drawing.Point(140, 161);
            this.LoadTournamentButton.Name = "LoadTournamentButton";
            this.LoadTournamentButton.Size = new System.Drawing.Size(297, 54);
            this.LoadTournamentButton.TabIndex = 2;
            this.LoadTournamentButton.Text = "Load Tournament";
            this.LoadTournamentButton.UseVisualStyleBackColor = true;
            this.LoadTournamentButton.Click += new System.EventHandler(this.LoadTournamentButton_Click);
            // 
            // CreateTournamentButton
            // 
            this.CreateTournamentButton.Location = new System.Drawing.Point(140, 232);
            this.CreateTournamentButton.Name = "CreateTournamentButton";
            this.CreateTournamentButton.Size = new System.Drawing.Size(297, 54);
            this.CreateTournamentButton.TabIndex = 3;
            this.CreateTournamentButton.Text = "New Tournament";
            this.CreateTournamentButton.UseVisualStyleBackColor = true;
            this.CreateTournamentButton.Click += new System.EventHandler(this.CreateTournamentButton_Click);
            // 
            // DashboardTournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 321);
            this.Controls.Add(this.CreateTournamentButton);
            this.Controls.Add(this.LoadTournamentButton);
            this.Controls.Add(this.AvailableTournamentsComboBox);
            this.Controls.Add(this.TitleHeaderLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.Name = "DashboardTournamentForm";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleHeaderLabel;
        private System.Windows.Forms.ComboBox AvailableTournamentsComboBox;
        private System.Windows.Forms.Button LoadTournamentButton;
        private System.Windows.Forms.Button CreateTournamentButton;
    }
}

